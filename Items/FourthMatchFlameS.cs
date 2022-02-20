using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class FourthMatchFlameS : SEgoItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A Complete E.G.O.\n" +
                               "\"I am coming to you. You, who will be reduced to ash like me.\"");
            PassiveText = "Warmth of a Flame - 400% increased damage on fourth use\n" +
                          "Fourth Match - Alternate attack makes you explode while on fourth use that deals 4000% increased damage\n" +
                          "Matchstick - Inflicts fire that gets stronger the more it is inflicted" +
                          "|Scorching Embers - Sets self on fire every fourth use";
            EgoColor = LobotomyCorp.TethRarity;
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
                if (player.altFunctionUse == 2)
                {
                    LobotomyModPlayer.ModPlayer(player).FourthMatchExplode(true);
                    LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR = 0;
                    return false;
                }

                item.useTime = 30;
                item.useAnimation = 30;
                item.shoot = mod.ProjectileType("FourthMatchFlameGigaSlash");
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage = (damage * 4);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void UseStyle(Player player)
        {
            Vector2 offset = new Vector2(-82, 0).RotatedBy(player.itemRotation - MathHelper.ToRadians(45 * player.direction + (player.direction > 0 ? 180 : 0)));
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Dust dust = Dust.NewDustPerfect(ownerMountedCenter + offset, 6, new Vector2(), 0, new Color(255, 255, 255), 2.8f * (float)(LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR / (float)3));
            dust.noGravity = true;
        }

        /*public override void ModifyWeaponDamage(Player player, ref float add, ref float mult)
        {
            if (LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR > 3)
                add += 5f;
        }*/

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.HasBuff(mod.BuffType("Matchstick")))
                target.buffTime[target.FindBuffIndex(mod.BuffType("Matchstick"))] += 120;
            else
                target.AddBuff(mod.BuffType("Matchstick"), 120);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            if (target.HasBuff(mod.BuffType("Matchstick")))
                target.buffTime[target.FindBuffIndex(mod.BuffType("Matchstick"))] += 120;
            else
                target.AddBuff(mod.BuffType("Matchstick"), 120);
        }

        public override bool AltFunctionUse(Player player)
        {
            return LobotomyModPlayer.ModPlayer(player).FourthMatchFlameR == 3;
        }

        public override void AddRecipes() 
		{
		}
	}
}