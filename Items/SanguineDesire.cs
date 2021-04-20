using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class SanguineDesire : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The axe may seem light, but the wielder musn't forget that it has hurt countless people as a consequence of poor choices.\n" +
                               "The weapon is stronger when used by an employee with strong conviction.");
		}

		public override void SetDefaults() 
		{
			item.damage = 13;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.scale = 0.7f;
		}

		public override void AddRecipes() 
		{
		}
	}
}