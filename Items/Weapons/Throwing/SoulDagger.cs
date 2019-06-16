using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.Weapons.Throwing
{
    public class SoulDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Cutter");
            Tooltip.SetDefault("Cuts through space and time");
        }

        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.width = 22;
            item.height = 46;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 10;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("SoulDagger");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 50; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}