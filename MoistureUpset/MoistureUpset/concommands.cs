using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoistureUpset
{
    public static class concommands
    {
        [ConCommand(commandName = "debugtime", flags = ConVarFlags.None, helpText = "Does the magic")]
        public static void DebugCommand(ConCommandArgs args)
        {
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "no_enemies");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "kill_all");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_money 1000000");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item SoldiersSyringe 100");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item AlienHead 100");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item ShapedGlass 10");
        }
    }
}
