﻿using ScratchCodeCompiler.Lexical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal static class ScratchUtility
    {
        private static ScratchVector2 lastPosition = new(-800, 0);

        public static ScratchVector2 GetNextGoodPosition()
        {
            ScratchVector2 newPosition = new(lastPosition.x + 800, lastPosition.y);
            lastPosition = newPosition;
            return newPosition;
        }
    }
}
