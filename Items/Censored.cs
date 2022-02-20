using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Censored : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("[CENSORED]"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("[CENSORED] has the ability to [CENSORED], but this is a horrendous sight for those watching.\n" +
							   "Looking at the E.G.O for more than 3 seconds will make you sick.\n" +
							   "Heal 40% damage taken");
		}

		public override void SetDefaults() 
		{
			item.damage = 42;
			item.knockBack = 6;
			item.scale = 1.3f;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 5;

			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.NPCHit18;
			item.autoReuse = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.shoot = mod.ProjectileType("CensoredGrab");
			item.shootSpeed = 1f;
		}
		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			if (player.altFunctionUse == 2)
            {
				item.shoot = mod.ProjectileType("CensoredSpike");
				item.useTime = 56;
				item.useAnimation = 56;
			}
			else
            {
				item.shoot = mod.ProjectileType("CensoredGrab");
				item.useTime = 22;
				item.useAnimation = 22;
			}

			return player.ownedProjectileCounts[item.shoot] < 1;
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override void AddRecipes() 
		{
		}
	}
}