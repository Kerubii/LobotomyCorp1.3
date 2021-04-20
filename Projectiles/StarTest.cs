using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class StarTest : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 120;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

		public override void SetDefaults() {
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
            //projectile.alpha = 0;
			projectile.penetrate = -1;
            projectile.timeLeft = 660;
			projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
		}
        
		public override void AI() {
            //Opacity = Utils.GetLerpValue(0f, 60f, timeLeft, clamped: true) * Utils.GetLerpValue(660f, 600f, timeLeft, clamped: true);
            float num = (float)System.Math.PI / 360f;
            float num2 = 30f;
            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[0]);
            if (projectile.ai[0] < num)
            {
                projectile.ai[0] += num / num2;
            }
            projectile.rotation = projectile.velocity.ToRotation() + (float)System.Math.PI / 2f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            lightColor = Color.LightBlue;
            lightColor.A = 255;
            return lightColor;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Rectangle frame = new Rectangle(0, 0, 144, 374);
            Vector2 origin = frame.Size() / 2;
            frame.Width = frame.Width/2;
            origin.X /= 2f;

            Rectangle frame2 = frame;
            frame2.X += frame.Width;

            /*for (int i = 79; i > 0; i--)
            {
                Color color = Color.Blue;
                float n = 79 - i;
                color *= n / (120f * 1.5f);
                Vector2 oldPos = projectile.oldPos[i];
                float rot = projectile.oldRot[i];
                Vector2 pos = oldPos + (projectile.Size / 2f) - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
                spriteBatch.Draw(tex, pos, (Rectangle?)frame2, color, rot, origin, 1f, SpriteEffects.None, 0f);
            }*/

            float scale = 0.9f;
            Color color2 = Color.LightBlue;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0,projectile.gfxOffY), (Rectangle?)frame, color2, projectile.rotation, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
