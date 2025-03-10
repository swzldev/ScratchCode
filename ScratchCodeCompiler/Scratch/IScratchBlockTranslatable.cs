using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal interface IScratchBlockTranslatable
    {
        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks);
    }
}
