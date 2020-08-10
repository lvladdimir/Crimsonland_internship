using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimsonland 
{
    public abstract class Unit
    {
		protected double x, y;
		protected int mapx, mapy,windowx,windowy;
		public System.Windows.Forms.PictureBox image = new System.Windows.Forms.PictureBox();
		protected Form1 baseform;

		protected Unit(Form1 f)
        {
			this.baseform = f;
			this.mapx = f.mapx;
			this.mapy = f.mapy;
			this.windowx = f.windowx;
			this.windowy = f.windowy;
		}

		public void displacement(int x, int y)
		{
			this.x += x;
			this.y += y;
			this.image.Location = new System.Drawing.Point(Convert.ToInt32(this.x), Convert.ToInt32(this.y));
		}

		public void Move(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.image.Location = new System.Drawing.Point(Convert.ToInt32(this.x), Convert.ToInt32(this.y));
		}

		public bool OutOfMap()
        {
			if ((x < 0) || (x > this.mapx) || (y < 0) || (y > this.mapy))
				return true;
			return false;
        }
	}
}
