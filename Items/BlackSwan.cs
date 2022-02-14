using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class BlackSwan : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Believing that it would turn white,\n" +
							   "The black swan wanted to lift the curse by weaving together nettles.\n" +
							   "All that was left is a worn parasol it once treasured.\n" +
							   "Small chance to reflect damage taken");
		}

		public override void SetDefaults() 
		{
			item.damage = 14;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void HoldItem(Player player)
        {
			LobotomyModPlayer.ModPlayer(player).BlackSwanParryChance += 10;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
        }

        public override void AddRecipes() 
		{

		}
	}
}