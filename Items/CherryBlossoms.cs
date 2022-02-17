using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class CherryBlossoms : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Petals scatter from the fan like afterimages...\n" +
                               "The spring breeze clad in cherry blossom petals is still cold and painful.");

        }

		public override void SetDefaults() 
		{
			item.damage = 24;
			item.magic = true;
            item.mana = 4;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
            item.noMelee = true;
			item.knockBack = 2.4f;
			item.value = 10000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("CherryBlossomsPetal");
            item.shootSpeed = 14f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage = (int)(damage * 0.6f);
            for (int i = 0; i < 3; i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Main.dust[Terraria.Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("BlossomDust"), 0f, 0f, 0, new Color(255, 255, 255), 1f)];
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