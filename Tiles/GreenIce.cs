using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    public class GreenIce : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = false;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            soundType = 21;
            dustType = 273;
            drop = mod.ItemType("GreenIce");   //put your CustomBlock name
            AddMapEntry(new Color(59, 255, 28));
            TileID.Sets.Conversion.Ice[Type] = true;
            TileID.Sets.Ices[Type] = true;
        }
    }
}