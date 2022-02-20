using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class BlackBox2 : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Black Box - Container");
			Tooltip.SetDefault("Contains an abnormal energy\n" +
							   "Allows the creation of HE E.G.O");
		}

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Red;
			item.value = Item.buyPrice(0, 20, 0, 0);
			item.createTile = ModContent.TileType<Tiles.BlackBox2>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "BlackBox");
			recipe.AddIngredient(ItemID.ShadowScale, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "BlackBox");
			recipe.AddIngredient(ItemID.TissueSample, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}