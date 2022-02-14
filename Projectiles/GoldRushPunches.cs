using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class GoldRushPunches : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ora");
        }

		public override void SetDefaults() {
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 0.7f;
            projectile.timeLeft = 8;
            
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;

			projectile.localNPCHitCooldown = 8;
			projectile.usesLocalNPCImmunity = true;

			drawHeldProjInFrontOfHeldItemAndArms = true;
		}

        public override void AI()
        {
			if (projectile.ai[0] == 0)
            {
				projectile.ai[0] = Main.rand.NextFloat(1.50f);
				projectile.ai[1] = Main.rand.Next(-10, 11);
            }
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.spriteDirection = Math.Sign(projectile.velocity.X);
			if (projectile.timeLeft < 4)
			{
				projectile.ai[0] -= 0.75f;
				projectile.alpha += 60;
			}
			else
			{
				projOwner.heldProj = projectile.whoAmI;
				projectile.ai[0]++;
			}

			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.spriteDirection < 0)
				projectile.rotation += MathHelper.ToRadians(180);

			float rot = projectile.velocity.ToRotation();
			Vector2 length = (projectile.ai[0] * projectile.velocity);

			projectile.Center = ownerMountedCenter + new Vector2(0, projectile.ai[1]).RotatedBy(rot) + length;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
			Vector2 origin = new Vector2(25, 11);

			spriteBatch.Draw(Main.projectileTexture[projectile.type], position, Main.projectileTexture[projectile.type].Frame(), lightColor * (1f - projectile.alpha / 255f), projectile.rotation, origin, projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
        }
    }
}
