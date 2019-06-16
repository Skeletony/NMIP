using Terraria;
using Terraria.ModLoader;

namespace NMIP.Items.ODIN
{
    public class ODINBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 36;
            item.height = 32;
            item.rare = 9;
            item.expert = true;
            bossBagNPC = mod.NPCType("ODIN");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            int choice = Main.rand.Next(2);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Skell"));
                player.QuickSpawnItem(mod.ItemType("AncientMachinery"), Main.rand.Next(15, 31));
                player.QuickSpawnItem(mod.ItemType("Shell"), Main.rand.Next(5, 11));
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("EldritchSword"));
                player.QuickSpawnItem(mod.ItemType("AncientMachinery"), Main.rand.Next(15, 31));
                player.QuickSpawnItem(mod.ItemType("Shell"), Main.rand.Next(5, 11));
            }
        }
    }
}
