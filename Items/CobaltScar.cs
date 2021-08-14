using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class CobaltScar : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The weapon resembles the claws of a vicious wolf.\n" +
                               "Once upon a time, these claws would cut open the bellies of numerous creatures and tear apart their guts.");
        }

        public override void SetDefaults() {
			item.CloneDefaults(ItemID.FetidBaghnakhs);
			item.damage = 32;
            item.scale = 0.60f;
		}

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (player.statLife <= player.statLifeMax / 2)
            {
                add += 0.5f;    
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(10) == 0)
                player.AddBuff(mod.BuffType("WillBeBad"), 120);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            if (Main.rand.Next(10) == 0)
                player.AddBuff(mod.BuffType("WillBeBad"), 120);
        }

        public override void AddRecipes() {
		}
	}
}