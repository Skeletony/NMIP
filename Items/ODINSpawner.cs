using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items
{
    public class ODINSpawner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Machinery");
            Tooltip.SetDefault("An ancient machine that contains ancient codes.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 20;
            item.maxStack = 20;
            item.rare = 10;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            // "player.ZoneUnderworldHeight" could also be written as "player.position.Y / 16f > Main.maxTilesY - 200"
            return NPC.downedPlantBoss && !player.ZoneDirtLayerHeight && !NPC.AnyNPCs(mod.NPCType("ODIN"));
        }
        public int pCenterY;
        public override bool UseItem(Player player)
        {
            pCenterY = (int)player.Center.Y;
            NPC.NewNPC((int)player.Center.X, (pCenterY - 2500), mod.NPCType("ODIN"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            Main.NewText("[c/FFA500:ODIN has awoken!]");
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AncientMachinery"), 15);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}