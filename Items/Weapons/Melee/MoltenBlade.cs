using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NMIP.Items.Weapons.Melee
{
    public class MoltenBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneous Blade");
            Tooltip.SetDefault("This blade is hot!");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.FieryGreatsword);
            item.damage = 76;           //The damage of your weapon
            item.melee = true;          //Is your weapon a melee weapon?
            item.width = 46;            //Weapon's texture's width
            item.height = 52;           //Weapon's texture's height
            item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 10;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(silver: 1);           //The value of the weapon
            item.rare = 7;              //The rarity of the weapon, from -1 to 13
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.UseSound = SoundID.Item34;
            item.useTurn = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenBar", 25);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire, 3.5f);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
        }
    }
}