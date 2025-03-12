using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ProgramNode : ASTNode
    {
        public List<EventDeclerationNode> Entrys { get; set; } = [];
        public List<ExpressionNode> InitExpressions { get; set; } = [];

        private ScratchBlock InitializationEntry { get; set; }

        public ProgramNode()
        {
            InitializationEntry = new(ScratchOpcode.Event_WhenFlagClicked, ScratchUtility.GetNextGoodPosition());
        }

        public ScratchBlock[] ToScratchBlocks()
        {
            List<ScratchBlock> blocks = [];
            blocks.Add(InitializationEntry);
            ScratchBlock lastInitEntry = InitializationEntry;
            foreach (ExpressionNode entry in InitExpressions)
            {
                ScratchBlock entryBlock = (entry as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                lastInitEntry.Stitch(entryBlock);
                lastInitEntry = entryBlock;
            }
            foreach (EventDeclerationNode entry in Entrys)
            {
                entry.ToScratchBlock(ref blocks);
            }
            return [.. blocks];
        }

        public override string ToString()
        {
            return $"Program {{\n{string.Join("\n", Entrys)}\n}}";
        }
    }
}
