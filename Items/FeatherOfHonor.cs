using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class FeatherOfHonor : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The feather strikes with vivid flame. It is not weak, nor faint.\n" +
                               "The flame pierces the body and melts the frost of the heart.");

        }

        public int FeatherShoot = 0;

		public override void SetDefaults() 
		{
			item.damage = 24;
			item.summon = true;
            item.mana = 6;
			item.width = 40;
			item.height = 40;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
			item.knockBack = 2.4f;
			item.value = 10000;
			item.rare = 1;
			//item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("FeatherOfHonor");
            item.shootSpeed = 1f;
            item.noUseGraphic = true;
            item.channel = true;
            FeatherShoot = 0;
		}

        public override void HoldItem(Player player)
        {
            if (FeatherShoot > 0)
                FeatherShoot--;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            /*damage = (int)(damage * 0.25f);
            for (int i = -2; i < 3; i++)
            {
                float rot = MathHelper.ToRadians(270 + 25 * i);
                Vector2 vel = new Vector2(item.shoot, 0).RotatedBy(rot);
                float delay = 0;
                switch (i)
                {
                    case -2: delay += 15; break;
                    case  0: delay += 10; break;
                    case  1: delay += 5; break;
                    case  2: delay += 20; break;
                }
                Projectile.NewProjectile(position, vel, type, damage, knockBack, player.whoAmI, delay);
            }*/

            for (int i = 0; i < 5; i++)
            {
                int order = i;
                if (i >= 2)
                    order++;
                if (i == 4)
                    order = 2;
                Projectile.NewProjectile(position, Vector2.Zero, type, damage, knockBack, player.whoAmI, order);
            }

            return false;
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Feather, 5);
            recipe.AddIngredient(ItemID.InfernoPotion, 2);
            recipe.AddIngredient(ItemID.Fireblossom, 10);
            recipe.AddTile(TileID.ImbuingStation);
            recipe.SetResult(this);
        }
	}
}