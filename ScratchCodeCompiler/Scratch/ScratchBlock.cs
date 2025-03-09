using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchBlock : IScratchJsonable
    {
        public ScratchId Id { get; }
        public ScratchOpcode Opcode { get; }
        public ScratchBlock? Next { get; set; }
        public ScratchBlock? Parent { get; set; }
        public ScratchBlockInput[] Inputs { get; set; }
        public ScratchVector2 Position { get; set; }

        public ScratchBlock(ScratchOpcode opcode, ScratchVector2 position)
        {
            Opcode = opcode;
            Position = position;
            Id = new ScratchId();
            Inputs = new ScratchBlockInput[2];
        }

        public string ToJson()
        {
            //{"vPIm|jD@a6e6.]Z,rmMf":{"opcode":"operator_add","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":479,"y":-93},"IoLU-C;j))X*t=1RFs/J":{"opcode":"operator_subtract","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":448,"y":-10},"qQ=X/EgRFbSsC4n?g]HN":{"opcode":"operator_multiply","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":459,"y":69},"lvy`K7Z1Wm~AJ?TcbV79":{"opcode":"operator_divide","next":null,"parent":null,"inputs":{"NUM1":[1,[4,""]],"NUM2":[1,[4,""]]},"fields":{},"shadow":false,"topLevel":true,"x":464,"y":140},"7-Ye~Q]vBuw+H?eM=otY":{"opcode":"data_setvariableto","next":"@7=2BhgQdrfdX`W+1QV|","parent":null,"inputs":{"VALUE":[1,[10,"0"]]},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":true,"x":344,"y":323},"@7=2BhgQdrfdX`W+1QV|":{"opcode":"data_changevariableby","next":"sF5=ECfqpZ*FB)sq1e,#","parent":"7-Ye~Q]vBuw+H?eM=otY","inputs":{"VALUE":[1,[4,"1"]]},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":false},"sF5=ECfqpZ*FB)sq1e,#":{"opcode":"data_showvariable","next":"kCCD{o{S!cvI3IC0OZB8","parent":"@7=2BhgQdrfdX`W+1QV|","inputs":{},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":false},"kCCD{o{S!cvI3IC0OZB8":{"opcode":"data_hidevariable","next":null,"parent":"sF5=ECfqpZ*FB)sq1e,#","inputs":{},"fields":{"VARIABLE":["my variable","`jEk@4|i[#Fk?(8x)AV.-my variable"]},"shadow":false,"topLevel":false}}
            string json = $"{Id.ToJson()}:{{";
            json += $"\"opcode\":\"{Opcode.ToString().ToLower()}\",";
            json += $"\"next\":{(Next == null ? "null" : Next.Id.ToJson())},";
            json += $"\"parent\":{(Parent == null ? "null" : Parent.Id.ToJson())},";
            json += "\"inputs\":{},"; // TODO: Implement inputs
            json += "\"fields\":{},";
            json += "\"shadow\":false,";
            json += "\"topLevel\":true,";
            json += Position.ToJson();
            json += "}";
            return json;
        }
    }
}
