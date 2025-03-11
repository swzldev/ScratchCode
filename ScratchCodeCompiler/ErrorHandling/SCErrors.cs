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
        public static readonly KeyValuePair<int, string> CS9 = new(9, "Undeclared function");
        /// <summary>
        /// Invalid number of arguments
        /// </summary>
        public static readonly KeyValuePair<int, string> CS10 = new(10, "Invalid number of arguments");
        /// <summary>
        /// Unknown event
        /// </summary>
        public static readonly KeyValuePair<int, string> CS11 = new(11, "Unknown event");
        /// <summary>
        /// Boolean expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS12 = new(12, "Boolean expected");
        /// <summary>
        /// Event expected
        /// </summary>
        public static readonly KeyValuePair<int, string> CS13 = new(13, "Event expected");
        /// <summary>
        /// Unexpected expression
        /// </summary>
        public static readonly KeyValuePair<int, string> CS14 = new(14, "Unexpected expression");
        /// <summary>
        /// Functions cannot be declared within other functions
        /// </summary>
        public static readonly KeyValuePair<int, string> CS15 = new(15, "Functions cannot be declared within other functions");
        /// <summary>
        /// Events cannot be declared within other events
        /// </summary>
        public static readonly KeyValuePair<int, string> CS16 = new(16, "Events cannot be declared within other events");
        /// <summary>
        /// Statements cannot be used outside a function or event
        /// </summary>
        public static readonly KeyValuePair<int, string> CS17 = new(17, "Statements cannot be used outside a function or event");
    }
}
