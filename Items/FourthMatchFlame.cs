using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class FourthMatchFlame : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("The fire roars and burns like the first flame.\n" +
                               "The light of the match will not go out.");

        }

		public override void SetDefaults() {
			item.damage = 22; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.ranged = true; // sets the damage type to ranged
			item.width = 40; // hitbox width of the item
			item.height = 38; // hitbox height of the item
			item.useTime = 38; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 32; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Blue; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item14; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 8f; // the speed of the projectile (measured in pixels per frame)
			item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("FourthMatchFlameShot");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.GoldBar, 15);
            recipe.AddTile(mod, "BlackBox");
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.PlatinumBar, 15);
            recipe.AddTile(mod, "BlackBox");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
