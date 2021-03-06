using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Smile : LobCorpHeavy
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("It has the pale faces of nameless employees and a giant mouth on it.\n" +
                               "Upon striking with the weapon, the monstrous mouth opens wide to devour the target, its hunger insatiable.");

        }

		public override void SetDefaults() 
		{
			item.damage = 72;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 24;
            item.useAnimation = 24;
			item.useStyle = 1;

			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Red;

			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shootSpeed = 1f;
			item.noUseGraphic = false;
			item.noMelee = false;
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override bool SafeCanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.shoot = mod.ProjectileType("SmileSpecial");
				item.noUseGraphic = true;
				item.noMelee = true;
			}
			else
			{
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.shoot = 0;
				item.noUseGraphic = false;
				item.noMelee = false;
			}

			return base.SafeCanUseItem(player);
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Pwnhammer);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddTile(mod, "BlackBox3");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}