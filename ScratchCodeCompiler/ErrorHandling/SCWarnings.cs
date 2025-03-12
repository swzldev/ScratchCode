using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.ErrorHandling
{
    internal static class SCWarnings
    {
        /// <summary>
        /// Code after forever loop will never execute
        /// </summary>
        public static readonly KeyValuePair<int, string> CSW1 = new(1, "Code after forever loop will never execute");
        /// <summary>
        /// If statement will never execute
        /// </summary>
        public static readonly KeyValuePair<int, string> CSW2 = new(1, "If statement will never execute");
    }
}
