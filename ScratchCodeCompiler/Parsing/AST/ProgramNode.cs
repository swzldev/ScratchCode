using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ProgramNode : ASTNode
    {
        public List<ASTNode> Code { get; private set; } = [];

        public void AddExpression(ASTNode expr)
        {
            Code.Add(expr);
        }

        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock, out ScratchVariable? returnVar)
        {
            returnBlock = null;
            returnVar = null;
            List<ScratchBlock> blocks = [];
            foreach (var node in Code)
            {
                ScratchBlock[] nodeBlocks = node.ToScratchBlocks(out _, out _);
                if (blocks.Count > 0)
                {
                    ScratchBlock[] topLevelBlocks = blocks.Where(x => x.IsTopLevel).ToArray();
                    ScratchBlock[] topLevelNodeBlocks = nodeBlocks.Where(x => x.IsTopLevel).ToArray();
                    topLevelBlocks.Last().Next = topLevelNodeBlocks.First();
                    topLevelNodeBlocks.First().Parent = topLevelBlocks.Last();
                }
                blocks.AddRange(nodeBlocks);
            }
            return blocks.ToArray();
        }

        public override string ToString()
        {
            return $"Program {{\n{string.Join("\n", Code)}\n}}";
        }
    }
}
