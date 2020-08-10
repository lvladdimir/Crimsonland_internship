using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimsonland
{
	public class Player : Unit
	{
		public Player(int x, int y, Form1 f) : base(f)
		{
			this.x = x;
			this.y = y;

			this.image.Size = new System.Drawing.Size(50, 40);
			this.image.Load("../../data/unicorn.png");
			this.image.Location = new System.Drawing.Point(x, y);
			f.Controls.Add(this.image);
			//this.image.Size = new System.Drawing.Size(50, 30);
			f.Controls.Add(this.image);
		}

		public void ChangeLocation(int x, int y)
		{
			this.x += x;
			this.y += y;
		}

		public bool OutOfMap(int x, int y)
		{
			if ((this.x+x < 0) || (this.x+x > this.mapx) || (this.y+ y < 0) || (this.y+ y > this.mapy))
				return true;
			return false;
		}

	}
}
