using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs
{
    public class MoltenSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 44;
            npc.damage = 70;
            npc.defense = 1;
            npc.lifeMax = 245;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 360f;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 1;
            aiType = NPCID.BlueSlime;
            animationType = NPCID.BlueSlime;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.40f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("MoltenSoul"), Main.rand.Next(1, 2));
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
                return SpawnCondition.Underworld.Chance / 3f;
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
