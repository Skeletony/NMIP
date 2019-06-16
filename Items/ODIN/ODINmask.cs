using Terraria.ModLoader;

namespace NMIP.Items.ODIN
{
    [AutoloadEquip(EquipType.Head)]
    public class ODINmask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.rare = 1;
            item.vanity = true;
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}