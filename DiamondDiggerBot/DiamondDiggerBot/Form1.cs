using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DiamondDiggerBot
{
	public partial class Form1 : Form
	{
        XNAPopupWindow xnaWindow;

        public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Console.WriteLine("hello world");


			//Console.WriteLine(testGem);

			//Console.WriteLine("Width = " + testGem.Width + " height = " + testGem.Height);

			/*
			for (int xCord = 0; xCord < testGem.Width; xCord++)
			{
				for (int yCord = 0; yCord < testGem.Height; yCord++)
				{
					Console.WriteLine($"Color at {xCord},{yCord} = {testGem.GetPixel(xCord, yCord)}");

				}
			}
			*/

			Bitmap testGem = (Bitmap)Image.FromFile("C:\\Users\\peter\\Documents\\DiamondDiggerBot\\DiamondDiggerBot\\DiamondDiggerBot\\TestImages\\PurpleGroundBlock.png");
			Color purpleGem = FindAverageColor(new Point(0, 0), testGem.Width, testGem.Height, testGem);
			//Console.WriteLine(purpleGem);
			Diamond d = new Diamond(purpleGem);
			Console.WriteLine(d);

			Bitmap testGem2 = (Bitmap)Image.FromFile("C:\\Users\\peter\\Documents\\DiamondDiggerBot\\DiamondDiggerBot\\DiamondDiggerBot\\TestImages\\GreenWaterBlock.png");
			Color greenGem = FindAverageColor(new Point(0, 0), testGem2.Width, testGem2.Height, testGem2);
			//Console.WriteLine(purpleGem);
			Diamond d2 = new Diamond(greenGem);
			Console.WriteLine(d2);
		}

		public static Color FindAverageColor(Point corner, int GEM_WIDTH, int GEM_HEIGHT, Bitmap bm)
		{
			int rSum = 0;
			int gSum = 0;
			int bSum = 0;
			int numColors = 36; // precalced for speed, needs to change if the bounds change

			int lowerBoundX = (GEM_WIDTH / 2) - 3;
			int upperBoundX = (GEM_WIDTH / 2) + 3;

			int lowerBoundY = (GEM_HEIGHT / 2) - 3;
			int upperBoundY = (GEM_HEIGHT / 2) + 3;

			for (int x = lowerBoundX; x < upperBoundX; x++)
			{
				for (int y = lowerBoundY; y < upperBoundY; y++)
				{
					Color pixel = bm.GetPixel(corner.X + x, corner.Y + y);
					rSum += pixel.R;
					gSum += pixel.G;
					bSum += pixel.B;
				}
			}
			return Color.FromArgb(rSum / numColors, gSum / numColors, bSum / numColors);
		}

        private void Button1_Click(object sender, EventArgs e)
        {
            if (xnaWindow == null)
            {
                xnaWindow = new XNAPopupWindow();
                int height = 500;
                int width = 500;
                int startBarHeight = 50;
                //xnaWindow.UpdateWindowRect(new Rectangle(0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 50));
                xnaWindow.UpdateWindowRect(new Rectangle((Screen.PrimaryScreen.WorkingArea.Width-width)/2, (Screen.PrimaryScreen.WorkingArea.Height - startBarHeight - height - XNAPopupWindow.STATUS_BAR_HEIGHT)/2, height, width));
                xnaWindow.UpdatePopupWindow(new Microsoft.Xna.Framework.Rectangle(0, 0, 500, 500), "     Alek smells");
            }
        }
    }
}
