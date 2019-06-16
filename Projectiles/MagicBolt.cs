using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Projectiles
{
    class MagicBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvish Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 10;
            projectile.alpha = 0;
            projectile.timeLeft = 300;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 234, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 234, default(Color));
            Main.dust[dust].noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(19f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.tileCollide = false;
            // Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 234, 0f, 0f, 234, default(Color), 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 234, 0f, 0f, 234, default(Color), 1.2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 234, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 234, default(Color), 1f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 234, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 234, default(Color), 0.5f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 234, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 234, default(Color), 1.5f);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}
