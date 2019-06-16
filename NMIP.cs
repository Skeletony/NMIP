using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics;
using Terraria.Localization;
using Terraria.DataStructures;

namespace NMIP
{
	class NMIP : Mod
	{
        public NMIP()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "ODIN", 10.5f, (Func<bool>)(() => NMIPWorld.downedODIN), "Use a [i:" + ItemType("ODINSpawner") + "] in the surface after Plantera is defeated.");
            }
        }
    }
}
