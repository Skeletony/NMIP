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

namespace NMIP
{
    public class NMIPWorld : ModWorld
    {
        private const int saveVersion = 0;
        public static int CrystalForestTile = 0;
        public static bool downedODIN = false;
        public static bool IceOre;
        public static bool MoltenOre;

        public override void Initialize()
        {
            downedODIN = false;
            IceOre = NPC.downedPlantBoss;
            MoltenOre = Main.hardMode;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedODIN) downed.Add("ODIN");

            return new TagCompound {
                {"downed", downed}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedODIN = downed.Contains("ODIN");
            IceOre = NPC.downedPlantBoss;
            MoltenOre = Main.hardMode;
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
            writer.Write(flags);

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
        }

        public void PlaceCrystalForest(int x, int y)
        {
            //initial pit
            WorldMethods.TileRunner(x, y, (double)150, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, true, true); //improve basic shape later
            bool leftpit = false;
            int PitX;
            int PitY;
            if (Main.rand.Next(2) == 0)
            {
                leftpit = true;
            }
            if (leftpit)
            {
                PitX = x - Main.rand.Next(5, 15);
            }
            else
            {
                PitX = x + Main.rand.Next(5, 15);
            }
            for (PitY = y - 16; PitY < y + 25; PitY++)
            {
                WorldGen.digTunnel(PitX, PitY, 0, 0, 1, 4, false);
                WorldGen.TileRunner(PitX, PitY, 11, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, false, true);
            }
            //tunnel off of pit
            int tunnellength = Main.rand.Next(50, 110);
            int TunnelEndX = 0;
            if (leftpit)
            {
                for (int TunnelX = PitX; TunnelX < PitX + tunnellength; TunnelX++)
                {
                    WorldGen.digTunnel(TunnelX, PitY, 0, 0, 1, 4, false);
                    WorldGen.TileRunner(TunnelX, PitY, 13, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, false, true);
                    TunnelEndX = TunnelX;
                }
            }
            else
            {
                for (int TunnelX = PitX; TunnelX > PitX - tunnellength; TunnelX--)
                {
                    WorldGen.digTunnel(TunnelX, PitY, 0, 0, 1, 4, false);
                    WorldGen.TileRunner(TunnelX, PitY, 13, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, false, true);
                    TunnelEndX = TunnelX;
                }
            }
            //More pits and spikes
            int TrapX;
            for (int TrapNum = 0; TrapNum < 10; TrapNum++)
            {
                if (leftpit)
                {
                    TrapX = Main.rand.Next(PitX, PitX + tunnellength);
                }
                else
                {
                    TrapX = Main.rand.Next(PitX - tunnellength, PitX);
                }
                for (int TrapY = PitY; TrapY < PitY + 15; TrapY++)
                {
                    WorldGen.digTunnel(TrapX, TrapY, 0, 0, 1, 3, false);
                    WorldGen.TileRunner(TrapX, TrapY, 11, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, false, true);
                }
                WorldGen.TileRunner(TrapX, PitY + 18, 9, 1, 48, false, 0f, 0f, false, true);
            }
            //Additional hole and tunnel
            int PittwoY = 0;
            for (PittwoY = PitY; PittwoY < PitY + 40; PittwoY++)
            {
                WorldGen.digTunnel(TunnelEndX, PittwoY, 0, 0, 1, 4, false);
                WorldGen.TileRunner(TunnelEndX, PittwoY, 11, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, false, true);
            }
            int PittwoX = 0;
            for (PittwoX = TunnelEndX - 50; PittwoX < TunnelEndX + 50; PittwoX++)
            {
                WorldGen.digTunnel(PittwoX, PittwoY, 0, 0, 1, 4, false);
                WorldGen.TileRunner(PittwoX, PittwoY, 13, 1, mod.TileType("ReachGrassTile"), false, 0f, 0f, false, true);
                WorldGen.PlaceChest(PittwoX, PittwoY, 21, false, 2);
                WorldGen.PlaceChest(PittwoX + 5, PittwoY + 3, 21, false, 2);
                WorldGen.PlaceChest(PittwoX + 1, PittwoY + 2, 21, false, 2);
            }
            //grass walls
            for (int wallx = x - 100; wallx < x + 100; wallx++)
            {
                for (int wally = y - 25; wally < y + 100; wally++)
                {
                    if (Main.tile[wallx, wally].wall != 0)
                    {
                        WorldGen.KillWall(wallx, wally);
                        WorldGen.PlaceWall(wallx, wally, 63);
                    }
                }
            }
            //campfires and shit
            int SkullStickY = 0;
            Tile tile = Main.tile[1, 1];
            for (int SkullStickX = x - 90; SkullStickX < x + 90; SkullStickX++)
            {
                if (Main.rand.Next(4) == 1)
                {
                    for (SkullStickY = y - 80; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0)
                        {
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 2, 215);//i dont know which of these is correct but i cant be bothered to test.
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 1, 215);
                            WorldGen.PlaceObject(SkullStickX, SkullStickY, 215);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 2, 215, 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 1, 215, 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY, 215, 0, 0, -1, -1);
                        }
                    }
                }
                if (Main.rand.Next(9) == 1)
                {
                    for (SkullStickY = y - 60; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0 || tile.type == mod.TileType("ReachGrassTile"))
                        {
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 3, mod.TileType("SkullStick")); //i dont know which of these is correct but i cant be bothered to test.
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 2, mod.TileType("SkullStick"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 1, mod.TileType("SkullStick"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY, mod.TileType("SkullStick"));
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 3, mod.TileType("SkullStick"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 2, mod.TileType("SkullStick"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 1, mod.TileType("SkullStick"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY, mod.TileType("SkullStick"), 0, 0, -1, -1);
                        }
                    }
                }
                if (Main.rand.Next(12) == 1)
                {
                    for (SkullStickY = y - 60; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0 || tile.type == mod.TileType("ReachGrassTile"))
                        {
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 3, mod.TileType("SkullStick2")); //i dont know which of these is correct but i cant be bothered to test.
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 2, mod.TileType("SkullStick2"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 1, mod.TileType("SkullStick2"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY, mod.TileType("SkullStick2"));
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 3, mod.TileType("SkullStick2"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 2, mod.TileType("SkullStick2"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 1, mod.TileType("SkullStick2"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY, mod.TileType("SkullStick2"), 0, 0, -1, -1);
                        }
                    }
                }
                if (Main.rand.Next(10) == 1)
                {
                    for (SkullStickY = y - 60; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0 || tile.type == mod.TileType("ReachGrassTile"))
                        {
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 3, mod.TileType("SkullStick3")); //i dont know which of these is correct but i cant be bothered to test.
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 2, mod.TileType("SkullStick3"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 1, mod.TileType("SkullStick3"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY, mod.TileType("SkullStick3"));
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 3, mod.TileType("SkullStick3"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 2, mod.TileType("SkullStick3"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 1, mod.TileType("SkullStick3"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY, mod.TileType("SkullStick3"), 0, 0, -1, -1);
                        }
                    }
                }
                if (Main.rand.Next(25) == 1)
                {
                    for (SkullStickY = y - 60; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0 || tile.type == mod.TileType("ReachGrassTile"))
                        {
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 3, mod.TileType("CreationAltarTile")); //i dont know which of these is correct but i cant be bothered to test.
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 2, mod.TileType("CreationAltarTile"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 1, mod.TileType("CreationAltarTile"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY, mod.TileType("CreationAltarTile"));
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 3, mod.TileType("CreationAltarTile"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 2, mod.TileType("CreationAltarTile"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 1, mod.TileType("CreationAltarTile"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY, mod.TileType("CreationAltarTile"), 0, 0, -1, -1);
                        }
                    }
                }
                if (Main.rand.Next(10) == 1)
                {
                    for (SkullStickY = y - 60; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0 || tile.type == mod.TileType("ReachGrassTile"))
                        {
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 3, mod.TileType("ReachGrass1")); //i dont know which of these is correct but i cant be bothered to test.
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 2, mod.TileType("ReachGrass1"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY - 1, mod.TileType("ReachGrass1"));
                            WorldGen.PlaceObject(SkullStickX, SkullStickY, mod.TileType("ReachGrass1"));
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 3, mod.TileType("ReachGrass1"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 2, mod.TileType("ReachGrass1"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY - 1, mod.TileType("ReachGrass1"), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacment(-1, SkullStickX, SkullStickY, mod.TileType("ReachGrass1"), 0, 0, -1, -1);
                        }
                    }
                }
                if (Main.rand.Next(16) == 1)
                {
                    for (SkullStickY = y - 60; SkullStickY < y + 75; SkullStickY++)
                    {
                        tile = Main.tile[SkullStickX, SkullStickY];
                        if (tile.type == 2 || tile.type == 1 || tile.type == 0 || tile.type == mod.TileType("ReachGrassTile"))
                        {
                            WorldGen.PlaceChest(SkullStickX, SkullStickY - 3, (ushort)mod.TileType("ReachChest"), false, 0);
                            WorldGen.PlaceChest(SkullStickX, SkullStickY - 2, (ushort)mod.TileType("ReachChest"), false, 0);
                            WorldGen.PlaceChest(SkullStickX, SkullStickY - 1, (ushort)mod.TileType("ReachChest"), false, 0);
                        }
                    }
                }
            }
            //loot placement
            for (PittwoX = TunnelEndX - 20; PittwoX < TunnelEndX + 20; PittwoX++)
            {
                if (Main.rand.Next(30) == 1)
                {
                    Main.tile[PittwoX, PittwoY + 1].active(true);
                    Main.tile[PittwoX + 1, PittwoY + 1].active(true);
                    Main.tile[PittwoX, PittwoY + 1].type = 1;
                    Main.tile[PittwoX + 1, PittwoY + 1].type = 1;
                    WorldGen.AddLifeCrystal(PittwoX + 1, PittwoY);
                    WorldGen.AddLifeCrystal(PittwoX + 1, PittwoY + 1);
                    break;
                }
            }
            for (int trees = 0; trees < 5000; trees++)
            {
                int E = x + Main.rand.Next(-200, 200);
                int F = y + Main.rand.Next(-30, 30);
                tile = Framing.GetTileSafely(E, F);
                if (tile.type == mod.TileType("ReachGrassTile"))
                {
                    WorldGen.GrowTree(E, F);
                }
            }
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
    }
}
