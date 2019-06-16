using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Projectiles
{
    public class IceProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 300;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 121, default(Color));
            Main.dust[dust].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 121, default(Color));
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 121, default(Color));
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 121, default(Color));
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 120);

            if (Main.rand.Next(6) == 0)
            {
                target.AddBuff(BuffID.Frozen, 60);
            }
        }
    }
}
