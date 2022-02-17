using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class StarShot : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

		public override void SetDefaults() {
			projectile.width = 24;
			projectile.height = 22;
			projectile.aiStyle = -1;
            projectile.alpha = 170;
			projectile.penetrate = 1;
            projectile.timeLeft = 660;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
		}

        public override void AI()
        {
            /*Player projOwner = Main.player[projectile.owner];
            if (projectile.ai[0] < 3)
                foreach (Player teammate in Main.player)
                {
                    if (teammate.active && teammate.team == projOwner.team && teammate.whoAmI != projOwner.whoAmI && projectile.getRect().Intersects(teammate.Hitbox))
                    {
                        if (projectile.ai[0] == 1)
                        {

                        }
                    }
                }*/
            projectile.rotation += 0.12f * Math.Sign(projectile.velocity.X);
            if (Main.rand.Next(4) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, default(Color), 1.2f);
                Gore.NewGore(projectile.position, new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            for (int num107 = 0; num107 < 10; num107++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, default(Color), 1.2f);
            }
            for (int num108 = 0; num108 < 3; num108++)
            {
                Gore.NewGore(projectile.position, new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = LobotomyCorp.RedDamage;
            switch (projectile.ai[0])
            {
                case 1:
                    color = LobotomyCorp.WhiteDamage;
                    break;
                case 2:
                    color = LobotomyCorp.BlackDamage;
                    break;
                case 3:
                    color = LobotomyCorp.PaleDamage;
                    break;
            }
            color.A = (byte)(color.A * (float)(1f - projectile.alpha / 255f));
            return color;
        }
        /*
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
            }

            float scale = 0.9f;
            Color color2 = Color.LightBlue;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0,projectile.gfxOffY), (Rectangle?)frame, color2, projectile.rotation, origin, scale, SpriteEffects.None, 0f);
            return false;
        }*/
    }
}
