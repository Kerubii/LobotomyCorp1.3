using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class SerumW : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Anime");
        }

		public override void SetDefaults() {
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 8;

			//projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}

        private Vector2 InitialPosition;

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.direction = projOwner.direction;
            if (projectile.ai[0] == 0)
            {
                projectile.ai[1] = -1;
                projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
                projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
                projectile.position += projectile.velocity * 32f;
                projOwner.velocity = projectile.velocity * 16f;
            }
            else if (projectile.ai[0] == 1)
            {
                InitialPosition = projOwner.position;
                projOwner.itemTime = 2;
                projOwner.itemAnimation = 2;
                NPC npc = Main.npc[(int)projectile.ai[1]];
                npc.Center = projOwner.Center;
                npc.position.X = projOwner.position.X + projOwner.width + 2;// + npc.width / 2;
                projOwner.velocity *= 0;
                if (projectile.timeLeft == 545)
                {
                    for (int n = -20; n < 20; n++)
                    {
                        Vector2 position = new Vector2(projOwner.Center.X, projOwner.Center.Y - n * 0.8f);
                        Dust dust = Main.dust[Terraria.Dust.NewDust(position, 2, 2, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.7f;
                    }
                }
                
                if (projectile.timeLeft == 541)
                    projectile.ai[0]++;
            }
            else if (projectile.ai[0] < 11)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    Dust dust2 = Main.dust[Terraria.Dust.NewDust(Main.screenPosition, Main.screenWidth, Main.screenHeight, 226, -8f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust2.velocity.Y *= 0;
                    dust2.noGravity = true;
                }

                NPC npc = Main.npc[(int)projectile.ai[1]];
                projOwner.itemTime = 2;
                projOwner.itemAnimation = 2;
                if (projectile.timeLeft % 2 == 0)
                {
                    Dust dust = Main.dust[Terraria.Dust.NewDust(projOwner.position, projOwner.width, projOwner.height, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.7f;
                }

                if (projectile.timeLeft % 30 == 29)
                {
                    Dust dust;
                    for (int n = -20; n < 20; n++)
                    {
                        Vector2 position = new Vector2(projOwner.Center.X, projOwner.Center.Y - n * 0.8f);
                        dust = Main.dust[Terraria.Dust.NewDust(position, 2, 2, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.7f;

                        dust = Main.dust[Terraria.Dust.NewDust(position, 2, 2, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                        dust.noGravity = true;
                        dust.velocity *= 2f;
                    }
                }
                if (projectile.timeLeft % 30 == 2)
                {
                    Dust dust;
                    for (int n = -20; n < 20; n++)
                    {
                        Vector2 position = new Vector2(npc.Center.X, npc.Center.Y - n * 0.8f);
                        dust = Main.dust[Terraria.Dust.NewDust(position, 2, 2, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.7f;
                    }
                }

                projOwner.velocity.X = 16f * (0.5f + ((float)(projectile.timeLeft % 30) / 30f));
                projOwner.velocity.Y = -projOwner.gravity + 0.01f;
                if (projectile.timeLeft % 30 == 0)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        int x = Main.rand.Next( 60, Main.maxTilesX - 60), y = Main.rand.Next( (int)(InitialPosition.Y / 16 - 60), Main.maxTilesY - 60);
                        bool invalid = false;
                        for (int j = -2; j <= 16; j++)
                        {
                            for (int k = -5; k <= 5; k++)
                            {
                                Tile tile = Main.tile[x + j, y + k];
                                if (tile.active() || tile.liquid > 0)
                                    invalid = true;
                                if (invalid)
                                    break;
                            }
                            if (invalid)
                                break;
                        }
                        if (!invalid)
                        {
                            projOwner.Teleport(new Vector2(x * 16, y * 16), -1, -1);
                            LobotomyModPlayer.ModPlayer(projOwner).shakeIntensity = 5;
                            LobotomyModPlayer.ModPlayer(projOwner).shakeTimer = 15;
                            break;
                        }
                    }
                    projectile.ai[0]++;

                    
                }
                npc.Center = projOwner.Center;
                npc.position.X = projOwner.position.X + projOwner.width + 2;
            }
            else if (projectile.ai[0] == 11)
            {
                projOwner.position = InitialPosition;
                
                NPC npc = Main.npc[(int)projectile.ai[1]];
                npc.Center = projOwner.Center;
                npc.position.X = projOwner.position.X + projOwner.width + 2;
                projectile.position = npc.position;
                projectile.ai[0]++;
            }
            else
            {
                Dust dust;
                for (int n = -20; n < 20; n++)
                {
                    Vector2 position = new Vector2(projOwner.Center.X, projOwner.Center.Y - n * 0.8f);
                    dust = Main.dust[Terraria.Dust.NewDust(position, 2, 2, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.7f;

                    dust = Main.dust[Terraria.Dust.NewDust(position, 2, 2, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }

                NPC npc = Main.npc[(int)projectile.ai[1]];  
                projOwner.ApplyDamageToNPC(npc, 9999, 7f, 1, false);
                projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] < 0)
            {
                projectile.ai[1] = target.whoAmI;
                projectile.ai[0]++;
                projectile.timeLeft = 600;
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return projectile.ai[1] < 0;
        }

        public override void Kill(int timeLeft)
        {
            Main.player[projectile.owner].velocity *= 0.75f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
