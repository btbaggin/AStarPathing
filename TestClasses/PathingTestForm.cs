using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPathing
{
    public partial class PathingTestForm : Form
    {
        public PathingTestForm()
        {
            InitializeComponent();
        }
        TileMap map;
        AStarPathing path;
        Pen pen;

        private void PathingTest2_Load(object sender, EventArgs e)
        {
            map = new TileMap(20, 20, Init);
            path = new AStarPathing(map);
            map.AllowDiagonal = true;

            pen = new Pen(new SolidBrush(Color.Black));
        }

        private static Node Init(int x, int y)
        {
            if (x == 10 || y == 10)
            {
                return new WaterNode(x, y);
            }
            else if (x == 5 && y > 5 && y < 15)
            {
                return new MountainNode(x, y);
            }
            else
            {
                return new LandNode(x, y);
            }
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            //Draw lines
            for (int x = 0; x <= map.Width; x++)
            {
                e.Graphics.DrawLine(pen, new Point(x * 10, 0), new Point(x * 10, Height)); 
            }

            for (int y = 0; y <= map.Height; y++)
            {
                e.Graphics.DrawLine(pen, new Point(0, y * 10), new Point(Width, y * 10));
            }

            //Draw tiles
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.GetNode(x,y).GetType() == typeof(LandNode))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Green), new Rectangle(x * 10, y * 10, 10, 10));
                    }
                    else if (map.GetNode(x, y).GetType() == typeof(MountainNode))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Brown), new Rectangle(x * 10, y * 10, 10, 10));
                    }
                    else if (map.GetNode(x, y).GetType() == typeof(WaterNode))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(x * 10, y * 10, 10, 10));
                    }
                }
            }

            //Draw path
            foreach (Node n in path.FindPath(map.GetNode(1, 6), map.GetNode(19, 19)))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(n.X * 10, n.Y * 10, 10, 10));
            }
        }
    }
}
