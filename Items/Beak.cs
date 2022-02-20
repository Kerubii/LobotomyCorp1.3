using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Beak : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("As if to prove that size doesn't matter when it comes to force,\n" +
                               "The weapon has high firepower despite its small size.");

        }

		public override void SetDefaults()
		{
			item.damage = 12;
			item.knockBack = 4;
			item.ranged = true;
			item.width = 40;
			item.height = 16;

			item.useTime = 16;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.noMelee = true;
			item.value = 7500;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;

			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddIngredient(ItemID.Feather, 5);
            recipe.AddTile(mod, "BlackBox");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
