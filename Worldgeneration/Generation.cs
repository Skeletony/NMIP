using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using BaseMod;
using NMIP.Tiles;
using NMIP.Walls;

namespace NMIP.Worldgeneration
{
    public class ToxinDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = NMIP.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[Color.Black] = -1;

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[Color.Black] = -1;

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/ToxinDelete"), colorToTile, mod.GetTexture("Worldgeneration/ToxinDelete"), colorToWall);
            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 30;
            gen.Generate(genX, genY, true, true);

            return true;
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class ToxinCave : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = NMIP.instance;
            ushort tileGrass = (ushort)mod.TileType("ToxinGrass"), tileDirt = TileID.Dirt, tileStone = (ushort)mod.TileType("ToxinStone"), tileIce = (ushort)mod.TileType("GreenIce"),
            tileSand = (ushort)mod.TileType("Toxicsand"), tileSandHardened = (ushort)mod.TileType("ToxicsandHardened"), tileSandstone = (ushort)mod.TileType("Toxicsandstone");

            byte StoneWall = (byte)mod.WallType("ToxinStoneWall"), SandstoneWall = (byte)mod.WallType("ToxicsandstoneWall"), HardenedSandWall = (byte)mod.WallType("ToxicsandHardenedWall"),
            GrassWall = (byte)mod.WallType("LivingToxinleafWall"), JungleWall = (byte)mod.WallType("ToxicJungleWall");

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180, biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = mod.TileType("ToxinStone"),
                [new Color(255, 0, 0)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = mod.WallType("ToxinStoneWall"),
                [Color.Black] = -1 //don't touch when genning
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/ToxinCave"), colorToTile, mod.GetTexture("Worldgeneration/ToxinCaveWalls"));
            Point newOrigin = new Point(origin.X, origin.Y - 10); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.JungleGrass, TileID.CorruptGrass, TileID.FleshGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Dirt }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileDirt, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //stone...
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone, TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileStone, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //ice...
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileIce, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //sand...
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sand, TileID.Ebonsand, TileID.Crimsand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSand, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //hardened sand...
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSandHardened, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSandstone, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.Stone, WallID.EbonstoneUnsafe, WallID.CrimstoneUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(StoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.Sandstone, WallID.CorruptSandstone, WallID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(SandstoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.GrassUnsafe, WallID.CorruptGrassUnsafe, WallID.CrimsonGrassUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(GrassWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.JungleUnsafe, WallID.JungleUnsafe1, WallID.JungleUnsafe2, WallID.JungleUnsafe3, WallID.JungleUnsafe4 }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(JungleWall, true)
            }));
            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 30;
            gen.Generate(genX, genY, true, true);

            return true;
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class InWorld : GenAction
    {
        public InWorld()
        {
        }

        public override bool Apply(Point origin, int x, int y, params object[] args)
        {
            if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                return Fail();
            return UnitApply(origin, x, y, args);
        }
    }

    public class ImpactSite : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = NMIP.instance;


            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 255)] = mod.TileType("CosmicOre");
            colorToTile[new Color(255, 255, 255)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/MeteorSite"), colorToTile);

            return true;
        }
    }
}
