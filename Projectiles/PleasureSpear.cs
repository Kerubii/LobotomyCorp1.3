using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class PleasureSpear : ModProjectile
    {
        public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

        public override void SetDefaults() {
            projectile.width = 24;
            projectile.height = 24;
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
            projectile.ai[0] = 10 * (float)Math.Sin(progress * 3.14f);

            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.rotation = rot;// + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            projectile.Center = ownerMountedCenter + 67 * velRot + projectile.ai[0] * projectile.velocity;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[0] > 8.5f)
                damage *= 2;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Pleasure"), 300);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Pleasure"), 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = mod.GetTexture("Projectiles/PleasureSpearHandle");
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //Dust.NewDustPerfect(ownerMountedCenter, 14, Vector2.Zero);
            Vector2 position = ownerMountedCenter - Main.screenPosition;
            Vector2 origin = new Vector2((projectile.spriteDirection == 1 ? 2 : 41), 5);
            float rotation = projectile.rotation + (projectile.spriteDirection == 1 ? 0 : 3.14f);
            SpriteEffects spriteEffect = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(tex, position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, tex.Width, tex.Height
                                        )
                                    ),
                lightColor * ((float)(255 - projectile.alpha) / 255f), rotation, origin, projectile.scale, spriteEffect, 0f);

            position += new Vector2(tex.Width + 2, 0).RotatedBy(projectile.rotation);
            tex = Main.projectileTexture[projectile.type];
            origin = new Vector2((projectile.spriteDirection == 1 ? 0 : 62), 15);
            float minScale = 0.2f;
            float maxScale = (projectile.velocity.Length() * 10) / 62f;
            float progress = 1f - (float)projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            Vector2 scale = new Vector2(minScale + (maxScale - minScale) * (float)Math.Sin(progress * 3.14f), 1) * projectile.scale;
            spriteBatch.Draw(tex, position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, tex.Width, tex.Height
                                        )
                                    ),
                lightColor * ((float)(255 - projectile.alpha) / 255f), rotation, origin, scale, spriteEffect, 0f);
            return false;
        }
    }
}
