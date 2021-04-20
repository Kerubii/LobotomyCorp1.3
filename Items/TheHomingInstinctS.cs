using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class TheHomingInstinctS : SEgoItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("\"Friends, Friends, let us all go home together!\"");
            PassiveText = "Home - Drop a house\n" +
                          "A Road Walked Together - Create temporary yellow brick road that gives buffs to nearby teammates\n" +
                          "|On the Way Home - While a yellow brick road or this weapon is active, the user gains a debuff when not near it";
            EgoColor = LobotomyCorp.HeRarity;
        }

		public override void SetDefaults() {
			item.damage = 9000; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.ranged = true; // sets the damage type to ranged
			item.width = 40; // hitbox width of the item
			item.height = 42; // hitbox height of the item
			item.useTime = 120; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 120; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = 10; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Green; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = mod.ProjectileType("HOUSE"); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 15f; // the speed of the projectile (measured in pixels per frame)
            item.noUseGraphic = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                if (player == Main.LocalPlayer)
                {
                    position.X = Main.MouseWorld.X;
                    position.Y = player.position.Y - 600;
                    speedX = 0;
                    speedY = item.shootSpeed;
                }
            }
            else
            {
                position.Y = player.position.Y + player.height;
                position.X = (int)(position.X / 16) * 16;
                position.Y = (int)(position.Y / 16) * 16;
                float rot = new Vector2(speedX, speedY).ToRotation();
                if (rot < 0)
                    rot += 6.28f;
                if (rot > 6.28f)
                    rot -= 6.28f;
                rot = (float)MathHelper.ToDegrees(rot);
                if ((rot > 360 - 45 || rot < 45) || (rot < 180 + 45 && rot > 180 - 45))
                {
                    speedY = 0;
                    speedX = 4f * Math.Sign(speedX);
                }
                else
                {
                    speedX = (float)Math.Sqrt(8) * Math.Sign(speedX);
                    speedY = (float)Math.Sqrt(8) * Math.Sign(speedY);
                }
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.UseSound = SoundID.Item11;
                item.useAnimation = 120;
                item.useTime = 120;
                item.shoot = mod.ProjectileType("HOUSE");
                item.shootSpeed = 15f;
            }
            else
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/House_MakeRoad").WithVolume(0.1f);
                item.useAnimation = 28;
                item.useTime = 28;
                item.shoot = mod.ProjectileType("HomingInstinct");
                item.shootSpeed = 8f;
            }
            return base.SafeCanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }
    }
}
