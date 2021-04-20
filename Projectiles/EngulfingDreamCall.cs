using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class EngulfingDreamCall : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("WAKE UP");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 192;
            projectile.height = 192;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 62;
            projectile.extraUpdates = 2;

            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            projectile.ai[0]++;

            Player player = Main.player[projectile.owner];
            Vector2 mountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.Center = mountedCenter + new Vector2(16, 0).RotatedBy(projectile.velocity.ToRotation());
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return CanHitRadius(projectile.ai[0] * 3, 8, 8, targetHitbox);
        }

        private bool CanHitRadius(float radius, float min, float max, Rectangle hitbox)
        {
            Vector2 delta = hitbox.Center() - projectile.Center;
            delta.Normalize();
            delta = delta.RotatedBy(3.14f) * (hitbox.Width > hitbox.Height ? hitbox.Width : hitbox.Height);
            delta = hitbox.Center() + delta - projectile.Center;
            return delta.Length() > radius - min && delta.Length() < radius + max;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(target.Center, 1, 1, 16, 4 * (float)Math.Cos((target.Center - projectile.Center).ToRotation()), 4 * (float)Math.Sin((target.Center - projectile.Center).ToRotation()));
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
            Vector2 origin = texture.Size() / 2;
            Color color = Color.White * 0.2f;
            color.A = (byte)(color.A * 0.8f);
            if (projectile.ai[0] > 31)
                color *= (projectile.ai[0] - 31) / 31;
            float scale = (projectile.ai[0]/23f);
            spriteBatch.Draw(texture, position, (Rectangle)texture.Frame(), color, Main.rand.NextFloat(6.28f), origin, scale, 0, 0f);
            return false;
        }
    }
}
