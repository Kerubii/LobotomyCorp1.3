using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class SoundOfAStar : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The star shines brighter as our despair gathers.\n" +
                               "The weapon's small, evocative sphere fires a warm ray.\n" +
                               "In the light, everything is equal.");

        }

		public override void SetDefaults() 
		{
			item.damage = 42;
			item.summon = true;
            item.mana = 6;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 30;
            item.reuseDelay = 10; 
			item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
			item.knockBack = 2.4f;
			item.value = 5000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item9;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("SoundOfAStar");
            item.shootSpeed = 4f;
            item.noUseGraphic = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage = (int)(damage * 0.6f);

            Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)) * Main.rand.NextFloat(0.8f, 1f);
            Projectile.NewProjectile(position, -speed, type, damage, knockBack, player.whoAmI);

            return false;
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Star, 100);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddIngredient(ItemID.SapphireGemsparkBlock, 25);
            recipe.AddTile(mod, "BlackBox3");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}