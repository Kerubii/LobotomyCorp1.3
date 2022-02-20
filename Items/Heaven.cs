using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Heaven : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Just contain it in your sight.\n" +
                               "As it spreads its wings for an old god, a heaven just for you burrows its way.") ;

        }

		public override void SetDefaults() 
		{
			item.damage = 16;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 20;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
            item.shootSpeed = 3.7f;
            item.shoot = mod.ProjectileType("Heaven");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Javelin);
			recipe.AddIngredient(ItemID.TheRottedFork);
			recipe.AddIngredient(ItemID.CrimtaneBar, 5);
			recipe.AddTile(mod, "BlackBox2");
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Javelin);
			recipe.AddIngredient(ItemID.BallOHurt);
			recipe.AddIngredient(ItemID.DemoniteBar, 5);
			recipe.AddTile(mod, "BlackBox2");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}