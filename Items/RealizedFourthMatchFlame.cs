using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class RealizedFourthMatchFlame : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A Complete E.G.O.\n" +
                               "\"I am coming to you. You, who will be reduced to ash like me.\"");
            PassiveText = "Warmth of a Flame - 400% increased damage on fourth use\n" +
                          "|Scorching Embers - Inflicts Onfire on target, also inflicts on user every fourth use\n" +
                          "Burning Regret - Has a very low chance to explode on every fourth use when either on full or less than 25% health";
		}

		public override void SetDefaults() 
		{
			item.damage = 41;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("FourthMatchFlameSlash");
            item.shootSpeed = 1f;
            item.noUseGraphic = true;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override bool SafeCanUseItem(Player player)
        {
            if (LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR < 3)
            {
                item.useTime = 24;
                item.useAnimation = 24;
                item.shoot = 0;
                item.noUseGraphic = false;
                item.noMelee = false;
                item.autoReuse = true;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MatchGirl_NoBoom").WithVolume(0.25f);
                LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR++;
            }
            else
            {
                item.useTime = 30;
                item.useAnimation = 30;
                item.shoot = mod.ProjectileType("FourthMatchFlameSlash");
                item.noUseGraphic = true;
                item.noMelee = true;
                item.autoReuse = true;
                item.UseSound = SoundID.Item1;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MatchGirl_Atk").WithVolume(0.25f);
                LobotomyModPlayer.ModPlayer(player).FourthMatchExplode();
                LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR = 0;
            }
            return true;
        }

        public override void UseStyle(Player player)
        {
            Vector2 offset = new Vector2(-82, 0).RotatedBy(player.itemRotation - MathHelper.ToRadians(45 * player.direction + (player.direction > 0 ? 180 : 0)));
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Dust dust = Dust.NewDustPerfect(ownerMountedCenter + offset, 6, new Vector2(), 0, new Color(255, 255, 255), 2.8f * (float)(LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR / (float)3));
            dust.noGravity = true;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult)
        {
            if (LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR == 3)
                add += 5f;
        }

        /*public override bool AltFunctionUse(Player player)
        {
            return FourthMatchFlame > 3;
        }*/

        public override void AddRecipes() 
		{
		}
	}
}