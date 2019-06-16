using Terraria;
using Terraria.ModLoader;

namespace NMIP.Buffs
{
    public class spikedIcePet : ModBuff
    {
        public override void SetDefaults()
        {
            // DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
            DisplayName.SetDefault("Spiked Ice Pet");
            Description.SetDefault("Summons a completely harmless spiked ice pet.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<NMIPPlayer>(mod).friendPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("IceThing")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("IceThing"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}