using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
    public class RedEyes : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The Spider Bud had dozens of eyes, and its children were always hungry.\n" +
                               "This tenacity carried over to the E.G.O., demonstrating an outstanding ability to track down targets.");
        }

        public override void SetDefaults()
        {
            item.damage = 18;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.AddBuff(BuffID.Swiftness, 180);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            player.AddBuff(BuffID.Swiftness, 180);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 100);
            recipe.AddIngredient(ItemID.Ruby, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
    }
}