using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace NMIP.NPCs
{
    class DroneEye : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drone Eye");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DemonEye);
            animationType = NPCID.DemonEye;
            npc.aiStyle = 2;
            npc.width = 48;
            npc.height = 36;
            npc.HitSound = SoundID.NPCHit4;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.lifeMax = 750;
            npc.damage = 55;
            npc.defense = 0;
            npc.value = 1500f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("AncientMachinery"), Main.rand.Next(3, 6));
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!NPC.downedPlantBoss)
            {
                return 0f;
            }
            if (SpawnCondition.OverworldNight.Chance > 0f)
            {
                return SpawnCondition.OverworldNight.Chance / 10f;
            }
            return SpawnCondition.OverworldNight.Chance;
        }
    }
}
