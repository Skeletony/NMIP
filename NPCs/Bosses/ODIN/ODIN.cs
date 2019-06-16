using System;
using System.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace NMIP.NPCs.Bosses.ODIN
{
    [AutoloadBossHead]
    public class ODIN : ModNPC
    {

        private int moveTime = 300;
        private int moveTimer = 60;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ODIN");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.EyeofCthulhu);
            npc.aiStyle = -1;
            npc.width = 142;
            npc.height = 176;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 35000;
            npc.damage = 120;
            npc.defense = 20;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DarkCity");
            animationType = NPCID.IceElemental;
            bossBag = mod.ItemType("ODINBag");
        }
        public void checkDead()
        {
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "ODIN";
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ODINtrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ODINmask"));
                }
                int choice = Main.rand.Next(2);

                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("skell"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientMachinery"), Main.rand.Next(15, 31));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shell"), Main.rand.Next(5, 11));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EldritchSword"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientMachinery"), Main.rand.Next(15, 31));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shell"), Main.rand.Next(5, 11));
                }
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.defense = 75;
        }

        public override void AI()
        {
            LookToPlayer();
            npc.netUpdate = true;
            Lighting.AddLight(npc.Center, 1.3F, 2.4F, 2.5F);
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                npc.velocity.Y = -100;
            }
            if (npc.ai[0] == 0) // Flying around and shooting projectiles
            {
                #region Flying Movement
                float speed = 7f;
                float acceleration = 0.09f;
                Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
                float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
                if (length > 400 && Main.expertMode)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 600)
                    {
                        ++speed;
                        acceleration += 0.05F;
                        if (length > 800)
                        {
                            ++speed;
                            acceleration += 0.05F;
                        }
                    }
                }
                float num10 = speed / length;
                xDir = xDir * num10;
                yDir = yDir * num10;
                if (npc.velocity.X < xDir)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X < 0 && xDir > 0)
                        npc.velocity.X = npc.velocity.X + acceleration;
                }
                else if (npc.velocity.X > xDir)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.X > 0 && xDir < 0)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                if (npc.velocity.Y < yDir)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                    if (npc.velocity.Y < 0 && yDir > 0)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                }
                else if (npc.velocity.Y > yDir)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (npc.velocity.Y > 0 && yDir < 0)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                }
                #endregion
                // Shadow Ball Shoot
                if (npc.ai[1] % 45 == 0)
                {
                    Vector2 dir = Main.player[npc.target].Center - npc.Center;
                    dir.Normalize();
                    dir *= 12;
                    int newNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ShadowBall"), npc.whoAmI);
                    Main.npc[newNPC].velocity = dir;
                }
                // Crystal Shadow Shoot.
                if (npc.ai[1] == 150)
                {
                    for (int i = 0; i < 8; ++i)
                    {
                        bool expertMode = Main.expertMode;
                        Vector2 targetDir = ((((float)Math.PI * 2) / 8) * i).ToRotationVector2();
                        targetDir.Normalize();
                        targetDir *= 9;
                        int dmg = expertMode ? 23 : 37;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, targetDir.X, targetDir.Y, mod.ProjectileType("CrystalShadow"), dmg, 0.5F, Main.myPlayer);
                    }
                }
                npc.ai[1]++;
                if (npc.ai[1] >= 300)
                {
                    npc.ai[0] = 1;
                    npc.ai[1] = 60;
                }
                // Rage Phase Switch
                if (npc.life <= 9000)
                {
                    npc.ai[0] = 2;
                    npc.ai[1] = 0;
                }
            }
            else if (npc.ai[0] == 1) // Charging.
            {
                npc.ai[1]++;
                if (npc.ai[1] % 25 == 0)
                {
                    npc.TargetClosest(true);
                    float speed = 10 ;
                    Vector2 vector2_1 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float dirX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_1.X;
                    float dirY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_1.Y;
                    float targetVel = Math.Abs(Main.player[npc.target].velocity.X) + Math.Abs(Main.player[npc.target].velocity.Y) / 4f;

                    float speedMultiplier = targetVel + (10f - targetVel);
                    if (speedMultiplier < 3.0)
                        speedMultiplier = 3f;
                    if (speedMultiplier > 10.0)
                        speedMultiplier = 10f;
                    float speedX = dirX - Main.player[npc.target].velocity.X * speedMultiplier;
                    float speedY = dirY - (Main.player[npc.target].velocity.Y * speedMultiplier / 4);
                    speedX = speedX * (float)(1 + Main.rand.Next(-10, 11) * 0.00999999977648258);
                    speedY = speedY * (float)(1 + Main.rand.Next(-10, 11) * 0.00999999977648258);
                    float speedLength = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                    float actualSpeed = speed / speedLength;
                    npc.velocity.X = speedX * actualSpeed;
                    npc.velocity.Y = speedY * actualSpeed;
                    npc.velocity.X = npc.velocity.X + Main.rand.Next(-40, 41) * 0.1f;
                    npc.velocity.Y = npc.velocity.Y + Main.rand.Next(-40, 41) * 0.1f;
                }
                if (npc.ai[1] >= 270)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.velocity *= 0.3F;
                }
            }
        }

        //public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        //{
        //{
        //SpriteEffects spriteEffects = SpriteEffects.None;
        //spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/ODIN_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
        //npc.frame, Color.White, npc.rotation,
        //new Vector2(npc.width, npc.height), 1f, spriteEffects, 0f);
        //}
        //}

        private void LookToPlayer()
        {
            Vector2 look = Main.player[npc.target].Center - npc.Center;
            LookInDirection(look);
        }

        private void LookInDirection(Vector2 look)
        {
            float angle = 0.5f * (float)Math.PI;
            if (look.X != 0f)
            {
                angle = (float)Math.Atan(look.Y / look.X);
            }
            else if (look.Y < 0f)
            {
                angle += (float)Math.PI;
            }
            if (look.X < 0f)
            {
                angle += (float)Math.PI;
            }
            npc.rotation = angle;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)moveTime);
            writer.Write((short)moveTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            moveTime = reader.ReadInt16();
            moveTimer = reader.ReadInt16();
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.ShadowFlame, 600, true);
            if (Main.expertMode)
            {
                player.AddBuff(BuffID.CursedInferno, 600, true);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ODIN1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ODIN2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ODIN3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ODIN4"), 1f);
            }
        }
    }
}