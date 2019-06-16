using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items
{
    public class IceThing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
            DisplayName.SetDefault("Spike Summoner");
            Tooltip.SetDefault("Summons a spiked ice thing that is suprisingly passive.");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = mod.ProjectileType("IceThing");
            item.buffType = mod.BuffType("spikedIcePet");
            item.width = 30;
            item.height = 30;
            item.rare = 6;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}