using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class AlriuneDeathAnimation : ModProjectile
	{
        public override void SetDefaults()
        {
            projectile.width = 108;
            projectile.height = 1080;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 300;
            
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }    
            
        public override void AI() {
            projectile.ai[1]++;

            if (projectile.ai[0] > -1)
            {
                NPC n = Main.npc[(int)projectile.ai[0]];
                if (!n.active || n.life <= 0)
                {
                    projectile.ai[0] = -1;
                }
                projectile.width = n.width + 30;
                projectile.height = n.height + 30;
                projectile.Center = n.Center;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("Misc/AlriuneDeathAnimationCurtain");
            Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
            Vector2 origin = new Vector2(texture.Width / 2, 0); 
            Color color = Color.White;
            Vector2 scale = new Vector2(1f, 1f);
            for (int i = -3; i < 0; i++)
            {
                scale = new Vector2(1f, 0f);
                scale.X *= (((float)projectile.width / 4f) / (float)texture.Width);
                if (projectile.ai[1] > 60 && projectile.ai[1] < 240)
                {
                    if (projectile.ai[1] > 180 && projectile.ai[1] < 240)
                        AnimationHelper(ref scale.Y, projectile.ai[1], 180, 240, true);
                    else if (projectile.ai[1] < 120)
                        AnimationHelper(ref scale.Y, projectile.ai[1], 60, 120);
                    else
                        scale.Y = 1f;
                }
                Vector2 Offset = new Vector2(((float)projectile.width / 7 * i) - ((float)projectile.width / 14) * Math.Sign(i), (-(float)projectile.height / 6) * 2);
                scale.Y *= (((float)projectile.height - (float) projectile.height / 6)/ 74);
                float startPosition = (projectile.position.X + ((float)projectile.width / 14) * (4 + i)) - (projectile.Center.X + Offset.X);
                float time = 1f;
                if (projectile.ai[1] < 120)
                {
                    time = 1f;
                }
                else if (projectile.ai[1] < 180)
                {
                    AnimationHelper( ref time, projectile.ai[1], 120, 180, true);
                }
                else
                {
                    time = 0f;
                }
                Offset.X += startPosition * time;

                position = projectile.Center + Offset - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
                spriteBatch.Draw(texture, position, (Rectangle)texture.Frame(), color, 0, origin, scale, 0, 0f);

                Offset.X *= -1;
                position = projectile.Center + Offset - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
                spriteBatch.Draw(texture, position, (Rectangle)texture.Frame(), color, 0, origin, scale, 0, 0f);
            }

            texture = Main.projectileTexture[projectile.type];
            origin = texture.Size() / 2;
            scale = new Vector2(1f, 1f);

            for (int i = -2; i < 0; i++)
            {
                scale = new Vector2(1f, 1f);
                if (projectile.ai[1] < 60)
                {
                    AnimationHelper(ref scale.Y, projectile.ai[1], 0 - 6 * i, 60);
                }
                else if (projectile.ai[1] > 240)
                {
                    AnimationHelper(ref scale.Y, projectile.ai[1], 240 + 6 * (i + 2), 300, true);
                }
                else
                {
                    scale.Y = 1f;
                }
                scale.Y *= (((float)projectile.height / 6f) / 18f);
                scale.X *= (((float)projectile.width / 5f) / 34f);

                position = projectile.Center + new Vector2(projectile.width/5 * i, (-projectile.height/6) * 2) - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
                spriteBatch.Draw(texture, position, (Rectangle)texture.Frame(), color, 0, origin, scale, 0, 0f);

                position = projectile.Center + new Vector2(projectile.width / 5 * -i, (-projectile.height / 6) * 2) - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
                spriteBatch.Draw(texture, position, (Rectangle)texture.Frame(), color, 0, origin, scale, 0, 0f);
            }
            scale = new Vector2(1f, 1f);
            if (projectile.ai[1] < 60)
            {
                AnimationHelper(ref scale.Y, projectile.ai[1], 0, 60);
            }
            else if (projectile.ai[1] > 240)
            {
                AnimationHelper(ref scale.Y, projectile.ai[1], 252, 300, true);
            }
            else
            {
                scale.Y = 1f;
            }
            scale.Y *= (((float)projectile.height / 6f) / 18f);
            scale.X *= (((float)projectile.width / 5f) / 34f);
            position = projectile.Center + new Vector2( 0, (-projectile.height / 6) * 2) - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
            spriteBatch.Draw(texture, position, (Rectangle)texture.Frame(), color, 0, origin, scale, 0, 0f);

            return false;
        }

        private void AnimationHelper(ref float scale, float timer,float min, float max, bool reverse = false)
        {
            if (!reverse)
            {
                if (timer < min)
                    scale = 0f;
                else if (timer > max)
                    scale = 1f;
                else
                    scale = (timer - min) / (max - min);
            }
            else
            {
                if (timer < min)
                    scale = 1f;
                else if (timer > max)
                    scale = 0f;
                else
                    scale = 1f - (timer - min) / (max - min);
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = target.life * 2;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return target.whoAmI == (int)projectile.ai[0] && projectile.ai[1] == 180;
        }
    }
}
