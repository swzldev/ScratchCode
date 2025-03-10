using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.CodeGeneration
{
    internal class CodeGenerator
    {
        public ProgramNode Program { get; }
        public string OutputPath { get; set; }

        private string IntermediatePath => Path.Combine(OutputPath, "imdt");

        public CodeGenerator(ProgramNode program, string outputPath)
        {
            Program = program;
            OutputPath = outputPath;
        }

        public void Generate()
        {
            // Set up output files
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
            if (!Directory.Exists(IntermediatePath))
            {
                Directory.CreateDirectory(IntermediatePath);
            }
            string projectJsonPath = Path.Combine(IntermediatePath, "project.json");

            ScratchProject project = new();
            // All projects contain a stage
            ScratchTarget stage = new("Stage", true);
            project.Targets.Add(stage);

            stage.blocks.AddRange(Program.ToScratchBlocks(out _, out _));

            File.WriteAllText(projectJsonPath, project.ToJson());
        }
    }
}
