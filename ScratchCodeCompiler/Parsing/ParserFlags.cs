using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing
{
    [Flags]
    internal enum ParserFlags
    {
        None = 0,
        ForeverStatementInCurrentBlock = 1,
    }
}
