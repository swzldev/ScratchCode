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

        private List<ScratchId> FunctionParamIds { get; set; } = [];

        public FunctionDeclerationNode(string name, List<string> parameters, CodeBlockNode body)
        {
            FunctionName = name;
            FunctionParams = parameters;
            FunctionBody = body;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock defineBlock = new(ScratchOpcode.Procedures_Definition, ScratchUtility.GetNextGoodPosition());

            // Setup function prototype
            ScratchBlock protoBlock = new(ScratchOpcode.Procedures_Prototype, defineBlock);
            blocks.Add(protoBlock);

            ScratchMutation protoMutation = new($"{FunctionName} " + string.Join(' ', FunctionParams.Select(x => "%s"))); // Use string_number for all inputs temporarily (including booleans)

            // Add function arguments
            for (int i = 0; i < FunctionParams.Count; i++)
            {
                FunctionParamIds.Add(new());

                ScratchBlock argumentReporter = new(ScratchOpcode.Argument_Reporter_String_Number, protoBlock); // Use string_number for all inputs temporarily (including booleans)
                argumentReporter.Fields.Add(new("VALUE", FunctionParams[i]));
                blocks.Add(argumentReporter);

                protoBlock.Inputs.Add(new(FunctionParamIds[i].Id, argumentReporter, ScratchInputType.CustomBlock)); // Use Param ID for input
                protoMutation.AddArgument(FunctionParamIds[i], FunctionParams[i], ""); // Since we are using string_number, all default will be ""
            }

            protoBlock.Mutation = protoMutation;

            // Add prototype to define block
            defineBlock.Inputs.Add(new("custom_block", protoBlock, ScratchInputType.CustomBlock)); // For some reason this is a value
            return defineBlock;
        }

        public override string ToString()
        {
            return $"Function {FunctionName}({string.Join(", ", FunctionParams)}) {FunctionBody}";
        }
    }
}
