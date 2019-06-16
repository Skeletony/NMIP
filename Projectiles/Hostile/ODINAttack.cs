using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace NMIP.Projectiles.Hostile
{
    public class ODINAttack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Otherworldy Missiles");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 16;               //The width of projectile hitbox
            projectile.height = 16;              //The height of projectile hitbox
            projectile.hostile = true;         //Can the projectile deal damage to the player?
            projectile.friendly = false;
            projectile.alpha = 0;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            projectile.damage = 64;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode || Main.rand.Next(2) == 0)
            {
                player.AddBuff(BuffID.ShadowFlame, 600, true);
            }
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(190f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(mod.GetTexture("Projectiles/Hostile/ODINAttack_Glow"), new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y),
                        new Rectangle(0, projectile.frame * projectile.height, projectile.width, projectile.height), drawColor, projectile.rotation,
                        new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Projectiles/Hostile/ODINAttack_Glow"), new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y),
                        new Rectangle(0, projectile.frame * projectile.height, projectile.width, projectile.height), Color.White, projectile.rotation,
                        new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
