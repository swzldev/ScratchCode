using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.CodeGeneration
{
    internal class CodeGenerator
    {
        public ProgramNode Program { get; }
        public string OutputPath { get; set; }

        public CodeGenerator(ProgramNode program, string outputPath)
        {
            Program = program;
            OutputPath = outputPath;
        }

        public void Generate()
        {
            
        }

        
    }
}
