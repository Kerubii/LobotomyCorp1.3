using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class BoundaryOfDeath : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Shi Association South Section 2 Sword (Yujin)");
			Tooltip.SetDefault("Sets your health to 1/4 when above that threshold.\n" +
							   "4% chance to increase weapon damage by 4444% and do a special attack");
		}

		public override void SetDefaults() 
		{
			item.damage = 44;
			item.melee = true;
			item.width = 44;
			item.height = 44;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 1;
			item.knockBack = 4.4f;
			item.value = 44444;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("BoundaryOfDeathInitial");
			item.shootSpeed = 8f;
			item.noUseGraphic = true;
		}

		public override void AddRecipes() 
		{
		}
	}
}