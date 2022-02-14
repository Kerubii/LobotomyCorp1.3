using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class CensoredGrab : ModProjectile
    {
        public override void SetStaticDefaults() {
        }

        public override void SetDefaults() {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;
            projectile.timeLeft = 120;

            //projectile.hide = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            drawHeldProjInFrontOfHeldItemAndArms = true;
        }

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            float rot = projectile.velocity.ToRotation();

            float progress = 1f - (float)projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            if (progress < 0.3f)
            {
                projectile.ai[0] = Lerp(0f, 90, progress % 0.3f / 0.3f);
            }
            else if (progress >= 0.5f)
            {
                projectile.ai[0] = 45 + 45f * (float)Math.Sin(1.57f + 3.14f * (progress % 0.5f / 0.5f));
            }
            else
                projectile.ai[0] = 90;

            if (progress > 0.25f)
            {
                if (projectile.localAI[0] < 0.959931f)
                    projectile.localAI[0] += 0.0898132f;
            }
            
            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.rotation = rot;// + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            projectile.Center = ownerMountedCenter + (40 + projectile.ai[0]) * velRot;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
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
            Vector2 position = ownerMountedCenter - Main.screenPosition + RandomizeTexture();
            Vector2 origin = new Vector2(projectile.spriteDirection == 1 ? 8 : 68, 14);
            float rotation = projectile.rotation + (projectile.spriteDirection == 1 ? 0 : 3.14f);
            float scale = projectile.scale * 0.33f;

            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);

            position += (projectile.Center - ownerMountedCenter) / 5 + RandomizeTexture();
            scale = projectile.scale * 0.50f;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);
            
            for (int i = -1; i < 2; i += 2)
            {
                position = projectile.Center + new Vector2(-14, -5 * i).RotatedBy(projectile.rotation) - Main.screenPosition;
                scale = projectile.scale * 0.33f;
                float rotation2 = projectile.rotation - 1.047f * i + projectile.localAI[0] * i;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], position + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation2 + (projectile.spriteDirection == 1 ? 0 : 3.14f), origin, scale, 0f, 0f);

                position += new Vector2(25, 0).RotatedBy(rotation2) + RandomizeTexture();
                rotation2 += 0.78f * i + (projectile.spriteDirection == 1 ? 0 : 3.14f);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], position, Main.projectileTexture[projectile.type].Frame(), lightColor, rotation2, origin, scale, 0f, 0f);
            }

            position = projectile.Center + new Vector2(-60, 0).RotatedBy(projectile.rotation) - Main.screenPosition + RandomizeTexture();
            scale = projectile.scale * 0.66f;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);

            return false;
        }

        private Vector2 RandomizeTexture()
        {
            return new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
        }
    }

    public class CensoredSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;
            projectile.timeLeft = 120;

            //projectile.hide = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            drawHeldProjInFrontOfHeldItemAndArms = true;
        }

        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            float rot = projectile.velocity.ToRotation();

            float progress = 1f - (float)projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            if (progress < 0.15f)
            {
                projectile.ai[0] = Lerp(0f, 360, progress % 0.15f / 0.15f);
            }
            else if (progress >= 0.5f)
            {
                projectile.ai[0] = 180 + 180 * (float)Math.Sin(1.57f + 3.14f * (progress % 0.5f / 0.5f));
            }
            else
                projectile.ai[0] = 360;

            if (progress > 0.0f)
            {
                if (projectile.localAI[0] < 1.0472)
                    projectile.localAI[0] += 0.1298132f;
            }

            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.rotation = rot;// + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            projectile.Center = ownerMountedCenter + (40 + projectile.ai[0]) * velRot;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 2)
                target.immune[projectile.owner] = Main.player[projectile.owner].itemAnimation;
            else
                target.immune[projectile.owner] = Main.player[projectile.owner].itemAnimationMax / 2;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            if (Collision.CheckAABBvLineCollision2(targetHitbox.TopLeft(), targetHitbox.Size(), ownerMountedCenter, projectile.Center))
                return true;
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Vector2 position = ownerMountedCenter - Main.screenPosition;
            Vector2 origin = new Vector2(projectile.spriteDirection == 1 ? 8 : 68, 14);
            float rotation = projectile.rotation + (projectile.spriteDirection == 1 ? 0 : 3.14f);
            float scale = projectile.scale * 0.33f;

            spriteBatch.Draw(Main.projectileTexture[projectile.type], position + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);

            position += (projectile.Center - ownerMountedCenter);
            position += new Vector2(-12, 0).RotatedBy(projectile.rotation);
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);

            for (int i = 0; i < 7; i++)
            {
                position -= (projectile.Center - ownerMountedCenter) / 14;
                if (i == 3)
                    position -= (projectile.Center - ownerMountedCenter) / 26;
                if (i > 2)
                    scale = projectile.scale * 0.5f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], position + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);
            }

            position = ownerMountedCenter - Main.screenPosition + (projectile.Center - ownerMountedCenter) / 12;
            scale = projectile.scale * 0.50f;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);

            position += (projectile.Center - ownerMountedCenter) / 8;

            for (int i = -1; i < 2; i += 2)
            {
                float length = 40;
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 2)
                    length = Lerp(-20, 40, (float)projOwner.itemAnimation / ((float)projOwner.itemAnimationMax / 2f));

                Vector2 position2 = position + new Vector2(length, -5 * i).RotatedBy(projectile.rotation);
                scale = projectile.scale * 0.33f;
                float rotation2 = projectile.rotation - 1.047f * i + projectile.localAI[0] * i;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], position2 + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation2 + (projectile.spriteDirection == 1 ? 0 : 3.14f), origin, scale, 0f, 0f);

                position2 += new Vector2(25, 0).RotatedBy(rotation2) + RandomizeTexture();
                rotation2 += 0.78f * i + (projectile.spriteDirection == 1 ? 0 : 3.14f) - (projectile.localAI[0] - 0.261799f) * i;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], position2 + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation2, origin, scale, 0f, 0f);
            }

            scale = projectile.scale * 0.66f;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position + RandomizeTexture(), Main.projectileTexture[projectile.type].Frame(), lightColor, rotation, origin, scale, 0f, 0f);

            return false;
        }

        private Vector2 RandomizeTexture()
        {
            return new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
        }
    }
}
