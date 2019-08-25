using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    public class ToxinStone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            soundType = 21;
            minPick = 65;
            TileID.Sets.JungleSpecial[Type] = true;
            dustType = 277;
            drop = mod.ItemType("ToxinStone");   //put your CustomBlock name
            AddMapEntry(new Color(105, 89, 75));
        }
    }
}