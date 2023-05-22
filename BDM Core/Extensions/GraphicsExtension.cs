using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace servr.integratex.ui
{
	static class GraphicsExtension
	{
		private static GraphicsPath GenerateRoundedRectangle(
			this Graphics graphics,
			RectangleF rectangle,
			float radius)
		{
			float diameter;
			GraphicsPath path = new GraphicsPath();
			if (radius <= 0.0F)
			{
				path.AddRectangle(rectangle);
				path.CloseFigure();
				return path;
			}
			else
			{
				if (radius >= (Math.Min(rectangle.Width, rectangle.Height)) / 2.0)
					return graphics.GenerateCapsule(rectangle);
				diameter = radius * 2.0F;
				SizeF sizeF = new SizeF(diameter, diameter);
				RectangleF arc = new RectangleF(rectangle.Location, sizeF);
				path.AddArc(arc, 180, 90);
				arc.X = rectangle.Right - diameter;
				path.AddArc(arc, 270, 90);
				arc.Y = rectangle.Bottom - diameter;
				path.AddArc(arc, 0, 90);
				arc.X = rectangle.Left;
				path.AddArc(arc, 90, 90);
				path.CloseFigure();
			}
			return path;
		}
		private static GraphicsPath GenerateCapsule(
			this Graphics graphics,
			RectangleF baseRect)
		{
			float diameter;
			RectangleF arc;
			GraphicsPath path = new GraphicsPath();
			try
			{
				if (baseRect.Width > baseRect.Height)
				{
					diameter = baseRect.Height;
					SizeF sizeF = new SizeF(diameter, diameter);
					arc = new RectangleF(baseRect.Location, sizeF);
					path.AddArc(arc, 90, 180);
					arc.X = baseRect.Right - diameter;
					path.AddArc(arc, 270, 180);
				}
				else if (baseRect.Width < baseRect.Height)
				{
					diameter = baseRect.Width;
					SizeF sizeF = new SizeF(diameter, diameter);
					arc = new RectangleF(baseRect.Location, sizeF);
					path.AddArc(arc, 180, 180);
					arc.Y = baseRect.Bottom - diameter;
					path.AddArc(arc, 0, 180);
				}
				else path.AddEllipse(baseRect);
			}
			catch { path.AddEllipse(baseRect); }
			finally { path.CloseFigure(); }
			return path;
		}


		/// <summary>
		/// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
		/// for the arcs that make the rounded edges.
		/// </summary>
		/// <param name="brush">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">Width of the rectangle to draw.</param>
		/// <param name="height">Height of the rectangle to draw.</param>
		/// <param name="radius">The radius of the arc used for the rounded edges.</param>

		public static void DrawRoundedRectangle(
			this Graphics graphics,
			Pen pen,
			float x,
			float y,
			float width,
			float height,
			float radius)
		{
			RectangleF rectangle = new RectangleF(x, y, width, height);
			GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius);
			SmoothingMode old = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.DrawPath(pen, path);
			graphics.SmoothingMode = old;
		}

        /// <summary>
        /// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
        /// for the arcs that make the rounded edges.
        /// </summary>
        /// <param name="graphics">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
        /// <param name="ctrl">Control to convert capsule shape.</param>		
        public static void DrawPasswordControlCapsuleShape(
            Graphics graphics, Color backColor, Color boderColor,
            Control ctrl)
        {
           
            graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 5, ctrl.Location.Y - 4, ctrl.Width + 5, (ctrl.Height) + 7, 10);
            graphics.DrawRoundedRectangle(new Pen(ControlPaint.LightLight(boderColor), 2F), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 14, (ctrl.Height) + 7, 24);

        }
        public static void DrawControlCapsuleShape(
			Graphics graphics, Color backColor, Color boderColor,
			Control ctrl)
		{
			//graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 13, (ctrl.Height) + 7, 24);
			//graphics.DrawRoundedRectangle(new Pen(ControlPaint.Light(boderColor, 0.02f)), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 14, (ctrl.Height) + 7, 24);
			//	g.FillRoundedRectangle(new SolidBrush(Color.Red), 12, 12 + ((ctrl.Height - 64) / 2), ctrl.Width - 44, (ctrl.Height - 64)/2, 10);
			//graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 13, (ctrl.Height) + 7, 24);
			//graphics.DrawRoundedRectangle(new Pen(ControlPaint.Light(boderColor, 0.02f)), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 14, (ctrl.Height) + 7, 24);
			graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 13, (ctrl.Height) + 7, 24);
			graphics.DrawRoundedRectangle(new Pen(ControlPaint.LightLight(boderColor), 2F), ctrl.Location.X - 7, ctrl.Location.Y - 4, ctrl.Width + 14, (ctrl.Height) + 7, 24);
           
        }
		public static void DrawComboBoxCapsuleShape(
			Graphics graphics, Color backColor, Color boderColor,
			Control ctrl)
		{
			//graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X-4, ctrl.Location.Y - 4, ctrl.Width + 2, (ctrl.Height) + 7, 24);
			//graphics.DrawRoundedRectangle(new Pen(ControlPaint.LightLight(boderColor), 2F), ctrl.Location.X - 10, ctrl.Location.Y - 4, ctrl.Width + 18, (ctrl.Height) + 7, 24);
            graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 4, ctrl.Location.Y - 4, ctrl.Width + 2, (ctrl.Height) + 7, 24);
            graphics.DrawRoundedRectangle(new Pen(ControlPaint.LightLight(boderColor), 2F), ctrl.Location.X - 10, ctrl.Location.Y - 4, ctrl.Width + 18, (ctrl.Height) + 7, 24);

        }
		/// <summary>
		/// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
		/// for the arcs that make the rounded edges.
		/// </summary>
		/// <param name="graphics">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
		/// <param name="ctrl">Control to convert capsule shape.</param>		

		public static void DrawButtonControlCapsuleShape(
			Graphics graphics, Color backColor, Color boderColor,
			Control ctrl)
		{

			//graphics.DrawRoundedRectangle(new Pen(ControlPaint.Light(boderColor, 0.00f)), ctrl.Location.X - 6, ctrl.Location.Y - 2, ctrl.Width + 12, (ctrl.Height) + 5, 22);
			//g.FillRoundedRectangle(new SolidBrush(Color.Red), 12, 12 + ((ctrl.Height - 64) / 2), ctrl.Width - 44, (ctrl.Height - 64)/2, 10);
			graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 11, ctrl.Location.Y - 2, ctrl.Width + 22, (ctrl.Height) + 5, 22);
			//graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 6, ctrl.Location.Y - 3, ctrl.Width + 12, (ctrl.Height) + 5, 22);

		}

        public static void DrawLoginButtonControlCapsuleShape(
            Graphics graphics, Color backColor, Color boderColor,
            Control ctrl)
        {

            //graphics.DrawRoundedRectangle(new Pen(ControlPaint.Light(boderColor, 0.00f)), ctrl.Location.X - 6, ctrl.Location.Y - 2, ctrl.Width + 12, (ctrl.Height) + 5, 22);
            //g.FillRoundedRectangle(new SolidBrush(Color.Red), 12, 12 + ((ctrl.Height - 64) / 2), ctrl.Width - 44, (ctrl.Height - 64)/2, 10);
            graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 11, ctrl.Location.Y + 5, ctrl.Width + 22, (ctrl.Height) + 5, 22);
            //graphics.FillRoundedRectangle(new SolidBrush(backColor), ctrl.Location.X - 6, ctrl.Location.Y - 3, ctrl.Width + 12, (ctrl.Height) + 5, 22);

        }
        internal static void DrawButtonControlCapsuleShape(Graphics g, Color white, Button btnLogin)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
		/// for the arcs that make the rounded edges.
		/// </summary>
		/// <param name="brush">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">Width of the rectangle to draw.</param>
		/// <param name="height">Height of the rectangle to draw.</param>
		/// <param name="radius">The radius of the arc used for the rounded edges.</param>

		public static void DrawRoundedRectangle(
			this Graphics graphics,
			Pen pen,
			int x,
			int y,
			int width,
			int height,
			int radius)
		{
			graphics.DrawRoundedRectangle(
				pen,
				Convert.ToSingle(x),
				Convert.ToSingle(y),
				Convert.ToSingle(width),
				Convert.ToSingle(height),
				Convert.ToSingle(radius));
		}
		public static void DrawRoundedMDIRectangle(
			Graphics graphics,
		   Pen pen,
		   int x,
		   int y,
		   int width,
		   int height,
		   int radius)
		{
			graphics.DrawRoundedRectangle(
				pen,
				Convert.ToSingle(x),
				Convert.ToSingle(y),
				Convert.ToSingle(width),
				Convert.ToSingle(height),
				Convert.ToSingle(radius));
		}
		/// <summary>
		/// Fills the interior of a rounded rectangle specified by a pair of coordinates, a width, a height
		/// and the radius for the arcs that make the rounded edges.
		/// </summary>
		/// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		/// <param name="radius">The radius of the arc used for the rounded edges.</param>

		public static void FillRoundedRectangle(
			this Graphics graphics,
			Brush brush,
			float x,
			float y,
			float width,
			float height,
			float radius)
		{
			RectangleF rectangle = new RectangleF(x, y, width, height);
			GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius);
			SmoothingMode old = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.FillPath(brush, path);
			graphics.SmoothingMode = old;
		}

		/// <summary>
		/// Fills the interior of a rounded rectangle specified by a pair of coordinates, a width, a height
		/// and the radius for the arcs that make the rounded edges.
		/// </summary>
		/// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		/// <param name="radius">The radius of the arc used for the rounded edges.</param>

		public static void FillRoundedRectangle(
			this Graphics graphics,
			Brush brush,
			int x,
			int y,
			int width,
			int height,
			int radius)
		{
			graphics.FillRoundedRectangle(
				brush,
				Convert.ToSingle(x),
				Convert.ToSingle(y),
				Convert.ToSingle(width),
				Convert.ToSingle(height),
				Convert.ToSingle(radius));
		}
		public static GraphicsPath GetRoundPath(RectangleF Rect, int radius)
		{
			float m = 0.5F;
			float r2 = radius / 2f;
			GraphicsPath GraphPath = new GraphicsPath();

			GraphPath.AddArc(Rect.X + m, Rect.Y + m, radius, radius, 180, radius);
			GraphPath.AddLine(Rect.X + r2 + m, Rect.Y + m, Rect.Width - r2 - m, Rect.Y + m);
			GraphPath.AddArc(Rect.X + Rect.Width - radius - m, Rect.Y + m, radius, radius, 270, radius);
			GraphPath.AddLine(Rect.Width - m, Rect.Y + r2, Rect.Width - m, Rect.Height - r2 - m);
			GraphPath.AddArc(Rect.X + Rect.Width - radius - m,
						   Rect.Y + Rect.Height - radius - m, radius, radius, 0, radius);
			GraphPath.AddLine(Rect.Width - r2 - m, Rect.Height - m, Rect.X + r2 - m, Rect.Height - m);
			GraphPath.AddArc(Rect.X + m, Rect.Y + Rect.Height - radius - m, radius, radius, 90, radius);
			GraphPath.AddLine(Rect.X + m, Rect.Height - r2 - m, Rect.X + m, Rect.Y + r2 + m);

			GraphPath.CloseFigure();
			return GraphPath;
		}
	}
}