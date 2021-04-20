using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Solitude : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A strong sense of loneliness still lingers, even in the form of an E.G.O.\n" +
                               "Its bullets create a void that cannot be filled on the victim's soul.\n" +
                               "It was a rusty weapon from the beginning.");
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
			item.shoot = 10;
			item.shootSpeed = 14f;
			item.useAmmo = AmmoID.Bullet;
            item.scale = 0.8f;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}
