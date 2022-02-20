using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Syrinx : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("What cry could be more powerful than one spurred by primal instinct?\n" +
                               "As if everything else were hollow and pointless,\nThe wailing numbs even the brain, making it impossible to think.");
        }

		public override void SetDefaults() {
			item.damage = 12;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SyrinxShot");
			item.shootSpeed = 14f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}
