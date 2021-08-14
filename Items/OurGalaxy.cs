using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class OurGalaxy : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("\"There's a universe inside a pebble.\n" + 
                               "When the child cries, the stars in the galaxy light the void.\n" +
                               "In your universe, am I to be found?\"");
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
            item.shoot = mod.ProjectileType("OurGalaxySparkle");
            item.shootSpeed = 10f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Star, 10);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
	}
}