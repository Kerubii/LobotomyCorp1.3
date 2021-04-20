using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class MimicryS : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("\"And the many shells cried out one word, \"Manager\".\"");
            PassiveText = "Hello? - Shoot a piercing projectile infront of you\n" +
                          "Goodbye - deal 200% more damage when hitting enemies on your attack's sweetspot\n" +
                          "Help! - Killing an enemy restores 20% of your health and grants A New Shell buff, disabling Shell" +
                          "|Shell - Gain the debuff Craving for 1 minute when using this ewapon. When the debuff times out, the user dies";
            EgoColor = LobotomyCorp.AlephRarity;
        }

        public override void SetDefaults() 
		{
			item.damage = 265;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 42;
			item.useAnimation = 42;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NothingThere_Goodbye");
            item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("MimicryS");
            item.shootSpeed = 1;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //CombatText.NewText(player.getRect(), Color.Red, "Goodbye", true);
            if (player.altFunctionUse == 2)
                damage = (int)(damage * 0.5f);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NothingThere_Goodbye").WithVolume(0.25f);
                item.useTime = 42;
                item.useAnimation = 42;
                item.shoot = mod.ProjectileType("MimicryS");
                item.shootSpeed = 1;
            }
            else
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NothingThere_Hello").WithVolume(0.18f);
                item.useTime = 24;
                item.useAnimation = 24;
                item.shoot = mod.ProjectileType("MimicrySHello");
                item.shootSpeed = 8;
            }
            return base.SafeCanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes() 
		{
		}
	}
}