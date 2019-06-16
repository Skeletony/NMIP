using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Melee
{
    public class Bloodstream : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Bone made sword used to ram enemies rather than stab or slash. Must've hurt.");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 110;           //The damage of your weapon
            item.melee = true;          //Is your weapon a melee weapon?
            item.width = 52;            //Weapon's texture's width
            item.height = 64;           //Weapon's texture's height
            item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(gold: 15);           //The value of the weapon
            item.rare = 10;              //The rarity of the weapon, from -1 to 13
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.useTurn = true;
            item.shoot = mod.ProjectileType("BloodSpike");
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenBones", 25);
            recipe.AddIngredient(mod, "BloodCotton", 30);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
        }
    }
}