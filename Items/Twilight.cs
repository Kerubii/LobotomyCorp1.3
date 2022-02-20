using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Twilight : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Just like how the ever-watching eyes...\n" +
                               "The scale that could measure any and all sin...\n" +
                               "The beak that could swallow everything protected the peace of the Black Forest...\n" +
                               "The wielder of this armament may also bring peace as they did");
		}

		public override void SetDefaults() 
		{
			item.damage = 47;
			item.scale = 1.3f;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 90;
			item.useAnimation = 90;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shoot = mod.ProjectileType("TwilightSpecial");
			item.shootSpeed = 3f;
			item.noUseGraphic = true;
			item.noMelee = true;
		}

        public override bool AltFunctionUse(Player player)
        {
			return LobotomyModPlayer.ModPlayer(player).TwilightSpecial >= 10;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			damage = (int)(damage * 1.3f);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				LobotomyModPlayer.ModPlayer(player).TwilightSpecial = 0;
				item.useTime = 90;
				item.useAnimation = 90;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.shoot = mod.ProjectileType("TwilightSpecial");
				item.shootSpeed = 4.2f;
				item.noUseGraphic = true;
				item.noMelee = true;
			}
			else
			{
				item.useTime = 26;
				item.useAnimation = 26;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.shoot = 0;
				item.noUseGraphic = false;
				item.noMelee = false;
			}
			return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			if (LobotomyModPlayer.ModPlayer(player).TwilightSpecial < 9)
            {
				LobotomyModPlayer.ModPlayer(player).TwilightSpecial++;
				if (LobotomyModPlayer.ModPlayer(player).TwilightSpecial == 9)
                {
					Main.PlaySound(SoundID.MaxMana, -1, -1, 1, 1f, 0.0f);
					for (int index1 = 0; index1 < 5; ++index1)
					{
						int index2 = Dust.NewDust(player.position, player.width, player.height, 45, 0.0f, 0.0f, (int)byte.MaxValue, new Color(), (float)Main.rand.Next(20, 26) * 0.1f);
						Main.dust[index2].noLight = true;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 0.5f;
					}
					LobotomyModPlayer.ModPlayer(player).TwilightSpecial++;
				}
			}
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "Justitia");
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.DarkShard);
			recipe.AddTile(mod, "BlackBox3");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}