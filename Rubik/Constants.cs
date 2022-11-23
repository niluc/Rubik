using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Rubik
{
    class Constants
    {
        public const float _orthor = 1.2f;        // Orthor
        public const float _lineWidth = 3;      //
        public const int _rubiktype = 3;        // 3x3
        // This is rubik draw size (w = h = d)
        public const int _rubikSize = 200;
        public const double Size = 200/3 - 0.003 ;
        public const int _squareSize = _rubikSize / 3;
        static public Color _lineColor = Color.Black;
        static public int language = 0;

    }
}
