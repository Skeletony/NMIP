using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Projectiles
{
    public class IceThing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            DisplayName.SetDefault("Frozen Spikemon"); // Automatic from .lang files
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ZephyrFish);
            aiType = ProjectileID.ZephyrFish;
            projectile.width = 46;
            projectile.height = 34;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.zephyrfish = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            NMIPPlayer modPlayer = player.GetModPlayer<NMIPPlayer>(mod);
            if (player.dead)
            {
                modPlayer.friendPet = false;
            }
            if (modPlayer.friendPet)
            {
                projectile.timeLeft = 4;
            }
        }
    }
}