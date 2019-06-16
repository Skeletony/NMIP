using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NMIP.Items.ODIN
{
    public class EldritchSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jupiter's Wrath");
            Tooltip.SetDefault("A long lost sword of the god, Jupiter. Use it with caution.");  //The (English) text shown below your weapon's name
        }

        public override void SetDefaults()
        {
            item.damage = 75;           //The damage of your weapon
            item.melee = true;          //Is your weapon a melee weapon?
            item.width = 34;            //Weapon's texture's width
            item.height = 34;           //Weapon's texture's height
            item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 20;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(gold: 11);           //The value of the weapon
            item.rare = 7;              //The rarity of the weapon, from -1 to 13
            item.UseSound = (SoundID.DD2_BetsysWrathImpact);     //The sound when the weapon is using
            item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            item.useTurn = true;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("JupitersRay");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 60);
        }
    }
}
