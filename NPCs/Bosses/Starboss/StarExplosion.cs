using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs.Bosses.Starboss
{
    public class StarExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Explosion");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 62;
            projectile.height = 62;
            projectile.penetrate = 1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 100;
        }

        bool playedSound = false;
        public override void AI()
        {
            if (!playedSound)
            {
                playedSound = true;
                Main.PlaySound(SoundID.Item88, (int)projectile.Center.X, (int)projectile.Center.Y);
            }
            projectile.velocity = Vector2.Zero;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame > 3)
                {
                    projectile.frame = 3;
                    if (Main.netMode != 1)
                        projectile.Kill();
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Purple;
        }
    }
}