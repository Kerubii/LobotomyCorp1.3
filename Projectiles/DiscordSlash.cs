using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class DiscordSlash : ModProjectile
    {
        public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

        public override void SetDefaults() {
            projectile.width = 38;
            projectile.height = 38;
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
            rot += MathHelper.ToRadians(205f * (float) Math.Sin(1.6f * progress) - 160f) * projectile.spriteDirection;

            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.rotation = rot + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            projectile.Center = ownerMountedCenter + (120 + projectile.ai[0]) * velRot;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();

            if (0.14f < progress && progress < 0.63f)
                for (int i = 0; i < 16; i++)
                {
                    Dust d = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Wraith)];
                    d.noGravity = true;
                    d.color = Color.Black;
                    d.fadeIn = 1.2f;
                    d.scale = 2;
                    d.velocity *= 0;
                }

            /*if (projectile.ai[1] == 0 && projOwner.itemAnimation < projOwner.itemAnimationMax / 2)
            {
                Projectile.NewProjectile(projectile.Center, projectile.velocity, mod.ProjectileType("DiscordLingeringSlash"), projectile.damage, 0f, projectile.owner);
                projectile.ai[1]++;
            }*/
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //Dust.NewDustPerfect(ownerMountedCenter, 14, Vector2.Zero);
            Vector2 position = ownerMountedCenter - Main.screenPosition;
            Vector2 originOffset = new Vector2(projectile.ai[0] - 20, 0).RotatedBy(MathHelper.ToRadians(projectile.direction == 1 ? 135 : 45));
            Vector2 origin = new Vector2((projectile.spriteDirection == 1 ? 14 : 95), 95) + originOffset;
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
