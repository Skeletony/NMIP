using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    public class ScorpionTail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Melee weapons have a chance of inflicting venom\n+10 defense");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.value = Item.buyPrice(0, 0, 15, 0);
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 10;
            player.GetModPlayer<NMIPPlayer>(mod).ToxicExtract = true;
        }
    }
}
