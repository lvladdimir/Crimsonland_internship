using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Crimsonland
{
    public class Enemy:Unit
    {
		Random rand = new Random();
		protected double vectx, vecty;
		protected int speed = 1;
		public System.Windows.Forms.Timer timer;

		//get random coords x,y on the field
		//without squere 200*200 if the center
		void spawn()
        {
			int radius = 100;
			this.x =this.windowx/2 + (radius +  rand.Next((this.windowx - radius) / 2)) * (1 - 2 * rand.Next(2));
			this.y =this.windowy/2 + (radius +  rand.Next((this.windowy - radius) / 2)) * (1 - 2 * rand.Next(2));			
        }

		public Enemy( Form1 f) : base(f)
		{
			spawn();
			if (this.intersect(this.x, this.y)) 
			{
				this.image.Dispose();
				return; 
			}
			vectx = speed * (f.Width/2 - this.x) / Math.Sqrt((f.Width/2 - this.x) * (f.Width/2 - this.x) + (f.Height/2 - this.y) * (f.Height/2 - this.y));
			vecty = speed * (f.Height/2 - this.y) / Math.Sqrt((f.Width/2 - this.x) * (f.Width/2 - this.x) + (f.Height/2 - this.y) * (f.Height/2 - this.y));
			this.image.Size = new System.Drawing.Size(35, 35);
			this.image.Load("../../data/enemy.png");
			this.image.Location = new System.Drawing.Point(Convert.ToInt32(this.x), Convert.ToInt32(this.y));
			//this.image.Size = new System.Drawing.Size(50, 30);
			f.Controls.Add(this.image);
			this.timer = new System.Windows.Forms.Timer();
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			this.timer.Start();
			this.image.Click += new System.EventHandler(f.Form1_Click);

			f.enemies.Add(this);
		}

		public void Dispose()
		{
			this.timer.Dispose();
			this.image.Dispose();
		}

		public bool intersect(double x, double y, int width=35, int height=35)
		{
			System.Drawing.Rectangle bounds =
				new System.Drawing.Rectangle(Convert.ToInt32( x),Convert.ToInt32( y), width, height);

			if (bounds.IntersectsWith(this.baseform.player.image.Bounds))
			{ this.baseform.EndGame();}

				foreach (Enemy enemy in this.baseform.enemies)
			{
				if (enemy.Equals(this))
					{ continue; }
				if (bounds.IntersectsWith(enemy.image.Bounds))
					{ return true; }
			}
			return false;
		}

		private void Move()
		{
			this.vectx = speed * (this.baseform.Width / 2 - this.x) / Math.Sqrt((this.baseform.Width / 2 - this.x) * (this.baseform.Width / 2 - this.x) + (this.baseform.Height / 2 - this.y) * (this.baseform.Height / 2 - this.y));
			this.vecty = speed * (this.baseform.Height / 2 - this.y) / Math.Sqrt((this.baseform.Width / 2 - this.x) * (this.baseform.Width / 2 - this.x) + (this.baseform.Height / 2 - this.y) * (this.baseform.Height / 2 - this.y));
			if  (!this.intersect(this.x+vectx,this.y+vecty))
			{	
				this.x += this.vectx;
				this.y += this.vecty;
				this.image.Location = new System.Drawing.Point(Convert.ToInt32(this.x), Convert.ToInt32(this.y));
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.Move();
		}
	}
}
