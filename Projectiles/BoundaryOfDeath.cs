using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
	public class BoundaryOfDeath : ModProjectile
	{
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 220;
            
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }    
            
        public override void AI() {
            projectile.ai[1]++;
			
			Player player = Main.player[projectile.owner];
			
			player.itemAnimation = 2;
			player.itemTime = 2;
			player.heldProj = projectile.whoAmI;
			
            if (projectile.ai[0] > -1)
            {
                NPC n = Main.npc[(int)projectile.ai[0]];
                if (!n.active || n.life <= 0)
                {
                    projectile.ai[0] = -1;
					return;
                }
                projectile.Center = n.Center;
				if (n.life < (int)(projectile.damage * 44.44f))
				{
					LobotomyGlobalNPC.LNPC(n).BODExecute = true;
					n.velocity *= 0;
					n.noGravity = true;
				}
				if (projectile.ai[1] == 90)
					n.StrikeNPC((int)(projectile.damage * 44.44f) + n.defense/2, 0f, 1, true);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
			Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height/6);
            Vector2 origin = new Vector2(texture.Width / 2, frame.Height / 2); 
            Color color = Color.White;
			//Background
			Vector2 Offset = new Vector2(24f, -8f);
			float scaleOffset = 1f;
			AnimationHelper(ref scaleOffset, projectile.ai[1], 90, 105);
			Offset *= scaleOffset;
			AnimationHelper(ref scaleOffset, projectile.ai[1], 105, 190);
			Offset *= 1f + 0.3f * scaleOffset;
			float Alpha = 1f;
			AnimationHelper(ref Alpha, projectile.ai[1], 120, 160, true);
			color *= Alpha;
            spriteBatch.Draw(texture, position + Offset, (Rectangle?)frame, color, 0, origin, projectile.scale, 0, 0f);
			
			Offset *= -1;
			frame.Y += frame.Height;
            spriteBatch.Draw(texture, position + Offset, (Rectangle?)frame, color, 0, origin, projectile.scale, 0, 0f);
			
			if (projectile.ai[1] < 90)
			{
				//Initial Slash
				frame.Y += frame.Height;
				spriteBatch.Draw(texture, position, (Rectangle?)frame, color, 0, origin, projectile.scale, 0, 0f);
				//RedPart
				frame.Y += frame.Height;
				float RedScale = 0f;
				if (projectile.ai[1] > 10)
				{
					Vector2 RedOffset = new Vector2(-10, 6);
					AnimationHelper(ref RedScale, projectile.ai[1], 10, 30, true);
					RedScale = 1f + RedScale * 1f;
					float RedAlpha = 1f;
					AnimationHelper(ref Alpha, projectile.ai[1], 10, 20);
					color *= RedAlpha;
					spriteBatch.Draw(texture, position + RedOffset, (Rectangle?)frame, color, 0, origin + RedOffset, projectile.scale * RedScale, 0, 0f);
				}
			}
			else
			{
				AnimationHelper(ref Alpha, projectile.ai[1], 160, 175, true);
				color = Color.White * Alpha;
				
				//Slash Split
				Offset *= -1;
				frame.Y += frame.Height * 3;
				spriteBatch.Draw(texture, position + Offset, (Rectangle?)frame, color, 0, origin, projectile.scale, 0, 0f);
				
				Offset *= -1;
				frame.Y += frame.Height;
				spriteBatch.Draw(texture, position + Offset, (Rectangle?)frame, color, 0, origin, projectile.scale, 0, 0f);
			}
			
			Player player = Main.player[projectile.owner];
			texture = mod.GetTexture("Projectiles/BoundaryOfDeathSword");
			position = player.RotatedRelativePoint(player.MountedCenter, true) - Main.screenPosition;
			origin = new Vector2(30,32);
			Offset = new Vector2(12 * player.direction, 16);
			AnimationHelper(ref scaleOffset, projectile.ai[1], 80, 85, true);
			
			spriteBatch.Draw(texture, position + Offset + new Vector2(0, -8 *scaleOffset), (Rectangle?)texture.Frame(), lightColor, MathHelper.ToRadians(135), origin, projectile.scale, 0, 0f);
			texture = mod.GetTexture("Projectiles/BoundaryOfDeathSheath");
			spriteBatch.Draw(texture, position + Offset, (Rectangle?)texture.Frame(), lightColor, MathHelper.ToRadians(135), origin, projectile.scale, 0, 0f);

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

        /*public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = target.life * 2;
        }*/

        public override bool? CanHitNPC(NPC target)
        {
            return false;//target.whoAmI == (int)projectile.ai[0] && projectile.ai[1] == 180;
        }
    }
}
