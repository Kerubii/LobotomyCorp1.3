using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles.QueenLaser
{
	public class ArcanaBeats : ModProjectile
	{
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
            Player player = Main.player[projectile.owner];
            Vector2 mountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            if (player.channel)
            {
                if (projectile.ai[0] < 30)
                    projectile.ai[0]++;
                if (player.whoAmI == Main.myPlayer)
                {
                    projectile.velocity = Main.MouseWorld - mountedCenter;
                    projectile.velocity.Normalize();
                }
                player.itemTime = 16;
                player.itemAnimation = 16;

                if (projectile.ai[1] >= 24)
                {
                    if (player.CheckMana(player.HeldItem, -1, true))
                        Projectile.NewProjectile(projectile.Center, projectile.velocity * 12f, ProjectileID.Starfury, projectile.damage, projectile.knockBack, projectile.owner);
                    else
                        player.channel = false;
                    projectile.ai[1] = 0;
                }
                projectile.ai[1]++;
                projectile.timeLeft = 300;
            }
            else
            {
                if (projectile.ai[0] <= 10)
                {
                    projectile.Kill();
                    Projectile.NewProjectile(projectile.Center, projectile.velocity * 12f, ProjectileID.Starfury, projectile.damage, projectile.knockBack, projectile.owner);
                    return;
                }
                else
                {
                    if (projectile.ai[0] == 11)
                    {
                        projectile.Kill();
                    }

                    else if (projectile.ai[0] < 30)
                    {
                        projectile.ai[0]--;
                    }
                    
                    else
                    {
                        projectile.ai[0]++;

                        if (projectile.ai[0] == 35)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Dust.NewDust(projectile.position, projectile.width, projectile.height, 71, projectile.velocity.X * 2f, projectile.velocity.Y * 2f);
                            }
                            for (int i = 0; i < 60; i++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 86, projectile.velocity.X * Main.rand.NextFloat(8f), projectile.velocity.Y * Main.rand.NextFloat(8f))];
                                dust.noGravity = true;
                                dust.fadeIn = 1.3f + Main.rand.NextFloat(1f);
                            }
                        }
                        if (projectile.ai[0] > 47)
                        {
                            projectile.ai[0] = 29;
                        }
                    }
                }

                player.itemTime = 2;
                player.itemAnimation = 2;
            }
            projectile.Center = mountedCenter + 105f * projectile.velocity;
            projectile.direction = Math.Sign(projectile.velocity.X);
            player.direction = projectile.direction;
            projectile.rotation += MathHelper.ToRadians(3);
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * player.direction, projectile.velocity.X * player.direction);
            player.heldProj = projectile.whoAmI ;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = LobotomyCorp.ArcanaSlaveBackground;
            float rot = projectile.velocity.ToRotation();
            Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
            Vector2 origin = new Vector2(61, 61);
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 scale = new Vector2(0.5f, 1f);
            Color color = Color.White * (1 - ((float)projectile.alpha / 255));

            float mult = 1f;
            MultRange(ref mult, 10, 30);
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color * 0.5f, rot, origin, (scale + new Vector2(0.03f + 0.02f * (float)Math.Sin(projectile.rotation))) * mult * 0.5f, SpriteEffects.None, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            var resizeShader = GameShaders.Misc["LobotomyCorp:Resize"];
            resizeShader.UseOpacity(projectile.rotation / (2 * (float)Math.PI));
            resizeShader.Apply(null);

            texture = mod.GetTexture("Projectiles/QueenLaser/Circle1Color");
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, (scale + new Vector2(0.03f + 0.02f * (float)Math.Sin(projectile.rotation))) * mult, SpriteEffects.None, 0f);

            texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, rot, origin, mult * scale, SpriteEffects.None, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            if (projectile.ai[0] > 35 && projectile.ai[0] < 48)
            {
                if (projectile.localAI[0] == 0)
                {
                    projectile.localAI[0] = Main.rand.Next(1, 360);
                }
                texture = mod.GetTexture("Projectiles/QueenLaser/ArcanaBeatsBlast");
                origin = new Vector2(8, 25);
                //int frameY = (int)((projectile.ai[0] - 35) /2);
                frame = new Rectangle(0, 0, 70, 50);
                float BlastScale = 1.1f * (float)Math.Sin(MathHelper.ToRadians(10 * (projectile.ai[0] -  35)));
                if (projectile.ai[0] > 40)
                    color *= 1 - ((projectile.ai[0] - 40) / 7);
                for (int i = 0; i < 9 ; i++)
                {
                    if (i == 4) continue;
                    float BeatRotation = MathHelper.ToRadians(-95 + 23.75f * i + 4 * (float)Math.Sin(MathHelper.ToRadians(projectile.localAI[0] * i))) + rot;
                    spriteBatch.Draw(texture, position, (Rectangle?)(frame), color, BeatRotation, origin, mult * BlastScale, SpriteEffects.None, 0f);
                }
            }

            return false;
        }

        private void MultRange(ref float mult , float min, float max)
        {
            if (projectile.ai[0] < min)
                mult = 0;
            else if (projectile.ai[0] > max)
                mult = 1;
            else
                mult = (projectile.ai[0] - min) / (max - min);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return projectile.ai[0] >= 35;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            //80
            Vector2 center = projectile.Center + projectile.velocity * 25f;
            hitbox = new Rectangle((int)projectile.Center.X - 50, (int)projectile.Center.Y - 50, 100, 100);
        }
    }
}
