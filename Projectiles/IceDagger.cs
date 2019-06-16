using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    class IceDagger : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Fang");
        }

        public override void SetDefaults()
        {
            // while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
            projectile.CloneDefaults(ProjectileID.FrostDaggerfish);
            projectile.width = 20;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = -1;

            // 5 second fuse.
            projectile.timeLeft = 300;

            // These 2 help the projectile hitbox be centered on the projectile sprite.
            drawOffsetX = 5;
            drawOriginOffsetY = 5;
        }
       
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }

            projectile.tileCollide = false;
            // Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 2f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[dustIndex].velocity *= 4f;
            }
        }

        public override void Kill(int timeLeft)
        {
            // Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 2f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[dustIndex].velocity *= 4f;
            }
        }
    }
}