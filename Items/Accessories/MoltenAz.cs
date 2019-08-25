using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    class MoltenAz : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of the Underworld");
            Tooltip.SetDefault("You have a warm heart now."
                + "\nGrants immunity to Lava and fireblocks"
                + "\nGrants immunity to On Fire! debuff"
                + "\nWhile submurged in lava you gain +15 defense");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 40;
            item.value = 30000;
            item.rare = 7;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[24] = true;
            if (player.lavaWet == true && player.GetModPlayer<NMIPPlayer>(mod).godMoltenEffect == false)
            {
                player.statDefense += 15;
                player.GetModPlayer<NMIPPlayer>(mod).moltenEffect = true;
            }
        }
    }
}
