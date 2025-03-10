using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    [Flags]
    internal enum ScratchBlockFlags
    {
        NotStitchableAbove = 1,
        NotStitchableBelow = 2,
    }
}
