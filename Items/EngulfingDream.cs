using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class EngulfingDream : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("We must be awake at all times.\n" +
                               "Not even sweet dreams in a sound sleep are allowed here; this weapon shall wake those who swim in such illusions.\n" +
                               "And when the crying stops, dawn will break.");
        }

		public override void SetDefaults() {
			item.damage = 9;
			item.magic = true;
            item.mana = 3;
			item.width = 26;
			item.height = 20;
			item.useTime = 9;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EngulfingDreamCall");
			item.shootSpeed = 0.1f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(8, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cloud, 30);
            recipe.AddIngredient(ItemID.Feather, 3);    
            recipe.AddIngredient(ItemID.Star, 8);
            recipe.AddTile(mod, "BlackBox");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
