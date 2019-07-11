using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace NMIP.Buffs
{
    public class CultistFlame : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Lunatic Flames");
            Description.SetDefault("You are burning like a lunatic.");
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 15;
            npc.defense -= 3;
            npc.GetGlobalNPC<NMIPGlobalNPC>(mod).cultistFire = true;

            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, 20);
                Main.dust[dust].scale = 3f;
                Main.dust[dust].noGravity = true;
                Dust.NewDust(npc.position, npc.width, npc.height, 21);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
