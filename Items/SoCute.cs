using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class SoCute : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("SO CUTE!!!");
            Tooltip.SetDefault("Beware that the beast inside you may awaken if you use this weapon too much...\n" +
                               "Oh but the soft jelly-like pawbs feel vewwy nice to touch.");
        }

        public override void SetDefaults() {
			item.CloneDefaults(ItemID.CopperShortsword);
			item.damage = 24;
            item.useAnimation = 12;
            item.useTime = 12;
            item.autoReuse = true;
            item.rare = ItemRarityID.Blue;

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
            recipe.AddIngredient(ItemID.Silk, 25);
            recipe.AddTile(mod, "BlackBox");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}