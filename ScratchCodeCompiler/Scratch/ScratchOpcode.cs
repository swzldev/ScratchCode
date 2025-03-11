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
        Motion_XPosition,
        Motion_YPosition,
        Motion_Direction,

        // Looks
        Looks_SwitchBackdropTo,
        Looks_Backdrops,
        Looks_SwitchBackdropToAndWait,
        Looks_NextBackdrop,
        Looks_ChangeEffectBy,
        Looks_SetEffectTo,
        Looks_ClearGraphicEffects,
        Looks_BackdropNumberName,

        // Sound
        Sound_PlayUntilDone,
        Sound_SoundsMenu,
        Sound_Play,
        Sound_StopAllSounds,
        Sound_ChangeEffectBy,
        Sound_SetEffectTo,
        Sound_ClearEffects,
        Sound_ChangeVolumeBy,
        Sound_SetVolumeTo,
        Sound_Volume,

        // Events
        Event_WhenFlagClicked,
        Event_WhenKeyPressed,
        Event_WhenStageClicked,
        Event_WhenBackdropSwitchesTo,
        Event_WhenGreaterThan,
        Event_WhenBroadcastReceived,
        Event_Broadcast,
        Event_BroadcastAndWait,

        // Control
        Control_Wait,
        Control_Repeat,
        Control_Forever,
        Control_If,
        Control_If_Else,
        Control_Wait_Until,
        Control_Repeat_Until,
        Control_Stop,
        Control_CreateCloneOf,
        Control_CreateCloneOfMenu,

        // Sensing
        Sensing_AskAndWait,
        Sensing_ResetTimer,
        Sensing_KeyPressed,
        Sensing_KeyOptions,
        Sensing_MouseDown,
        Sensing_MouseX,
        Sensing_MouseY,
        Sensing_Loudness,
        Sensing_Timer,
        Sensing_Answer,
        Sensing_Of,
        Sensing_OfObjectMenu,
        Sensing_Current,
        Sensing_DaysSince2000,
        Sensing_Username,

        // Operators
        Operator_Add,
        Operator_Subtract,
        Operator_Multiply,
        Operator_Divide,
        Operator_GT,
        Operator_LT,
        Operator_Equals,
        Operator_Not,
        Operator_And,
        Operator_Or,
        Operator_Join,
        Operator_LetterOf,
        Operator_Length,
        Operator_Contains,
        Operator_Mod,
        Operator_Round,
        Operator_MathOp,
        Operator_Random,

        // Variables
        Data_SetVariableTo,
        Data_ChangeVariableBy,
        Data_ShowVariable,
        Data_HideVariable,

        // Procedures/My Blocks
        Procedures_Definition,
        Procedures_Prototype,
        Procedures_Call,

        // Other/Special
        Argument_Reporter_String_Number,
        Argument_Reporter_Boolean,
    }
}
