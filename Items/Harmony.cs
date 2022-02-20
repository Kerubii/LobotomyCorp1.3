using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Harmony : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("It may look like a deteriorating machine at first glance,\n" +
                               "But the music it makes captures its audience more than any other instrument could.\n" +
                               "The wielder must dedicate himself in return.\n" +
                               "After all, art is a devil's gift, born from despair and suffering.\n" +
                               "30% increased damage when consuming 2% hp while having above 2% maximum health");
        }

		public override void SetDefaults() {
			item.damage = 77; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.magic = true; // sets the damage type to ranged
			item.width = 40; // hitbox width of the item
			item.height = 38; // hitbox height of the item
			item.useTime = 38; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 32; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Yellow; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item23; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 8f; // the speed of the projectile (measured in pixels per frame)
            item.mana = 10;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 pos2 = position + Vector2.Normalize(new Vector2(speedX, speedY)) * 90f;
            if (Collision.CanHit(position, 0, 0, pos2, 0, 0))
            {
                position = pos2;
                for (int i = 0; i < 7; i++)
                {
                    int n = Dust.NewDust(position, 1, 1, mod.DustType("NoteDust"), speedX * 0.5f, speedY * 0.5f);
                    Main.dust[n].fadeIn = 1.2f;
                }
            }

            type = mod.ProjectileType("HarmonyShot");
            if (player.statLifeMax2 > player.statLifeMax2 * 0.02f)
            {
                damage = (int)(damage * 1.3f);
                player.statLife -= (int)(player.statLifeMax2 * 0.02f);
                CombatText.NewText(player.getRect(), CombatText.DamagedFriendly, (int)(player.statLifeMax2 * 0.02f));
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sawmill);
            recipe.AddIngredient(ItemID.SilverBar, 15);
            recipe.AddIngredient(ItemID.Chain, 16);
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddTile(mod, "BlackBox2");
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sawmill);
            recipe.AddIngredient(ItemID.SilverBar, 15);
            recipe.AddIngredient(ItemID.Chain, 16);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(mod, "BlackBox2");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
