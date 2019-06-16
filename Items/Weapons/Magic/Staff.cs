using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Magic
{
    class Staff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvish Staff");
            Tooltip.SetDefault("Made of the ancient metal and magic of the elves.");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 20;           //The damage of your weapon
            item.magic = true;          //Is your weapon a melee weapon?
            item.noMelee = true;
            item.width = 44;            //Weapon's texture's width
            item.height = 48;           //Weapon's texture's height
            item.useTime = 10;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 10;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(copper: 10);           //The value of the weapon
            item.rare = 4;              //The rarity of the weapon, from -1 to 13
            item.autoReuse = false;          //Whether the weapon can use automatically by pressing mousebutton
            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("MagicBolt");
            item.mana = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Bard", 10);
            recipe.AddIngredient(mod, "Shard", 5);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
