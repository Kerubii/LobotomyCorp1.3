using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LobotomyCorp.Items
{
	public class SerumW : ModItem
	{
        public override bool Autoload(ref string name)
        {
            mod.AddEquipTexture(null, EquipType.HandsOff, "Wingbeat_HandsOff", "LobotomyCorp/Items/Wingbeat_Hands");
            mod.AddEquipTexture(null, EquipType.HandsOff, "Wingbeat_HandsOn", "LobotomyCorp/Items/Wingbeat_Hands");
            return base.Autoload(ref name);
        }

        public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("W Singularity");
        }

        public override void SetDefaults() 
		{
			item.damage = 16;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 22;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = 2;
            item.shootSpeed = 1f;
            item.shoot = mod.ProjectileType("SerumW");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override void AddRecipes() 
		{
		}
	}

}