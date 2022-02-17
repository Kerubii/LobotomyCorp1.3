using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LobotomyCorp.Utils;
using Terraria.Graphics.Shaders;

namespace LobotomyCorp.Projectiles
{
	public class RealizedSwordSharpenedWithTearsProj : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

		public override void SetDefaults() {
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 16;

			//projectile.hide = true;
			//projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
		}

		private float state
        {
            get => projectile.ai[1];
            set => projectile.ai[1] = value;
        }

        public float order
        {
            get => projectile.ai[0];
        }

        private bool playerAttack(bool rightClick = false)
        {
            Player player = Main.player[projectile.owner];
            return player.itemAnimation == player.itemAnimationMax - 1 && (rightClick ? player.altFunctionUse == 2: true);
        }

		public override void AI() {
            if (state < 2)
            {
                state++;
            }
            if (state == 2)
            {
                Player player = Main.player[projectile.owner];

                projectile.rotation = (Main.MouseWorld - projectile.Center).ToRotation() + MathHelper.ToRadians(135);

                Vector2 Pos = player.Center + new Vector2(24, 0).RotatedBy((Main.MouseWorld - projectile.Center).ToRotation());
                Pos.Y -= 76;
                Pos.X += 64 * order;
                if (order != 0)
                    Pos.Y += 16;

                float speed = 4f;
                Vector2 delta = Pos - projectile.Center;
                float mag = 1f;
                if (delta.Length() > 0f)
                    mag = speed / delta.Length();
                if (delta.Length() > speed)
                    delta *= mag;

                projectile.velocity = delta;
                projectile.timeLeft = 300;

                if (player.HeldItem.type != mod.ItemType("SwordSharpenedWithTearsS"))
                    projectile.Kill();

                projectile.localAI[0]++;
                if (projectile.localAI[0] > 360)
                    projectile.localAI[0] = 0;
                projectile.gfxOffY = 8 * (float)Math.Sin(MathHelper.ToRadians(projectile.localAI[0]));

                if (playerAttack(true))
                {
                    projectile.velocity = new Vector2(16, 0).RotatedBy(projectile.rotation - MathHelper.ToRadians(135));
                    Projectile.NewProjectile(player.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, order);
                    projectile.tileCollide = true;
                    state++;
                }
                else if (playerAttack())
                {
                    if (LobotomyModPlayer.ModPlayer(player).RealizedSword != order || LobotomyModPlayer.ModPlayer(player).RealizedSwordShoot)
                        return;
                    LobotomyModPlayer.ModPlayer(player).RealizedSword++;
                    if (LobotomyModPlayer.ModPlayer(player).RealizedSword > 1)
                        LobotomyModPlayer.ModPlayer(player).RealizedSword = -1;
                    LobotomyModPlayer.ModPlayer(player).RealizedSwordShoot = true;
                    projectile.velocity = new Vector2(16, 0).RotatedBy(projectile.rotation - MathHelper.ToRadians(135));
                    Projectile.NewProjectile(player.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, order);
                    projectile.tileCollide = true;
                    state++;
                }
            }
            else if (state == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15);
                    Main.dust[d].fadeIn = 0.15f;
                    Main.dust[d].noGravity = false;
                }
                if (projectileColliding() || projectile.timeLeft == 1)
                {
                    projectile.velocity *= 0;
                    projectile.timeLeft = 300;
                    projectile.tileCollide = false;
                    state = 5;
                }
            }
            else if (state == 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15);
                    Main.dust[d].fadeIn = 0.15f;
                    Main.dust[d].noGravity = false;
                }
                projectile.tileCollide = false;
                if (projectile.timeLeft < 10)
                    projectile.alpha += 25;
            }
            else if (state >= 5)
            {
                Player player = Main.player[projectile.owner];
                float rotation = state - 5;
                if (state == 5)
                    state = 5 + (float)MathHelper.ToRadians(Main.rand.Next(1, 361));
                if (projectile.timeLeft > 260)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int d = Dust.NewDust(player.Center + new Vector2(320, 0).RotatedBy(rotation + Math.PI), projectile.width, projectile.height, 15);
                        Main.dust[d].fadeIn = 0.15f;
                        Main.dust[d].noGravity = false;
                    }
                    //projectile.rotation += MathHelper.ToRadians(10 * (projectile.timeLeft % 2 == 0 ? 1 : -1));
                    if (projectile.timeLeft < 270)
                        projectile.alpha -= 25;
                }
                else
                {
                    Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("DespairSword"), projectile.damage, projectile.knockBack, projectile.owner, rotation);
                    projectile.Kill();
                }
            }
		}

        private bool projectileColliding()
        {
            bool collide = false;
            Vector2 targetPos = projectile.position;
            for (int i = (int)targetPos.X; i < targetPos.X + projectile.width; i += 16)
            {
                for (int j = (int)targetPos.Y; j < targetPos.Y + projectile.width; j += 16)
                {
                    if (Main.tile[i/16, j/16].active() && Main.tileSolid[Main.tile[i/16, j/16].type] && !Main.tileSolidTop[Main.tile[i / 16, j / 16].type])
                        collide = true;
                }
            }
            return collide;
        }

        public override bool CanDamage()
        {
            return state == 3 || state == 4;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            OnHitDissapear();
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            OnHitDissapear();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            OnHitDissapear();
        }

        private void OnHitDissapear()
        {
            state = 4;
            if (projectile.timeLeft > 30)
                projectile.timeLeft = 15;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = true;
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 position = projectile.Center - Main.screenPosition;
            float alpha = ((float)(255f - (float)projectile.alpha) / 255f);
            if (state > 2)
                for (int i = 0; i < 4; i++)
                {
                    //Texture2D texture = mod.GetTexture("Projectiles/SwordSharpenedWithTearsGlow");
                    position = projectile.oldPos[i] + projectile.Size / 2 - Main.screenPosition;

                    Color color = lightColor * ((float)(255 - projectile.alpha) / 255f) * 0.5f;
                    color *= (4f - i) / 4f;

                    spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                            (
                                new Rectangle
                                (
                                    0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                )
                            ),
                    color * alpha, projectile.rotation, projectile.Size / 2, projectile.scale, SpriteEffects.None, 0f);

                    /*
                    spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?
                                        (
                                            new Rectangle
                                            (
                                                0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                            )
                                        ),
                    color, projectile.rotation, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);*/
                }
            position = projectile.Center - Main.screenPosition;
            position.Y += projectile.gfxOffY;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                lightColor * alpha, projectile.rotation, projectile.Size/2, projectile.scale, SpriteEffects.None, 0f);

            /*spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);*/

            //GameShaders.Misc["TrailingShader"].Apply();

            //SlashTrail trail = new SlashTrail();
            //trail.DrawTrail(projectile, LobotomyCorp.LobcorpShaders["TwilightSlash"]);

            return false;
        }
    }

    public class DespairSword : ModProjectile
    {
        public override string Texture => "LobotomyCorp/Projectiles/RealizedSwordSharpenedWithTearsProj";

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.alpha = 255;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            if (projectile.ai[1] < 60)
            {
                Player player = Main.player[projectile.owner];
                projectile.ai[1]++;
                projectile.Center = player.Center + new Vector2(320, 0).RotatedBy(projectile.ai[0] + Math.PI);
                projectile.rotation = projectile.ai[0] + MathHelper.ToRadians(135);
                if (projectile.alpha > 0)
                    projectile.alpha -= 25;
            }
            else if (projectile.ai[1] == 60)
            {
                projectile.ai[1]++;
                projectile.velocity = new Vector2(10, 0).RotatedBy(projectile.ai[0]);
            }
            else
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15);
                Main.dust[d].fadeIn = 0.15f;
                Main.dust[d].noGravity = false;
            }
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.whoAmI == projectile.owner;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 position = projectile.Center - Main.screenPosition;
            float alpha = ((float)(255f - (float)projectile.alpha) / 255f);
            for (int i = 0; i < 4; i++)
            {
                //Texture2D texture = mod.GetTexture("Projectiles/SwordSharpenedWithTearsGlow");
                position = projectile.oldPos[i] + projectile.Size / 2 - Main.screenPosition;

                Color color = lightColor * ((float)(255 - projectile.alpha) / 255f) * 0.5f;
                color *= (4f - i) / 4f;

                spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                        (
                            new Rectangle
                            (
                                0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                            )
                        ),
                color * alpha, projectile.rotation, projectile.Size / 2, projectile.scale, SpriteEffects.None, 0f);

                /*
                spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                color, projectile.rotation, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);*/
            }
            position = projectile.Center - Main.screenPosition;
            position.Y += projectile.gfxOffY;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                lightColor * alpha, projectile.rotation, projectile.Size / 2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
