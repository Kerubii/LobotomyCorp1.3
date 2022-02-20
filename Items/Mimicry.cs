using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class Mimicry : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The yearning to imitate the human form is sloppily reflected on the E.G.O.\n" +
							   "As if it were a reminder that it should remain a mere desire.\n" +
							   "It can deliver a powerful downswing that should be impossible for a human.\n" +
							   "Recovers 25% damage dealt on hit");
		}

		public override void SetDefaults() 
		{
			item.damage = 32;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.channel = true;
			item.rare = ItemRarityID.Red;
			//item.UseSound = SoundID.Item1;
		}

        public override bool CanUseItem(Player player)
        {
			item.scale = 1f;
			LobotomyModPlayer.ModPlayer(player).ChargeWeaponHelper = 0;
			return true;
        }

        public override void UseStyle(Player player)
        {
            if (player.channel)
            {
				LobotomyModPlayer modPlayer = LobotomyModPlayer.ModPlayer(player);
				player.itemAnimation = (int)(item.useAnimation * ItemLoader.MeleeSpeedMultiplier(item, player));
				if (modPlayer.ChargeWeaponHelper < 1f)
					modPlayer.ChargeWeaponHelper += 0.0166f;
				else
				{
					modPlayer.ChargeWeaponHelper = 1f;
					player.channel = false;
				}
				item.scale = 1f + 0.5f * modPlayer.ChargeWeaponHelper;
			}
			if (player.itemAnimation == (int)(item.useAnimation * ItemLoader.MeleeSpeedMultiplier(item, player)) - 1)
				Main.PlaySound(SoundID.Item1, player.Center);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += 2f * LobotomyModPlayer.ModPlayer(player).ChargeWeaponHelper;
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            noHitbox = player.channel;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			int heal = (int)(damage * 0.25f);
			player.HealEffect(heal);
			player.statLife += heal;
        }

        public override void AddRecipes() 
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BreakerBlade);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(mod, "BlackBox3");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (!player.channel && Main.rand.Next(3) == 0)
            {
				Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Blood);
            }
        }
    }
}