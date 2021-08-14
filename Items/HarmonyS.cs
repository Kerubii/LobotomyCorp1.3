using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class HarmonyS : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("\"But nothing could compare to the music it makes when it eats a human.\"\n");
            PassiveText = "Music - Hitting an enemy gives all allies \"Musical Addiction\" debuff.\n" +
                          "Musical Addiction - 8% increased damage, decreases life regeneration.\n" +
                          "Performance - Shoot a soundwave that marks enemies and decreases their defense.\n" +
                          "Rhythm - Hitting a marked enemy releases music notes that gives \"Satiated\" buff.\n" +
                          "|Satiated - Negates negative life regeneration and increases life regeneration";
            EgoColor = LobotomyCorp.HeRarity;
        }

		public override void SetDefaults() 
		{
			item.damage = 34;
			item.melee = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 22;
			item.useAnimation = 20;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 3;
            item.shootSpeed = 3.7f;
            item.shoot = mod.ProjectileType("HarmonyS");

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
			item.autoReuse = true;
            item.channel = true;
		}

        public override bool SafeCanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = mod.GetTexture("Items/HarmonySHead");
            spriteBatch.Draw(tex, position, frame, drawColor, 0, origin, scale, 0, 0);
            tex = mod.GetTexture("Items/HarmonySString");
            spriteBatch.Draw(tex, position, frame, drawColor, 0, origin, scale, 0, 0);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = mod.GetTexture("Items/HarmonySHead");
            spriteBatch.Draw(tex, item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - tex.Height / 2), tex.Frame(), lightColor, rotation, tex.Size() / 2, scale, 0, 0);
            tex = mod.GetTexture("Items/HarmonySString");
            spriteBatch.Draw(tex, item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - tex.Height / 2), tex.Frame(), lightColor, rotation, tex.Size() / 2, scale, 0, 0);
        }

        public override void AddRecipes() 
		{
		}
	}
}