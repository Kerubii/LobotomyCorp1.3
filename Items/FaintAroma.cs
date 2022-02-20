using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class FaintAroma : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Even after the E.G.O was extracted, it still carried the fragrance of the archetype.\n" +
							   "Simply carrying it gives the illusion that you're standing in a forest in the middle of nowhere.");
        }

		public override void SetDefaults() {
			item.damage = 6;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Arrow;
            item.scale = 0.8f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenBow);
			recipe.AddIngredient(ItemID.Acorn, 10);
			recipe.AddIngredient(ItemID.FlowerWall, 50);
			recipe.AddTile(mod, "BlackBox3");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
