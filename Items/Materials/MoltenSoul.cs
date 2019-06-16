using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace NMIP.Items.Materials
{
    public class MoltenSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Soul");
            Tooltip.SetDefault("Souls of creatures of hell.");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 22;
            item.height = 26;
            item.rare = 6;
        }
    }
}