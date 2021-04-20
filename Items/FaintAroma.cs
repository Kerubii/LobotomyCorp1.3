using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class FaintAroma : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Hair has grown on the crossbow as if to express that the woman's dejection will never be forgotten.\n" +
                               "The sound of the projectile splitting the air is reminiscent of her piercing scream.");
        }

		public override void SetDefaults() {
			item.damage = 6;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Arrow;
            item.scale = 0.8f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}
