using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // 确保引用了 WinForms 命名空间
using Markdig; // 用于 Markdown 到 HTML 转换
using System.Drawing; // 用于 Font

namespace appfruit
{
    // partial 关键字表示这个类的一部分在其他文件中（例如 Designer.cs）
    public partial class AIQAUserControl : UserControl // 确保继承自 UserControl
    {
        // Spark API 配置
        // 请务必将此处的 ApiKey 替换为你在讯飞控制台“HTTP服务接口认证信息”下查看到的API password。
        // 它通常是一个形如 "mzj****Kcz" 的字符串，而不是 "AK:SK" 的组合。
        private const string ApiKey = "pBaYWnmWFPYAhptFcnQL:xBKAdeJOBJUgcGGloGKh";
        private const string BaseUrl = "https://spark-api-open.xf-yun.com/v2";
        private const string Model = "x1"; // 根据您的模型版本调整，例如 "v3", "v3.5"

        private HttpClient httpClient;
        private StringBuilder chatContentBuilder; // 用于构建 WebBrowser 的 HTML 内容

        public AIQAUserControl()
        {
            InitializeComponent(); // 调用 Designer.cs 中定义的 UI 初始化方法
            InitializeHttpClient(); // 初始化 HttpClient
            chatContentBuilder = new StringBuilder(); // 初始化 StringBuilder
            InitializeChatSystemPrompt(); // 添加初始 AI 问候语
        }

        /// <summary>
        /// 初始化 HttpClient 并设置默认请求头。
        /// </summary>
        private void InitializeHttpClient()
        {
            httpClient = new HttpClient();
            // 设置一个合理的超时时间，以避免长时间等待无响应
            httpClient.Timeout = TimeSpan.FromSeconds(60); // 增加超时时间到60秒
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // 授权头应使用 "Bearer" 方案和您的 API Key
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        }

        /// <summary>
        /// 设置 AI 的初始问候语。
        /// </summary>
        private void InitializeChatSystemPrompt()
        {
            AppendMessageToChat("AI", "你好！我是水果购买顾问，有任何关于水果挑选、储存或食用的问题都可以问我哦～");
        }

