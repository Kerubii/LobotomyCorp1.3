using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class WristCutter : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Its sharp blade can make a clean cut through bone like a hot knife through butter,\n" +
                               "Leaving a wound that will never heal.");
        }

        public override void SetDefaults() {
			item.CloneDefaults(ItemID.CopperShortsword);
			item.damage = 32;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Scars"), 60);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Scars"), 60);
        }

        public override void AddRecipes() {
		}
	}
}