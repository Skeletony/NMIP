using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.ODIN
{
    public class skell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame of the Eldritch");
            Tooltip.SetDefault("An ancient weapon used to obliterate enemies in sight. It's one of the weapons made by the master craftswoman, Hel.");
        }
        public override void SetDefaults()
        {
            item.damage = 40;
            item.magic = true;
            item.width = 40;
            item.height = 36;
            item.useTime = 10;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = 8;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SansFlame");
            item.shootSpeed = 15f;
            item.mana = 5;
        }
    }
}