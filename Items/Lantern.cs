using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Lantern : LobCorpHeavy
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The luminous organ shines brilliantly, making it useful for lighting up the dark.\n" +
                               "Though teeth sticking out of some spots of the equipment is a rather frightening sight.");
        }

		public override void SetDefaults() 
		{
			item.damage = 62;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 24;
            item.useAnimation = 24;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ItemID.Topaz, 4);  
            recipe.AddIngredient(ItemID.AntlionMandible, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}