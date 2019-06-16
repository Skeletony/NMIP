using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace NMIP.NPCs
{
    class ice : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Wanderer");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.IceElemental);
            aiType = NPCID.IceElemental;
            animationType = NPCID.FlyingFish;
            npc.aiStyle = 44;
            npc.width = 48;
            npc.height = 36;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 350;
            npc.damage = 60;
            npc.defense = 5;
            npc.value = 1000f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("FrostFragment"), Main.rand.Next(3, 6));
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneSnow && spawnInfo.spawnTileY < Main.rockLayer ? 0.05f : 0f;
        }
    }
}
