using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Projectiles
{
    class MagmaShower : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.alpha = 0;
            projectile.timeLeft = 300;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 4;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 6, default(Color), 3f);
            Main.dust[dust].noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(19f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
            projectile.tileCollide = false;
            // Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 6, default(Color), 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 6, default(Color), 1.2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 6, default(Color), 2f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 2f, projectile.velocity.Y * 1f, 6, default(Color), 1f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 6, default(Color), 1.5f);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}
