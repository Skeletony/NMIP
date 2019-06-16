using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Melee
{
    public class BoneSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Blade");
            Tooltip.SetDefault("Bone made sword used to ram enemies rather than stab or slash. Must've hurt.");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 25;           //The damage of your weapon
            item.melee = true;          //Is your weapon a melee weapon?
            item.width = 52;            //Weapon's texture's width
            item.height = 52;           //Weapon's texture's height
            item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 20;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(silver: 1);           //The value of the weapon
            item.rare = 3;              //The rarity of the weapon, from -1 to 13
            item.UseSound = (SoundID.DD2_SkeletonHurt);     //The sound when the weapon is using
            item.autoReuse = false;          //Whether the weapon can use automatically by pressing mousebutton
            item.useTurn = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.WoodenSword);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}