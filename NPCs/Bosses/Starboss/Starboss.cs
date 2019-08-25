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
            Angle = -1;
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

        public int Angle
        {
            get => (int)npc.ai[2];
            set => npc.ai[2] = value;
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
                //floats 40 (pixels?) above the player
                npc.velocity.X = Target.Center.X - npc.Center.X;
                npc.velocity.Y = npc.position.Y - Target.position.Y - 40;
                npc.velocity.Normalize();
                //drag veloc is 60% of player veloc
                npc.velocity *= Target.velocity * .6f;

                //spawn proj every 30 ticks
                if (Main.netMode != 1 && Counter % 30 == 0)
                {
                    Angle = Main.rand.Next(360);
                    npc.netUpdate = true;
                }
                else if (Angle != -1)
                {
                    //proj speed of 4
                    Projectile.NewProjectile(npc.Center, NMIPUtils.SinCos(Angle) * 4,
                        mod.ProjectileType("AstralShot"), Damage: 100, KnockBack: 1.2f);
                    Angle = -1;
                }
            }
            else if (Phase == CirclePhase)
            {
                var t = NMIPUtils.SinCos(++Angle);
                //avg dist to player is 40
                t *= 40f;
                t += Target.Center;
                npc.velocity = t - npc.position;
                npc.velocity.Normalize();
                //drag veloc is 90% of player veloc
                npc.velocity *= Target.velocity * .9f;

                //spawn proj every 30 ticks
                if (Counter % 30 == 0)
                {
                    //ten projectiles, perfect circle
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
                //teleport every 30 ticks
                if (Main.netMode != 1 && Counter % 30 == 0)
                {
                    Angle = Main.rand.Next(360);
                    Angle |= (Main.rand.Next(50) << 16);
                    npc.netUpdate = true;
                }
                else if (Angle != -1)
                {
                    //i'm sorry this is a super crappy method of doing anything - Agrair
                    var veloc = NMIPUtils.SinCos(Angle & 0b1111111111111111);
                    veloc *= Angle >> 16;
                    npc.position = Target.Center + veloc;
                }
            }

            //each phase lasts 5 seconds
            if (Counter == 5 * 60)
            {
                Phase++;
                if (Phase == 3) Phase = 0;
                Angle = -1;
            }
            Counter++;
        }
    }
}
