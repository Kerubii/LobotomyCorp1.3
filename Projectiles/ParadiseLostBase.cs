using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class ParadiseLostBase : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Judgement");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 60;
            projectile.alpha = 255;

            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            if (projectile.localAI[0]++ == 0)
            {
                //projectile.localAI[0] = Main.rand.Next(3) + 1;
                projectile.spriteDirection = Main.rand.Next(2) == 0 ? -1 : 1;
                projectile.scale = 0f;
                projectile.alpha = 0;
                projectile.localAI[1] = Main.rand.NextFloat(3.1432179865f);

                Vector2 speed = Vector2.Normalize(projectile.velocity) * 8;
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, speed.X, speed.Y, 0, default(Color), 1.5f)];
                    dust.fadeIn = 1.25f;
                    dust.noGravity = true;
                }
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            if (projectile.timeLeft < 8)
            {
                projectile.scale -= 0.125f;
                return;
            }

            if (projectile.scale < 1f)
                projectile.scale += 0.125f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            bool hit = false;
            for (int i = -1; i < 2; i++)
            {
                Vector2 origin = projectile.Center + new Vector2( 6 * i, projectile.height / 2 - 2);
                float scale = 1f;
                if (i < 0)
                    scale = 0.8f;
                if (i > 0)
                    scale = 0.9f;
                float rotOffset = MathHelper.ToRadians(15 * i);
                Vector2 endpoint = origin + new Vector2(128 * scale, 0).RotatedBy(projectile.velocity.ToRotation() + rotOffset);
                if (Collision.CheckAABBvLineCollision2(targetHitbox.TopLeft(), targetHitbox.Size(), origin, endpoint))
                {
                    hit = true;
                    break;
                }
            }
            return hit;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 speed = Vector2.Normalize(projectile.velocity) * 4;
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, speed.X, speed.Y, 0, default(Color), 1)];
                dust.fadeIn = 1.25f;
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            string texname = "Projectiles/Paradise";
            switch(Math.Abs(projectile.ai[0] % 3))
            {
                case 1:
                    texname += "Scythe";
                    break;
                case 2:
                    texname += "Spear";
                    break;
                default:
                    texname += "Staff";
                    break;
            }
            Texture2D tex = mod.GetTexture(texname);
            Vector2 Position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.height / 2);//.RotatedBy(projectile.rotation);
            Vector2 origin = tex.Size();
            origin.X /= 2;
            SpriteEffects effect = projectile.spriteDirection > 0 ? SpriteEffects.FlipHorizontally : 0;
            for (int i = -1; i < 2; i++)
            {
                float rotOffset = MathHelper.ToRadians(15 * i);
                Vector2 posOffset = new Vector2(6 * i, -2);//.RotatedBy(projectile.rotation + rotOffset);
                float scale = 1f;
                if (i < 0)
                    scale = 0.8f;
                if (i > 0)
                    scale = 0.9f;
                spriteBatch.Draw(tex, Position + posOffset, tex.Frame(), lightColor, projectile.rotation + rotOffset, origin, projectile.scale * scale, effect, 0f);
            }
            tex = mod.GetTexture("Projectiles/ParadiseScythe");
            float scalep = 0.3f;
            float extra = 0;
            if (projectile.ai[0] < 0)
                extra = MathHelper.ToRadians(30);
            else if (projectile.ai[0] > 0)
                extra = -MathHelper.ToRadians(30);
            float RotOffset = MathHelper.ToRadians(30) * (float)Math.Sin(projectile.localAI[1]) + extra;
            Vector2 PosOffset = new Vector2(8 * (float)Math.Sin(projectile.localAI[1]), 0);
            spriteBatch.Draw(tex, Position + PosOffset, tex.Frame(), lightColor, RotOffset, origin, projectile.scale * scalep, effect, 0f);

            tex = mod.GetTexture("Projectiles/ParadiseSpear");
            RotOffset = MathHelper.ToRadians(30) * (float)Math.Sin(projectile.localAI[1] + 2.0944f) + extra;
            PosOffset.X = (8 * (float)Math.Sin(projectile.localAI[1] + 2.0944f));
            spriteBatch.Draw(tex, Position + PosOffset, tex.Frame(), lightColor, RotOffset, origin, projectile.scale * scalep, effect, 0f);

            tex = mod.GetTexture("Projectiles/ParadiseStaff");
            RotOffset = MathHelper.ToRadians(30) * (float)Math.Sin(projectile.localAI[1] + 2.0944f * 2) + extra;
            PosOffset.X = (8 * (float)Math.Sin(projectile.localAI[1] + 2.0944f * 2));
            spriteBatch.Draw(tex, Position + PosOffset, tex.Frame(), lightColor, RotOffset, origin, projectile.scale * scalep, effect, 0f);

            tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(tex, Position, tex.Frame(), lightColor, 0, origin, projectile.scale, effect, 0f);
            return false;
        }
    }
}
