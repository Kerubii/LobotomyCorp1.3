using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Soda : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A pistol painted in a refreshing purple.\n" +
                               "Whenever this E.G.O. is used, a faint scent of grapes wafts through the air.");
        }

		public override void SetDefaults() {
			item.damage = 9;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 14f;
			item.useAmmo = AmmoID.Bullet;
            item.scale = 0.8f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.LesserHealingPotion);
            recipe.AddIngredient(ItemID.LesserManaPotion);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
    }
}
