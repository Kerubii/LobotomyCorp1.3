using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Christmas : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("It is patched with heavy leather of unknown origin.\n" +
							   "The stitches are carefully woven, but for whom or for what, exactly, is unclear.\n" +
							   "It is not elegant, but you can feel the devotion of its creator.");
        }

		public override void SetDefaults() 
		{
			item.damage = 24;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 5000;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void AddRecipes() 
		{
		}
	}
}