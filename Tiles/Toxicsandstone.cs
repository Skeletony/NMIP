using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    public class Toxicsandstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Sandstone[Type] = true;
            Main.tileLighted[Type] = false;
            dustType = 277;
            drop = mod.ItemType("Toxicsandstone");   //put your CustomBlock name
            AddMapEntry(new Color(118, 109, 85));
            minPick = 65;
        }
    }
}