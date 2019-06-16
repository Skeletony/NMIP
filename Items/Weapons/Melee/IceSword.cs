using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Melee
{
    public class IceSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero");
            Tooltip.SetDefault("Now THIS freezes everything on touch.");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 80;           //The damage of your weapon
            item.melee = true;          //Is your weapon a melee weapon?
            item.width = 50;            //Weapon's texture's width
            item.height = 50;           //Weapon's texture's height
            item.useTime = 40;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 40;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 10;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(silver: 1);           //The value of the weapon
            item.rare = 8;              //The rarity of the weapon, from -1 to 13
            item.UseSound = (SoundID.Item79);     //The sound when the weapon is using
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.useTurn = true;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("IceProjectile");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 120);
            if (Main.rand.Next(6) == 0)
            {
                target.AddBuff(BuffID.Frozen, 60);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IceBar", 25);
            recipe.AddIngredient(mod, "FrostFragment", 20);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}