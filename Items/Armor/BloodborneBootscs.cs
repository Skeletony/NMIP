using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BloodborneBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Armor of the blood god.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 10000;
            item.rare = 10;
            item.defense = 50;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.meleeDamage += 0.1f;
            player.minionDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.maxRunSpeed += 0.5f;
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodCotton", 45);
            recipe.AddIngredient(mod, "MoltenBones", 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}