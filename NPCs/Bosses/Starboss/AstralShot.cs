using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.NPCs.Bosses.Starboss
{
    class AstralShot : ModProjectile
    {
        public override void SetDefaults()
		{
            projectile.width = 18;
            projectile.height = 42;
			projectile.hostile = true;
			projectile.penetrate = -1;
            projectile.alpha = 0;
        }

        public override bool PreAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(50f);
            int newDust = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, mod.DustType<Dusts.StarDust>(), projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
            Main.dust[newDust].velocity *= -0.25f;
            Main.dust[newDust].noGravity = true;
            newDust = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, mod.DustType<Dusts.StarDust>(), projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
            Main.dust[newDust].velocity *= -0.25f;
            Main.dust[newDust].position -= projectile.velocity * 0.5f;
            Main.dust[newDust].noGravity = true;

            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 30; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, 1, mod.DustType<Dusts.StarDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.StarDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            if (Main.netMode != 1)
            {
                int projID = Projectile.NewProjectile(projectile.Top.X, projectile.Top.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("StarExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[projID].Bottom = projectile.Bottom + new Vector2(0, 10);
                Main.projectile[projID].netUpdate = true;
            }
        }
    }
}
