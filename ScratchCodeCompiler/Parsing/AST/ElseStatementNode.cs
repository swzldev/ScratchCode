using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ElseStatementNode
    {
        public IfStatementNode ParentIf { get; }
        public IfStatementNode? ElseIfStatement { get; }
        public CodeBlockNode Body { get; set; }

        public ElseStatementNode(IfStatementNode parentIf, CodeBlockNode body)
        {
            ParentIf = parentIf;
            Body = body;
        }

        public ElseStatementNode(IfStatementNode parentIf, IfStatementNode ifStatement)
        {
            ParentIf = parentIf;
            ElseIfStatement = ifStatement;
            Body = ifStatement.Body;
        }

        public ScratchBlock[] ToScratchBlocks()
        {
            List<ScratchBlock> blocks = [];
            if (ElseIfStatement != null)
            {
                ElseIfStatement.ToScratchBlock(ref blocks);
                return [.. blocks];
            }
            else
            {
                return Body.ToScratchBlocks();
            }
        }

        public override string ToString()
        {
            string str = $"Else ";
            if (ElseIfStatement != null)
            {
                str += ElseIfStatement.ToString();
            }
            else
            {
                str += Body.ToString();
            }
            return str;
        }
    }
}
