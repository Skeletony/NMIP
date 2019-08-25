using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs
{
    public class IceEaterHead : IceEater
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.lifeMax = 1750;
            npc.defense = 6;
            npc.damage = 55;
            npc.value = 1500f;
            npc.width = 48;
            npc.height = 48;
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
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/IceEaterHead"), 1f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneSnow && spawnInfo.spawnTileY < Main.rockLayer ? 0.08f : 0f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("FrostFragment"), Main.rand.Next(3, 6));
            }
        }
    }

    internal class IceEaterBody : IceEater
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.width = 48;
            npc.height = 48;
            npc.lifeMax = 1500;
            npc.defense = 6;
            npc.damage = 55;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void CustomBehavior()
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/IceEaterBody"), 1f);
            }
        }
    }

    internal class IceEaterTail : IceEater
    {

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
            npc.width = 48;
            npc.height = 48;
            npc.lifeMax = 1500;
            npc.defense = 0;
            npc.damage = 60;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void CustomBehavior()
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/IceEaterTail"), 1f);
            }
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    public abstract class IceEater : Worm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Eater");
        }

        public int IceEaterlife = 1;

        public override void Init()
        {
            minLength = 7;
            maxLength = 7;
            tailType = mod.NPCType<IceEaterTail>();
            bodyType = mod.NPCType<IceEaterBody>();
            headType = mod.NPCType<IceEaterHead>();
            speed = 10.5f;
            turnSpeed = 0.145f;
        }
    }
}
