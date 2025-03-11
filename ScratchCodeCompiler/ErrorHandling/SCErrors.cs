using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.ErrorHandling
{
    internal static class SCErrors
    {
        /// <summary>
        /// Identifier expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS1 = new(1, "Identifier expected");
        /// <summary>
        /// '(' expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS2 = new(2, "'(' expected");
        /// <summary>
        /// ')' expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS3 = new(3, "')' expected");
        /// <summary>
        /// '{' expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS4 = new(4, "'{' expected");
        /// <summary>
        /// '}' expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS5 = new(5, "'}' expected");
        /// <summary>
        /// Expected expression
        /// </summary>
        public static readonly KeyValuePair<int, string> CS6 = new(6, "Expected expression");
        /// <summary>
        /// Left side of assignment must be a variable
        /// </summary>
        public static readonly KeyValuePair<int, string> CS7 = new(7, "Left side of assignment must be a variable");
        /// <summary>
        /// Function already declared
        /// </summary>
        public static readonly KeyValuePair<int, string> CS8 = new(8, "Function already declared");
        /// <summary>
        /// Undeclared function
        /// </summary>
        public static readonly KeyValuePair<int, string> CS9 = new(8, "Undeclared function");
    }
}
