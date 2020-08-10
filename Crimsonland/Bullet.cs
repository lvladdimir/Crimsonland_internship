using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Crimsonland
{
	public class Bullet:Unit
	{
		protected double vectx, vecty ;
		protected int speed=40, num_ammo=3;
		protected System.Windows.Forms.Timer timer;

		public Bullet(double x, double y, Form1 f):base(f)
		{
			this.num_ammo = f.num_ammo;
			this.x = f.Width/2+20;
			this.y = f.Height/2+20;
			vectx = speed * (x - this.x) / Math.Sqrt((x - this.x)* (x - this.x) + (y - this.y) * (y - this.y));
			vecty = speed * (y - this.y) / Math.Sqrt((x - this.x) * (x - this.x) + (y - this.y) * (y - this.y));
			this.image.Size = new System.Drawing.Size(20, 14);
			this.image.Load("../../data/bullet1.png");
			this.image.Location = new System.Drawing.Point(Convert.ToInt32(this.x), Convert.ToInt32(this.y));
			//this.image.Size = new System.Drawing.Size(50, 30);
			f.Controls.Add(this.image);
			this.timer = new System.Windows.Forms.Timer();
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			this.timer.Start();	

		}

		private void Reload()
        {
			if ((this.baseform.bullets.Count == this.num_ammo) && (this.baseform.bullets.Last().Equals(this)))
			{
				this.baseform.bullets.Clear();
			}
		}


		public void Dispose()
		{
			this.timer.Dispose();
			this.image.Dispose();
			this.Reload();

		}

		private void intersect()
        {
			foreach(Enemy enemy in this.baseform.enemies)
            {
				if (this.image.Bounds.IntersectsWith(enemy.image.Bounds))
                {
					enemy.Dispose();					
					this.baseform.enemies.Remove(enemy);
					this.Dispose();
					break;
				}
            }
        }
		

		private void Move()
        {
			this.x += this.vectx;
			this.y += this.vecty;
			this.image.Location = new System.Drawing.Point(Convert.ToInt32( this.x),Convert.ToInt32( this.y));
			this.intersect();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.Move();

			if (this.OutOfMap())
			{
				this.Dispose();
			}
		}

	}
}