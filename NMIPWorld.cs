using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria.ID;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;
using NMIP.Walls;
using NMIP.Tiles;
using NMIP.Worldgeneration;
using Terraria.Localization;
using BaseMod;

namespace NMIP
{
    public class NMIPWorld : ModWorld
    {
        private const int saveVersion = 0;
        public static int CrystalForestTile = 0;
        public static bool downedODIN = false;
        public static bool IceOre;
        public static bool MoltenOre;
        public static bool downedStarboss = false;

        //tile ints
        public static int toxinTiles = 0;

        private Vector2 ToxinCenter = -Vector2.One;
        private Vector2 toxinpos = new Vector2(0, 0);
        private int otherSide = 0;

        public override void Initialize()
        {
            downedODIN = false;
            IceOre = NPC.downedPlantBoss;
            MoltenOre = Main.hardMode;
            ToxinCenter = -Vector2.One;
            toxinpos = new Vector2(0, 0);
            downedStarboss = false;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedODIN) downed.Add("ODIN");
            if (downedStarboss) downed.Add("Starboss");

            return new TagCompound {
                {"downed", downed},
                {"TXCenter", ToxinCenter}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedODIN = downed.Contains("ODIN");
            downedStarboss = downed.Contains("Starboss");
            IceOre = NPC.downedPlantBoss;
            MoltenOre = Main.hardMode;

            if (tag.ContainsKey("TXCenter")) // check if the altar coordinates exist in the save file
            {
                ToxinCenter = tag.Get<Vector2>("TXCenter");
            }
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedODIN = flags[0];
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedODIN;
            flags[1] = downedStarboss;
            writer.Write(flags);
            writer.WriteVector2(ToxinCenter);

            //If you prefer, you can use the BitsByte constructor approach as well.
            //writer.Write(saveVersion);
            //BitsByte flags = new BitsByte(downedJim, downedPuritySpirit);
            //writer.Write(flags);

            // This is another way to do the same thing, but with bitmasks and the bitwise OR assignment operator (the |=)
            // Note that 1 and 2 here are bit masks. The next values in the pattern are 4,8,16,32,64,128. If you require more than 8 flags, make another byte.
            //writer.Write(saveVersion);
            //byte flags = 0;
            //if (downedJim)
            //{
            //	flags |= 1;
            //}
            //if (downedPuritySpirit)
            //{
            //	flags |= 2;
            //}
            //writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedODIN = flags[0];
            downedStarboss = flags[1];
            ToxinCenter = reader.ReadVector2();
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            int ChaosIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Slush"));
            int shiniesIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

            tasks.Insert(ChaosIndex + 1, new PassLegacy("ToxinCave", delegate (GenerationProgress progress)
            {
                ToxinCave(progress);
            }));
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            Main.sandTiles += tileCounts[mod.TileType<Toxicsand>()] + tileCounts[mod.TileType<Toxicsandstone>()] + tileCounts[mod.TileType<ToxicsandHardened>()];
            Main.snowTiles += tileCounts[mod.TileType<GreenIce>()];
            toxinTiles = tileCounts[mod.TileType<ToxinGrass>()] + tileCounts[mod.TileType<ToxinStone>()] + tileCounts[mod.TileType<Toxicsand>()] + tileCounts[mod.TileType<Toxicsandstone>()] + tileCounts[mod.TileType<ToxicsandHardened>()] + tileCounts[mod.TileType<GreenIce>()];
            Main.jungleTiles += toxinTiles;
        }

        private void ToxinCave(GenerationProgress progress)
        {
            otherSide = (Main.dungeonX > Main.maxTilesX / 2) ? (-1) : 1;
            toxinpos.X = (Main.maxTilesX >= 8000) ? (otherSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (otherSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
            int q = (int)WorldGen.worldSurfaceLow - 30;
            while (Main.tile[(int)toxinpos.X, q] != null && !Main.tile[(int)toxinpos.X, q].active())
            {
                q++;
            }
            for (int l = (int)toxinpos.X - 25; l < (int)toxinpos.X + 25; l++)
            {
                for (int m = q - 6; m < q + 90; m++)
                {
                    if (Main.tile[l, m] != null && Main.tile[l, m].active())
                    {
                        int type = Main.tile[l, m].type;
                        if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                        {
                            q++;
                        }
                    }
                }
            }
            toxinpos.Y = q;

            ToxinCenter = toxinpos;

            progress.Message = "Growing the toxins";
            ToxinCave();
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX <= 4200) { return 1; }
            else if (Main.maxTilesX <= 6400) { return 2; }
            else if (Main.maxTilesX <= 8400) { return 3; }
            return 1;
        }

        public void ToxinCave()
        {
            Point origin = new Point((int)toxinpos.X, (int)toxinpos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            ToxinDelete delete = new ToxinDelete();
            ToxinCave biome = new ToxinCave();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }

        public override void PostUpdate()
        {
            if (NPC.downedPlantBoss == true)
            {
                if (IceOre == false)
                {
                    IceOre = true;
                    Main.NewText("Frozen metal blooms in the underground...", Color.Aquamarine.R, Color.Aquamarine.G, Color.LightBlue.B);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 300), WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(7, 9), (ushort)mod.TileType("IceOre"));
                    }
                }
            }

