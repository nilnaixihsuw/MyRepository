using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mediinfo.WinForm.Common
{

    /// <summary>
    /// 加载等待控件
    /// </summary>
    [ToolboxBitmap(typeof(MediWaitCircleControl), "volume16")]
    public class MediWaitCircleControl : Control
    {
        private int _numberofspokes = 8;            // 控件包含的总的辐条数量
        private int _hotspokes = 5;                 // 活动辐条数
        private float _innerRadius;                 // 内环半径,外环半径根据控件的大小计算
        private Color _spokeColor = Color.LightGray;        // 辐条颜色
        private Color _hotSpokeColor = Color.Gray;          // 活动辐条颜色

        private float _thickness = 2;               // 辐条的宽度
        private bool _antialias = true;             // 控件绘制时是否反走样
        private bool _colockWise = true;            // 活动辐条的旋转方向是否顺时针旋转

        private List<PointF[]> _spokes;             // 辐条轮廓线列表
        protected Color[] _palette;                 // 渐变色调色板
        private Timer _timer;                       // 计时器
        private Pen _pen;                           // 绘制WaitingCircle的画笔
        private int _offset = 0;                    // 活动辐条偏移量
        private string _description = "系统正在初始化...";     // 描述信息
        private Font _descriptionFont = new Font("Arial", 20);
        private Color _descriptionFontColor = Color.Black;

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediWaitCircleControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            _timer = new Timer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _pen = new Pen(_spokeColor, _thickness);
            GeneratePalette();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _pen.Dispose();
                _timer.Dispose();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CircleF c = new CircleF(ClientRectangle);
            c.Inflate(-5.5f);
            _innerRadius = c.Radius / 2.0f;
            _spokes = ExtendedShapes.CreateWaitCircleSpokes(new PointF(c.Radius + 10, ClientRectangle.Height / 2), c.Radius, _innerRadius, _numberofspokes);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawSpokes(e.Graphics);
        }

        protected void DrawSpokes(Graphics g)
        {
            Rectangle rectangle = new Rectangle(0, 0, ClientRectangle.Size.Width - 1, ClientRectangle.Size.Height - 1);
            Pen pen = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(190)))), ((int)(((byte)(245))))), 1);

            g.DrawRectangle(pen, rectangle);
            g.SmoothingMode = _antialias ? SmoothingMode.AntiAlias : SmoothingMode.Default;
            if (!Activate)
            {
                _pen.Color = _spokeColor;
                foreach (PointF[] spoke in _spokes)
                {
                    g.DrawLines(_pen, spoke);
                }
                String drawString = Description; // Create font and brush.
                SolidBrush drawBrush = new SolidBrush(DescriptionFontColor);// Create point for upper-left corner of drawing.
                CircleF c = new CircleF(ClientRectangle);
                c.Inflate(-5.0f);

                float x = 2.0f * c.Radius + 50; float y = ClientRectangle.Height / 2f - DescriptionFont.Height / 2f;// Draw string to screen.

                g.DrawString(drawString, DescriptionFont, drawBrush, x, y);
            }
            else
            {
                List<int> hot = new List<int>();//存储活动辐条的索引；
                for (int i = 0; i < _hotspokes; i++)
                {
                    int index = ((_colockWise ? _offset - i : _offset + i) + _numberofspokes) % _numberofspokes;
                    hot.Add(index);
                }
                _pen.Color = _spokeColor;
                for (int i = 0; i < _numberofspokes; i++)//首先绘制非活动辐条
                {
                    if (!hot.Contains(i)) g.DrawLines(_pen, _spokes[i]);
                }
                for (int i = 0; i < _hotspokes; i++)//绘制活动辐条
                {
                    _pen.Color = _palette[_hotspokes - 1 - i];
                    g.DrawLines(_pen, _spokes[hot[i]]);
                }
                String drawString = Description; // Create font and brush.

                SolidBrush drawBrush = new SolidBrush(DescriptionFontColor);// Create point for upper-left corner of drawing.

                CircleF c = new CircleF(ClientRectangle);
                c.Inflate(-5.0f);
                float x = 2.0f * c.Radius + 15; float y = ClientRectangle.Height / 2f - DescriptionFont.Height / 2f;// Draw string to screen.

                g.DrawString(drawString, DescriptionFont, drawBrush, x, y);
            }
        }

        protected virtual void GeneratePalette()
        {
            _palette = new Color[_hotspokes];
            float a = (float)(_hotSpokeColor.A - _spokeColor.A) / (float)_hotspokes;
            float r = (float)(_hotSpokeColor.R - _spokeColor.R) / (float)_hotspokes;
            float g = (float)(_hotSpokeColor.G - _spokeColor.G) / (float)_hotspokes;
            float b = (float)(_hotSpokeColor.B - _spokeColor.B) / (float)_hotspokes;
            for (int i = 0; i < _hotspokes; i++)
            {
                _palette[i] = Color.FromArgb(_hotSpokeColor.A - (int)(i * a), _hotSpokeColor.R - (int)(i * r), _hotSpokeColor.G - (int)(i * g), _hotSpokeColor.B - (int)(i * b));
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            _offset = ((_colockWise ? --_offset : ++_offset) + _numberofspokes) % _numberofspokes;
        }

        [DefaultValue(100)]
        public int Speed
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        [DefaultValue(false)]
        public bool Activate
        {
            get { return _timer.Enabled; }
            set
            {
                _timer.Enabled = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        public bool Antialias
        {
            get { return _antialias; }
            set
            {
                _antialias = value;
                Invalidate();
            }
        }

        public float InnerRadius
        {
            get { return _innerRadius; }
            set
            {
                if (_innerRadius != value && value > 0)
                {
                    _innerRadius = value;
                    CircleF c = new CircleF(ClientRectangle);
                    c.Inflate(-5.0f);
                    _spokes = ExtendedShapes.CreateWaitCircleSpokes(c.Pivot, c.Radius, _innerRadius, _numberofspokes);
                    Invalidate();
                }
            }
        }

        [DefaultValue(true)]
        public bool ColockWise
        {
            get { return _colockWise; }
            set { _colockWise = value; }
        }

        public Color SpokeColor
        {
            get { return _spokeColor; }
            set
            {
                _spokeColor = value;
                GeneratePalette();
                Invalidate();
            }
        }

        public Color HotSpokeColor
        {
            get { return _hotSpokeColor; }
            set
            {
                _hotSpokeColor = value;
                GeneratePalette();
            }
        }

        [DefaultValue("正在加载请稍后......")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        [DefaultValue(20)]
        public int NumberOfSpokes
        {
            get { return _numberofspokes; }
            set
            {
                _numberofspokes = Math.Max(2, Math.Max(_hotspokes, value));
                CircleF c = new CircleF(ClientRectangle);
                c.Inflate(-5.0f);
                _spokes = ExtendedShapes.CreateWaitCircleSpokes(c.Pivot, c.Radius, _innerRadius, _numberofspokes);
                Invalidate();
            }
        }

        [DefaultValue(5)]
        public int HotSpokes
        {
            get { return _hotspokes; }
            set
            {
                _hotspokes = Math.Max(1, Math.Min(_numberofspokes, value));
                GeneratePalette();
            }
        }

        public LineCap StartCap
        {
            get { return _pen.StartCap; }
            set { _pen.StartCap = value; Invalidate(); }
        }

        public LineCap EndCap
        {
            get { return _pen.EndCap; }
            set { _pen.EndCap = value; Invalidate(); }
        }

        [DefaultValue(3)]
        public float Thickness
        {
            get { return _thickness; }
            set
            {
                if (value > 0)
                    _thickness = value;
                _pen.Width = _thickness;
                Invalidate();
            }
        }

        /// <summary>
        /// 描述字体
        /// </summary>
        public Font DescriptionFont
        {
            get { return _descriptionFont; }
            set { _descriptionFont = value; }
        }

        /// <summary>
        /// 描述字体颜色
        /// </summary>
        public Color DescriptionFontColor
        {
            get { return _descriptionFontColor; }
            set { _descriptionFontColor = value; }
        }
    }

    [ComVisible(true)]
    [Serializable]
    public struct Circle : ICloneable
    {
        private Point _pivot;
        private int _radius;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Point Pivot
        {
            get
            {
                return this._pivot;
            }
            set
            {
                this._pivot = value;
            }
        }

        public int Diameter
        {
            get
            {
                return this._radius;
            }
            set
            {
                this._radius = value;
            }
        }

        public Circle(Rectangle rect)
        {
            this._pivot = Circle.GetCirclePivot(rect);
            this._radius = Circle.GetCircleDiameter(rect);
        }

        public Circle(Point Pivot, int radius)
        {
            this._pivot = Pivot;
            this._radius = radius;
        }

        public Circle FromRectangle(Rectangle rect)
        {
            this._pivot = Circle.GetCirclePivot(rect);
            this._radius = Circle.GetCircleDiameter(rect);
            return this;
        }

        public static Point GetCirclePivot(Rectangle rect)
        {
            return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        }

        public static int GetCircleDiameter(Rectangle rect)
        {
            return Math.Min(rect.Width / 2, rect.Height / 2);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(this._pivot.X - this._radius, this._pivot.Y - this._radius, this._radius * 2, this._radius * 2);
        }

        public static Rectangle GetBounds(Rectangle rect)
        {
            Point p = Circle.GetCirclePivot(rect);
            int d = Circle.GetCircleDiameter(rect);
            return new Rectangle(p.X - d, p.Y - d, d * 2, d * 2);
        }

        public Point PointOnPath(double sweepAngle)
        {
            return new Point(this._pivot.X + (int)((float)this._radius * (float)Math.Cos(sweepAngle * 0.017453292519943295)), this._pivot.Y + (int)((float)this._radius * (float)Math.Sin(sweepAngle * 0.017453292519943295)));
        }

        public void Offset(int x, int y)
        {
            this._pivot.X = this._pivot.X + x;
            this._pivot.Y = this._pivot.Y + y;
        }

        public void Offset(Point pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        public void Inflate(int d)
        {
            this._radius += d;
        }

        public static bool operator !=(Circle left, Circle right)
        {
            return left._pivot == right._pivot && left._radius == right._radius;
        }

        public static bool operator ==(Circle left, Circle right)
        {
            return !(left != right);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static implicit operator Rectangle(Circle c)
        {
            return c.GetBounds();
        }

        public override string ToString()
        {
            var s = $"Pivot={this._pivot},Radius={this._radius}";
            return "{" + s + "}";
        }

        public object Clone()
        {
            return new Circle(this._pivot, this._radius);
        }
    }

    public class CircleFConvertor : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(CircleF), attributes).Sort(new string[]
            {
                "Radius",
                "Pivot"
            });
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object result;
            if (value is string s)
            {
                string[] ti = s.Split(',');
                float x = float.Parse(ti[0].Substring(1));
                float y = float.Parse(ti[1].Substring(0, ti[1].Length - 1));
                float r = float.Parse(ti[2]);
                result = new CircleF(new PointF(x, y), r);
            }
            else
            {
                result = base.ConvertFrom(context, culture, value);
            }
            return result;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object result;
            if (value is CircleF c && destinationType == typeof(string))
            {
                result = string.Format("({0},{1}),{2}", c.Pivot.X, c.Pivot.Y, c.Radius);
                return result;
            }
            result = base.ConvertTo(context, culture, value, destinationType);
            return result;
        }
    }

    public static class ExtendedShapes
    {
        public static PointF[] CreateRegularPolygon(PointF pivot, float outterRadius, int points, float angleOffset)
        {
            if (outterRadius < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(outterRadius), "多边形的外接圆半径必须大于0");
            }
            if (points < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "多边形的边数必须大于等于3");
            }
            PointF[] ret = new PointF[points];
            CircleF c = new CircleF(pivot, outterRadius);
            float ang = 360f / (float)points;
            for (int i = 0; i < points; i++)
            {
                ret[i] = c.PointOnPath((double)(angleOffset + (float)i * ang));
            }
            return ret;
        }

        public static PointF[] CreateRegularPolygon(PointF pivot, float outterRadius, int points)
        {
            return ExtendedShapes.CreateRegularPolygon(pivot, outterRadius, points, 0f);
        }

        public static GraphicsPath CreateStar(PointF pivot, float outterRadius, float innerRadius, int points, float angleOffset)
        {
            if (outterRadius <= innerRadius)
            {
                throw new ArgumentException("参数outterRadius必须大于innerRadius。");
            }
            if (points < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }
            GraphicsPath gp = new GraphicsPath();
            CircleF outter = new CircleF(pivot, outterRadius);
            CircleF inner = new CircleF(pivot, innerRadius);
            float ang = 360f / (float)points;
            for (int i = 0; i < points; i++)
            {
                gp.AddLine(outter.PointOnPath((double)(angleOffset + (float)i * ang)), inner.PointOnPath((double)(angleOffset + (float)i * ang + ang / 2f)));
            }
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath CreateStar(PointF pivot, float outterRadius, float innerRadius, int points)
        {
            return ExtendedShapes.CreateStar(pivot, outterRadius, innerRadius, points, 0f);
        }

        public static List<PointF[]> CreateWaitCircleSpokes(PointF pivot, float outterRadius, float innerRadius, int spokes, float angleOffset)
        {
            if (spokes < 1)
            {
                throw new ArgumentException("参数spokes必须大于等于1。");
            }
            List<PointF[]> lst = new List<PointF[]>();
            CircleF outter = new CircleF(pivot, outterRadius);
            CircleF inner = new CircleF(pivot, innerRadius);
            float ang = 360f / (float)spokes;
            for (int i = 0; i < spokes; i++)
            {
                lst.Add(new PointF[]
                {
                    outter.PointOnPath((double)(angleOffset + (float)i * ang)),
                    inner.PointOnPath((double)(angleOffset + (float)i * ang))
                });
            }
            return lst;
        }

        public static List<PointF[]> CreateWaitCircleSpokes(PointF pivot, float outterRadius, float innerRadius, int spokes)
        {
            return ExtendedShapes.CreateWaitCircleSpokes(pivot, outterRadius, innerRadius, spokes, 0f);
        }

        public static GraphicsPath CreateChamferBox(RectangleF rect, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            GraphicsPath result;
            if (radius <= 0f)
            {
                gp.AddRectangle(rect);
                result = gp;
            }
            else
            {
                gp.AddArc(rect.Right - 2f * radius, rect.Top, radius * 2f, radius * 2f, 270f, 90f);
                gp.AddArc(rect.Right - radius * 2f, rect.Bottom - radius * 2f, radius * 2f, radius * 2f, 0f, 90f);
                gp.AddArc(rect.Left, rect.Bottom - 2f * radius, 2f * radius, 2f * radius, 90f, 90f);
                gp.AddArc(rect.Left, rect.Top, 2f * radius, 2f * radius, 180f, 90f);
                gp.CloseFigure();
                result = gp;
            }
            return result;
        }
    }

    [TypeConverter(typeof(CircleFConvertor))]
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class CircleF : ICloneable
    {
        public static CircleF Empty = new CircleF();
        private PointF _pivot;
        private float _radius;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PointF Pivot
        {
            get => this._pivot;
            set => this._pivot = value;
        }

        public float Radius
        {
            get => this._radius;
            set
            {
                if (value >= 0f)
                {
                    this._radius = value;
                }
            }
        }

        public CircleF(RectangleF rectf)
        {
            this._pivot = CircleF.GetCirclePivot(rectf);
            this._radius = CircleF.GetCircleRadius(rectf);
        }

        public CircleF(PointF pivot, float radius)
        {
            this._pivot = pivot;
            this._radius = radius;
        }

        public CircleF() : this(default(PointF), 0f)
        {
        }

        public CircleF FromRectangle(RectangleF rect)
        {
            this._pivot = CircleF.GetCirclePivot(rect);
            this._radius = CircleF.GetCircleRadius(rect);
            return this;
        }

        public static PointF GetCirclePivot(RectangleF rect)
        {
            return new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
        }

        public static float GetCircleRadius(RectangleF rect)
        {
            return Math.Min(rect.Width / 2f, rect.Height / 2f);
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(this._pivot.X - this._radius, this._pivot.Y - this._radius, this._radius * 2f, this._radius * 2f);
        }

        public static RectangleF GetBounds(RectangleF rect)
        {
            PointF p = CircleF.GetCirclePivot(rect);
            float d = CircleF.GetCircleRadius(rect);
            return new RectangleF(p.X - d, p.Y - d, d * 2f, d * 2f);
        }

        public bool Contains(float x, float y)
        {
            return this.Contains(new PointF(x, y));
        }

        public bool Contains(PointF p)
        {
            return this.Distance(p) <= this._radius;
        }

        public float Distance(CircleF c)
        {
            return this.Distance(c.Pivot);
        }

        public float Distance(PointF p)
        {
            return (float)Math.Sqrt((double)((p.X - this._pivot.X) * (p.X - this._pivot.X) + (p.Y - this._pivot.Y) * (p.Y - this._pivot.Y)));
        }

        public float Area()
        {
            return 3.14159274f * this._radius * this._radius;
        }

        public PointF PointOnPath(double sweepAngle)
        {
            return new PointF(this._pivot.X + this._radius * (float)Math.Cos(sweepAngle * 0.017453292519943295), this._pivot.Y - this._radius * (float)Math.Sin(sweepAngle * 0.017453292519943295));
        }

        public void Offset(float x, float y)
        {
            this._pivot.X = this._pivot.X + x;
            this._pivot.Y = this._pivot.Y + y;
        }

        public void Offset(PointF pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        public void Inflate(float d)
        {
            this.Radius += d;
        }

        public static bool operator !=(CircleF left, CircleF right)
        {
            return left._pivot == right._pivot && left._radius == right._radius;
        }

        public static bool operator ==(CircleF left, CircleF right)
        {
            return !(left != right);
        }
        
        public static implicit operator RectangleF(CircleF c)
        {
            return c.GetBounds();
        }


        public override string ToString()
        {
            return string.Format("{{Pivot={0},Radius={1}}}", this._pivot, this._radius);
        }

        public object Clone()
        {
            return new CircleF(this._pivot, this._radius);
        }
    }
}