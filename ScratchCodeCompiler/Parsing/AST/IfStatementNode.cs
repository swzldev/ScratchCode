﻿namespace ScratchCodeCompiler.Parsing.AST
{
    internal class IfStatementNode : StatementNode
    {
        public ExpressionNode Condition { get; set; }
        public CodeBlockNode Body { get; set; }
        public ElseStatementNode? ElseNode { get; set; }

        public IfStatementNode(ExpressionNode condition, CodeBlockNode body)
        {
            Condition = condition;
            Body = body;
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
