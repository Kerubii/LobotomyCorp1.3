using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles.QueenLaser
{
	public class Circle1 : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Arcana Slave");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 300;

            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            projectile.ai[1]++;

            projectile.rotation += MathHelper.ToRadians(3);
            if (projectile.rotation > (float)Math.PI * 2)
                projectile.rotation -= (float)Math.PI * 2;

            //86
            //71
            if (projectile.timeLeft < 30)
                projectile.alpha += 15;

            if (projectile.ai[1] > 60)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 71, projectile.velocity.X * 2f, projectile.velocity.Y * 2f);
                }

                for (projectile.ai[0] = 0; projectile.ai[0] <= 2200f; projectile.ai[0] += 5f)
                {
                    var start = projectile.Center + projectile.velocity * projectile.ai[0];
                    if (!Collision.CanHit(projectile.Center, 1, 1, start, 1, 1))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Dust.NewDust(start - new Vector2 (15, 15), 30, 30, 86, -projectile.velocity.X * 2f, -projectile.velocity.Y * 2f);
                        }
                        projectile.ai[0] -= 5f;
                        break;
                    }
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[1] < 60)
                return false;
            
            Vector2 unit = projectile.velocity;
            float point = 0f;
            // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
            // It will look for collisions on the given line using AABB
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center,
                projectile.Center + unit * projectile.ai[0], 22, ref point);
        }


        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = LobotomyCorp.ArcanaSlaveBackground;
            float rot = projectile.velocity.ToRotation();
            Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY) - new Vector2(32f, 0).RotatedBy(rot);
            Vector2 origin = new Vector2(61, 61);
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 scale = new Vector2(0.5f, 1f);
            Color color = Color.White * (1 - ((float)projectile.alpha / 255));

            float mult = 1f;
            MultRange(ref mult, 0, 13);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, (scale + new Vector2(0.03f + 0.02f * (float)Math.Sin(projectile.rotation))) * mult, SpriteEffects.None, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            var resizeShader = GameShaders.Misc["LobotomyCorp:Resize"];
            resizeShader.UseOpacity(projectile.rotation / (2 * (float)Math.PI));
            resizeShader.Apply(null);

            texture = mod.GetTexture("Projectiles/QueenLaser/Circle1Color");
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, (scale + new Vector2(0.03f + 0.02f * (float)Math.Sin(projectile.rotation))) * mult, SpriteEffects.None, 0f);

            texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale, SpriteEffects.None, 0f);

            resizeShader.UseOpacity(1f - (projectile.rotation / (2 * (float)Math.PI)));
            resizeShader.Apply(null);

            texture = mod.GetTexture("Projectiles/QueenLaser/Circle1Outer");
            MultRange(ref mult, 6, 24);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale, SpriteEffects.None, 0f);

            resizeShader.UseOpacity(projectile.rotation / (2 * (float)Math.PI));
            resizeShader.Apply(null);

            origin = new Vector2(63, 63);
            frame = new Rectangle(0, 0, texture.Width, texture.Height);

            position += new Vector2(32f, 0).RotatedBy(rot);
            texture = mod.GetTexture("Projectiles/QueenLaser/Circle2");
            MultRange(ref mult, 20, 42);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale, SpriteEffects.None, 0f);

            position += new Vector2(32f, 0).RotatedBy(rot);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            origin = new Vector2(61, 61);
            frame = new Rectangle(0, 0, texture.Width, texture.Height);

            SpriteEffects spriteeffect = rot > 1.57f || rot < -1.57f ? SpriteEffects.FlipVertically : SpriteEffects.None;

            string Side = "R";

            texture = mod.GetTexture("Projectiles/QueenLaser/HeartWing" + Side);
            MultRange(ref mult, 38, 50);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, (scale + new Vector2(0.05f + 0.025f * (float)Math.Cos(projectile.rotation)) * 1.2f) * mult, spriteeffect, 0f);
            texture = mod.GetTexture("Projectiles/QueenLaser/HeartColor" + Side);
            MultRange(ref mult, 40, 60);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale * 1.2f, spriteeffect, 0f);
            texture = mod.GetTexture("Projectiles/QueenLaser/HeartOutline" + Side);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale * 1.2f, spriteeffect, 0f);

            if (projectile.ai[1] >= 60)
            {
                float alpha = 1f;
                float laserScale = 1f;
                if (projectile.ai[1] < 90)
                {
                    alpha = (projectile.ai[1] - 60f) / 30f;
                    laserScale = 0.2f + 0.8f * ((projectile.ai[1] - 60) / 30);
                }
                else
                    laserScale += 0.05f * (float)Math.Cos(projectile.rotation);
                float step = 8f * laserScale;
                texture = LobotomyCorp.ArcanaSlaveLaser;
                for (float i = 8; i <= projectile.ai[0]; i += step)
                {
                    Color c = Color.White;
                    origin = projectile.Center + i * projectile.velocity;
                    spriteBatch.Draw(texture, origin - Main.screenPosition,
                        new Rectangle(0, 0, 60, 8), Color.White * alpha * (1 - ((float)projectile.alpha / 255)), rot + 1.57f,
                        new Vector2(30, 4), laserScale, 0, 0);
                }
            }

            Side = "L";
            origin = new Vector2(61, 61);

            texture = mod.GetTexture("Projectiles/QueenLaser/HeartWing" + Side);
            MultRange(ref mult, 38, 50);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, (scale + new Vector2(0.05f + 0.025f * (float)Math.Cos(projectile.rotation)) * 1.2f) * mult, spriteeffect, 0f);
            texture = mod.GetTexture("Projectiles/QueenLaser/HeartColor" + Side);
            MultRange(ref mult, 40, 60);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale * 1.2f, spriteeffect, 0f);
            texture = mod.GetTexture("Projectiles/QueenLaser/HeartOutline" + Side);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale * 1.2f, spriteeffect, 0f);

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        private void MultRange(ref float mult , float min, float max)
        {
            if (projectile.ai[1] < min)
                mult = 0;
            else if (projectile.ai[1] > max)
                mult = 1;
            else
                mult = (projectile.ai[1] - min) / (max - min);
        }
    }
}
