using ScratchCodeCompiler.Parsing.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal static class ScratchReservedFunctions
    {
        public static readonly FunctionDeclerationNode[] All =
        [
            new("MoveSteps", ["STEPS"], ScratchOpcode.Motion_MoveSteps),
            new("TurnRight", ["DEGREES"], ScratchOpcode.Motion_TurnRight),
            new("TurnLeft", ["DEGREES"], ScratchOpcode.Motion_TurnLeft),
            new("Goto", ["TO"], ScratchOpcode.Motion_Goto),
            //new("GotoMenu", ["TO"], ScratchOpcode.Motion_GotoMenu),
            new("GotoXy", ["X", "Y"], ScratchOpcode.Motion_GotoXy),
            new("GlideTo", ["SECS", "TO"], ScratchOpcode.Motion_GlideTo),
            new("GlideToMenu", ["TO"], ScratchOpcode.Motion_GlideToMenu),
            new("GlideSecsToXy", ["SECS", "X", "Y"], ScratchOpcode.Motion_GlideSecsToXy),
            new("PointInDirection", ["DIRECTION"], ScratchOpcode.Motion_PointInDirection),
            new("PointTowards", ["TOWARDS"], ScratchOpcode.Motion_PointTowards),
            new("PointTowardsMenu", ["TOWARDS"], ScratchOpcode.Motion_PointTowardsMenu),
            new("ChangeXBy", ["DX"], ScratchOpcode.Motion_ChangeXBy),
            new("SetX", ["X"], ScratchOpcode.Motion_SetX),
            new("ChangeYBy", ["DY"], ScratchOpcode.Motion_ChangeYBy),
            new("SetY", ["Y"], ScratchOpcode.Motion_SetY),
            new("IfOnEdgeBounce", [], ScratchOpcode.Motion_IfOnEdgeBounce),
            new("SetRotationStyle", ["STYLE"], ScratchOpcode.Motion_SetRotationStyle),
            //new("XPosition", [], ScratchOpcode.Motion_XPosition),
            //new("YPosition", [], ScratchOpcode.Motion_YPosition),
            //new("Direction", [], ScratchOpcode.Motion_Direction),

            new("SwitchBackdropTo", ["BACKDROP"], ScratchOpcode.Looks_SwitchBackdropTo),
            //new("Backdrops", ["BACKDROP"], ScratchOpcode.Looks_Backdrops),
            new("SwitchBackdropToAndWait", ["BACKDROP"], ScratchOpcode.Looks_SwitchBackdropToAndWait),
            new("NextBackdrop", [], ScratchOpcode.Looks_NextBackdrop),
            new("Looks_ChangeEffectBy", ["EFFECT", "CHANGE"], ScratchOpcode.Looks_ChangeEffectBy),
            new("Looks_SetEffectTo", ["EFFECT", "VALUE"], ScratchOpcode.Looks_SetEffectTo),
            new("ClearGraphicEffects", [], ScratchOpcode.Looks_ClearGraphicEffects),
            new("BackdropNumberName", ["NUMBER_NAME"], ScratchOpcode.Looks_BackdropNumberName),

            new("PlayUntilDone", ["SOUND_MENU"], ScratchOpcode.Sound_PlayUntilDone),
            new("Play", ["SOUND_MENU"], ScratchOpcode.Sound_Play),
            new("StopAllSounds", [], ScratchOpcode.Sound_StopAllSounds),
            new("Sounds_ChangeEffectBy", ["EFFECT", "VALUE"], ScratchOpcode.Sound_ChangeEffectBy),
            new("Sounds_SetEffectTo", ["EFFECT", "VALUE"], ScratchOpcode.Sound_SetEffectTo),
            new("ClearEffects", [], ScratchOpcode.Sound_ClearEffects),
            new("ChangeVolumeBy", ["VOLUME"], ScratchOpcode.Sound_ChangeVolumeBy),
            new("SetVolumeTo", ["VOLUME"], ScratchOpcode.Sound_SetVolumeTo),
            //new("Volume", [], ScratchOpcode.Sound_Volume),

            new("Wait", ["DURATION"], ScratchOpcode.Control_Wait),
            //new("Repeat", ["TIMES"], ScratchOpcode.Control_Repeat),
            //new("Forever", [], ScratchOpcode.Control_Forever),
            //new("If", ["CONDITION"], ScratchOpcode.Control_If),
            //new("IfElse", ["CONDITION"], ScratchOpcode.Control_If_Else),
            //new("WaitUntil", ["CONDITION"], ScratchOpcode.Control_Wait_Until),
            //new("RepeatUntil", ["CONDITION"], ScratchOpcode.Control_Repeat_Until),
            new("Stop", ["STOP_OPTION"], ScratchOpcode.Control_Stop),
            new("CreateCloneOf", ["CLONE_OPTION"], ScratchOpcode.Control_CreateCloneOf),
            //new("CreateCloneOfMenu", ["CLONE_OPTION"], ScratchOpcode.Control_CreateCloneOfMenu),

            new("AskAndWait", ["QUESTION"], ScratchOpcode.Sensing_AskAndWait),
            //new("Answer", [], ScratchOpcode.Sensing_Answer),
            //new("KeyPressed", ["KEY_OPTION"], ScratchOpcode.Sensing_KeyPressed),
            //new("KeyOptions", ["KEY_OPTION"], ScratchOpcode.Sensing_KeyOptions),
            //new("MouseDown", [], ScratchOpcode.Sensing_MouseDown),
            //new("MouseX", [], ScratchOpcode.Sensing_MouseX),
            //new("MouseY", [], ScratchOpcode.Sensing_MouseY),
            //new("Loudness", [], ScratchOpcode.Sensing_Loudness),
            //new("Timer", [], ScratchOpcode.Sensing_Timer),
            new("ResetTimer", [], ScratchOpcode.Sensing_ResetTimer),
            //new("Of", ["PROPERTY", "OBJECT"], ScratchOpcode.Sensing_Of),
            //new("OfObjectMenu", ["OBJECT"], ScratchOpcode.Sensing_OfObjectMenu),
            //new("Current", ["CURRENTMENU"], ScratchOpcode.Sensing_Current),
            //new("DaysSince2000", [], ScratchOpcode.Sensing_DaysSince2000),
            //new("Username", [], ScratchOpcode.Sensing_Username),

            //new("GT", ["OPERAND1", "OPERAND2"], ScratchOpcode.Operator_GT),
            //new("LT", ["OPERAND1", "OPERAND2"], ScratchOpcode.Operator_LT),
            //new("Equals", ["OPERAND1", "OPERAND2"], ScratchOpcode.Operator_Equals),
            //new("Not", ["OPERAND"], ScratchOpcode.Operator_Not),
            //new("And", ["OPERAND1", "OPERAND2"], ScratchOpcode.Operator_And),
            //new("Or", ["OPERAND1", "OPERAND2"], ScratchOpcode.Operator_Or),
            new("Join", ["STRING1", "STRING2"], ScratchOpcode.Operator_Join),
            new("LetterOf", ["LETTER", "STRING"], ScratchOpcode.Operator_LetterOf),
            new("Length", ["STRING"], ScratchOpcode.Operator_Length),
            new("Contains", ["STRING1", "STRING2"], ScratchOpcode.Operator_Contains),
            new("Mod", ["NUM1", "NUM2"], ScratchOpcode.Operator_Mod),
            new("Round", ["NUM"], ScratchOpcode.Operator_Round),
            new("MathOp", ["OPERATOR", "NUM"], ScratchOpcode.Operator_MathOp),
            new("Random", ["FROM", "TO"], ScratchOpcode.Operator_Random),

            new("ShowVariable", ["VARIABLE"], ScratchOpcode.Data_ShowVariable),
            new("HideVariable", ["VARIABLE"], ScratchOpcode.Data_HideVariable),
        ];
    }
}
