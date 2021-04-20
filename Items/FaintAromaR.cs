using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class FaintAromaS : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("\"Bearing the hope to return to dust, it shall go back to the grave with all that desires to live.\"");
            PassiveText = "Autumn's Passing - Hitting enemies or charging up grants a petal that dissapears overtime.\n" +
                          "Flower Petals - Each petal enhances your normal attacks and damage\n" +
                          "Spring's Genesis - Hitting an enemy with 3 petals removes them deal 300% of your weaponÅfs damage to all enemies around you\n" +
                          "Doll Fashioned from the Earth - Negate any negative life regen while a petal is active\n" +
                          "|Winter's Inception - 10% increased damage taken from enemies with increased 10% per petals";
            EgoColor = LobotomyCorp.WawRarity;
        }

		public override void SetDefaults() 
		{
			item.damage = 40;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("AlriuneDeathAnimation");
            item.shootSpeed = 1f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            /*foreach (NPC n in Main.npc)
            {
                if (n.active && n.chaseable && n.CanBeChasedBy(mod.ProjectileType("AlriuneDeathAnimation")) && (n.Center - player.Center).Length() < 800)
                    Projectile.NewProjectile(n.Center, Vector2.Zero, mod.ProjectileType("AlriuneDeathAnimation"), item.damage, 0, player.whoAmI, n.whoAmI);
            }*/
            Projectile.NewProjectile(position,new Vector2 (speedX, speedY), type, damage, 0, player.whoAmI, (int)(LobotomyModPlayer.ModPlayer(player).FaintAromaPetal/LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax) - 1);
            return false;
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.useTime = 26;
                item.useAnimation = 26;
                if (LobotomyModPlayer.ModPlayer(player).FaintAromaPetal > LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax)
                {
                    item.shoot = mod.ProjectileType("FaintAromaS");
                    item.useStyle = 5;
                    item.noUseGraphic = true;
                    item.noMelee = true;
                    switch ((int)(LobotomyModPlayer.ModPlayer(player).FaintAromaPetal / LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax))
                    {
                        case 1:
                            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Ali_Sub_Atk");
                            break;
                        case 2:
                            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Ali_StrongAtk");
                            break;
                        case 3:
                            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Ali_StrongAtk_Finish");
                            break;
                        default:
                            item.UseSound = SoundID.Item1;
                            break;
                    }
                }
                else
                {
                    item.shoot = 0;
                    item.useStyle = 1;
                    item.noUseGraphic = false;
                    item.noMelee = false;
                    item.UseSound = SoundID.Item1;
                }
                item.shootSpeed = 1f;
            }
            else
            {
                item.useTime = 2;
                item.useAnimation = 2;
                item.shoot = 0;
                item.shootSpeed = 0;
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.noUseGraphic = true;
                item.noMelee = true;
                item.UseSound = SoundID.Item1;
            }
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2 && LobotomyModPlayer.ModPlayer(player).FaintAromaPetal < LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30)
            {
                LobotomyModPlayer.ModPlayer(player).FaintAromaPetal += 1f + LobotomyModPlayer.ModPlayer(player).FaintAromaDecay * 2;
                if (LobotomyModPlayer.ModPlayer(player).FaintAromaPetal > LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30)
                    LobotomyModPlayer.ModPlayer(player).FaintAromaPetal = LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            LobotomyModPlayer.ModPlayer(player).FaintAromaPetal += 30f;
            if (LobotomyModPlayer.ModPlayer(player).FaintAromaPetal > LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30)
                LobotomyModPlayer.ModPlayer(player).FaintAromaPetal = LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(mod.GetTexture("FaintAromaRDisplay"), position, frame, drawColor, 0, origin, scale, 0, 0);
            return false;
        }

        public override void AddRecipes() 
		{
		}
	}
}