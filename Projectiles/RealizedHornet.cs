using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class RealizedHornet : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

		public override void SetDefaults() {
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 16;

			//projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;

		}
        
		public override void AI() {
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
			projectile.position += projectile.velocity;
			// When we reach the end of the animation, we can kill the spear projectile
			/*if (projOwner.itemAnimation == 1) {
				projectile.Kill();
			}*/
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            projOwner.velocity = 20f * Vector2.Normalize(projectile.velocity);
            projOwner.immune = true;
            projOwner.immuneTime = 2;
			// Offset by 90 degrees here
			if (projectile.spriteDirection == -1) {
				projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}

        public override void Kill(int timeLeft)
        {
            Main.player[projectile.owner].velocity *= 0.2f;
            Main.player[projectile.owner].itemAnimation = 30;
            Main.player[projectile.owner].itemTime = 30;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            LobotomyGlobalNPC.LNPC(target).QueenBeeSpore = 120;
            if (target.life <= 0)
            {
                LobotomyGlobalNPC.SpawnHornet(target, projectile.owner, damage, knockback);
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 position = projectile.Center - Main.screenPosition;
            for (int i = 0; i < 4; i++)
            {
                position = projectile.oldPos[i] + projectile.Size/2 - Main.screenPosition;
                Color color = lightColor;
                //color.A = (byte)(color.A * 0.15f);
                color *= (4 - i) / 4f;

                spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                color, projectile.rotation, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);
            }
            position = projectile.Center - Main.screenPosition;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                lightColor * ((float)(255 - projectile.alpha) / 255f), projectile.rotation, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
