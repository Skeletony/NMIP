using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    class MoltenAzz : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire God's Idol");
            Tooltip.SetDefault("You have the power of a fire god."
                + "\nGrants immunity to Lava and fireblocks"
                + "\nGrants immunity to all fire debuffs"
                + "\nWhile submurged in lava you gain +20 defense and +20% damage to all classes");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 48;
            item.value = 40000;
            item.rare = 8;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[24] = true;
            player.buffImmune[153] = true;
            player.buffImmune[39] = true;
            if (player.lavaWet == true && player.GetModPlayer<NMIPPlayer>(mod).MoltenEffect == false)
            {
                player.statDefense += 20;
                player.magicDamage += 0.2f;
                player.meleeDamage += 0.2f;
                player.thrownDamage += 0.2f;
                player.rangedDamage += 0.2f;
                player.GetModPlayer<NMIPPlayer>(mod).GodMoltenEffect = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenAz");
            recipe.AddIngredient(ItemID.MagmaStone);
            recipe.AddIngredient(mod, "MoltenBar", 15);
            recipe.AddIngredient(ItemID.LavaBucket, 3);

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
