using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Adoration : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A big mug filled with mysterious slime that never runs out.\n" +
                               "ItÅ's the byproduct of some horrid experiment in a certain laboratory that eventually failed.");

        }

		public override void SetDefaults() {
			item.damage = 42;
			item.magic = true;
            item.mana = 6;
			item.width = 40;
			item.height = 16;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("MeltyLove");
            item.shootSpeed = 7.6f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 300);
            recipe.AddIngredient(ItemID.PinkGel, 20);
            recipe.AddIngredient(ItemID.AdamantiteBar, 10);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 300);
            recipe.AddIngredient(ItemID.PinkGel, 20);
            recipe.AddIngredient(ItemID.TitaniumBar, 10);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
        }
    }
}
