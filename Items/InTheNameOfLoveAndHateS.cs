using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class InTheNameOfLoveAndHateS : SEgoItem
	{
        public int ArcanaManaCost = 500;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("In the name of Love and Justice~ Here comes Magical Girl!");
            PassiveText = "Arcana Slave - Summon a laser of love, costs " + ArcanaManaCost + " mana\n" +
                          "Love - Hitting an enemy or getting hit reduces Arcana Slave cost\n" +
                          "Villain - Hitting an enemy with Arcana Beats marks them, increasing Justice and Hate gained\n" +
                          "Justice - Increases defense and lowers damage when hitting enemies\n" +
                          "|Hate - Getting hit reduces defense and increases damage\n" +
                          "A Real Magical Girl? - Getting maximum Justice or Hate gives a specific buff on either";
            EgoColor = LobotomyCorp.WawRarity;
            //Item.staff[item.type] = true;
        }

        public override void SetDefaults() {
			item.damage = 30; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.magic = true; // sets the damage type to ranged
			item.width = 40; // hitbox width of the item
			item.height = 20; // hitbox height of the item
			item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Green; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = mod.ProjectileType("ArcanaBeats"); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 1f; // the speed of the projectile (measured in pixels per frame)
            item.channel = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.X -= 42 * player.direction;
            position.Y -= 30;
            /*Vector2 newSpeed = new Vector2(speedX, speedY).RotatedByRandom(6.28f);
            speedX = newSpeed.X;
            speedY = newSpeed.Y;*/
            //Projectile.NewProjectile(position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = mod.ProjectileType("Circle1");
            }
            else
                item.shoot = mod.ProjectileType("ArcanaBeats");
            return base.SafeCanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }
    }
}
