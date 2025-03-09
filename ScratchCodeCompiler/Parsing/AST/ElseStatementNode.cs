using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
