using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Hypocrisy : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("The tree turned out to be riddled with hypocrisy and deception;\n" +
                               "Those who wear its blessing act in the name of bravery and faith.\n" +
							   "However, be warned that nature does not know the difference between a blessing and a curse.");
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
