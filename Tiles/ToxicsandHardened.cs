using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    public class ToxicsandHardened : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Terraria.ID.TileID.Sets.Conversion.HardenedSand[Type] = true;
            Main.tileLighted[Type] = false;
            dustType = 277;
            drop = mod.ItemType("ToxicsandHardened");   //put your CustomBlock name
            AddMapEntry(new Color(143, 111, 63));
            minPick = 65;
        }
    }
}