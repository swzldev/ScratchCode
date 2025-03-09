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
        motion_movesteps,
        motion_turnright,
        motion_turnleft,
        motion_goto,
        motion_goto_menu,
        motion_gotoxy,
        motion_glideto,
        motion_glideto_menu,
        motion_glidesecstoxy,
        motion_pointindirection,
        motion_pointtowards,
        motion_pointtowards_menu,
        motion_changexby,
        motion_setx,
        motion_changeyby,
        motion_sety,
        motion_ifonedgebounce,
        motion_setrotationstyle,

        // Operators
        operator_add,
        operator_subtract,
        operator_multiply,
        operator_divide,

        // Data
        data_setvariableto,
        data_changevariableby,
        data_showvariable,
        data_hidevariable,
    }
}
