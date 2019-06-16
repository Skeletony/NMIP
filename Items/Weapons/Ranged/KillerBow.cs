using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Ranged
{
    public class KillerBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Annihilator");
            Tooltip.SetDefault("Annihilate!" +
                               "\nConverts wooden arrows into killer arrows.");
        }

        public override void SetDefaults()
        {
            item.damage = 85;
            item.ranged = true;
            item.width = 34;
            item.height = 52;
            item.useTime = 5;
            item.useAnimation = 20;
            item.reuseDelay = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = 10000;
            item.rare = 10;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("KillerArrow");
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenBones", 30);
            recipe.AddIngredient(mod, "BloodCotton", 25);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}