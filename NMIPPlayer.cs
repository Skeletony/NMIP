using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

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
        public bool MoltenEffect = false;
        public bool GodMoltenEffect = false;
        public bool AresEffect = false;

        public override void ResetEffects()
        {
            constantDamage = 0;
            percentDamage = 0f;
            defenseEffect = -1f;
            badHeal = false;
            healHurt = 0;
            friendPet = false;
            MoltenEffect = false;
            GodMoltenEffect = false;
            AresEffect = false;
        }

        public override void clientClone(ModPlayer clientClone)
        {
            NMIPPlayer clone = clientClone as NMIPPlayer;
            // Here we would make a backup clone of values that are only correct on the local players Player instance.
            // Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
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

        public override void PostUpdateBuffs()
        {
            if (AresEffect == false)
            {
                player.ClearBuff(113);
                player.ClearBuff(114);
                player.ClearBuff(115);
                player.ClearBuff(117);
            }
        }
    }
}
