using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LobotomyCorp.Items
{
	public class GreenStemS : ModItem
	{
        public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("\"The day a ripe apple fell off the tree in the garden where the princess and the king stood, the witch's heart shattered.\"\n" +
                               "Enroach your surroundings with vines, slowing down nearby projectiles once\n" +
                               "Your vines automatically attack anything in it\n" +
                               "The user's malice makes it take double damage while its vines are spread\n");
		}

        public override void SetDefaults()
        {
            item.damage = 34;
            item.magic = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 10;
            item.useAnimation = 10;

            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 0;
            item.value = 10000;
            item.rare = 2;
            item.shootSpeed = 1f;
            item.shoot = mod.ProjectileType("GreenStemArea");
            
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.autoReuse = true;
            item.channel = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.ownedProjectileCounts[type] == 0;
        }

        public override void AddRecipes() 
		{
		}
	}

}