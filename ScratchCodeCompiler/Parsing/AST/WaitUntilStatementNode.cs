using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class WaitUntilStatementNode : StatementNode, IScratchBlockTranslatable
    {
        public ExpressionNode Condition { get; }

        public WaitUntilStatementNode(ExpressionNode condition)
        {
            Condition = condition;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock waitUntilBlock = new(ScratchOpcode.Control_Wait_Until);
            blocks.Add(waitUntilBlock);

            // Add the condition
            waitUntilBlock.AddInputExpression("CONDITION", Condition, ref blocks);

            return waitUntilBlock;
        }
    }
}
