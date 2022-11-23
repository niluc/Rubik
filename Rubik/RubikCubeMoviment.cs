using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class RubikCubeMoviment
    {
        public Depth Depth { get; private set; }
        public Spin Spin { get; private set; }
        public Axis Axis { get; private set; }
        public int n { get; private set; }
        public RubikCubeMoviment(Depth Depth, Spin Spin, Axis Axis, int n)
        {
            this.Axis = Axis;
            this.Depth = Depth;
            this.Spin = Spin;
            this.n = n;
        }
    }
}
