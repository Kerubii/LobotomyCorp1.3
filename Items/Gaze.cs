using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Gaze : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The gaze from the keyhole is fixed on its target without ever stopping.\n" +
                               "No one knows what it wanted to peep at so dearly.\n" + 
                               "Can cause additional damage over time");

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
            item.shootSpeed = 2.2f;
            item.shoot = mod.ProjectileType("Gaze");

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
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddTile(mod, "BlackBox2");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}