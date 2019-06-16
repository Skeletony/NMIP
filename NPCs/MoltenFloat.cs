using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace NMIP.NPCs
{
    class MoltenFloat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmatic Wanderer");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            aiType = NPCID.IceElemental;
            animationType = NPCID.FlyingFish;
            npc.aiStyle = 44;
            npc.width = 34;
            npc.height = 30;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.lifeMax = 460;
            npc.damage = 100;
            npc.defense = 5;
            npc.value = 1000f;
            npc.knockBackResist = 0.40f;
            npc.lavaImmune = true;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("MoltenSoul"), Main.rand.Next(1, 3));
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!NMIPWorld.downedODIN)
            {
                return 0f;
            }
            if (SpawnCondition.Underworld.Chance > 0f)
            {
                return SpawnCondition.Underworld.Chance / 5f;
            }
            return SpawnCondition.Underworld.Chance;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 6, default(Color));
            Main.dust[dust].noGravity = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < 1)
            {
                for (int a = 0; a < 24; a++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, new Color(0, 0, 0), 1.5f);
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 1.5f);
                    Main.dust[dust].noGravity = true;
                }
            }
            else
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, new Color(0, 0, 0), 1.5f);
            }
        }
    }
}
