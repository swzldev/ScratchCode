using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal enum ScratchOpcode
    {
        // Motion
        Motion_MoveSteps,
        Motion_TurnRight,
        Motion_TurnLeft,
        Motion_Goto,
        Motion_GotoMenu,
        Motion_GotoXy,
        Motion_GlideTo,
        Motion_GlideToMenu,
        Motion_GlideSecsToXy,
        Motion_PointInDirection,
        Motion_PointTowards,
        Motion_PointTowardsMenu,
        Motion_ChangeXBy,
        Motion_SetX,
        Motion_ChangeYBy,
        Motion_SetY,
        Motion_IfOnEdgeBounce,
        Motion_SetRotationStyle,

        // Operators
        Operator_Add,
        Operator_Subtract,
        Operator_Multiply,
        Operator_Divide,

        // Data
        Data_SetVariableTo,
        Data_ChangeVariableBy,
        Data_ShowVariable,
        Data_HideVariable,
    }
}
