using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Pink : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Pink is considered to be the color of warmth and love, but is that true?\n" +
                               "Can guns really bring peace and love?\n" +
                               "Can only be used while standing absolutely still");
        }

		public override void SetDefaults() {
			item.damage = 80; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.ranged = true; // sets the damage type to ranged
			item.width = 40; // hitbox width of the item
			item.height = 42; // hitbox height of the item
			item.useTime = 38; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 38; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Red; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
			item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}

        public override bool CanUseItem(Player player)
        {
            return player.velocity.X == 0 && player.velocity.Y == 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileID.BulletHighVelocity;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Musket);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
            recipe.AddIngredient(ItemID.PinkDye, 3);
            recipe.AddIngredient(ItemID.PinkThread, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
        }
    }
}
