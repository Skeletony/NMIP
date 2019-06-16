using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Ranged
{
    public class FrostBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bowzzard");
            Tooltip.SetDefault("Fires arrows like blizzard." +
                               "\nConverts wooden arrows into blizzard arrows.");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.width = 38;
            item.height = 46;
            item.useTime = 5;
            item.useAnimation = 20;
            item.reuseDelay = 15;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = 8;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("BlizzardArrow");
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IceBar", 25);
            recipe.AddIngredient(mod, "FrostFragment", 20);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}