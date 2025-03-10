using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class CodeBlockNode : ASTNode
    {
        public List<ASTNode> Children { get; set; }

        public bool IsEmpty => Children.Count == 0;

        public CodeBlockNode()
        {
            Children = [];
        }

        public ScratchBlock[] ToScratchBlocks()
        {
            List<ScratchBlock> blocks = [];
            ScratchBlock? lastBlock = null;
            foreach (ASTNode child in Children)
            {
                if (child is IScratchBlockTranslatable translatable)
                {
                    ScratchBlock scratchBlock = translatable.ToScratchBlock(ref blocks);
                    lastBlock?.Stitch(scratchBlock);
                    lastBlock = scratchBlock;
                }
                else
                {
                    throw new NotImplementedException("Found non translatable node");
                }
            }
            return [.. blocks];
        }

        public override string ToString()
        {
            string str = "{\n";
            foreach (ASTNode child in Children)
            {
                str += child.ToString() + "\n";
            }
            str += "}";
            return str;
        }
    }
}
