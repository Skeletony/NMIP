using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs
{
    public class GiantIgneousWormHead : GiantIgneousWorm
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.lifeMax = 4000;
            npc.defense = 10;
            npc.damage = 200;
            npc.value = 1500f;
            npc.width = 56;
            npc.height = 80;
            npc.lavaImmune = true;
        }

        public override void Init()
        {
            base.Init();
            head = true;
        }

        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void CustomBehavior()
        {
            if (Main.netMode != 1)
            {
                if (attackCounter > 0)
                {
                    attackCounter--;
                }
                if (npc.life >= 20)
                {
                    Player target = Main.player[npc.target];
                    if (attackCounter <= 0 && Vector2.Distance(npc.Center, target.Center) < 200 && Collision.CanHit(npc.Center, 1, 1, target.Center, 1, 1))
                    {
                        Vector2 direction = (target.Center - npc.Center).SafeNormalize(Vector2.UnitX);
                        direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
                        attackCounter = 10;
                        npc.netUpdate = true;
                    }
                }
                if (npc.life <= 0)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantIgneousWormHead"), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("MoltenSoul"), Main.rand.Next(5, 9));
            }

            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("MoltenAz"));
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
                return SpawnCondition.Underworld.Chance / 10f;
            }
            return SpawnCondition.Underworld.Chance;
        }
    }

    internal class GiantIgneousWormBody : GiantIgneousWorm
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.width = 56;
            npc.height = 80;
            npc.lifeMax = 3900;
            npc.defense = 10;
            npc.damage = 200;
            npc.lavaImmune = true;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void CustomBehavior()
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantIgneousWormBody"), 1f);
            }
        }
    }

    internal class GiantIgneousWormTail : GiantIgneousWorm
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
            npc.width = 56;
            npc.height = 80;
            npc.lifeMax = 3900;
            npc.defense = 1;
            npc.damage = 200;
            npc.lavaImmune = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void CustomBehavior()
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantIgneousWormTail"), 1f);
            }
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    public abstract class GiantIgneousWorm : Worm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Igneus Worm");
        }

        public int GiantIgneusWormlife = 1;

        public override void Init()
        {
            minLength = 9;
            maxLength = 9;
            tailType = mod.NPCType<GiantIgneousWormTail>();
            bodyType = mod.NPCType<GiantIgneousWormBody>();
            headType = mod.NPCType<GiantIgneousWormHead>();
            speed = 14.5f;
            turnSpeed = 0.155f;
        }
    }
}
