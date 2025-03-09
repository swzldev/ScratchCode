using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchVector2 : IScratchJsonable
    {
        public int x, y;

        public ScratchVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string ToJson()
        {
            return $"\"x\":{x},\"y\":{y}";
        }
    }
}
