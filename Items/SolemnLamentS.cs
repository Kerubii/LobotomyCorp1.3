using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LobotomyCorp.Items
{
	public class SolemnLamentS : SEgoItem
	{
        public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("\"Where does one go when they die?\"");
            PassiveText = "Coffin - The second gun uses the next ammo type, it does not shoot otherwise\n" +
                          "Tranquility - The weapon's firerate is increased when alternating between the two guns\n" +
                          "Lament - 15% increased damage against enemies with the lowest health\n" +
                          "Coffin for the Dead - Hitting enemies with the lowest health spawn additional butterflies that target the enemy\n" +
                          "|Eternal Rest - Killing enemies with a total of 30,000 health disables this weapon for 5 minutes\n";
            EgoColor = LobotomyCorp.WawRarity;
        }

		public override void SetDefaults() 
		{
			item.damage = 33;
			item.ranged = true;
			item.width = 40;
			item.height = 40;

			item.useTime = 30;
            item.useAnimation = 12;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = 2;
            item.shootSpeed = 9f;
            item.shoot = 1;
            item.useAmmo = AmmoID.Bullet;

            item.noUseGraphic = true;
			item.UseSound = SoundID.Item11;
            item.noMelee = true;
			item.autoReuse = true;
            LobotomyGlobalItem.LobItem(item).CustomDraw = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return (!modPlayer(player).SolemnSwitch || player.itemTime == 0) && AltAmmo(player);
        }

        public override bool SafeCanUseItem(Player player)
        {
            LobotomyGlobalItem lobItem = LobotomyGlobalItem.LobItem(item);
            if (player.altFunctionUse == 2)
            {
                player.itemTime = 0;
                lobItem.CustomTexture = mod.GetTexture("Items/SolemnLamentS1");
                modPlayer(player).SolemnSwitch = true;
                //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ButterFlyMan_StongAtk_Black");
                return true;
            }
            else if (modPlayer(player).SolemnSwitch || player.itemTime == 0)
            {
                player.itemTime = 0;
                lobItem.CustomTexture = mod.GetTexture("Items/SolemnLamentS2");
                modPlayer(player).SolemnSwitch = false;
                //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ButterFlyMan_StongAtk_White");
                return true;
            }
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
                AltAmmoConsume(player, ref type, ref speedX, ref speedY, ref damage);
            Main.projectile[Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI)].GetGlobalProjectile<LobotomyGlobalProjectile>().Lament = LobotomyModPlayer.ModPlayer(player).SolemnSwitch ? (byte)1 : (byte)2;
            return false;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return player.altFunctionUse != 2;
        }

        public override void AddRecipes() 
		{
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }

        LobotomyModPlayer modPlayer(Player player)
        {
            return LobotomyModPlayer.ModPlayer(player);
        }

        /*public override void HoldStyle(Player player)
        {
            if (modPlayer(player).SolemnSwitch)
                player.itemRotation
        }*/

        private bool AltAmmo(Player player)
        {
            bool canUse = false;
            if (item.useAmmo > 0)
            {
                for (int index = 0; index < 58; ++index)
                {
                    if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 0)
                    {
                        int type = player.inventory[index].type;
                        for (int index2 = 0; index2 < 58; ++index2)
                        {
                            if (player.inventory[index2].ammo == item.useAmmo && player.inventory[index2].stack > 0 && type != player.inventory[index2].type)
                            {
                                canUse = true;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return canUse;
        }

        private void AltAmmoConsume(Player player, ref int shoot, ref float speedX, ref float speedY, ref int damage)
        {
            int type = -1;
            bool pew = false;
            bool ammoCheck = false;
            for (int index = 54; index < 58; ++index)
            {
                if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 0)
                {
                    type = player.inventory[index].type;
                    ammoCheck = true;
                    break;
                }
            }
            if (!ammoCheck)
            {
                for (int index = 0; index < 54; ++index)
                {
                    if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 0)
                    {
                        type = player.inventory[index].type;
                        break;
                    }
                }
            }
            ammoCheck = false;
            Item ammo = new Item();
            if (type > -1)
            {
                for (int index = 54; index < 58; ++index)
                {
                    if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 0 && type != player.inventory[index].type)
                    {
                        ammo = player.inventory[index];
                        ammoCheck = true;
                        pew = true;
                        break;
                    }
                }
                if (!ammoCheck)
                {
                    for (int index = 0; index < 54; ++index)
                    {
                        if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 0 && type != player.inventory[index].type)
                        {
                            ammo = player.inventory[index];
                            pew = true;
                            break;
                        }
                    }
                }
            }
            if (!pew)
                return;

            Vector2 dir = new Vector2(speedX, speedY);
            dir.Normalize();
            dir *= (item.shootSpeed + ammo.shootSpeed);
            speedX = dir.X;
            speedY = dir.Y;

            if (ammo.damage > 0)
                damage = player.GetWeaponDamage(item) + (int)(ammo.damage * player.rangedDamage);
            else
                damage = item.damage;

            shoot = ammo.shoot;
            bool consume = (player.itemAnimation < item.useAnimation - 2);
            if (player.ammoBox && Main.rand.Next(5) == 0)
                consume = true;
            if (player.ammoPotion && Main.rand.Next(5) == 0)
                consume = true;
            if (player.ammoCost80 && Main.rand.Next(5) == 0)
                consume = true;
            if (player.ammoCost75 && Main.rand.Next(4) == 0)
                consume = true;
            if (!PlayerHooks.ConsumeAmmo(player, item, ammo))
                consume = true;
            if (!ammo.consumable)
                consume = true;
            if (consume)
                return;
            PlayerHooks.OnConsumeAmmo(player, item, ammo);
            ItemLoader.OnConsumeAmmo(item, ammo, player);
            ammo.stack--;
            if (ammo.stack > 0)
                return;
            ammo.active = false;
            ammo.TurnToAir();
        }
        /*public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("LobotomyCorp/Items/SolemnLamentS");
            spriteBatch.Draw(texture, position, frame, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }*/
    }

}