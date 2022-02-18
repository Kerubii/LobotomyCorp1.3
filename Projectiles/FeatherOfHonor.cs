using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
    public class FeatherOfHonor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 20;

            projectile.tileCollide = false;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.extraUpdates = 3;
        }

        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

            if (projectile.ai[1] < 3)
            {
                if (!projOwner.channel)
                {
                    projectile.ai[1] = -1;
                }
                else
                {
                    projectile.Center = ownerMountedCenter + new Vector2(52, 0).RotatedBy(MathHelper.ToRadians(-30 - 30 * projectile.ai[0]));
                    projectile.timeLeft = 20;
                    projectile.spriteDirection = projOwner.direction;
                    projOwner.itemTime = 2;
                    projOwner.itemAnimation = 2;

                    if (Main.rand.Next(3) == 0)
                    {
                        int i = Dust.NewDust(projectile.Center - new Vector2(24, 9) * projectile.scale, (int)(48 * projectile.scale), (int)(18 * projectile.scale), DustID.Fire);
                        Main.dust[i].noGravity = true;
                    }
                }
            }

            if (projectile.ai[1] == 0)
            {
                projectile.scale = 0;
                projectile.ai[1] = 1;
            }
            else if (projectile.ai[1] == 1)
            {
                if (projectile.scale < 1f)
                    projectile.scale += 0.02f;
                if (projectile.scale >= 1f)
                {
                    projectile.scale = 1f;
                    projectile.ai[1] ++;
                }
            }
            else if (projectile.ai[1] == 2)
            {
                if (projOwner.whoAmI == Main.LocalPlayer.whoAmI)
                {
                    Items.FeatherOfHonor modItem = (Items.FeatherOfHonor)projOwner.HeldItem.modItem;
                    if (modItem.FeatherShoot == 0)
                    {
                        Main.PlaySound(SoundID.Item45, projectile.Center);

                        projectile.ai[1]++;
                        modItem.FeatherShoot = 24;

                        Vector2 speed = Main.MouseWorld - projectile.Center;
                        speed.Normalize();
                        speed *= 12f;
                        projectile.timeLeft = 600;
                        projectile.velocity = speed;
                        projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0 : 3.14f);

                        projectile.ai[1]++;
                    }
                }
            }
            else if (projectile.ai[1] < 0)
            {
                projectile.scale -= 0.05f;
                if (projectile.scale <= 0f)
                    projectile.Kill();
            }
            else
            {
                int i = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
                Main.dust[i].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5;i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X, projectile.velocity.Y);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects sp = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (projectile.ai[1] > 2)
                for (int i = 0; i < 10; i++)
                {
                    Vector2 Pos = projectile.oldPos[i] + projectile.Size / 2 - Main.screenPosition;
                    Color color = lightColor * (1f - ((float)(i + 1f) / 10f));

                    spriteBatch.Draw(
                    Main.projectileTexture[projectile.type],
                    Pos,
                    Main.projectileTexture[projectile.type].Frame(),
                    lightColor * (1f - i / 10f),
                    projectile.oldRot[i],
                    Main.projectileTexture[projectile.type].Size() / 2,
                    projectile.scale,
                    sp, 0f);
                }
            spriteBatch.Draw(
                Main.projectileTexture[projectile.type],
                projectile.Center - Main.screenPosition,
                Main.projectileTexture[projectile.type].Frame(),
                lightColor,
                projectile.rotation,
                Main.projectileTexture[projectile.type].Size() / 2,
                projectile.scale,
                sp, 0f);
            return false;
        }
    }
}
