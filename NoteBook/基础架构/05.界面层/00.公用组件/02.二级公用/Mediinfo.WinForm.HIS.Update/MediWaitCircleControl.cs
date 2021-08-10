using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
 
    [ToolboxBitmap(typeof(MediWaitCircleControl), "volume16")]
    public class MediWaitCircleControl : Control
    {
        private int _numberofspokes = 8;//控件包含的总的辐条数量
        private int _hotspokes = 5;//活动辐条数
        private float _innerRadius;//内环半径,外环半径根据控件的大小计算
        private Color _spokeColor = Color.LightGray;//辐条颜色
        private Color _hotSpokeColor = Color.Gray;//活动辐条颜色

        private float _thickness = 2;//辐条的宽度
        private bool _antialias = true;//控件绘制时是否反走样
        private bool _colockWise = true;//活动辐条的旋转方向是否顺时针旋转

        private List<PointF[]> _spokes;//辐条轮廓线列表
        protected Color[] _palette;//渐变色调色板
        private Timer _timer;//计时器
        private Pen _pen;//绘制WaitingCircle的画笔
        private int _offset = 0;//活动辐条偏移量
        private string _description = "系统正在初始化...";//描述信息
        private Font _descriptionFont = new Font("Arial", 20);
        private Color _descriptionFontColor = Color.Black;

        public MediWaitCircleControl()
        {

            
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            _timer = new Timer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _pen = new Pen(_spokeColor, _thickness);
            GeneratePalette();
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
            c.Inflate(-5.0f);
            _innerRadius = c.Radius / 2.0f;
            _spokes = ExtendedShapes.CreateWaitCircleSpokes(new PointF(c.Radius+10, ClientRectangle.Height / 2), c.Radius, _innerRadius, _numberofspokes);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawSpokes(e.Graphics);
        }

        protected void DrawSpokes(Graphics g)
        {

            Rectangle rectangle = new Rectangle(0, 0,ClientRectangle.Size.Width-1, ClientRectangle.Size.Height-1);
            g.DrawRectangle(Pens.DodgerBlue, rectangle);
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
}
