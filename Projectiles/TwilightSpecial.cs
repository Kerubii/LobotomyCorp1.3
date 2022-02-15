using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class TwilightSpecial : ModProjectile
    {
        public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
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

        //private Vector2 PreviousPosition;

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            /*if (projectile.ai[1] == 0)
            {
                PreviousPosition = projOwner.position;
                projectile.ai[1] = 1;
            }*/

            float rot = projectile.velocity.ToRotation();
            float distance = 320;
            float progress = 1f - (float)projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            if (progress < 0.33f)
            {
                float progress2 = progress % .33f / .33f;
                if (progress2 < .3f)
                {
                    rot += MathHelper.ToRadians(Lerp(-140, 30, progress2 % 0.3f / 0.3f)) * projectile.spriteDirection;
                    projOwner.velocity += projectile.velocity;
                    projOwner.velocity.Y -= projOwner.gravity;
                    //projOwner.position = PreviousPosition + projectile.velocity * Lerp(0, distance, progress2 % .3f / .3f);
                }
                else if (progress2 < .5f)
                {
                    rot += MathHelper.ToRadians(30) * projectile.spriteDirection;
                    projOwner.velocity = Vector2.Zero;
                    projOwner.velocity.Y -= projOwner.gravity;
                    //projOwner.position = PreviousPosition + projectile.velocity * distance;
                }
                else if (progress2 < .8f)
                {
                    rot += MathHelper.ToRadians(Lerp(30, 180, (progress2 - 0.2f) % 0.3f / 0.3f)) * projectile.spriteDirection;
                    projOwner.velocity.Y -= projOwner.gravity;
                    //projOwner.position = PreviousPosition + projectile.velocity * distance;
                }
                else
                {
                    rot += 180 * projectile.spriteDirection;
                    projOwner.velocity.Y -= projOwner.gravity;
                    //projOwner.position = PreviousPosition + projectile.velocity * distance;
                }
            }
            else if (progress < 0.66f)
            {
                float progress2 = progress % .33f / .33f;
                if (progress2 < .3f)
                {
                    rot += MathHelper.ToRadians(Lerp(180, -75, progress2 % 0.3f / .3f)) * projectile.spriteDirection;
                    //projOwner.position = PreviousPosition + projectile.velocity * distance * (float)Math.Sin(1.57f + 1.57f * (progress2 % .3f / .3f));
                    projOwner.velocity -= projectile.velocity;
                    projOwner.velocity.Y -= projOwner.gravity;
                }
                else if (progress < .8f)
                {
                    rot += MathHelper.ToRadians(-75) * projectile.spriteDirection; if (projectile.ai[1] == 0)
                    {
                        projOwner.velocity *= 0;
                        projectile.ai[1] = 1;
                    }
                }
                else
                {
                    rot += MathHelper.ToRadians(Lerp(-75, -110, (progress2) % 0.2f / .2f)) * projectile.spriteDirection;
                }
            }
            else
            {
                float progress2 = progress % .33f / .33f;

                if (progress2 < 0.5f)
                {
                    rot += MathHelper.ToRadians(-80 + 230 * (float)Math.Sin(1.65f * (progress2 % .5f / .5f))) * projectile.spriteDirection;
                }
                else
                {
                    rot += MathHelper.ToRadians(180 + 320 * (0.5f * (float)Math.Cos(3.14f * (progress2 % .5f / .5f) + 3.14f) + 0.5f)) * projectile.spriteDirection;
                }  

                //rot += MathHelper.ToRadians(Lerp(-140, 505, progress2)) * projectile.spriteDirection;

                //rot += MathHelper.ToRadians(360 * (float)Math.Sin(progress2 * 6.48) - 140) * projectile.spriteDirection;
            }
            //Main.NewText(projOwner.itemAnimation);

            if (((0.33f < progress && progress < 0.4f) ||
                (0.46f < progress && progress < 0.50f) ||
                (0.7f < progress && progress < 0.8f)) && projectile.localAI[0] == 0)
            {
                Main.PlaySound(SoundID.Item1, ownerMountedCenter);
                projectile.localAI[0]++;
            }

            if (progress < .7f)
            {
                projOwner.immune = true;
                projOwner.immuneTime = 15;
                projOwner.immuneNoBlink = true;
                projOwner.armorEffectDrawShadow = true;
            }


            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.rotation = rot + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            projectile.Center = ownerMountedCenter + (80 + projectile.ai[0]) * velRot;

            if (progress < .1f ||
                (.33f <= progress && progress < .43f) ||
                (.66f <= progress && progress < .77f) ||
                (.87f <= progress && progress < .95f))
            {
                for (int i = 0; i < 24; i++)
                {
                    Vector2 posOffset = new Vector2(Main.rand.NextFloat(-20, projectile.width / 2f), Main.rand.NextFloat(-projectile.height / 2, 0)).RotatedBy(projectile.rotation);
                    Dust d = Dust.NewDustPerfect(projectile.Center + posOffset, DustID.Wraith);
                    d.noGravity = true;
                    d.color = Color.Black;
                    d.fadeIn = 1.2f;
                    d.scale = 2;
                    d.velocity *= 0;
                }

                for (int i = 0; i < 4; i++)
                {
                    Vector2 posOffset = new Vector2(Main.rand.NextFloat(-projectile.width / 2f, projectile.width / 2f), Main.rand.NextFloat(-projectile.height / 2, 0)).RotatedBy(projectile.rotation);
                    Dust d = Dust.NewDustPerfect(projectile.Center + posOffset, 64);
                    d.noGravity = true;
                    d.scale = Main.rand.NextFloat(1f, 2.5f);
                    d.velocity *= 0;
                }
            }

            if (projOwner.itemAnimation == 1)
            {
                /*
                projOwner.velocity *= 0;
                if (!projOwner.mount.Active)
                    projOwner.velocity.Y -= 6f;*/
                projectile.Kill();
            }
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
            if (progress < .1f ||
                (.30f <= progress && progress < .5f) ||
                (.60f <= progress && progress < .95f))
            {
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            if (LobotomyModPlayer.ModPlayer(Main.player[projectile.owner]).TwilightSpecial < 9)
            {
                LobotomyModPlayer.ModPlayer(Main.player[projectile.owner]).TwilightSpecial++;
                if (LobotomyModPlayer.ModPlayer(Main.player[projectile.owner]).TwilightSpecial == 9)
                {
                    Main.PlaySound(SoundID.MaxMana, -1, -1, 1, 1f, 0.0f);
                    for (int index1 = 0; index1 < 5; ++index1)
                    {
                        int index2 = Dust.NewDust(Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height, 45, 0.0f, 0.0f, (int)byte.MaxValue, new Color(), (float)Main.rand.Next(20, 26) * 0.1f);
                        Main.dust[index2].noLight = true;
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].velocity *= 0.5f;
                    }
                    LobotomyModPlayer.ModPlayer(Main.player[projectile.owner]).TwilightSpecial++;
                }
            }
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
