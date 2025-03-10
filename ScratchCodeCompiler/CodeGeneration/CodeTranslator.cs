using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.CodeGeneration
{
    internal static class CodeTranslator
    {
        public static ScratchBlock[] TranslateAST(ProgramNode ast, out CodeTranslatorMetadata metadata)
        {
            metadata = new();
            List<ScratchBlock> blocks = [];



            return blocks.ToArray();
        }
    }
}
