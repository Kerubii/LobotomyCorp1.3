using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
	public class DaCapo : ModProjectile
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
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.spriteDirection = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;

            float rot = projectile.velocity.ToRotation();

            int AnimationMax = projOwner.itemAnimationMax / 3;
            int AnimationRest = (int)(AnimationMax * 0.5f);

            if (projOwner.itemAnimation > AnimationMax * 2)
            {
                float progress = ((float)projOwner.itemAnimation - ((float)AnimationMax * 2)) / ((float)AnimationMax - AnimationRest) - 0.5f;
                rot += MathHelper.ToRadians(Lerp(-90, 90, progress, projectile.spriteDirection == 1));
            }
            else if (projOwner.itemAnimation > AnimationMax)
            {
                float progress = ((float)projOwner.itemAnimation - ((float)AnimationMax)) / ((float)AnimationMax - AnimationRest) - 0.5f;
                rot += MathHelper.ToRadians(Lerp(90, -110, progress, projectile.spriteDirection == 1));
                projectile.spriteDirection *= -1;
            }
            else
            {
                float progress = ((float)projOwner.itemAnimation) / ((float)AnimationMax - AnimationRest) - 0.5f;
                rot += MathHelper.ToRadians(Lerp(-110, 120, progress, projectile.spriteDirection == 1));
            }

            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projectile.rotation = rot + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);

            projectile.ai[0] = 20 * ( 1f - (float)projOwner.itemAnimation / (float)projOwner.itemAnimationMax);

            projectile.Center = ownerMountedCenter + (42 + projectile.ai[0]) * velRot;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
		}

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player projOwner = Main.player[projectile.owner];
            damage = Main.DamageVar(projOwner.GetWeaponDamage(projOwner.HeldItem) * 0.75f);
            if (projOwner.itemAnimation > projOwner.itemAnimationMax / 3)
                knockback *= 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = (Main.player[projectile.owner].itemAnimation % Main.player[projectile.owner].itemAnimationMax /3);
            if (target.immune[projectile.owner] <= 5)
                target.immune[projectile.owner] = 8;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //Dust.NewDustPerfect(ownerMountedCenter, 14, Vector2.Zero);
            Vector2 position = ownerMountedCenter - Main.screenPosition;// - new Vector2(projectile.direction == 1 ? 8 : 16, 10) + new Vector2(10, 0).RotatedBy(projectile.rotation);
            Vector2 originOffset = new Vector2(projectile.ai[0], 0).RotatedBy(MathHelper.ToRadians(projectile.direction == 1 ? 135 : 45));
            Vector2 origin = new Vector2((projectile.spriteDirection == 1 ? 31 : 78), 78) + originOffset;
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
