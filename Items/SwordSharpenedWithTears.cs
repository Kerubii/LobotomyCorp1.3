using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class SwordSharpenedWithTears : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A sword suitable for swift thrusts.\n" +
                               "Even someone unskilled in dueling can rapidly puncture an enemy using this E.G.O with remarkable agility.");
		}

		public override void SetDefaults() 
		{
			item.damage = 36;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 16;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
            item.shootSpeed = 2.4f;
            item.shoot = mod.ProjectileType("SwordSharpenedWithTearsProj");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY);
            speed = speed.RotatedByRandom((float)MathHelper.ToRadians(player.altFunctionUse == 2 ? 30 : 15));
            speedX = speed.X;
            speedY = speed.Y;
            if (player.altFunctionUse == 2)
                damage = (int)(damage * 0.75f);
            return true;//base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.knockBack = 6;
                item.useTime = 7;
                item.reuseDelay = 30;
            }
            else
            {
                item.knockBack = 2.4f;
                item.useTime = 16;
                item.reuseDelay = 0;
            }

            return base.CanUseItem(player);
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Swordfish);
            recipe.AddIngredient(ItemID.Sapphire);
            recipe.AddIngredient(ItemID.Star, 8);
            recipe.AddTile(mod, "BlackBox3");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}