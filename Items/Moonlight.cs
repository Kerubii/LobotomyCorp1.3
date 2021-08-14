using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Moonlight : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The snakeÅfs open mouth represents the endless yearning for music.\n" +
                               "It temporarily invites the user to the world of trance.\n" +
                               "25% chance to apply Black Shields to nearby allies when used");
        }

		public override void SetDefaults() 
		{
			item.damage = 25;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(4) == 0)
                LobotomyModPlayer.ModPlayer(player).ApplyShield("B", 900, item.damage * 2);
            foreach (Player teammate in Main.player)
            {
                if (teammate.active && teammate.whoAmI != player.whoAmI && !teammate.dead && teammate.team == player.team && Vector2.Distance(teammate.Center, player.Center) < 1600)
                {
                    LobotomyModPlayer.ModPlayer(teammate).ApplyShield("B", 900, item.damage * 2);
                }
            }
        }

        public override void AddRecipes() 
		{
		}
	}
}