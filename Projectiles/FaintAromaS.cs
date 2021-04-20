using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
	public class FaintAromaS : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
        }

		public override void SetDefaults() {
			projectile.width = 26;
			projectile.height = 26;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 60;

			//projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}
        
		public override void AI() {
			Player projOwner = Main.player[projectile.owner];
            LobotomyModPlayer modPlayer = LobotomyModPlayer.ModPlayer(projOwner);
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
            projectile.rotation = projectile.velocity.ToRotation();

            if (projectile.ai[0] == 0)
            {
                projectile.ai[1] = projOwner.direction > 0 ? MathHelper.ToRadians(165) : MathHelper.ToRadians(15);

                if (projOwner.itemAnimation >= projOwner.itemAnimationMax / 2)
                {
                    float progress = (float)(projOwner.itemAnimation - projOwner.itemAnimationMax / 2) / (float)(projOwner.itemAnimationMax - projOwner.itemAnimationMax / 2);
                    projectile.rotation += Lerp(-MathHelper.ToRadians(90), MathHelper.ToRadians(60), progress) * projOwner.direction;
                    for (int i = 0; i < 3; i++)
                    {
                        Dust dust;
                        dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 205, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 255, 255), 1f)];
                        dust.velocity = projectile.velocity * 4f;
                        dust.noGravity = true;
                        dust.fadeIn = 1.5f * (1f - progress);
                    }

                    projOwner.velocity = projectile.velocity * 12f;
                }
                else
                {
                    if (projOwner.itemAnimation == (projOwner.itemAnimationMax / 2) - 1)
                        projOwner.velocity *= 0.2f;
                    projectile.rotation += -MathHelper.ToRadians(90) * projOwner.direction;
                }
            }
            else if (projectile.ai[0] == 1)
            {
                projectile.ai[1] = projectile.rotation;
                projectile.rotation = projOwner.direction > 0 ? MathHelper.ToRadians(135) : MathHelper.ToRadians(45);
                if (projOwner.itemAnimation >= projOwner.itemAnimationMax / 2)
                {
                    Vector2 DustPos = ownerMountedCenter + new Vector2(64, 0).RotatedBy(projectile.ai[1]) - projectile.Size / 2;
                    for (int i = 0; i < 3; i++)
                    {
                        Dust dust;
                        dust = Main.dust[Dust.NewDust(DustPos, projectile.width, projectile.height, 205, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 255, 255), 1f)];
                        dust.velocity = projectile.velocity * 4f;
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                    }

                    projOwner.velocity = projectile.velocity * 16f;
                }
                else
                {
                    if (projOwner.itemAnimation == (projOwner.itemAnimationMax / 2) - 1)
                        projOwner.velocity *= 0.2f;
                }
            }
            else if (projectile.ai[0] >= 2)
            {
                if (projOwner.itemAnimation >= projOwner.itemAnimationMax / 2)
                {
                    float progress = (float)(projOwner.itemAnimation - projOwner.itemAnimationMax / 2) / (float)(projOwner.itemAnimationMax - projOwner.itemAnimationMax / 2);
                    projectile.ai[1] = projectile.rotation - MathHelper.ToRadians(120) * (1f - progress) * projOwner.direction;
                    projectile.rotation += MathHelper.ToRadians(220) * (1f - progress) * projOwner.direction;

                    Vector2 DustPos = ownerMountedCenter + new Vector2(64, 0).RotatedBy(projectile.ai[1]) - projectile.Size/2;
                    for (int i = 0; i < 3; i++)
                    {
                        Dust dust;
                        dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 205, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 255, 255), 1f)];
                        dust.velocity = projectile.velocity * 4f;
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                        
                        dust = Main.dust[Dust.NewDust(DustPos, projectile.width, projectile.height, 205, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 255, 255), 1f)];
                        dust.velocity = projectile.velocity * 4f;
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                    }
                }
                else
                {
                    projectile.rotation += MathHelper.ToRadians(220) * projOwner.direction;
                }
            }

            projectile.Center = ownerMountedCenter + new Vector2(64, 0).RotatedBy(projectile.rotation);
            projOwner.itemRotation = (float)Math.Atan2((float)Math.Sin(projectile.rotation) * projOwner.direction, (float)Math.Cos(projectile.rotation) * projOwner.direction);

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
		}

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            Player projOwner = Main.player[projectile.owner];
            if (projectile.ai[0] == 1 || (projectile.ai[0] == 2 && projOwner.itemAnimation % 2 == 1))
            {
                Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
                Vector2 location = ownerMountedCenter + new Vector2(64, 0).RotatedBy(projectile.ai[1]);
                hitbox = new Rectangle((int)location.X - projectile.width / 2, (int)location.Y - projectile.height / 2, projectile.width, projectile.height);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player projOwner = Main.player[projectile.owner];
            Texture2D tex = Main.projectileTexture[projectile.type];
            float rot = projectile.rotation;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true) + new Vector2(8, 0).RotatedBy(rot);
            Vector2 position = ownerMountedCenter - Main.screenPosition;
            Vector2 origin = new Vector2(2, 42);

            spriteBatch.Draw(tex, position, tex.Frame(), lightColor, rot + MathHelper.ToRadians(45), origin, projectile.scale * 1.2f, 0, 0);
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] >= 2 && player.ownedProjectileCounts[mod.ProjectileType("AlriuneDeathAnimation")] == 0)
            {
                LobotomyModPlayer.ModPlayer(player).FaintAromaPetal = 0;
                for (int i = 0; i < 32; i++)
                {
                    Dust dust;
                    Vector2 dustVel = new Vector2(8f, 0f).RotatedBy(MathHelper.ToRadians(11.25f * i));
                    dust = Main.dust[Dust.NewDust(player.Center, 1, 1, 205, dustVel.X, dustVel.Y, 0, new Color(255, 255, 255), 1f)];
                    //dust.velocity = projectile.velocity * 4f;
                    dust.noGravity = true;
                    dust.fadeIn = 1.2f;
                }
                foreach (NPC n in Main.npc)
                {
                    if (n.active && n.chaseable && n.CanBeChasedBy(mod.ProjectileType("AlriuneDeathAnimation")) && (n.Center - player.Center).Length() < 800)
                        Projectile.NewProjectile(n.Center, Vector2.Zero, mod.ProjectileType("AlriuneDeathAnimation"), (int)(projectile.damage * 3f), 0, player.whoAmI, n.whoAmI);
                }
            }
            target.immune[projectile.owner] = player.itemAnimation;
            LobotomyModPlayer.ModPlayer(player).FaintAromaPetal += 30f;
            if (LobotomyModPlayer.ModPlayer(player).FaintAromaPetal > LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30)
                LobotomyModPlayer.ModPlayer(player).FaintAromaPetal = LobotomyModPlayer.ModPlayer(player).FaintAromaPetalMax * 3 + 30;
        }
    }
}
