using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class CrimsonScar : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("With steel in one hand and gunpowder in the other, there's nothing to fear in this place.\n" +
                               "It's more important to deliver a decisive strike in blind hatred without hesitation than to hold on to insecure courage.");
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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (player.statLife <= player.statLifeMax / 2)
            {
                add += 0.5f;    
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.melee = false;
                item.ranged = true;
                item.shoot = 10;
                item.useTime = 36;
                item.useAnimation = 36;
                item.useStyle = ItemUseStyleID.HoldingOut;
                Main.itemTexture[item.type] = mod.GetTexture("Items/CrimsonScarGun");
                item.noMelee = true;
                item.UseSound = SoundID.Item11;
            }
            else
            {
                item.melee = true;
                item.ranged = false;
                item.shoot = 0;
                item.useTime = 18;
                item.useAnimation = 18;
                item.useStyle = ItemUseStyleID.SwingThrow;
                Main.itemTexture[item.type] = mod.GetTexture("Items/CrimsonScarScythe");
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            position = position - (Main.inventoryBackTexture.Size() * Main.inventoryScale) / 2f + frame.Size() * scale / 2f;
            Texture2D tex = mod.GetTexture("Items/CrimsonScar");
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
            Texture2D tex = mod.GetTexture("Items/CrimsonScar");
            spriteBatch.Draw(tex, item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - tex.Height / 2), tex.Frame(), lightColor, rotation, tex.Size() / 2, scale, 0, 0);
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse == 2;
        }

        /*public override bool ConsumeAmmo(Player player)
        {
            return player.altFunctionUse != 2;
        }*/

        public override void AddRecipes() {
		}
	}
}