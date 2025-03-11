using ScratchCodeCompiler.CodeGeneration;
using ScratchCodeCompiler.ErrorHandling;
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
            string inputFilePath = string.Empty;
            string? exePath = Environment.ProcessPath;
            if (exePath == null)
            {
                SCOutput.Error("Could not determine executable path.");
                return;
            }
            DirectoryInfo exeDir = new FileInfo(exePath).Directory!;
            string outputDirectory = Path.Combine(exeDir.FullName, "Output");
            if (args.Length == 0)
            {
                while (!File.Exists(inputFilePath))
                {
                    SCOutput.Write("Input file path: ");
                    inputFilePath = Console.ReadLine() ?? "";
                    if (!File.Exists(inputFilePath))
                    {
                        SCOutput.Error("File does not exist.");
                    }
                }
            }
            else
            {
                inputFilePath = args[0];
            }

            string[] input = File.ReadAllLines(inputFilePath);

            DateTime start = DateTime.UtcNow;

            SCOutput.Log("Generating project...");
            Lexer lexer = new(input);
            List<Token> tokens = lexer.Tokenize();

            Parser parser = new(tokens);
            ProgramNode program = parser.Parse();

            CodeGenerator generator = new(program, outputDirectory);
            generator.Generate();

            DateTime complete = DateTime.UtcNow;
            TimeSpan duration = complete - start;

            SCOutput.Log($"\nCompilation successful. [Time: {duration}]", ConsoleColor.Green);

            Console.ReadKey();
        }
    }
}
