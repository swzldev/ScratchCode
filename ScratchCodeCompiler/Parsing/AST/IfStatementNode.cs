using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class IfStatementNode : StatementNode, IScratchBlockTranslatable
    {
        public ExpressionNode Condition { get; set; }
        public CodeBlockNode Body { get; set; }
        public ElseStatementNode? ElseNode { get; set; }

        public IfStatementNode(ExpressionNode condition, CodeBlockNode body)
        {
            Condition = condition;
            Body = body;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            if (ElseNode != null)
            {
                ScratchBlock ifElseBlock = new(ScratchOpcode.Control_If_Else, new ScratchVector2(0, 0));
                blocks.Add(ifElseBlock);

                // Fix these functions at some point
                ScratchBlock[] ifBodyBlocks = Body.ToScratchBlocks();
                ifElseBlock.Inputs.Add(new("SUBSTACK", ifBodyBlocks.First()));
                ifBodyBlocks.First().Parent = ifElseBlock;
                blocks.AddRange(ifBodyBlocks);

                ScratchBlock[] elseBlocks = ElseNode.ToScratchBlocks();
                ifElseBlock.Inputs.Add(new("SUBSTACK2", elseBlocks.First()));
                elseBlocks.First().Parent = ifElseBlock;
                blocks.AddRange(elseBlocks);

                ScratchBlock ieCndBlock = (Condition as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                ieCndBlock.Parent = ifElseBlock;
                ifElseBlock.Inputs.Add(new("CONDITION", ieCndBlock));
                blocks.Add(ieCndBlock);

                return ifElseBlock;
            }
            ScratchBlock ifBlock = new(ScratchOpcode.Control_If, new ScratchVector2(0, 0));
            blocks.Add(ifBlock);

            ScratchBlock cndBlock = (Condition as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
            cndBlock.Parent = ifBlock;
            ifBlock.Inputs.Add(new("CONDITION", cndBlock));
            blocks.Add(cndBlock);

            ScratchBlock[] bodyBlocks = Body.ToScratchBlocks();
            ifBlock.Inputs.Add(new("SUBSTACK", bodyBlocks.First()));
            bodyBlocks.First().Parent = ifBlock;
            blocks.AddRange(bodyBlocks);

            return ifBlock;
        }

        public override string ToString()
        {
            string str = $"If ({Condition}) {Body}";
            if (ElseNode != null)
            {
                str += ElseNode.ToString();
            }
            return str;
        }
    }
}
