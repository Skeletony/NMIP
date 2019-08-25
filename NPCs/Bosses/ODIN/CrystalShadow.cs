using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.NPCs.Bosses.ODIN
{
    public class CrystalShadow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 1;
			projectile.hostile = true;
			projectile.penetrate = -1;
            projectile.alpha = 0;
        }

        public override bool PreAI()
		{
			int newDust = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, 111, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
			Main.dust[newDust].velocity *= -0.25f;
			Main.dust[newDust].noGravity = true;
			newDust = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, 59, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
			Main.dust[newDust].velocity *= -0.25f;
			Main.dust[newDust].position -= projectile.velocity * 0.5f;
			Main.dust[newDust].noGravity = true;

			return false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(mod.BuffType("Shadowflame"), 150);
		}
	}
}
