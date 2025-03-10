using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class FunctionDeclerationNode : StatementNode, IScratchBlockTranslatable
    {
        private static Dictionary<string, FunctionDeclerationNode> GlobalFunctions { get; set; } = [];

        public string FunctionName { get; }
        public List<string> FunctionParams { get; }
        public CodeBlockNode FunctionBody { get; }

        public FunctionDeclerationNode(string name, List<string> parameters, CodeBlockNode body)
        {
            FunctionName = name;
            FunctionParams = parameters;
            FunctionBody = body;

            FunctionPrototype = new(ScratchOpcode.Procedures_Prototype, ScratchUtility.GetNextGoodPosition());
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock defineBlock = new(ScratchOpcode.Procedures_Definition, ScratchUtility.GetNextGoodPosition());

            // Setup function prototype
            ScratchBlock protoBlock = new(ScratchOpcode.Procedures_Prototype, defineBlock);
            protoBlock.Mutation = new();
        }

        public override string ToString()
        {
            return $"Function {FunctionName}({string.Join(", ", FunctionParams)}) {FunctionBody}";
        }
    }
}
