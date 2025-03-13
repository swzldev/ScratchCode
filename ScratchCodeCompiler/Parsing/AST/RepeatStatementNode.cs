using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class RepeatStatementNode : StatementNode, IScratchBlockTranslatable
    {
        public ExpressionNode RepeatCount { get; }
        public CodeBlockNode Body { get; set; }

        public RepeatStatementNode(ExpressionNode repeatCount, CodeBlockNode body)
        {
            RepeatCount = repeatCount;
            Body = body;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock repeatBlock = new(ScratchOpcode.Control_Repeat);
            blocks.Add(repeatBlock);

            // Add the repeat count
            repeatBlock.AddInputExpression("TIMES", RepeatCount, ref blocks);

            // Add the body
            if (Body.IsEmpty)
            {
                repeatBlock.Inputs.Add(new("SUBSTACK"));
                return repeatBlock;
            }
            ScratchBlock[] bodyBlocks = Body.ToScratchBlocks();
            repeatBlock.Inputs.Add(new("SUBSTACK", bodyBlocks.First()));
            bodyBlocks.First().Parent = repeatBlock;
            blocks.AddRange(bodyBlocks);
            return repeatBlock;
        }
    }
}
