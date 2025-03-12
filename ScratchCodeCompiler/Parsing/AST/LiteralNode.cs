using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class LiteralNode : ExpressionNode
    {
        public virtual string GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
