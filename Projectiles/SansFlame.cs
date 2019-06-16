using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Projectiles
{
    public class SansFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.Homing[projectile.type] = true;
            Main.projFrames[projectile.type] = 2;
            DisplayName.SetDefault("Ghost Flame");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 18;               //The width of projectile hitbox
            projectile.height = 28;              //The height of projectile hitbox
            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.magic = true;           //Is the projectile shoot by a magic weapon?
            projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in)
            projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = false;          //Can the projectile collide with tiles?
            projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void AI()
        {
            BaseMod.BaseAI.Look(projectile, 0);
            projectile.velocity *= 1.025f;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && !Main.npc[k].immortal)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (10 * projectile.velocity + move) / 11f;
                AdjustMagnitude(ref projectile.velocity);
            }

            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 29, default(Color), 1f);
            dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 29, default(Color), 1.5f);
            dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 29, default(Color), 0.5f);
            Main.dust[dust].noGravity = true;
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 29, default(Color), 1f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 29, default(Color), 1.5f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X * 1f, projectile.velocity.Y * 1f, 29, default(Color), 0.5f);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.ShadowFlame, 300);
            }
        }
    }
}