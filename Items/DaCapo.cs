using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class DaCapo : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A scythe that swings silently and with discipline like a conductor's gestures and baton.\n" +
                               "If there were a score for this song, it would be one that sings of the apocalypse.");
		}

		public override void SetDefaults() 
		{
			item.damage = 64;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shootSpeed = 1f;
            item.scale = 0.8f;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 48;
                item.useAnimation = 48;
                item.useStyle = 5;
                item.shoot = mod.ProjectileType("DaCapo");
                item.noUseGraphic = true;
                item.noMelee = true;
            }
            else
            {
                item.useTime = 26;
                item.useAnimation = 26;
                item.useStyle = 1;
                item.shoot = 0;
                item.noUseGraphic = false;
                item.noMelee = false;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DarkShard);
            recipe.AddIngredient(ItemID.LightShard);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(mod, "BlackBox3");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}