using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    public class MediProgressBarControl:Control
    {
        public MediProgressBarControl()
        {

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.MouseDown += MySlider_MouseDown;
            this.MouseMove += MySlider_MouseMove;
            this.MouseUp += MySlider_MouseUp;
        }
        Rectangle foreRect;
        Rectangle backRect;
        Rectangle setRect;

        Color backgroundColor = Color.White;
        Color foregroundColor = Color.Gray;
        Color setRectColor = Color.Black;
        Color fontColor = Color.Black;

        int maximum = 100;
        int minimum = 0;
        double myValue = 0;

        bool showPercent;
        float fontSize = 9;
        FontFamily myFontFamily = new FontFamily("宋体");
        [Category("General"), Description("Show Percent Tag"), Browsable(true)]
        public bool ShowPercentTag
        {
            get { return showPercent; }
            set
            {
                showPercent = value;
                Invalidate();
            }
        }
        [Category("General"), Description("Control's Maximum"), Browsable(true)]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                Invalidate();
            }
        }
        [Category("General"), Description("Control's Minimum"), Browsable(true)]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                Invalidate();
            }
        }

        [Category("General"), Description("Control's FontSize"), Browsable(true)]
        public float FontSize
        {
            get { return fontSize; }
            set
            {
                this.fontSize = value;
                Invalidate();
            }
        }
        [Category("General"), Description("Control's FontFamily"), Browsable(true)]
        public FontFamily MyFontFamily
        {
            get { return myFontFamily; }
            set
            {
                this.myFontFamily = value;
                Invalidate();
            }
        }

        [Category("Color"), Browsable(true)]
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                this.backgroundColor = value;
                Invalidate();
            }
        }
        [Category("Color"), Browsable(true)]
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                this.foregroundColor = value;
                Invalidate();
            }
        }
        [Category("Color"), Browsable(true)]
        public Color SetRectColor
        {
            get { return setRectColor; }
            set
            {
                this.setRectColor = value;
                Invalidate();
            }
        }
        [Category("Color"), Browsable(true)]
        public Color FontColor
        {
            get { return fontColor; }
            set
            {
                this.fontColor = value;
                Invalidate();
            }
        }
      

        [Category("General"), Description("Control's Width"), Browsable(true)]
        public new int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                foreRect.X = backRect.X = 0;
                backRect.Width = base.Width-1;
                foreRect.Width = (int)(myValue / maximum * backRect.Width);
                setRect.X = (int)(myValue / maximum * (backRect.Width - backRect.Height) + foreRect.X);

                Invalidate();
            }
        }
        [Category("General"), Description("Control's Height"), Browsable(true)]
        public new int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                foreRect.Height = backRect.Height = setRect.Height  = this.ClientRectangle.Height-1;
                setRect.Width = base.Width;
                foreRect.Y = backRect.Y = setRect.Y = 0;
                Invalidate();
            }
        }


        protected EventHandler OnValueChanged;
        public event EventHandler ValueChanged
        {
            add
            {
                if (OnValueChanged != null)
                {
                    foreach (Delegate d in OnValueChanged.GetInvocationList())
                    {
                        if (object.ReferenceEquals(d, value)) { return; }
                    }
                }
                OnValueChanged = (EventHandler)Delegate.Combine(OnValueChanged, value);
            }
            remove
            {
                OnValueChanged = (EventHandler)Delegate.Remove(OnValueChanged, value);
            }
        }


        [Category("General"), Description("Control's Value"), Browsable(true)]
        public double Value
        {
            get { return myValue; }
            set
            {
                if (myValue < Minimum)
                    throw new ArgumentException("小于最小值");
                if (myValue > Maximum)
                    throw new ArgumentException("超过最大值");

                myValue = value;
                foreRect.Width = (int)(myValue / maximum * backRect.Width);
                setRect.X = (int)(myValue / maximum * (backRect.Width) + backRect.X);

                if ((myValue - maximum) > 0)
                {
                    foreRect.Width = backRect.Width;
                    setRect.X = backRect.Width + backRect.X;
                }

                //如果添加了响应函数,则执行该函数
                if (OnValueChanged != null)
                {
                    OnValueChanged(this, EventArgs.Empty);
                }

                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawRect(e.Graphics);
            DrawText(e.Graphics);
        }
        private void DrawRect(Graphics e)
        {
            Pen pen = new Pen(this.foregroundColor);

            //e.FillRectangle(new SolidBrush(this.backgroundColor), backRect);
            //e.DrawRectangle(new Pen(Color.Black), backRect);

            //e.FillRectangle(new SolidBrush(this.foregroundColor), foreRect);
            //e.DrawRectangle(new Pen(Color.Black), foreRect);

            e.FillRectangle(new SolidBrush(this.setRectColor), setRect);
            e.DrawRectangle(new Pen(this.setRectColor), setRect);
        }
        private void DrawText(Graphics e)
        {
            Point point = new Point();
            point.X = this.backRect.X + this.backRect.Width * 3 / 7;
            point.Y = this.backRect.Y + this.backRect.Height / 3;

            SolidBrush brush = new SolidBrush(fontColor);
            Font font = new Font(myFontFamily, this.fontSize);
            string percent = ((int)this.myValue).ToString() + "%";

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            e.DrawString(percent, font, brush, backRect, format);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = Width;
            this.Height = Height;
            Invalidate();
        }

       

             Point originPoint;
        Point originsetRectPoint;
        bool setRectDown = false;

        void MySlider_MouseUp(object sender, MouseEventArgs e)
        {
            setRectDown = false;
        }
        void MySlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (setRectDown)
            {
                int dd = e.Location.X - originPoint.X;

                double percent = (double)(originsetRectPoint.X + dd - this.backRect.X) / (this.backRect.Width - this.backRect.Height);
                if (percent < 0)
                {
                    this.Value = minimum;
                    this.foreRect.Width = 0;
                    this.setRect.X = backRect.X;
                }
                else if (percent > 1)
                {
                    this.Value = maximum;
                    this.foreRect.Width = this.backRect.Width;
                    this.setRect.X = backRect.X + backRect.Width - backRect.Height;
                }
                else
                {
                    this.Value = percent * maximum;
                    this.foreRect.Width = (int)(percent * this.backRect.Width);
                    this.setRect.X = originsetRectPoint.X + dd;
                }
                Invalidate();
            }
        }
        void MySlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (setRect.Contains(e.Location))
            {
                this.originPoint = e.Location;
                originsetRectPoint = this.setRect.Location;
                this.setRectDown = true;
            }
        }
    }
}
