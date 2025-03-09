using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class VariableNode : ASTNode
    {
        private static List<string> variables = [];

        public string VariableName { get; }
        public bool IsDecleration { get; }

        public VariableNode(string variableName)
        {
            VariableName = variableName;
            if (!variables.Contains(VariableName))
            {
                IsDecleration = true;
                variables.Add(VariableName);
            }
        }

        public override string ToString()
        {
            if (IsDecleration)
            {
                return $"VariableDecleration({VariableName})";
            }
            return $"Variable({VariableName})";
        }
    }
}
