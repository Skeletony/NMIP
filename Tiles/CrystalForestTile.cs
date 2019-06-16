using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace NMIP.Tiles
{
    public class CrystalForestTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new CrystalForestTree());
            Main.tileMerge[Type][mod.TileType("CrystalForestTile")] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            //Main.tileLighted[Type] = true;
            AddMapEntry(new Color(129, 167, 168));
            drop = mod.ItemType("CrystalForestTile");
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            TileObject toBePlaced;
            if (!TileObject.CanPlace(x, y, type, style, direction, out toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("CrystalForestSapling");
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.135f;
            g = 0.220f;
            b = 0.224f;
        }
    }
}

