using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs
{
    public class MoltenWormHead : MoltenWorm
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.lifeMax = 2000;
            npc.defense = 6;
            npc.damage = 155;
            npc.value = 1500f;
            npc.width = 40;
            npc.height = 42;
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
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MoltenWormHead"), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("MoltenSoul"), Main.rand.Next(2, 4));
            }

            if (Main.rand.Next(100) == 0)
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
                return SpawnCondition.Underworld.Chance / 7f;
            }
            return SpawnCondition.Underworld.Chance;
        }
    }

    internal class MoltenWormBody : MoltenWorm
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.width = 40;
            npc.height = 42;
            npc.lifeMax = 1900;
            npc.defense = 6;
            npc.damage = 155;
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
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MoltenWormBody"), 1f);
            }
        }
    }

    internal class MoltenWormTail : MoltenWorm
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
            npc.width = 40;
            npc.height = 42;
            npc.lifeMax = 1900;
            npc.defense = 0;
            npc.damage = 155;
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
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MoltenWormTail"), 1f);
            }
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    public abstract class MoltenWorm : Worm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneus Worm");
        }

        public int IgneusWormlife = 1;

        public override void Init()
        {
            minLength = 10;
            maxLength = 10;
            tailType = mod.NPCType<MoltenWormTail>();
            bodyType = mod.NPCType<MoltenWormBody>();
            headType = mod.NPCType<MoltenWormHead>();
            speed = 12.5f;
            turnSpeed = 0.150f;
        }
    }
}
