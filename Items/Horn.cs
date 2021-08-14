using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Horn : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The green - eyed beauty's favorite flower was the dahlia; \"Your love makes me happy.\"\n" +
                               "The lady's happiness came to an end with the budding of those unsightly horns.\n" +
                               "The dahlia's unfulfilled meaning was borne as a seed in this E.G.O, carrying a lingering emotion.");

        }

		public override void SetDefaults() 
		{
			item.damage = 24;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 20;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
            item.shootSpeed = 3.7f;
            item.shoot = mod.ProjectileType("Horn");

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
            recipe.AddIngredient(ItemID.JungleRose);
            recipe.AddIngredient(ItemID.Lens, 5);
            recipe.AddIngredient(ItemID.JungleSpores, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
		}
	}
}