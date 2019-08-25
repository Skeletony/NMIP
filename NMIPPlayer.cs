using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace NMIP
{
    class NMIPPlayer : ModPlayer
    {
        private const int saveVersion = 0;
        public int score = 0;
        public int constantDamage = 0;
        public float percentDamage = 0f;
        public float defenseEffect = -1f;
        public bool badHeal = false;
        public int healHurt = 0;
        public bool friendPet = false;
        public bool infinity = false;
        public bool toxicExtract = false;
        public bool moltenEffect = false;
        public bool godMoltenEffect = false;
        public bool aresEffect = false;
        public bool cultistFlame = false;
        public bool graniteCore = false;
        public bool honeyVeil = false;
        public bool zoneToxin = false;

        public override void ResetEffects()
        {
            constantDamage = 0;
            percentDamage = 0f;
            defenseEffect = -1f;
            toxicExtract = false;
            badHeal = false;
            healHurt = 0;
            friendPet = false;
            moltenEffect = false;
            godMoltenEffect = false;
            aresEffect = false;
            cultistFlame = false;
            graniteCore = false;
            honeyVeil = false;
            zoneToxin = false;
        }

        public override void UpdateBiomes()
        {
            zoneToxin = NMIPWorld.toxinTiles > 100;
        }

        public override bool CustomBiomesMatch(Player other)
        {
            NMIPPlayer modOther = other.GetModPlayer<NMIPPlayer>();
            return zoneToxin == modOther.zoneToxin;
            // If you have several Zones, you might find the &= operator or other logic operators useful:
            // bool allMatch = true;
            // allMatch &= zoneToxin == modOther.zoneToxin;
            // allMatch &= ZoneModel == modOther.ZoneModel;
            // return allMatch;
            // Here is an NMIP just using && chained together in one statemeny 
            // return zoneToxin == modOther.zoneToxin && ZoneModel == modOther.ZoneModel;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            NMIPPlayer modOther = other.GetModPlayer<NMIPPlayer>();
            modOther.zoneToxin = zoneToxin;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = zoneToxin;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            zoneToxin = flags[0];
        }
        
        public override Texture2D GetMapBackgroundImage()
        {
            if (zoneToxin)
            {
                return mod.GetTexture("NMIP:ToxinSky");
            }
            return null;
        }

        public override void clientClone(ModPlayer clientClone)
        {
            NMIPPlayer clone = clientClone as NMIPPlayer;
            // Here we would make a backup clone of values that are only correct on the local players Player instance.
            // Some Examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
            // clone.someLocalVariable = someLocalVariable;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)player.whoAmI);
            packet.Send(toWho, fromWho);
        }

        public override void UpdateDead()
        {
            badHeal = false;
        }
        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            score = reader.ReadInt32();
        }
        public override void UpdateBadLifeRegen()
        {
            if (healHurt > 0)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 120 * healHurt;
            }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
            ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (constantDamage > 0 || percentDamage > 0f)
            {
                int damageFromPercent = (int)(player.statLifeMax2 * percentDamage);
                damage = Math.Max(constantDamage, damageFromPercent);
                customDamage = true;
            }
            else if (defenseEffect >= 0f)
            {
                if (Main.expertMode)
                {
                    defenseEffect *= 1.5f;
                }
                damage -= (int)(player.statDefense * defenseEffect);
                if (damage < 0)
                {
                    damage = 1;
                }
                customDamage = true;
            }
            constantDamage = 0;
            percentDamage = 0f;
            defenseEffect = -1f;
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (toxicExtract && Main.rand.Next(2) == 1 && item.melee)
                target.AddBuff(BuffID.Venom, 240);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (cultistFlame && proj.magic)
                target.AddBuff(mod.BuffType("CultistFlame"), 300);
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (graniteCore == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Random upward vector.
                    Vector2 vel = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-4, -3));
                    Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("GraniteShard"), 40, 2, player.whoAmI, 0, 1);
                }
                Main.PlaySound(SoundID.Shatter, player.position);
            }

            if (honeyVeil == true)
            {
                float x = player.position.X + Main.rand.Next(0, 0);
                float y = player.position.Y - Main.rand.Next(0, 0);
                Vector2 vector = new Vector2(x, y);
                Vector2 vel = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-4, -3));
                Vector2 starvel = new Vector2(Main.rand.NextFloat(16, 12), Main.rand.NextFloat(20, 16));
                Projectile.NewProjectile(vector, starvel, ProjectileID.HallowStar, 4, player.whoAmI, 0, 1);
                Projectile.NewProjectile(player.Center, vel, ProjectileID.Bee, 40, 4, player.whoAmI, 0, 1);
                player.AddBuff(BuffID.Panic, 300);
                player.immuneTime += 10;
            }
        }

        public override void PostUpdateBuffs()
        {
            if (aresEffect == false)
            {
                player.ClearBuff(113);
                player.ClearBuff(114);
                player.ClearBuff(115);
                player.ClearBuff(117);
            }
        }
    }
}
