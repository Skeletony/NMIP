using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    public class LCExpertDrop : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunatic Artifact");
            Tooltip.SetDefault("Magic weapons inflict Lunatic Flames");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 42;
            item.value = Item.buyPrice(0, 5, 30, 0);
            item.rare = -12;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 10;
            player.GetModPlayer<NMIPPlayer>(mod).cultistFlame = true;
        }
    }
}
