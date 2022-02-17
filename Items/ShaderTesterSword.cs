using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class ShaderTesterSword : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A complete E.G.O.\n" +
                               "\"All that remains is the hollow pride of a weathered knight.\"\n");
            PassiveText = "Permanent Sword - A fucking sword is in front of you\n" +
                          "|You dont see it but its there im serious winky face";
            EgoColor = LobotomyCorp.WawRarity;
		}

		public override void SetDefaults() 
		{
			item.damage = 63;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<Projectiles.ShaderTester>();
			item.shootSpeed = 1;
		}

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[item.shoot] <= 0)
            {
				Projectile.NewProjectile(player.Center, Vector2.Zero, item.shoot, 0, 0, player.whoAmI);
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes() 
		{
		}
	}
}