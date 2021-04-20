using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class LifeForADaredevil : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("An ancient sword.\n" +
                               "Just as its archetype desired, it will be useless in the hands of the frightened");
        }

        public override void SetDefaults() {
			item.CloneDefaults(ItemID.Katana);
			item.damage = 24;
            item.melee = false;
		}

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            damage += target.defense / 2;
        }

        public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            damage = (int)(player.statLifeMax2 * (float)item.damage * 0.01f);
        }

        public override void AddRecipes() {
		}
	}
}