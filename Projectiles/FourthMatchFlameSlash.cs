using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class FourthMatchFlameSlash : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 100;

            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            Player player = Main.player[projectile.owner];
            //player.heldProj = projectile.whoAmI;
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
            projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
            projectile.position += projectile.velocity * 113f;
            projectile.direction = player.direction;
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == 1 ? 0 : MathHelper.ToRadians(180));
            
            if (projectile.frameCounter++ >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }

            if (projectile.frame > 5 || player.dead)
                projectile.Kill();

            if (projectile.frame != 5)
            {
                Rectangle dustRect = new Rectangle((int)(projectile.position.X), (int)projectile.position.Y, 100, 100);
                if (projectile.frame == 0 || projectile.frame == 4)
                {
                    Vector2 pos = new Vector2(-134, projectile.frame == 0 ? 0 : 50 * projectile.direction).RotatedBy(projectile.rotation + (projectile.direction == 1 ? 0 : MathHelper.ToRadians(180)));
                    dustRect = new Rectangle((int)(projectile.position.X + pos.X), (int)(projectile.position.Y + pos.Y), 70, 50);
                }
                else if (projectile.frame == 1 || projectile.frame == 3)
                {
                    Vector2 pos = new Vector2(-84, projectile.frame == 1 ? 0 : 50 * projectile.direction).RotatedBy(projectile.rotation + (projectile.direction == 1 ? 0 : MathHelper.ToRadians(180)));
                    dustRect = new Rectangle((int)(projectile.position.X + pos.X), (int)(projectile.position.Y + pos.Y), 100, 50);
                }

                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(dustRect.TopLeft(), dustRect.Width, dustRect.Height, 6, projectile.velocity.X, projectile.velocity.Y, 50, new Color(), Main.rand.NextFloat(1.2f, 2.0f));
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (projectile.frame == 0 || projectile.frame == 4 || projectile.frame == 5)
            {
                Vector2 pos = new Vector2(-134, 0).RotatedBy(projectile.rotation + (projectile.direction == 1 ? 0 : MathHelper.ToRadians(180)));
                hitbox = new Rectangle(hitbox.X + (int)pos.X, hitbox.Y + (int)pos.Y, 70, 100);
            }
            if (projectile.frame == 1 || projectile.frame == 3)
            {
                Vector2 pos = new Vector2(-84, 0).RotatedBy(projectile.rotation + (projectile.direction == 1 ? 0 : MathHelper.ToRadians(180)));
                hitbox = new Rectangle(hitbox.X + (int)pos.X, hitbox.Y + (int)pos.Y, 100, 100);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 pos = projectile.Center - Main.screenPosition;
            Vector2 origin = new Vector2(projectile.direction == 1 ? 184 : 50, 35);
            Rectangle frame = new Rectangle(0, projectile.frame * 70, 234, 70);
            spriteBatch.Draw(Main.projectileTexture[projectile.type], pos, new Rectangle?(frame), Color.White, projectile.rotation, origin, projectile.scale, projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            return false;
        }
    }

}
