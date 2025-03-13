using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        }

        public override ScratchType GetReturnType() => Decleration.ReturnType;

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            if (Decleration.IsBuiltIn)
            {
                ScratchBlock funcBlock = new(Decleration.Opcode!.Value);
                blocks.Add(funcBlock);
                for (int i = 0; i < Decleration.Parameters.Count; i++)
                {
                    string paramName = Decleration.Parameters[i].Name;
                    funcBlock.AddInputExpression(paramName, FunctionArguments[i], ref blocks);
                }
                return funcBlock;
            }
            ScratchBlock callBlock = new(ScratchOpcode.Procedures_Call);
            blocks.Add(callBlock);
            ScratchMutation callMutation = new(Decleration.ProcCode);
            for (int i = 0; i < Decleration.Parameters.Count; i++)
            {
                ScratchId paramId = Decleration.Parameters[i].Id;
                callMutation.AddArgument(paramId, Decleration.Parameters[i].Name, "");
                callBlock.AddInputExpression(paramId.Id, FunctionArguments[i], ref blocks);
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
