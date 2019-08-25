using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    class CharmofAres : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charm of Ares");
            Tooltip.SetDefault("An ancient artifact of Ares. It makes you strong!"
                + "\nGrants Rage, Wrath, Endurance and Lifeforce buff"
                + "\n+20% melee damage");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 32;
            item.value = 30000;
            item.rare = 10;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(113, 18000);
            player.AddBuff(115, 18000);
            player.AddBuff(114, 18000);
            player.AddBuff(117, 18000);
            player.meleeDamage += 0.2f;
            player.GetModPlayer<NMIPPlayer>(mod).aresEffect = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenBones", 20);
            recipe.AddIngredient(mod, "BloodCotton", 15);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
