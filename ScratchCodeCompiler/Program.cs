using ScratchCodeCompiler.CodeGeneration;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Parsing;
using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ScratchProject project = new();

            //ScratchTarget stage = new("Stage", true);
            //ScratchCostume costume = new("backdrop1", ImageDataFormat.SVG, new ScratchVector2(240, 180));
            //stage.costumes.Add(costume);

            //stage.blocks.Add(new(ScratchOpcode.Motion_MoveSteps, new ScratchVector2(0, 0)));

            //project.Targets.Add(stage);
            
            //Console.WriteLine(project.ToJson());
            //Console.ReadKey();
            //return;

            string input = "c = 2 + 2\nc = 4";
            Lexer lexer = new(input);
            List<Token> tokens = lexer.Tokenize();

            foreach (Token token in tokens)
            {
                Console.WriteLine(token.ToString());
            }
            Console.WriteLine();

            Parser parser = new(tokens);
            ProgramNode program = parser.Parse();

            Console.WriteLine(program.ToString());

            CodeGenerator generator = new(program, "D:\\ting.json");
            generator.Generate();

            Console.ReadKey();
        }
    }
}
