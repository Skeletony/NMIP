using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.NPCs.Bosses.Starboss
{
    [AutoloadBossHead]
    public class Starboss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Boss");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 118;
            npc.height = 88;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.damage = 125;
            npc.defense = 10;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.netAlways = true;
            npc.friendly = false;
            npc.lifeMax = 16000;
            npc.value = Item.sellPrice(0, 15, 0, 0);
            npc.behindTiles = true;
            npc.knockBackResist = 0f;
            bossBag = mod.ItemType("StarbossBag");
            npc.npcSlots = 200;
        }
    }
}
