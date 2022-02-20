using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Lumber : ModItem
    {
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A versatile equipment made to cut down trees and people alike.\n" +
                               "Perhaps sharpening the axe was the one thing it didn't neglect. The blade is always shiny.");
		}

		public override void SetDefaults() 
		{
			item.damage = 26;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronAxe);
			recipe.AddIngredient(ItemID.IronBar, 20);
			recipe.AddIngredient(ItemID.Wood, 50);
			recipe.AddTile(mod, "BlackBox2");
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadAxe);
			recipe.AddIngredient(ItemID.LeadBar, 20);
			recipe.AddIngredient(ItemID.Wood, 50);
			recipe.AddTile(mod, "BlackBox2");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}