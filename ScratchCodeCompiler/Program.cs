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
            ScratchProject project = new();
            ScratchTarget stage = new("Stage", true);

            ScratchCostume stageCostume = new("backdrop1", ImageDataFormat.SVG, new(240, 180));
            stage.costumes.Add(stageCostume);

            ScratchVariable myVar = new("myVar");
            stage.variables.Add(myVar);

            ScratchBlock scratchBlock = new(ScratchOpcode.Data_SetVariableTo, new(0, 0));
            scratchBlock.Inputs.Add(new("VALUE", ScratchInputFormat.Number, "123"));
            scratchBlock.Fields.Add(new("VARIABLE", myVar));
            stage.blocks.Add(scratchBlock);

            ScratchBlock moveStepsBlock = new(ScratchOpcode.Motion_MoveSteps, new(0, 0), scratchBlock);
            moveStepsBlock.Inputs.Add(new("STEPS", ScratchInputFormat.Number, "10"));
            stage.blocks.Add(moveStepsBlock);

            project.Targets.Add(stage);

            Console.WriteLine(project.ToJson());

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

            CodeGenerator generator = new(program, "D:\\ting.json");
            generator.Generate();

            Console.ReadKey();
        }
    }
}
