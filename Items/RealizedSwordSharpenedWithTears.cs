using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class RealizedSwordSharpenedWithTears : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A complete E.G.O.\n" +
                               "\"All that remains is the hollow pride of a weathered knight.\"\n");
            PassiveText = "Sharpened with Tears - Always deals a flat amount of damage and ignores all defense\n" +
                          "Blessing - When a teammate stands near you while synchronized with this weapon, they gain Blessed buff\n" +
                          "|Despair - The swords pierce back after missing a target\n" +
                          "A Real Magical Girl? - Gain a buff while a Blessed teammate is alive, gain a debuff when the Blessed dies";
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
		}

        public override bool SafeCanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("RealizedSwordSharpenedWithTearsProj")] == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("RealizedSwordSharpenedWithTearsProj"), item.damage, item.knockBack, player.whoAmI, i);
                }
            }
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 40;
                item.useTime = 52;
            }
            else
            {
                item.useTime = 26;
                item.useAnimation = 20;
            }

            return base.SafeCanUseItem(player);
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