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
			item.damage = 6;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
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
