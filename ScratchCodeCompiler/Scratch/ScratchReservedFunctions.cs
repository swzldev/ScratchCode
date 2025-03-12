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
            new("MoveSteps", [new("STEPS")], ScratchOpcode.Motion_MoveSteps),
            new("TurnRight", [new("DEGREES")], ScratchOpcode.Motion_TurnRight),
            new("TurnLeft", [new("DEGREES")], ScratchOpcode.Motion_TurnLeft),
            new("Goto", [new("TO")], ScratchOpcode.Motion_Goto),
            //new("GotoMenu", ["TO"], ScratchOpcode.Motion_GotoMenu),
            new("GotoXy", [new("X"), new("Y")], ScratchOpcode.Motion_GotoXy),
            new("GlideTo", [new("SECS"), new("TO")], ScratchOpcode.Motion_GlideTo),
            new("GlideToMenu", [new("TO")], ScratchOpcode.Motion_GlideToMenu),
            new("GlideSecsToXy", [new("SECS"), new("X"), new("Y")], ScratchOpcode.Motion_GlideSecsToXy),
            new("PointInDirection", [new("DIRECTION")], ScratchOpcode.Motion_PointInDirection),
            new("PointTowards", [new("TOWARDS")], ScratchOpcode.Motion_PointTowards),
            new("PointTowardsMenu", [new("TOWARDS")], ScratchOpcode.Motion_PointTowardsMenu),
            new("ChangeXBy", [new("DX")], ScratchOpcode.Motion_ChangeXBy),
            new("SetX", [new("X")], ScratchOpcode.Motion_SetX),
            new("ChangeYBy", [new("DY")], ScratchOpcode.Motion_ChangeYBy),
            new("SetY", [new("Y")], ScratchOpcode.Motion_SetY),
            new("IfOnEdgeBounce", [], ScratchOpcode.Motion_IfOnEdgeBounce),
            new("SetRotationStyle", [new("STYLE")], ScratchOpcode.Motion_SetRotationStyle),
            //new("XPosition", [], ScratchOpcode.Motion_XPosition),
            //new("YPosition", [], ScratchOpcode.Motion_YPosition),
            //new("Direction", [], ScratchOpcode.Motion_Direction),

            new("SwitchBackdropTo", [new("BACKDROP")], ScratchOpcode.Looks_SwitchBackdropTo),
            //new("Backdrops", ["BACKDROP"], ScratchOpcode.Looks_Backdrops),
            new("SwitchBackdropToAndWait", [new("BACKDROP")], ScratchOpcode.Looks_SwitchBackdropToAndWait),
            new("NextBackdrop", [], ScratchOpcode.Looks_NextBackdrop),
            new("Looks_ChangeEffectBy", [new("EFFECT"), new("CHANGE")], ScratchOpcode.Looks_ChangeEffectBy),
            new("Looks_SetEffectTo", [new("EFFECT"), new("VALUE")], ScratchOpcode.Looks_SetEffectTo),
            new("ClearGraphicEffects", [], ScratchOpcode.Looks_ClearGraphicEffects),
            new("BackdropNumberName", [new("NUMBER_NAME")], ScratchOpcode.Looks_BackdropNumberName),

            new("PlayUntilDone", [new("SOUND_MENU")], ScratchOpcode.Sound_PlayUntilDone),
            new("Play", [new("SOUND_MENU")], ScratchOpcode.Sound_Play),
            new("StopAllSounds", [], ScratchOpcode.Sound_StopAllSounds),
            new("Sounds_ChangeEffectBy", [new("EFFECT"), new("VALUE")], ScratchOpcode.Sound_ChangeEffectBy),
            new("Sounds_SetEffectTo", [new("EFFECT"), new("VALUE")], ScratchOpcode.Sound_SetEffectTo),
            new("ClearEffects", [], ScratchOpcode.Sound_ClearEffects),
            new("ChangeVolumeBy", [new("VOLUME")], ScratchOpcode.Sound_ChangeVolumeBy),
            new("SetVolumeTo", [new("VOLUME")], ScratchOpcode.Sound_SetVolumeTo),
            //new("Volume", [], ScratchOpcode.Sound_Volume),

            new("Wait", [new("DURATION")], ScratchOpcode.Control_Wait),
            //new("Repeat", ["TIMES"], ScratchOpcode.Control_Repeat),
            //new("Forever", [], ScratchOpcode.Control_Forever),
            //new("If", ["CONDITION"], ScratchOpcode.Control_If),
            //new("IfElse", ["CONDITION"], ScratchOpcode.Control_If_Else),
            //new("WaitUntil", ["CONDITION"], ScratchOpcode.Control_Wait_Until),
            //new("RepeatUntil", ["CONDITION"], ScratchOpcode.Control_Repeat_Until),
            new("Stop", [new("STOP_OPTION")], ScratchOpcode.Control_Stop),
            new("CreateCloneOf", [new("CLONE_OPTION")], ScratchOpcode.Control_CreateCloneOf),
            //new("CreateCloneOfMenu", ["CLONE_OPTION"], ScratchOpcode.Control_CreateCloneOfMenu),

            new("AskAndWait", [new("QUESTION")], ScratchOpcode.Sensing_AskAndWait),
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
            new("Join", [new("STRING1"), new("STRING2")], ScratchOpcode.Operator_Join),
            new("LetterOf", [new("LETTER"), new("STRING")], ScratchOpcode.Operator_LetterOf),
            new("Length", [new("STRING")], ScratchOpcode.Operator_Length),
            new("Contains", [new("STRING1"), new("STRING2")], ScratchOpcode.Operator_Contains),
            new("Mod", [new("NUM1"), new("NUM2")], ScratchOpcode.Operator_Mod),
            new("Round", [new("NUM")], ScratchOpcode.Operator_Round),
            new("MathOp", [new("OPERATOR"), new("NUM")], ScratchOpcode.Operator_MathOp),
            new("Random", [new("FROM"), new("TO")], ScratchOpcode.Operator_Random),

            new("ShowVariable", [new("VARIABLE")], ScratchOpcode.Data_ShowVariable),
            new("HideVariable", [new("VARIABLE")], ScratchOpcode.Data_HideVariable),
        ];
    }
}
