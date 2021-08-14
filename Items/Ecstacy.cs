using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Ecstacy : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The colorful pattern is vivid, similar to a child's plaything.");

        }

		public override void SetDefaults() 
		{
			item.damage = 24;
			item.magic = true;
            item.mana = 8;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 5;
            item.noMelee = true;
			item.knockBack = 2.4f;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item85;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("Candy");
            item.shootSpeed = 8f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.rand.Next(5));
            for (int i = 0; i < 5; i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position, speed * Main.rand.NextFloat(0.3f, 0.7f), type, (int)(damage * 0.2f), knockBack, player.whoAmI, Main.rand.Next(5));
            }
            return false;
        }

        public override void AddRecipes() 
		{
		}
	}
}