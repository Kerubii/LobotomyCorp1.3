using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Regret : LobCorpHeavy
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Enemies crushed by this regret can never return to their normal life.\n" +
                               "Before swinging this weapon, expressing one's condolences for the demise of the inmate who couldn't even have a funeral would be nice.");

        }

		public override void SetDefaults() 
		{
			item.damage = 72;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 24;
            item.useAnimation = 24;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
		}
	}
}