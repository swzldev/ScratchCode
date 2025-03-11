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
        public FunctionDeclerationNode Decleration { get; set; }

        public FunctionCallNode(string functionName, List<ExpressionNode> functionArgs, FunctionDeclerationNode decleration)
        {
            FunctionName = functionName;
            FunctionArguments = functionArgs;
            Decleration = decleration;
            // TODO: add support for built-in functions
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock callBlock = new(ScratchOpcode.Procedures_Call);
            blocks.Add(callBlock);
            ScratchMutation callMutation = new(Decleration.ProcCode);
            for (int i = 0; i < Decleration.FunctionParams.Count; i++)
            {
                callMutation.AddArgument(Decleration.FunctionParamIds[i], Decleration.FunctionParams[i], "");
                callBlock.Inputs.Add(new(Decleration.FunctionParamIds[i].Id, ScratchInputFormat.String, ""));
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
