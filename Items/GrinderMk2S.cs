using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class GrinderMk2S : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A complete E.G.O.\n" +
                               "\"Blood covers the whole floor, screams echo, people are running away...\"");
            PassiveText = "Clean - Invincibility while spinning your blades\n" +
                          "|Charge - Using the weapon decreases battery, Become immobilized after draining your battery";
            EgoColor = LobotomyCorp.HeRarity;
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
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.autoReuse = true;
            item.channel = true;
		}

        public override bool SafeCanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("GrinderMk2Cleaner")] == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("GrinderMk2Cleaner"), item.damage, item.knockBack, player.whoAmI, i);
                }
            }
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 20;
                item.useTime = 20;
                LobotomyModPlayer.ModPlayer(player).GrinderMk2Order = Main.rand.Next(5);
            }
            else
            {
                item.useTime = 26;
                item.useAnimation = 20;
            }

            return !LobotomyModPlayer.ModPlayer(player).GrinderMk2Recharging;
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