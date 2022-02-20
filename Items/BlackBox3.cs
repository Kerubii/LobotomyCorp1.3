using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class BlackBox3 : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Black Box - Extractor");
			Tooltip.SetDefault("A Tool to extract from the subconsciousness of mankind\n" +
							   "Allows the creation of WAW and ALEPH E.G.O");
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
			item.createTile = ModContent.TileType<Tiles.BlackBox3>();
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "BlackBox2");
			recipe.AddIngredient(ItemID.HellstoneBar, 4);
			recipe.AddIngredient(ItemID.JungleSpores, 8);
			recipe.AddIngredient(ItemID.Bone, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "BlackBox2");
			recipe.AddIngredient(ItemID.HellstoneBar, 4);
			recipe.AddIngredient(ItemID.JungleSpores, 8);
			recipe.AddIngredient(ItemID.BeeWax, 4);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}