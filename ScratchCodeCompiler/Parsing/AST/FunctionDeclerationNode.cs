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
        public string Name { get; }
        public List<ScratchFunctionParameter> Parameters { get; } = [];
        public CodeBlockNode? Body { get; }
        public string ProcCode { get; }
        // TODO: IMPLEMENT THIS
        public ScratchType ReturnType { get; set; }

        public ScratchOpcode? Opcode { get; } = null;

        public bool IsBuiltIn => Opcode != null;

        public FunctionDeclerationNode(string name, List<ScratchFunctionParameter> parameters, CodeBlockNode body)
        {
            Name = name;
            Parameters = parameters;
            Body = body;
            ProcCode = $"{Name} " + string.Join(' ', Parameters.Select(x => "%s"));
        }

        public FunctionDeclerationNode(string name, List<ScratchFunctionParameter> parameters, ScratchOpcode opcode)
        {
            Name = name;
            Parameters = parameters;
            Body = null;
            Opcode = opcode;
            ProcCode = string.Empty; // Not used for built in functions
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            if (IsBuiltIn)
            {
                throw new Exception("Attempted to translate built in function to definition");
            }
            ScratchBlock defineBlock = new(ScratchOpcode.Procedures_Definition, ScratchUtility.GetNextGoodPosition())
            {
                flags = ScratchBlockFlags.NotStitchableAbove
            };
            blocks.Add(defineBlock);

            if (!Body!.IsEmpty)
            {
                ScratchBlock[] bodyBlocks = Body.ToScratchBlocks();
                defineBlock.Stitch(bodyBlocks.First());
                blocks.AddRange(bodyBlocks);
            }
            
            // Setup function prototype
            ScratchBlock protoBlock = new(ScratchOpcode.Procedures_Prototype, defineBlock);
            blocks.Add(protoBlock);

            ScratchMutation protoMutation = new(ProcCode); // Use string_number for all inputs temporarily (including booleans)

            // Add function arguments
            for (int i = 0; i < Parameters.Count; i++)
            {
                ScratchBlock argumentReporter = Parameters[i].Reporter;
                argumentReporter.Parent = protoBlock;
                argumentReporter.Fields.Add(new("VALUE", Parameters[i].Name));
                blocks.Add(argumentReporter);

                protoBlock.Inputs.Add(new(Parameters[i].Id.Id, argumentReporter, ScratchInputType.CustomBlock)); // Use Param ID for input
                protoMutation.AddArgument(Parameters[i].Id, Parameters[i].Name, ""); // Since we are using string_number, all default will be ""
            }

            protoBlock.Mutation = protoMutation;

            // Add prototype to define block
            defineBlock.Inputs.Add(new("custom_block", protoBlock, ScratchInputType.CustomBlock)); // For some reason this is a value
            return defineBlock;
        }

        public override string ToString()
        {
            return $"Function {Name}({string.Join(", ", Parameters)}) {Body}";
        }
    }
}
