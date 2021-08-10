using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
namespace Mediinfo.WinForm.HIS.Update
{
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
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override string ToString()
		{
			return string.Format("{Pivot={0},Radius={1}}", this._pivot, this._radius);
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
            if (value is string)
            {
                string[] ti = ((string)value).Split(new char[]
                {
                    ','
                });
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
            if (value is CircleF)
            {
                if (destinationType == typeof(string))
                {
                    CircleF c = value as CircleF;
                    result = string.Format("({0},{1}),{2}", c.Pivot.X, c.Pivot.Y, c.Radius);
                    return result;
                }
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
                throw new ArgumentOutOfRangeException("outterRadius", "多边形的外接圆半径必须大于0");
            }
            if (points < 3)
            {
                throw new ArgumentOutOfRangeException("points", "多边形的边数必须大于等于3");
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
                throw new ArgumentOutOfRangeException("points");
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
            get
            {
                return this._pivot;
            }
            set
            {
                this._pivot = value;
            }
        }
        public float Radius
        {
            get
            {
                return this._radius;
            }
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
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public static implicit operator RectangleF(CircleF c)
        {
            return c.GetBounds();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
