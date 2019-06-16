using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class BloodborneChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Armor of the blood god.");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 22;
            item.value = 10000;
            item.rare = 10;
            item.defense = 60;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.meleeDamage += 0.1f;
            player.minionDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.maxMinions += 1;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawArms = true;
            drawHands = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodCotton", 50);
            recipe.AddIngredient(mod, "MoltenBones", 60);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}