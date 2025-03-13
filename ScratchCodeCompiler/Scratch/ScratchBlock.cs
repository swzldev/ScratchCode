using ScratchCodeCompiler.Parsing.AST;
using static System.Reflection.Metadata.BlobBuilder;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchBlock : IScratchJsonable
    {
        public ScratchId Id { get; }
        public ScratchOpcode Opcode { get; }
        public ScratchBlock? Next { get; set; }
        public ScratchBlock? Parent { get; set; }
        public List<ScratchInput> Inputs { get; set; } = [];
        public List<ScratchField> Fields { get; set; } = [];
        public ScratchMutation? Mutation { get; set; }
        public ScratchVector2 Position { get; set; }

        public bool IsTopLevel => Parent == null;

        public ScratchBlockFlags flags;

        public ScratchBlock(ScratchOpcode opcode)
        {
            Opcode = opcode;
            Position = new(0, 0);
            Id = new ScratchId();
        }

        public ScratchBlock(ScratchOpcode opcode, ScratchVector2 position)
        {
            Opcode = opcode;
            Position = position;
            Id = new ScratchId();
        }

        public ScratchBlock(ScratchOpcode opcode, ScratchBlock parent)
        {
            Opcode = opcode;
            Position = new(0, 0);
            Id = new ScratchId();
            Parent = parent;
        }

        public void AddInputExpression(string name, ExpressionNode expression, ref List<ScratchBlock> blocks)
        {
            // Expression is a variable
            if (expression is VariableNode variable)
            {
                // Regular var
                if (variable.ScratchVariable.Type == ScratchVariableType.Regular)
                {
                    // String format is default for variable assignment
                    Inputs.Add(new(name, ScratchInputFormat.String, variable.ScratchVariable));
                }
                // Parameter var
                else if (variable.ScratchVariable.Type == ScratchVariableType.Parameter)
                {
                    Inputs.Add(new(name, ScratchInputFormat.String, variable.ScratchVariable));
                }
            }
            // Expression is a literal
            else if (expression is LiteralNode literal)
            {
                Inputs.Add(new(name, ScratchInputFormat.String, literal.GetValue()));
            }
            // Expression is another expression
            else
            {
                ScratchBlock exprResult = (expression as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                blocks.Add(exprResult);
                Inputs.Add(new(name, exprResult));
            }
        }

        public void Stitch(ScratchBlock block)     
        {
            Next = block;
            block.Parent = this;
        }

        public string ToJson()
        {
            // {"vPIm|jD@a6e6.]Z,rmMf":{"opcode":"operator_add","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":479,"y":-93},"IoLU-C;j))X*t=1RFs/J":{"opcode":"operator_subtract","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":448,"y":-10},"qQ=X/EgRFbSsC4n?g]HN":{"opcode":"operator_multiply","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":459,"y":69},"lvy`K7Z1Wm~AJ?TcbV79":{"opcode":"operator_divide","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":464,"y":140},"7-Ye~Q]vBuw+H?eM=otY":{"opcode":"data_setvariableto","next":"@7=2BhgQdrfdX`W+1QV|","parent":null,"inputs":{"VALUE":[1,[10,"0"]]},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":true,"x":344,"y":323},"@7=2BhgQdrfdX`W+1QV|":{"opcode":"data_changevariableby","next":"sF5=ECfqpZ*FB)sq1e,#","parent":"7-Ye~Q]vBuw+H?eM=otY","inputs":{"VALUE":[1,[4,"1"]]},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":false},"sF5=ECfqpZ*FB)sq1e,#":{"opcode":"data_showvariable","next":"kCCD{o{S!cvI3IC0OZB8","parent":"@7=2BhgQdrfdX`W+1QV|","inputs":{},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":false},"kCCD{o{S!cvI3IC0OZB8":{"opcode":"data_hidevariable","next":null,"parent":"sF5=ECfqpZ*FB)sq1e,#","inputs":{},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":false}}
            string json = $"{Id.ToJson()}:{{";
            json += $"\"opcode\":\"{Opcode.ToString().ToLower()}\",";
            json += $"\"next\":{(Next == null ? "null" : Next.Id.ToJson())},";
            json += $"\"parent\":{(Parent == null ? "null" : Parent.Id.ToJson())},";
            json += "\"inputs\":{";
            json += string.Join(",", Inputs.Select(i => i.ToJson()));
            json += "},";
            json += "\"fields\":{";
            json += string.Join(",", Fields.Select(f => f.ToJson()));
            json += "},";
            json += "\"shadow\":false,";
            json += $"\"topLevel\":{IsTopLevel.ToString().ToLower()}";
            if (Mutation != null)
            {
                json += $",\"mutation\":{{{Mutation.ToJson()}}}";
            }
            if (IsTopLevel)
            {
                json += $",{Position.ToJson()}";
            }
            json += "}";
            return json;
        }
    }
}
