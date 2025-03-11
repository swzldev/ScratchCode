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
            if (RepeatCount is VariableNode variable)
            {
                repeatBlock.Inputs.Add(new("TIMES", ScratchInputFormat.String, variable.ScratchVariable));
            }
            else if (RepeatCount is NumberLiteralNode literal)
            {
                repeatBlock.Inputs.Add(new("TIMES", ScratchInputFormat.String, literal.Value.ToString()));
            }
            else
            {
                ScratchBlock exprResult = (RepeatCount as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                blocks.Add(exprResult);
                repeatBlock.Inputs.Add(new("TIMES", exprResult));
            }

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
