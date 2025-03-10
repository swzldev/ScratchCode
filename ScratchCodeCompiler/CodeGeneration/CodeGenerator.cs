using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;
using System.IO.Compression;

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
            // Create default stage costume
            stage.costumes.Add(new("costume1", ImageDataFormat.SVG, new(240, 180)));
            project.Targets.Add(stage);

            // Add blocks
            stage.blocks.AddRange(Program.ToScratchBlocks(out _, out _));

            // Add variables
            stage.variables.AddRange(VariableNode.GlobalVariables.Values);

            File.WriteAllText(projectJsonPath, project.ToJson());
            if (File.Exists(Path.Combine(OutputPath, "project.sb3")))
            {
                File.Delete(Path.Combine(OutputPath, "project.sb3"));
            }
            ZipFile.CreateFromDirectory(IntermediatePath, Path.Combine(OutputPath, "project.sb3"));
        }
    }
}