            if (Main.hardMode == true && MoltenOre == false)
            {
                MoltenOre = true;
                Main.NewText("The Underworld gets hotter...", Color.Red.R, Color.Red.G, Color.LightYellow.B);
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                {
                    WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.maxTilesY - 200, Main.maxTilesY), WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(7, 9), (ushort)mod.TileType("MoltenRock"));
                }
            }
        }

        public static void Spawn(Player player, Mod mod, string name)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-200f, 200f, (float)Main.rand.NextDouble()), 100f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }

        public static void WorldConvert(int i, int j, int conversionType, int size = 4)
        {
            Mod mod = NMIP.instance;
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;
                        bool sendNet = false;
                        if (conversionType == 1)
                        {
                            if (WallID.Sets.Conversion.Stone[wall])
                            {
                                Main.tile[k, l].type = (ushort)mod.WallType<ToxinStoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<ToxicsandstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<ToxicsandHardenedWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            if (TileID.Sets.Conversion.Stone[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<ToxinStone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Grass[type] && type != TileID.JungleGrass)
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<ToxinGrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Ice[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<GreenIce>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sand[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Toxicsand>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<ToxicsandHardened>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Toxicsandstone>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }

                            if (sendNet)
                                NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (conversionType == 6) //Jungle
                        {
                            if (wall == 2)
                            {
                                Main.tile[k, l].wall = 15;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == 63)
                            {
                                Main.tile[k, l].wall = 64;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.Stone[wall] && wall != WallID.Stone)
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != WallID.HardenedSand)
                            {
                                Main.tile[k, l].wall = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall] && wall != WallID.Sandstone)
                            {
                                Main.tile[k, l].wall = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == 0 && Main.tile[k, l].active())
                            {
                                Main.tile[k, l].type = TileID.Mud;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Grass[type] || type == TileID.MushroomGrass)
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Stone[type] && type != TileID.Stone)
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 3)
                            {
                                Main.tile[k, l].type = 61;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 52)
                            {
                                Main.tile[k, l].type = 62;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 73)
                            {
                                Main.tile[k, l].type = 74;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 7) //Jungle Removal
                        {
                            if (wall == 15)
                            {
                                Main.tile[k, l].wall = 2;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (wall == 64)
                            {
                                Main.tile[k, l].wall = 63;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == TileID.Mud && Main.tile[k, l].active())
                            {
                                Main.tile[k, l].type = 0;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            else if (type == 60)
                            {
                                Main.tile[k, l].type = 2;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 61)
                            {
                                Main.tile[k, l].type = 3;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 62)
                            {
                                Main.tile[k, l].type = 52;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 74)
                            {
                                Main.tile[k, l].type = 73;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 8) //Snow
                        {
                            if (wall == 2 || wall == 63 || wall == 65)
                            {
                                Main.tile[k, l].wall = 40;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (type == 0 && Main.tile[k, l].active() || type == 2 || type == 23 || type == 109 || type == 199)
                            {
                                Main.tile[k, l].type = 147;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 1)
                            {
                                Main.tile[k, l].type = 161;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 25)
                            {
                                Main.tile[k, l].type = 163;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 117)
                            {
                                Main.tile[k, l].type = 164;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            else if (type == 203)
                            {
                                Main.tile[k, l].type = 200;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<ToxinStone>())
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<ToxinStone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 9) //Snowmelt
                        {
                            if (wall == WallID.SnowWallUnsafe)
                            {
                                Main.tile[k, l].wall = WallID.GrassUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (wall == WallID.IceUnsafe)
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (type == TileID.SnowBlock)
                            {
                                if ((WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1].type == TileID.Trees) || (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1].type == TileID.Trees) ||
                                    (WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1] == null) ||
                                    (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null))
                                {
                                    Main.tile[k, l].type = 2;
                                }
                                else
                                {
                                    Main.tile[k, l].type = 0;
                                }
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.IceBlock)
                            {
                                Main.tile[k, l].type = 1;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.CorruptIce)
                            {
                                Main.tile[k, l].type = TileID.Ebonstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.HallowedIce)
                            {
                                Main.tile[k, l].type = TileID.Pearlstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.FleshIce)
                            {
                                Main.tile[k, l].type = TileID.Crimstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<GreenIce>())
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<ToxinStone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 11) //Order
                        {
                            if (wall == mod.WallType<ToxinStoneWall>())
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<ToxicJungleWall>())
                            {
                                Main.tile[k, l].wall = WallID.JungleUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<ToxicsandHardenedWall>())
                            {
                                Main.tile[k, l].wall = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<ToxicsandstoneWall>())
                            {
                                Main.tile[k, l].wall = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<ToxinStone>())
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<ToxinGrass>())
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Toxicsand>())
                            {
                                Main.tile[k, l].type = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<ToxicsandHardened>())
                            {
                                Main.tile[k, l].type = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Toxicsandstone>())
                            {
                                Main.tile[k, l].type = TileID.Sandstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<GreenIce>())
                            {
                                Main.tile[k, l].type = TileID.IceBlock;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                    }
                }
            }
        }
    }
}