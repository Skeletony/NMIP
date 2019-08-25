using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items
{
    public class Axe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvish Axe");
            Tooltip.SetDefault("Axe made of Elves' metal.");
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 40;
            item.height = 34;
            item.useTime = 15;
            item.useAnimation = 15;
            item.axe = 14;          //How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Bard", 10);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}