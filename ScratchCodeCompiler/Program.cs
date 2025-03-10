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
            FunctionDeclerationNode funcdecl = new("sayhello", ["param1", "param2"], new());
            List<ScratchBlock> blocks = [];
            ScratchBlock block = funcdecl.ToScratchBlock(ref blocks);
            blocks.Add(block);
            Console.WriteLine(string.Join(',', blocks.Select(x => x.ToJson())));

            return;
            string inputFilePath = string.Empty;
            if (args.Length == 0)
            {
                while (!File.Exists(inputFilePath))
                {
                    Console.Write("Input file path: ");
                    inputFilePath = Console.ReadLine() ?? "";
                }
            }
            else
            {
                inputFilePath = args[0];
            }

            string[] inputFileLines = File.ReadAllLines(inputFilePath);
            string input = string.Join("\n", inputFileLines);

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

            CodeGenerator generator = new(program, "D:\\ScratchCompilerOutput");
            generator.Generate();

            Console.ReadKey();
        }
    }
}
