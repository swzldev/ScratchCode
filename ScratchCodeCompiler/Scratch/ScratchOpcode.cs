using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal enum ScratchOpcode
    {
        None,

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

        // Looks

        // Sound

        // Events
        Event_WhenFlagClicked,

        // Control
        Control_Wait,
        Control_Repeat,
        Control_Forever,
        Control_If,
        Control_If_Else,
        Control_Wait_Until,
        Control_Repeat_Until,

        // Sensing

        // Operators
        Operator_Add,
        Operator_Subtract,
        Operator_Multiply,
        Operator_Divide,
        Operator_GT,
        Operator_LT,
        Operator_Equals,

        Operator_Not,

        // Variables
        Data_SetVariableTo,
        Data_ChangeVariableBy,
        Data_ShowVariable,
        Data_HideVariable,
    }
}
