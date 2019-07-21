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
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Console.WriteLine("hello world");

			Bitmap testGem = (Bitmap)Image.FromFile("C:\\Users\\peter\\Documents\\DiamondDiggerBot\\DiamondDiggerBot\\DiamondDiggerBot\\TestImages\\PurpleGroundBlock.png");
			Console.WriteLine(testGem);

			Console.WriteLine("Width = " + testGem.Width + " height = " + testGem.Height);

			for (int xCord = 0; xCord < testGem.Width; xCord++)
			{
				for (int yCord = 0; yCord < testGem.Height; yCord++)
				{
					Console.WriteLine($"Color at {xCord},{yCord} = {testGem.GetPixel(xCord, yCord)}");

				}
			}
		}
	}
}
