using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LobotomyCorp.Items
{
	public class RealizedWingbeat : SEgoItem
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
			Tooltip.SetDefault("\"Everything will be peaceful while you are under the fairies' care.\"\n");
            PassiveText = "Fairies Care - Send a fairy to heal your enemies\n" +
                          "False Care - Hitting an enemy with a fairy increases all damage and lifesteal\n" +
                          "Prepared Meal - After hitting an enemy, auto aim and prioritizes them as your target with this EGO" +
                          "|Predatory Hunger - All your hits heal you, Constantly drains your health";
            EgoColor = LobotomyCorp.ZayinRarity;
        }

        public override void SetDefaults() 
		{
			item.damage = 78;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 22;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = 2;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("RealizedWingbeat");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shootSpeed = 8f;
                item.shoot = mod.ProjectileType("WingbeatFairy");
            }
            else
            {
                item.shootSpeed = 16f;
                item.shoot = mod.ProjectileType("RealizedWingbeat");
            }
            return base.SafeCanUseItem(player);
        }

        public override void HoldItem(Player player)
        {
            player.handoff = (sbyte)mod.GetEquipSlot("Wingbeat_HandsOff", EquipType.HandsOff);
            player.handon = (sbyte)mod.GetEquipSlot("Wingbeat_HandsOn", EquipType.HandsOn);
            player.AddBuff(mod.BuffType("Gluttony"), 180);
        }

        public override void AddRecipes() 
		{
		}
	}

}