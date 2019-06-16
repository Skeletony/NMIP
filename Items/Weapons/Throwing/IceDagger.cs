using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Throwing
{
    public class IceDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Fang");
            Tooltip.SetDefault("Dagger made out of ice. Sharp as hell!");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ThrowingKnife);
            item.damage = 44;           //The damage of your weapon
            item.thrown = true;          //Is your weapon a melee weapon?
            item.width = 20;            //Weapon's texture's width
            item.height = 36;           //Weapon's texture's height
            item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 20;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(silver: 25);           //The value of the weapon
            item.rare = 8;              //The rarity of the weapon, from -1 to 13
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.useTurn = true;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("IceDagger");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ThrowingKnife, 20);
            recipe.AddIngredient(mod, "FrostFragment", 20);
            recipe.AddIngredient(mod, "IceBar", 10);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
        }
    }
}