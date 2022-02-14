using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class JustitiaAlt : ModProjectile
    {
        public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
        }

        public override void SetDefaults() {
            projectile.width = 82;
            projectile.height = 82;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;
            projectile.timeLeft = 120;

            //projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            float rot = projectile.velocity.ToRotation();

            float progress = 1f - (float)projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            if (progress < 0.33f)
            {
                if (progress < 0.05f)
                    rot += MathHelper.ToRadians(-160) * projectile.spriteDirection;
                else if (progress < 0.2f)
                    rot += MathHelper.ToRadians(Lerp(-160, 45, (progress - 0.05f) % 0.15f / 0.15f)) * projectile.spriteDirection;
                else if (progress < 0.25f)
                    rot += MathHelper.ToRadians(45) * projectile.spriteDirection;
                else if (progress < 0.3f)
                {
                    rot += MathHelper.ToRadians(Lerp(45, 0, progress % 0.05f / 0.05f)) * projectile.spriteDirection;
                    projectile.ai[0] = Lerp(0, -30, progress % 0.05f / 0.05f);
                }
                projectile.ai[1] = Main.rand.Next(-10, 10);
            }
            else if (progress < 0.66f)
            {
                if (Main.LocalPlayer.whoAmI == projOwner.whoAmI)
                {
                    projectile.velocity = Vector2.Normalize(Main.MouseWorld - ownerMountedCenter);
                }

                if (progress < 0.43f)
                    projectile.ai[0] = Lerp(-30, 10, (progress - 0.03f) % 0.1f / 0.1f);
                else if (progress < 0.46f)
                    projectile.ai[0] = Lerp(10, -30, (progress - 0.01f) % 0.03f / 0.03f);
                else if (progress < 0.56f)
                {
                    rot += MathHelper.ToRadians(projectile.ai[1]) * projectile.spriteDirection;
                    projectile.ai[0] = Lerp(-30, 10, (progress - 0.06f) % 0.1f / 0.1f);
                }
                else if (progress < 0.60f)
                {
                    rot += MathHelper.ToRadians(projectile.ai[1]) * projectile.spriteDirection;
                    projectile.ai[0] = 10;
                }
                else
                {
                    rot += MathHelper.ToRadians(Lerp(projectile.ai[1], -160, progress % 0.06f / 0.06f)) * projectile.spriteDirection;
                    projectile.ai[0] = Lerp(10, 0, progress % 0.06f / 0.06f);
                }
            }
            else
            {
                if (progress < 0.7f)
                {
                    projectile.ai[0] = 0;
                    rot += MathHelper.ToRadians(-160);
                }
                else if (progress < 0.95f)
                {
                    rot += MathHelper.ToRadians(Lerp(-160, 45, (progress - 0.19f) % 0.25f / 0.25f)) * projectile.spriteDirection;
                }
                else
                {
                    rot += 45 * projectile.spriteDirection;
                }
            }

            if (((0.33f < progress && progress < 0.4f) ||
                (0.46f < progress && progress < 0.50f) ||
                (0.7f < progress && progress < 0.8f)) && projectile.localAI[0] == 0)
            {
                Main.PlaySound(SoundID.Item1, ownerMountedCenter);
                projectile.localAI[0]++;
            }
            else if (!((0.33f < progress && progress < 0.4f) ||
                (0.46f < progress && progress < 0.50f) ||
                (0.7f < progress && progress < 0.8f)))
                projectile.localAI[0] = 0;

            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.rotation = rot + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            if ((0.05f < progress && progress < 0.25f) ||
                (0.66f < progress && progress < 0.95f))
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDustPerfect(ownerMountedCenter + new Vector2(30 + Main.rand.Next(100), 0).RotatedBy(rot), 15).noGravity = true;
                }
            }

            if ((0.33f < progress && progress < 0.43f) ||
                (0.46f < progress && progress < 0.6f))
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDustPerfect(ownerMountedCenter + new Vector2(30 + Main.rand.Next(100), Main.rand.Next(-5, 5)).RotatedBy(rot), 15, -projectile.velocity * 4).noGravity = true;
                }
            }

            projectile.Center = ownerMountedCenter + (80 + projectile.ai[0]) * velRot;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += (int)(target.defense / 2f);
            knockback *= 0.1f;
        }

        public override bool? CanHitNPC(NPC target)
        {
            float progress = 1f - (float)Main.player[projectile.owner].itemAnimation / (float)Main.player[projectile.owner].itemAnimationMax;
            return (!(0.25f < progress && progress < 0.33f) ||
                    !(0.56f < progress && progress < 0.7f));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //Dust.NewDustPerfect(ownerMountedCenter, 14, Vector2.Zero);
            Vector2 position = ownerMountedCenter - Main.screenPosition;
            Vector2 originOffset = new Vector2(projectile.ai[0] + 12, 0).RotatedBy(MathHelper.ToRadians(projectile.direction == 1 ? 135 : 45));
            Vector2 origin = new Vector2((projectile.spriteDirection == 1 ? 9 : 74), 74) + originOffset;
            SpriteEffects spriteEffect = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                lightColor * ((float)(255 - projectile.alpha) / 255f), projectile.rotation, origin, projectile.scale, spriteEffect, 0f);
            return false;
        }
    }
}
