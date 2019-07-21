using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DiamondDiggerBot
{
	class Diamond
	{
		// Properties
		public DiamondColor myColor = DiamondColor.UNKNOWN;
		public bool isGroundBlock = false;

		// Constructors
		public Diamond(Color inputColor) {
			if (inputColor == PURPLE_COLOR)
			{
				myColor = DiamondColor.PURPLE;
			}
		}

		// Methods
		public override string ToString()
		{
			return $"Is a ground block? {isGroundBlock} Diamond color? {myColor}";
		}

		// Constants
		public enum DiamondColor
		{
			PURPLE = 0,
			UNKNOWN,
		}

		public Color PURPLE_COLOR = Color.FromArgb(251, 56, 251);
	}
}
