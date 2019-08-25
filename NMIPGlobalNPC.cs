using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace NMIP
{
    public class NMIPGlobalNPC : GlobalNPC
    {
        public bool cultistFire = false;

        public override void ResetEffects(NPC npc)
        {
            cultistFire = false;
        }

        public override bool InstancePerEntity => true;

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.DesertScorpionWalk)
            {
                if (Main.rand.Next(7) == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType("ScorpionTail"));
            }

            if (npc.type == NPCID.DesertScorpionWall)
            {
                if (Main.rand.Next(7) == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType("ScorpionTail"));
            }

            if (npc.type == NPCID.KingSlime)
            {
                if (Main.expertMode)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("SKExpertDrop"));
                }
            }

            if (npc.type == NPCID.CultistBoss)
            {
                if (Main.expertMode)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("LCExpertDrop"));
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            int before = npc.lifeRegen;
            bool drain = false;
            bool noDamage = damage <= 1;
            int damageBefore = damage;

            if (cultistFire)
            {
                npc.lifeRegen -= 4;
                damage = 5;
                drain = true;
            }
        }
    }
}
