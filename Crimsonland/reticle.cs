using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimsonland
{
    class Reticle:Unit
    {
		public Reticle(int x, int y, Form1 f) : base(f)
		{
			this.x = x;
			this.y = y;
			this.image.Size = new System.Drawing.Size(35, 35);
			this.image.Load("../../data/reticle.png");
			this.image.Location = new System.Drawing.Point(Convert.ToInt32(f.Left), Convert.ToInt32(f.Top));
			f.Controls.Add(this.image);
			this.image.Click += new System.EventHandler(f.Form1_Click);

		}

	}
}
