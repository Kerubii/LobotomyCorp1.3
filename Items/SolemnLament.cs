using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class SolemnLament : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The somber design is a reminder that not a sliver of frivolity is allowed for the minds of those who mourn.\n" +
                               "One handgun symbolizes grief for the dead, while the other symbolizes early lament for the living.\n" +
                               "Switches between range and magic depending on the gun used");
        }

        public override void SetDefaults() {
            item.width = 32;
            item.height = 32;
            item.value = 3000;
            item.rare = ItemRarityID.Purple;
			item.damage = 32;
            item.shootSpeed = 12f;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.ranged = true;
            item.UseSound = SoundID.Item11;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.magic = true;
                item.ranged = false;
                item.shoot = 10;
                item.mana = 8;
                Main.itemTexture[item.type] = mod.GetTexture("Items/SolemnLament2");
                
            }
            else
            {
                item.magic = false;
                item.ranged = true;
                item.shoot = 10;
                item.mana = 0;
                Main.itemTexture[item.type] = mod.GetTexture("Items/SolemnLament1");
            }
            return base.CanUseItem(player);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            position = position - (Main.inventoryBackTexture.Size() * Main.inventoryScale) / 2f + frame.Size() * scale / 2f;
            Texture2D tex = mod.GetTexture("Items/SolemnLament");
            frame = tex.Frame();
            scale = 1f;
            if (frame.Width > 32 || frame.Height > 32)
                scale = ((frame.Width <= frame.Height) ? (32f / (float)frame.Height) : (32f / (float)frame.Width));
            scale *= Main.inventoryScale;
            position = position + (Main.inventoryBackTexture.Size() * Main.inventoryScale) / 2f - frame.Size() * scale / 2f;
            origin = frame.Size() * (1f / 2f - 0.5f);
            spriteBatch.Draw(tex, position, frame, drawColor, 0, origin, scale, 0, 0);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D tex = mod.GetTexture("Items/SolemnLament");
            spriteBatch.Draw(tex, item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - tex.Height / 2), tex.Frame(), lightColor, rotation, tex.Size() / 2, scale, 0, 0);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.SilverDye, 3);
            recipe.AddIngredient(ItemID.BlackDye, 3);
            recipe.AddRecipeGroup("LobotomyCorp:Butterflies", 5);
            recipe.AddTile(mod, "BlackBox2");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}