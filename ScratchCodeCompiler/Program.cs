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
            ScratchBlock block = new(ScratchOpcode.motion_goto, new ScratchVector2(0, 0));
            Console.WriteLine(block.ToJson());
            Console.ReadKey();
            return;

            string input = "c = 2 + 2\nc = 4";
            List<Token> tokens = Lexer.Tokenize(input);

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
