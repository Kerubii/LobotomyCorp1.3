using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class ParadiseLost : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Behold: you stood at the door and knocked, and it was opened to you.\n" +
                               "I come from the end, and I am here to stay for but a moment.\n" +
                               "At the same time, I am the one who kindled the light to face the world.\n" +
                               "My loved ones, who now eagerly desire the greater gifts; I will show you the most excellent way.");

        }

		public override void SetDefaults() 
		{
			item.damage = 66;
			item.magic = true;
            item.mana = 4;
			item.width = 40;
			item.height = 40;
			item.useTime = 34;
			item.useAnimation = 34;
			item.useStyle = ItemUseStyleID.HoldingUp;
            item.noMelee = true;
			item.knockBack = 0.8f;
			item.value = 10000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("ParadiseLostBase");
            item.shootSpeed = 14f;
            item.noUseGraphic = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            /*damage = (int)(damage * 0.6f);
            for (int i = 0; i < 3; i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI);
            }*/
            position = Main.MouseWorld;
            int x = (int)(position.X / 16), y = (int)(position.Y / 16);
            int add = Main.rand.Next(3);
            int dir = Main.rand.Next(2) == 0 ? -1: 1;
            for (int i = -1; i < 2; i++)
            {
                if (Main.tile[x + 2 * i, y].active() && Main.tileSolid[Main.tile[x + 2 * i, y].type] && Main.tileSolidTop[Main.tile[x + 2 * i, y].type])
                {
                    continue;
                }
                for (int j = 0; j < 20; j++)
                {
                    Tile tile = Main.tile[x + (2 * i), y + j];
                    if (tile.active() && Main.tileSolid[tile.type])
                    {
                        position = new Vector2(Main.MouseWorld.X + (32 * i) + Main.rand.Next(-8, 9), (y + j) * 16 - 8);
                        Vector2 speed = Main.MouseWorld - position;

                        Projectile.NewProjectile(position, speed, item.shoot, damage, knockBack, player.whoAmI, (1 + i + add) * dir);                        
                        break;
                    }
                }
            }
            return false;
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Acorn, 3);
            recipe.AddIngredient(ItemID.DynastyWood, 20);
            recipe.AddIngredient(ItemID.SharkToothNecklace);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
	}
}