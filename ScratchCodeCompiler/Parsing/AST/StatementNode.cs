using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class StatementNode : ASTNode
    {
        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock, out ScratchVariable? returnVar)
        {
            throw new NotImplementedException();
        }
    }
}
