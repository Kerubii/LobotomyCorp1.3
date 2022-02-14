using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Justitia : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("It remembers the balance of the Long Bird that never forgot others' sins.\n" +
                               "This weapon may be able to not only cut flesh but trace of sins as well.\n" +
							   "Special attack ignores immunity frames");
		}

		public override void SetDefaults() 
		{
			item.damage = 36;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 80;
			item.useAnimation = 80;
			item.useStyle = 5;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shootSpeed = 1f;
			item.scale = 1.3f;	
		}

        public override bool CanUseItem(Player player)
        {
			if (Main.rand.Next(3) == 0)
            {
				item.useTime = 80;
				item.useAnimation = 80;
				item.useStyle = 5;
				item.noMelee = true;
				item.noUseGraphic = true;
				item.shoot = mod.ProjectileType("JustitiaAlt");
			}
			else
            {
				item.useTime = 22;
				item.useAnimation = 22;
				item.useStyle = 1;
				item.noMelee = false;
				item.noUseGraphic = false;
				item.shoot = 0;
			}
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			damage /= 2;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
			target.immune[player.whoAmI] = 3;
		}

        public override void AddRecipes() 
		{
		}
	}
}