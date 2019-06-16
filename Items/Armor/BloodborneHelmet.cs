using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BloodborneHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Armor of the blood god.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.rare = 10;
            item.defense = 55;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BloodborneChestplate") && legs.type == mod.ItemType("BloodborneBoots");
        }

        public override void UpdateEquip(Player player)
        {
            player.statDefense += 30;
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.meleeDamage += 0.1f;
            player.minionDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.maxMinions += 2;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+30 Defense" + "\n+10% damage to all classes" + "\n+2 minion slots";
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodCotton", 55);
            recipe.AddIngredient(mod, "MoltenBones", 65);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}