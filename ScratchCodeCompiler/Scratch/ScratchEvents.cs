using ScratchCodeCompiler.Parsing.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal static class ScratchEvents
    {
        // TODO: Implement broadcasts
        //new("WhenBroadcastReceived", ["BROADCAST_OPTION"], ScratchOpcode.Event_WhenBroadcastReceived),
        //new("Broadcast", ["BROADCAST_INPUT"], ScratchOpcode.Event_Broadcast),
        //new("BroadcastAndWait", ["BROADCAST_INPUT"], ScratchOpcode.Event_BroadcastAndWait),
        public static ScratchEvent[] All =
        [
            new("OnFlagClicked", [], ScratchOpcode.Event_WhenFlagClicked),
            new("OnKeyPressed", ["KEY_OPTION"], ScratchOpcode.Event_WhenKeyPressed),
            new("WhenStageClicked", [], ScratchOpcode.Event_WhenStageClicked),
            new("WhenBackdropSwitchesTo", ["BACKDROP"], ScratchOpcode.Event_WhenBackdropSwitchesTo),
            new("WhenGreaterThan", ["WHENGREATERTHANMENU", "VALUE"], ScratchOpcode.Event_WhenGreaterThan)
        ];
    }
}
