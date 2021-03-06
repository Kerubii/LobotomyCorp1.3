using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class GrinderMk4 : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The sharp sawtooth of the grinder makes a clean cut through its enemy.\n" +
                               "Its operation is simple and straightforward, but that doesn't necessarily make it easy to wield.");

        }

		public override void SetDefaults() 
		{
			item.damage = 14;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 42;
			item.useAnimation = 40;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
            item.shootSpeed = 1.8f;
            item.shoot = mod.ProjectileType("GrinderMk4");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item23;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            //On hit, 10% chance to increase sp by 40% for 30 seconds
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodYoyo);
            recipe.AddIngredient(ItemID.Lens, 5);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddTile(mod, "BlackBox2");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}