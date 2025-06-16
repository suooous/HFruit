using System;
using System.Drawing;
using System.Drawing.Drawing2D; // Required for GraphicsPath
using System.Windows.Forms;     // **CORRECTED**: Use the Windows Forms Panel

namespace appfruit
{
    public class RoundedPanel : Panel
    {
        private int _cornerRadius = 10; // 圆角半径，可以根据需要调整

        public int CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                if (value < 0) value = 0; // 圆角半径不能小于0
                _cornerRadius = value;
                // **CORRECTED**: Call the base Control's Invalidate() method
                this.Invalidate();
            }
        }

        // **REMOVED**: The incorrect `Invalidate()` method

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 禁用默认的边框绘制，因为它会绘制矩形边框
            // IMPORTANT: Setting BorderStyle here means it will be set on every paint.
            // If you want it to be configurable or set once, consider doing it in the constructor
            // or by checking its current value. For a custom panel, BorderStyle.None is usually desired.
            this.BorderStyle = BorderStyle.None;

            using (GraphicsPath path = new GraphicsPath())
            {
                // Define the rectangle for the arcs
                Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);

                // Define round corners by adding arcs and lines
                path.AddArc(bounds.X, bounds.Y, _cornerRadius * 2, _cornerRadius * 2, 180, 90); // Top-Left
                path.AddArc(bounds.Right - _cornerRadius * 2, bounds.Y, _cornerRadius * 2, _cornerRadius * 2, 270, 90); // Top-Right
                path.AddArc(bounds.Right - _cornerRadius * 2, bounds.Bottom - _cornerRadius * 2, _cornerRadius * 2, _cornerRadius * 2, 0, 90); // Bottom-Right
                path.AddArc(bounds.X, bounds.Bottom - _cornerRadius * 2, _cornerRadius * 2, _cornerRadius * 2, 90, 90); // Bottom-Left
                path.CloseFigure();

                this.Region = new Region(path); // Set the control's region to the rounded rectangle shape

                // If you want to draw a border, do it here
                // Note: The base.OnPaint() will draw the background *before* this,
                // so the fill for the rounded area will come from the panel's BackColor.
                using (Pen pen = new Pen(Color.LightGray, 3)) // Border color and thickness
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Enable anti-aliasing for smooth edges
                    e.Graphics.DrawPath(pen, path); // Draw the border
                }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            // When the control size changes, its region needs to be recalculated and redrawn.
            this.Invalidate();
        }
    }
}