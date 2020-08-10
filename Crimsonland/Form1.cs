using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Crimsonland
{
    public partial class Form1 : Form
    {
        public int windowx = 800, windowy = 600, mapx = 1000, mapy = 1000;
        public int num_ammo=3, num_enemies=10;

        Reticle reticle; 
        public Player player; 

        public List<Bullet> bullets = new List<Bullet>();
        public List<Enemy> enemies = new List<Enemy>();

        void ConsoleParcer()
        {
            System.IO.StreamWriter file = new StreamWriter("log.txt"); 
            string[] variables=  Environment.GetCommandLineArgs();
            if (variables.Length % 2 == 0)
            {
                file.WriteLine("Wrong format");
                return;
            }
            string key, value;
             for( int i=0;i<variables.Length /2; i++)
            {
                key = variables[i * 2 + 1];
                value = variables[i * 2 + 2];
                file.WriteLine( key + " " + value);
                if (key == "-window")
                {
                    file.WriteLine(key + " " + value);
                    //  this.StartButton.Text = value;              
                    this.windowy = Convert.ToInt32(value.Split('x')[1]);
                    this.windowx = Convert.ToInt32(value.Split('x')[0]);
                }
                if (key == "-map")
                {
                    file.WriteLine(key + " " + value);
                    this.mapy = Convert.ToInt32(value.Split('x')[1]);
                    this.mapx = Convert.ToInt32(value.Split('x')[0]);

                }
                if (key == "-num_enemies")
                {
                    file.WriteLine(key + " " + value);
                    this.num_enemies = Convert.ToInt32(value);
                }
                if (key == "-num_ammo")
                {
                    file.WriteLine(key + " " + value);
                    this.num_ammo = Convert.ToInt32(value);
                }
            }
            file.Close();

        }

        void MyInit()
        {

            this.StartButton = new System.Windows.Forms.Button();
            this.StartButton.Text = "Start";
            this.StartButton.Size = new System.Drawing.Size(50, 30);
            this.StartButton.Location = new System.Drawing.Point(windowx / 2, windowy / 2 - 100);
            this.StartButton.Click += new System.EventHandler(button1_Click);
            this.Controls.Add(this.StartButton);

            // 
            // Form1
            //
            this.ClientSize = new System.Drawing.Size(windowx, windowy);
            this.KeyPreview = true;

        }

        public Form1()
        {
            this.ConsoleParcer();
            InitializeComponent();
            MyInit();
            this.reticle = new Reticle(this.Width / 2, this.Height / 2, this);
            this.player = new Player(this.Width / 2, this.Height / 2, this);
    
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        public void Form1_Click(object sender, EventArgs e)
        {
            if (this.bullets.Count < this.num_ammo)
            { this.bullets.Add(new Bullet(MousePosition.X - this.Left, MousePosition.Y - this.Top, this)); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.enemies.Count < this.num_enemies)
            {
                Random rand = new Random();
                double x = rand.Next(this.Width), y = rand.Next(this.Height);
                new Enemy(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Hide();
            this.StartButton.Visible = false;
            foreach (Enemy enemy in this.enemies)
            { enemy.Dispose(); }
            this.enemies.Clear();
            foreach (Bullet bullet in this.bullets)
            { bullet.Dispose(); }
            this.bullets.Clear();
            this.timer1.Start();
        }

        public void EndGame()
        {
            Cursor.Show();
            this.StartButton.Visible = true;
            this.timer1.Stop();
            foreach (Enemy enemy in this.enemies)
            { enemy.timer.Stop(); ; }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.reticle.Move(Convert.ToInt32(MousePosition.X - this.Left+1), Convert.ToInt32(MousePosition.Y - this.Top+1));
        }

 /*       private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.player.Move(this.Width / 2, this.Height / 2);
        }
 */
        private void MoveAll(int x, int y)
        {
            player.ChangeLocation(-x, -y);
            foreach (Bullet b in this.bullets)
            {
                b.displacement(x, y);
            }
            foreach (Enemy e in this.enemies)
            {
                e.displacement(x, y);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 's') && (!player.OutOfMap(0,10)))
            {
                this.MoveAll(0, -10);
            } 
            if ((e.KeyChar == 'w') && (!player.OutOfMap(0, -10)))
            {
                this.MoveAll(0, 10);
            }
            if ((e.KeyChar == 'a') && (!player.OutOfMap(-10, 0)))
            {
                this.MoveAll(10, 0);
            }
            if ((e.KeyChar == 'd') && (!player.OutOfMap(10, 0)))
            {
                this.MoveAll(-10, 0);
            }
        }
    }
}
