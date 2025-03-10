using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ForeverStatementNode : StatementNode, IScratchBlockTranslatable
    {
        public CodeBlockNode Body { get; }

        public ForeverStatementNode(CodeBlockNode body)
        {
            Body = body;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock foreverBlock = new(ScratchOpcode.Control_Forever, new ScratchVector2(0, 0));
            blocks.Add(foreverBlock);
            if (Body.IsEmpty)
            {
                foreverBlock.Inputs.Add(new("SUBSTACK"));
                return foreverBlock;
            }
            ScratchBlock[] bodyBlocks = Body.ToScratchBlocks();
            foreverBlock.Inputs.Add(new("SUBSTACK", bodyBlocks.First()));
            bodyBlocks.First().Parent = foreverBlock;
            blocks.AddRange(bodyBlocks);
            return foreverBlock;
        }

        public override string ToString()
        {
            return $"Forever({Body})";
        }
    }
}