        /// <summary>
        /// 将消息添加到聊天显示（WebBrowser）。
        /// </summary>
        /// <param name="sender">发送者名称（例如，“你”，“AI”）</param>
        /// <param name="message">消息内容（可以是 Markdown 格式）。</param>
        private void AppendMessageToChat(string sender, string message)
        {
            // 将 Markdown 转换为 HTML
            string htmlMessage = Markdown.ToHtml(message);

            // 将消息添加到 StringBuilder，并更新 WebBrowser
            // 使用简单的 HTML 结构和内联样式来美化显示
            chatContentBuilder.Append($@"
                <div style='margin-bottom: 10px; {(sender == "你" ? "text-align: right;" : "text-align: left;")}'>
                    <div style='
                        display: inline-block;
                        padding: 8px 12px;
                        border-radius: 15px;
                        max-width: 70%;
                        background-color: {(sender == "你" ? "#DCF8C6" : "#E0E0E0")};
                        color: #333;
                        font-family: Arial, sans-serif;
                        font-size: 14px;
                        line-height: 1.4;
                        box-shadow: 0 1px 2px rgba(0,0,0,0.1);
                        '>
                        <strong>{sender}:</strong> {htmlMessage}
                    </div>
                </div>
            ");

            UpdateChatBrowser();
        }

        /// <summary>
        /// 更新 WebBrowser 以显示当前的聊天内容。
        /// </summary>
        private void UpdateChatBrowser()
        {
            string fullHtml = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{ margin: 10px; background-color: #F8F8F8; }}
                    </style>
                </head>
                <body>
                    {chatContentBuilder.ToString()}
                    <script type='text/javascript'>
                        // 自动滚动到底部
                        window.onload = function() {{
                            window.scrollTo(0, document.body.scrollHeight);
                        }};
                    </script>
                </body>
                </html>
            ";
            // 确保 DocumentText 在 UI 线程上设置
            if (chatBrowser.InvokeRequired)
            {
                chatBrowser.Invoke(new MethodInvoker(() => chatBrowser.DocumentText = fullHtml));
            }
            else
            {
                chatBrowser.DocumentText = fullHtml;
            }
        }

        /// <summary>
        /// 处理发送按钮点击事件。
        /// </summary>
        private async void sendButton_Click(object sender, EventArgs e)
        {
            string userMessage = inputBox.Text.Trim();
            if (!string.IsNullOrEmpty(userMessage))
            {
                // 先显示用户消息
                AppendMessageToChat("你", userMessage);
                inputBox.Clear();

                // 显示 AI 思考中...
                // 为思考中消息生成唯一ID，便于后续移除或替换
                string thinkingMessageId = Guid.NewGuid().ToString();
                chatContentBuilder.Append($@"
                    <div id='{thinkingMessageId}' style='margin-bottom: 10px; text-align: left;'>
                        <div style='
                            display: inline-block;
                            padding: 8px 12px;
                            border-radius: 15px;
                            max-width: 70%;
                            background-color: #E0E0E0;
                            color: #333;
                            font-family: Arial, sans-serif;
                            font-size: 14px;
                            line-height: 1.4;
                            box-shadow: 0 1px 2px rgba(0,0,0,0.1);
                            '>
                            <strong>AI:</strong> 思考中...
                        </div>
                    </div>
                ");
                UpdateChatBrowser(); // 更新浏览器显示思考中...

                try
                {
                    string aiMarkdownResponse = await GetFeiYuResponse(userMessage);

                    // 移除“思考中...”消息的 HTML 元素
                    // 这是一个简化的方法，直接替换 StringBuilder 中的文本。
                    // 实际应用中，如果需要更精细的DOM操作，WebBrowser.Document 对象更合适。
                    chatContentBuilder.Replace($"<div id='{thinkingMessageId}'", "<div style='display:none;'");

                    // 显示 AI 的实际回复
                    AppendMessageToChat("AI", aiMarkdownResponse);
                }
                catch (Exception ex)
                {
                    // 移除或更新“思考中...”为错误信息
                    chatContentBuilder.Replace($"<div id='{thinkingMessageId}'", "<div style='display:none;'");
                    AppendMessageToChat("AI", $"抱歉，发生错误: {ex.Message}");
                }
                UpdateChatBrowser(); // 确保最终状态被更新
            }
        }

        /// <summary>
        /// 处理输入框按键事件，用于回车发送消息。
        /// </summary>
        private void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                sendButton_Click(sender, e);
                e.Handled = true; // 阻止默认的回车换行行为
            }
        }

        /// <summary>
        /// 调用 Spark API 获取响应。
        /// </summary>
        /// <param name="prompt">用户的输入提示。</param>
        /// <returns>AI 的响应。</returns>
        private async Task<string> GetFeiYuResponse(string prompt)
        {
            var requestBody = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                },
                model = Model,
                stream = false, // 设置为 false 获取单个非流式响应
                user = "WinFormsUser" // 用于 API 的用户 ID
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8,
                "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync($"{BaseUrl}/chat/completions", jsonContent);

                // ***修改点***: 在 EnsureSuccessStatusCode() 之前捕获错误响应
                // 这样即使在旧的 .NET Framework 版本中，也能安全地获取错误详情
                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    // 返回更详细的错误信息，包括 HTTP 状态码和服务器返回的错误内容
                    return $"API请求失败 (HTTP Status: {(int)response.StatusCode} {response.ReasonPhrase}): {errorDetails}";
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);

                if (result != null && result.choices != null && result.choices.Count > 0 &&
                    result.choices[0].message != null && result.choices[0].message.content != null)
                {
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    return "抱歉，无法解析AI的回答。返回数据格式不正确。";
                }
            }
            // ***修改点***: 捕获 HttpRequestException 时不再尝试访问 HttpResponseMessage 属性
            catch (HttpRequestException httpEx)
            {
                // HttpRequestException 通常表示网络连接问题、DNS解析失败、超时等
                // 其 Message 属性通常已包含足够的信息
                return $"API连接或请求发送失败: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                // 捕获其他任何未预料的异常
                return $"API调用发生未知异常: {ex.Message}";
            }
        }

        // AIQAUserControl_Load 事件
        private void AIQAUserControl_Load(object sender, EventArgs e)
        {
            // 当 UserControl 加载时，可以在此处添加初始化逻辑
        }

        private void chatBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}