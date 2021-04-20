using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class RedEyes : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The Spider Bud had dozens of eyes, and its children were always hungry.\n" +
                               "This tenacity carried over to the E.G.O., demonstrating an outstanding ability to track down targets.");
		}

		public override void SetDefaults() 
		{
			item.damage = 13;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
		}
	}
}