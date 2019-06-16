using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Magic
{
    class BloodspikeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BloodSpike Staff");
            Tooltip.SetDefault("Shoots out multiple blood spikes at once!");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 65;           //The damage of your weapon
            item.magic = true;          //Is your weapon a melee weapon?
            item.noMelee = true;
            item.width = 60;            //Weapon's texture's width
            item.height = 48;           //Weapon's texture's height
            item.useTime = 5;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(gold: 10);           //The value of the weapon
            item.rare = 10;              //The rarity of the weapon, from -1 to 13
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("BloodSpike");
            item.mana = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "BloodCotton", 25);
            recipe.AddIngredient(mod, "MoltenBones", 20);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}