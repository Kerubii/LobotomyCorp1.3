using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class BearPaw : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Its adorable appearance makes it something that might even appeal to a child as a gift.\n" +
                               "Do not underestimate the weaponÅfs power because of its fluffy exterior.");
        }

        public override void SetDefaults() {
			item.CloneDefaults(ItemID.CopperShortsword);
			item.damage = 24;
            item.useAnimation = 12;
            item.useTime = 12;
            item.autoReuse = true;
		}
        /*
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.immune = true;
            player.immuneTime = 15;
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            player.immune = true;
            player.immuneTime = 15;
        }
        */
        public override void AddRecipes() {

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 30);
            recipe.AddIngredient(ItemID.Cobweb, 20);
            recipe.AddIngredient(ItemID.BrownDye, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
	}
}