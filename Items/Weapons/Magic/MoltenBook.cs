﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Magic
{
    class MoltenBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magma Shower");
            Tooltip.SetDefault("Have a hot shower!");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 65;           //The damage of your weapon
            item.magic = true;          //Is your weapon a magic weapon?
            item.noMelee = true;
            item.width = 40;            //Weapon's texture's width
            item.height = 40;           //Weapon's texture's height
            item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 10;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(gold: 10);           //The value of the weapon
            item.rare = 7;              //The rarity of the weapon, from -1 to 13
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("MagmaShower");
            item.mana = 6;
            item.UseSound = SoundID.Item34;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenBar", 15);
            recipe.AddIngredient(ItemID.SpellTome);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}