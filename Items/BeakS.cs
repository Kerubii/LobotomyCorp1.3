using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class BeakS : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("People have been committing sins since long ago. \"Why do they commit sins, knowing it is wrong?\"");
            PassiveText = "Fluttering Wings - Right click to reduce the next incoming damage by half and counterattack\n" +
                          "Punishing Beak - Mark enemies and enable punishment mode when getting hit" +
                          "Punishment! - Deal 3 times more damage against marked enemies\n" +
                          "|Misdeeds Not Allowed! - Can only hit marked enemies during punishment mode";
            EgoColor = LobotomyCorp.TethRarity;
		}

		public override void SetDefaults() 
		{
			item.damage = 1;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 1;
			item.knockBack = 0.1f;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return player.itemTime == 0;
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            return LobotomyModPlayer.ModPlayer(player).BeakPunish > 0 || LobotomyGlobalNPC.LNPC(target).BeakTarget > 0;
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                LobotomyModPlayer.ModPlayer(player).BeakParry = 30;
                item.useAnimation = 30;
                item.useTime = 45;
            }
            else
            {
                item.useAnimation = 8;
                item.useTime = 8;
            }

            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (LobotomyModPlayer.ModPlayer(player).BeakPunish > 0)
                add += 82f;
        }

        public override void AddRecipes() 
		{
		}
	}
}