using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Discord : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A falchion edge shadowed by the world's discord.\n" +
							   "Every life is trapped in the cycle of reincarnation.");

        }

		public override void SetDefaults() 
		{
			item.damage = 28;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.scale = 1.2f;	

			item.useTime = 62;
			item.useAnimation = 58;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
            item.shootSpeed = 10.2f;
            item.shoot = mod.ProjectileType("Discord2");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == mod.ProjectileType("DiscordSlash"))
				damage *= 3;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
			if (player.altFunctionUse == 2)
            {
				//item.useStyle = ItemUseStyleID.HoldingOut;
				item.shootSpeed = 10.2f;
				item.shoot = mod.ProjectileType("Discord2");
				item.useTime = 62;
				item.useAnimation = 58;
				//item.noUseGraphic = true;
				//item.noMelee = true;
			}
			else
            {
				//item.useStyle = ItemUseStyleID.SwingThrow;
				item.shootSpeed = 1f;
				item.shoot = mod.ProjectileType("DiscordSlash");
				item.useTime = 26;
				item.useAnimation = 26;
				//item.noUseGraphic = false;
				//item.noMelee = false;
			}
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DarkLance);
			recipe.AddIngredient(ItemID.BlackDye);
			recipe.AddIngredient(ItemID.EbonsandBlock, 100);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
		}
	}
}