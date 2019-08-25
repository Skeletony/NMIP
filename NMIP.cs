using NMIP.Backgrounds;
using BaseMod;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;


namespace NMIP
{
	class NMIP : Mod
	{
        internal static NMIP instance;
        public static NMIP self = null;
        internal ILog Logging = LogManager.GetLogger("NMIP");

        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();

        public NMIP()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
            instance = this;
        }

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "ODIN", 10.5f, (Func<bool>)(() => NMIPWorld.downedODIN), "Use a [i:" + ItemType("ODINSpawner") + "] in the surface after Plantera is defeated.");
            }
        }

        public void LoadClient()
        {
            Filters.Scene["NMIP:ToxinSky"] = new Filter(new ToxinSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["NMIP:ToxinSky"] = new ToxinSky();
            ToxinSky.SkyTexture = GetTexture("Backgrounds/ToxinSky");
        }
       
        public void CleanupStaticArrays()
        {
            if (Main.netMode != 2) //handle clearing all static texture arrays
            {
                precachedTextures.Clear();

                ToxinSky.SkyTexture = null;
                ToxinSky.BGTexture = null;
            }
        }
    }
}
