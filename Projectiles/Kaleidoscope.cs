using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class Kaleidoscope : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Kaleidoscope of Butterflies");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 300;

            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }

        public override void AI()
        {
            if (projectile.ai[1] < 30)
            {
                projectile.ai[1]++;
            }
            else
            {
                if (projectile.ai[0] >= 0)
                {
                    NPC target = Main.npc[(int)projectile.ai[0]];
                    if (target.active && target.life > 0)
                    {
                        Vector2 delt = target.Center - projectile.Center;
                        delt.Normalize();
                        delt *= 6f;

                        float speed = 0.3f;
                        if (projectile.position.X < target.position.X)
                        {
                            projectile.velocity.X += speed;
                            if (projectile.velocity.X < 0)
                                projectile.velocity.X += speed;
                            if (projectile.velocity.X > delt.X)
                                projectile.velocity.X = delt.X;
                        }
                        else if (projectile.position.X > target.position.X)
                        {
                            projectile.velocity.X -= speed;
                            if (projectile.velocity.X > 0)
                                projectile.velocity.X -= speed;
                            if (projectile.velocity.X < delt.X)
                                projectile.velocity.X = delt.X;
                        }

                        if (projectile.position.Y < target.position.Y)
                        {
                            projectile.velocity.Y += speed;
                            if (projectile.velocity.Y < 0)
                                projectile.velocity.Y += speed;
                            if (projectile.velocity.Y > delt.Y)
                                projectile.velocity.Y = delt.Y;
                        }
                        else if (projectile.position.Y > target.position.Y)
                        {
                            projectile.velocity.Y -= speed;
                            if (projectile.velocity.Y > 0)
                                projectile.velocity.Y -= speed;
                            if (projectile.velocity.Y < delt.Y)
                                projectile.velocity.Y = delt.Y;
                        }
                    }
                }
                else
                    projectile.ai[0] = -1;
            }            

            projectile.rotation = (projectile.velocity.X / 6f) * (float)MathHelper.ToRadians(60);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);

            if (projectile.frameCounter <= 24)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 18)
                    projectile.frame = 1;
                else
                    projectile.frame = (int)Math.Floor(projectile.frameCounter / 6f);
            }
            else
                projectile.frameCounter = 0;
        }
    }
}
