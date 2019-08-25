using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs.Bosses.Starboss
{
    [AutoloadBossHead]
    public class Starboss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Boss");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 118;
            npc.height = 88;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.damage = 125;
            npc.defense = 10;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.netAlways = true;
            npc.friendly = false;
            npc.lifeMax = 16000;
            npc.value = Item.sellPrice(0, 15, 0, 0);
            npc.behindTiles = true;
            npc.knockBackResist = 0f;
            bossBag = mod.ItemType("StarbossBag");
            npc.npcSlots = 200;

            Phase = 0;
            Counter = 0;
        }

        public int Phase
        {
            get => (byte)npc.ai[0];
            set => npc.ai[0] = value;
        }

        public int Counter
        {
            get => (int)npc.ai[1];
            set => npc.ai[1] = value;
        }

        private Player Target => Main.player[npc.target];

        private const byte FloatPhase = 0;
        private const byte CirclePhase = 1;
        private const byte TelePhase = 2;

        public override void AI()
        {
            npc.TargetClosest();
            if (Phase == FloatPhase)
            {
                //floats 240 pixels above the player
                npc.velocity = Target.Center - new Vector2(0, 240) - npc.Center;
                npc.velocity.Normalize();
                //drag veloc is 60% of player veloc
                npc.velocity *= (Target.velocity.Length() + 3) * .6f;

                //spawn proj every 30 ticks
                if (Counter % 30 == 0 && Main.netMode != 1)
                {
                    var a = Main.rand.Next(360);
                    var p = Projectile.NewProjectileDirect(npc.Center, NMIPUtils.SinCos(a) * 4,
                        mod.ProjectileType("AstralShot"), Damage: 100, KnockBack: 1.2f);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, p.whoAmI);
                    p = Projectile.NewProjectileDirect(npc.Center, NMIPUtils.SinCos(a + 180) * 4,
                        mod.ProjectileType("AstralShot"), Damage: 100, KnockBack: 1.2f);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, p.whoAmI);
                }
            }
            else if (Phase == CirclePhase)
            {
                //adjust .8f for speed
                var d = NMIPUtils.SinCos((int)(Counter * .8f) + 200) * 240 + Target.Center;
                npc.velocity = d - npc.Center;

                //spawn proj every second
                if (Counter % 60 == 0)
                {
                    for (int i = 0; i < 360; i += 36)
                    {
                        //proj speed of 4
                        Projectile.NewProjectile(npc.Center, NMIPUtils.SinCos(i) * 4,
                            mod.ProjectileType("AstralShot"), Damage: 100, KnockBack: 1.2f);
                    }
                }
            }
            else if (Phase == TelePhase)
            {
                //teleport every 2 seconds
                if (Counter % 120 == 0 && Main.netMode != 1)
                {
                    //auto syncs
                    npc.Teleport(Target.position + NMIPUtils.SinCos(Main.rand.Next(360)) * Main.rand.Next(300, 400));
                }
                //spawn proj every 30 ticks
                else if (Counter % 30 == 0)
                {
                    var v = Target.position - npc.position;
                    v.Normalize();
                    //speed of 4
                    Projectile.NewProjectile(npc.Center, v * 4,
                        mod.ProjectileType<AstralShot>(), Damage: 100, KnockBack: 1.2f);
                }
            }

            //each phase lasts 10 seconds
            if (Counter++ == 10 * 60)
            {
                Counter = 0;
                switch (Phase)
                {
                    case 0:
                        var v = Target.position - npc.position;
                        Main.NewText(Counter);
                        npc.velocity = Vector2.Zero;
                        Phase = CirclePhase;
                        return;
                    case 1:
                        npc.velocity = Vector2.Zero;
                        Phase = TelePhase;
                        return;
                    case 2:
                        Phase = FloatPhase;
                        return;
                }
            }
        }
    }
}
