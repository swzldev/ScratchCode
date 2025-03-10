using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class FunctionCallNode : ExpressionNode, IScratchBlockTranslatable
    {
        public string FunctionName { get; }
        public List<ExpressionNode> FunctionArguments { get; set; }

        private FunctionDeclerationNode decleration;

        public FunctionCallNode(string functionName, List<ExpressionNode> functionArgs)
        {
            FunctionName = functionName;
            FunctionArguments = functionArgs;

            FunctionDeclerationNode? decl = FunctionDeclerationNode.GetDecleration(functionName);
            if (decl == null)
            {
                throw new Exception($"Function {functionName} is not defined");
            }
            if (decl.FunctionParams.Count != FunctionArguments.Count)
            {
                throw new Exception($"Function {functionName} expects {decl.FunctionParams.Count} arguments, but {FunctionArguments.Count} were provided");
            }
            decleration = decl;

            // TODO: add support for built-in functions
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock callBlock = new(ScratchOpcode.Procedures_Call);
            blocks.Add(callBlock);
            ScratchMutation callMutation = new(decleration.ProcCode);
            for (int i = 0; i < decleration.FunctionParams.Count; i++)
            {
                callMutation.AddArgument(decleration.FunctionParamIds[i], decleration.FunctionParams[i], "");
                callBlock.Inputs.Add(new(decleration.FunctionParamIds[i].Id, ScratchInputFormat.String, ""));
            }
            callBlock.Mutation = callMutation;
            return callBlock;
        }

        public override string ToString()
        {
            return $"{FunctionName}({string.Join(", ", FunctionArguments)})";
        }
    }
}
