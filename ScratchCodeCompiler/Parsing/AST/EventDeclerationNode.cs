using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class EventDeclerationNode : StatementNode, IScratchBlockTranslatable
    {
        public ScratchEvent ScratchEvent { get; set; }
        public CodeBlockNode Body { get; set; }

        public EventDeclerationNode(ScratchEvent scratchEvent, CodeBlockNode body)
        {
            ScratchEvent = scratchEvent;
            Body = body;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock eventBlock = new(ScratchEvent.Opcode, ScratchUtility.GetNextGoodPosition());
            blocks.Add(eventBlock);
            if (Body.IsEmpty)
            {
                return eventBlock;
            }
            ScratchBlock[] bodyBlocks = Body.ToScratchBlocks();
            eventBlock.Stitch(bodyBlocks.First());
            blocks.AddRange(bodyBlocks);
            return eventBlock;
        }
    }
}
