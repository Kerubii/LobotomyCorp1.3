using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Tough : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A glock reminiscent of a certain detective who fought evil for 25 years, losing hair as time went by.\n" +
                               "Only those who maintain a clean ÅghairstyleÅh with no impurities on their head will be deemed worthy of equipping this weapon.");
        }

		public override void SetDefaults() {
			item.damage = 16;
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

        public override bool CanUseItem(Player player)
        {
            return player.hair == 14 || player.hair == 75;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.StoneSlab, 10);
            recipe.AddIngredient(ItemID.Lens, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
    }
}
