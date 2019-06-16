using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NMIP.Tiles
{
    public class ODINtrophy : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);
            dustType = 217;
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("ODIN Trophy");
            AddMapEntry(new Color(91, 121, 156), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int item = 0;
            item = mod.ItemType("ODINtrophy");
            Item.NewItem(i * 16, j * 16, 48, 48, item);
        }
    }
}