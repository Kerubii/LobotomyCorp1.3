using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Amrita : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A cane used by monks for when they travel away from their temple.\n" +
                               "It is commonly used to measure the depth of a body of water or to drive animals or insects away.");

        }

		public override void SetDefaults() 
		{
			item.damage = 24;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 42;
			item.useAnimation = 40;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 1;
            item.shootSpeed = 4.4f;
            item.shoot = mod.ProjectileType("Amrita");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
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
            
        }
	}
}