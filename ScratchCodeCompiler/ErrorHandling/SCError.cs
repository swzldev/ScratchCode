using ScratchCodeCompiler.Lexical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.ErrorHandling
{
    internal class SCError
    {
        public static void HandleError(KeyValuePair<int, string> error, Token errorToken)
        {
            SCOutput.Error($"\nError CS{error.Key.ToString().PadLeft(4, '0')}: {error.Value}.");
            if (errorToken.Line > 0) SCOutput.Error(SCErrorHelper.InputLines[errorToken.Line - 2]);
            SCOutput.Error(SCErrorHelper.InputLines[errorToken.Line - 1]);
            SCOutput.Error("^ HERE".PadLeft(errorToken.Column - 1 + 6));
            if (errorToken.Line < SCErrorHelper.InputLines.Length) SCOutput.Error(SCErrorHelper.InputLines[errorToken.Line]);
            SCOutput.Error($"\nCompilation failed with exit code {error.Key}.");
            SCOutput.Log("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(error.Key);
        }

        public static void HandleWarning(KeyValuePair<int, string> warning, Token warningToken)
        {
            SCOutput.Warn($"\nWarning CSW{warning.Key.ToString().PadLeft(4, '0')}: {warning.Value}.");
            if (warningToken.Line > 0) SCOutput.Warn(SCErrorHelper.InputLines[warningToken.Line - 2]);
            SCOutput.Warn(SCErrorHelper.InputLines[warningToken.Line - 1]);
            SCOutput.Warn("^ HERE".PadLeft(warningToken.Column - 1 + 6));
            if (warningToken.Line < SCErrorHelper.InputLines.Length) SCOutput.Warn(SCErrorHelper.InputLines[warningToken.Line]);
        }
    }
}
