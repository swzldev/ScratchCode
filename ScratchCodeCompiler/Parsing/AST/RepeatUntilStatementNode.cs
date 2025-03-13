﻿using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class RepeatUntilStatementNode : StatementNode, IScratchBlockTranslatable
    {
        public ExpressionNode Condition { get; }
        public CodeBlockNode Body { get; set; }

        public RepeatUntilStatementNode(ExpressionNode condition, CodeBlockNode body)
        {
            Condition = condition;
            Body = body;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            ScratchBlock repeatBlock = new(ScratchOpcode.Control_Repeat_Until);
            blocks.Add(repeatBlock);

            // Add the condition
            repeatBlock.AddInputExpression("CONDITION", Condition, ref blocks);

            // Add the body
            if (Body.IsEmpty)
            {
                repeatBlock.Inputs.Add(new("SUBSTACK"));
                return repeatBlock;
            }
            ScratchBlock[] bodyBlocks = Body.ToScratchBlocks();
            repeatBlock.Inputs.Add(new("SUBSTACK", bodyBlocks.First()));
            bodyBlocks.First().Parent = repeatBlock;
            blocks.AddRange(bodyBlocks);
            return repeatBlock;
        }
    }
}
