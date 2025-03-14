using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing
{
    internal enum CodeBlockContext
    {
        Event,
        Function,
        IfStatement,
        ElseStatement,
        ForeverStatement,
        RepeatStatement,
        RepeatUntilStatement,
    }
}
