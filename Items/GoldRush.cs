using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class GoldRush : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The weapon she used to brandish in her prime, before the greed solidified and became what it is now.\n" +
							   "One can release their primal desires and strike enemies with full force; technical skill is unneeded.");

        }

		public override void SetDefaults() 
		{
			item.damage = 50;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 5;
			item.useAnimation = 16;
			item.reuseDelay = 18;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
            item.shootSpeed = 11f;
            item.shoot = mod.ProjectileType("GoldRushPunches");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
            {
				item.useTime = 28;
				item.useAnimation = 28;
				item.shootSpeed = 4f;
				item.shoot = mod.ProjectileType("GoldRushPunch");
			}
			else
            {
				item.useTime = 5;
				item.useAnimation = 16;
				item.shootSpeed = 7f;
				item.shoot = mod.ProjectileType("GoldRushPunches");
			}
			return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == mod.ProjectileType("GoldRushPunch"))
			{
				damage *= 4;
				knockBack *= 9;
			}
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FeralClaws);
			recipe.AddIngredient(ItemID.Amber);
			recipe.AddIngredient(ItemID.GoldBar, 20);
			recipe.AddIngredient(ItemID.FallenStar, 8);			
			recipe.AddTile(mod, "BlackBox3");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}