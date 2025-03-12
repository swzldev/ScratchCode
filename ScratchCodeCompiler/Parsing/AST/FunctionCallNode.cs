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
                for (int i = 0; i < Decleration.FunctionParams.Count; i++)
                {
                    string paramName = Decleration.FunctionParams[i];
                    if (FunctionArguments[i] is VariableNode variable)
                    {
                        funcBlock.Inputs.Add(new(paramName, ScratchInputFormat.String, variable.ScratchVariable));
                    }
                    else if (FunctionArguments[i] is NumberLiteralNode literal)
                    {
                        funcBlock.Inputs.Add(new(paramName, ScratchInputFormat.String, literal.Value.ToString()));
                    }
                    else
                    {
                        ScratchBlock exprResult = (FunctionArguments[i] as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                        blocks.Add(exprResult);
                        funcBlock.Inputs.Add(new(paramName, exprResult));
                    }
                }
                return funcBlock;
            }
            ScratchBlock callBlock = new(ScratchOpcode.Procedures_Call);
            blocks.Add(callBlock);
            ScratchMutation callMutation = new(Decleration.ProcCode);
            for (int i = 0; i < Decleration.FunctionParams.Count; i++)
            {
                ScratchId paramId = Decleration.FunctionParamIds[i];
                callMutation.AddArgument(paramId, Decleration.FunctionParams[i], "");
                if (FunctionArguments[i] is VariableNode variable)
                {
                    callBlock.Inputs.Add(new(paramId.Id, ScratchInputFormat.String, variable.ScratchVariable));
                }
                else if (FunctionArguments[i] is NumberLiteralNode literal)
                {
                    callBlock.Inputs.Add(new(paramId.Id, ScratchInputFormat.String, literal.Value.ToString()));
                }
                else
                {
                    ScratchBlock exprResult = (FunctionArguments[i] as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                    blocks.Add(exprResult);
                    callBlock.Inputs.Add(new(paramId.Id, exprResult));
                }
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
