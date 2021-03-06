using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Diffraction : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("To see this E.G.O, one must thoroughly concentrate.\n" +
                               "You can ignore the ridiculous advice that you can see it with your mind.");
        }

		public override void SetDefaults() 
		{
			item.damage = 6;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 5000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TheBreaker);
			recipe.AddIngredient(ItemID.InvisibilityPotion, 4);
			recipe.AddTile(mod, "BlackBox3");
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FleshGrinder);
			recipe.AddIngredient(ItemID.InvisibilityPotion, 4);
			recipe.AddTile(mod, "BlackBox3");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}