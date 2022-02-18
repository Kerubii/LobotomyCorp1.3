using LobotomyCorp.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class SmileSpecial : ModProjectile
    {
        public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

        public override void SetDefaults() {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;
            projectile.timeLeft = 600;

            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.extraUpdates = 2;
        }

        //private Vector2 PreviousPosition;

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.spriteDirection = Math.Sign(projectile.velocity.X);
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            /*if (projectile.ai[1] == 0)
            {
                PreviousPosition = projOwner.position;
                projectile.ai[1] = 1;
            }*/

            int dir = projectile.spriteDirection;

            if (projectile.ai[0] == 0) //Ready Swing
            {
                projectile.scale = 0.8f;
                projectile.ai[1]++;
                if (projectile.ai[1] > 8)
                {
                    projOwner.velocity.Y -= 6f;
                    projectile.ai[0]++;
                    projectile.ai[1] = 0;
                }
                projectile.rotation = MathHelper.ToRadians(dir == 1 ? 45 : 135);
            }
            else if (projectile.ai[0] == 1) //Raise up
            {
                if (projectile.scale < 1f)
                    projectile.scale += 0.01f;
                if ((dir ==  1 && projectile.rotation > MathHelper.ToRadians(-140)) ||
                    (dir == -1 && projectile.rotation < MathHelper.ToRadians(320)))
                {
                    projectile.rotation -= MathHelper.ToRadians(4) * dir;
                }
                else
                {
                    projectile.scale = 1f;
                    projOwner.velocity.Y = 6f;
                    projectile.ai[0]++;
                }
            }
            else if (projectile.ai[0] == 2) //Swing Downwards
            {
                for (int i = 0; i < 3; i++)
                {
                    Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Wraith)].noGravity = true;
                }

                if ((dir ==  1 && projectile.rotation < MathHelper.ToRadians(135)) ||
                    (dir == -1 && projectile.rotation > MathHelper.ToRadians(45)))
                {
                    projectile.rotation += MathHelper.ToRadians(6) * dir;
                }
                else
                {
                    projectile.ai[0] += 2;
                }
                Vector2 nextPos = ownerMountedCenter + 110 * (new Vector2(1, 0).RotatedBy(projectile.rotation + MathHelper.ToRadians(6 * dir)));
                if (projectile.rotation > MathHelper.ToRadians(-80) && TileCollision(new Rectangle((int)nextPos.X - projectile.width / 2, (int)nextPos.Y - projectile.height / 2,projectile.width, projectile.height)))
                {
                    Vector2 vel = new Vector2(16, 0).RotatedBy(projectile.rotation + 1.57f * dir);
                    Collision.HitTiles(projectile.position, vel, projectile.width, projectile.height);
                    projectile.ai[0]++;
                }
            }
            else if (projectile.ai[0] == 3) //Hit a tile/Hit an enemy cause a shockwave
            {
                if (projectile.ai[1] == 0)
                {
                    Projectile.NewProjectile(projectile.Center + new Vector2(20, 0).RotatedBy(projectile.rotation + 1.57f * dir), Vector2.Zero, mod.ProjectileType("SmileShockwave"), projectile.damage, projectile.knockBack, projectile.owner, projectile.rotation);
                    Main.PlaySound(SoundID.Item14, projectile.Center);
                }
                projectile.ai[1]++;
                if (projectile.ai[1] > projOwner.itemAnimationMax * 1.5f)
                {
                    projectile.Kill();
                    projOwner.itemAnimation = 0;
                }
            }
            else if (projectile.ai[0] == 4) //Failure
            {
                projectile.ai[1]++;
                if (projectile.ai[1] >= projOwner.itemAnimationMax)
                {
                    projectile.Kill();
                    projOwner.itemAnimation = 0;
                }
            }


            Vector2 velRot = new Vector2(1, 0).RotatedBy(projectile.rotation);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projOwner.direction = projectile.spriteDirection;
            projectile.Center = ownerMountedCenter + 90 * velRot;

            projOwner.itemTime = 2;
            projOwner.itemAnimation = 2;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 2)
            {
                projectile.ai[0]++;
            }
        }

        private bool TileCollision(Rectangle hitbox)
        {
            for (int x = (int)(hitbox.X / 16); x < (int)((hitbox.X + hitbox.Width) / 16); x++)
            {
                for (int y = (int)(hitbox.Y / 16); y < (int)((hitbox.Y + hitbox.Height) / 16); y++)
                {
                    Tile t = Main.tile[x, y];
                    if (t.active() && (Main.tileSolid[t.type] || Main.tileSolidTop[t.type]))
                        return true;
                }
            }
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return projectile.ai[0] == 2;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //Dust.NewDustPerfect(ownerMountedCenter, 14, Vector2.Zero);
            Vector2 position = ownerMountedCenter - Main.screenPosition;
            Vector2 originOffset = new Vector2(12, 0).RotatedBy(MathHelper.ToRadians(projectile.direction == 1 ? 135 : 45));
            Vector2 origin = new Vector2((projectile.spriteDirection == 1 ? 9 : 74), 74) + originOffset;
            float rotation = projectile.rotation + MathHelper.ToRadians(projectile.spriteDirection == 1 ? 45 : 135);
            SpriteEffects spriteEffect = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                lightColor * ((float)(255 - projectile.alpha) / 255f), rotation, origin, projectile.scale, spriteEffect, 0f);
            /*
            SlashTrail trail = new SlashTrail(80, 1.57f);
            trail.DrawTrail(projectile, LobcorpShaders["TwilightSlash"]);*/

            return false;
        }
    }

    class SmileShockwave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.alpha = 0;
            projectile.timeLeft = 15;

            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.ai[0];
            projectile.scale = 1f - (projectile.timeLeft / 15f);// (float)Math.Sin(1.57f * (1f - (projectile.timeLeft / 15f)));
            projectile.alpha = 255 - (int)(255 * (1f - (projectile.timeLeft / 15f)));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float scaleFactor = (float)Math.Sin(1.57f * (projectile.scale));
            Vector2 lineStart = projHitbox.Center() - new Vector2(200 * scaleFactor, 0).RotatedBy(projectile.rotation);
            Vector2 lineEnd = projHitbox.Center() + new Vector2(200 * scaleFactor, 0).RotatedBy(projectile.rotation);
            float e = 0;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), lineStart, lineEnd, 20, ref e))
                return true;
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = LobotomyCorp.SmileShockwave;
            Vector2 position = projectile.Center - Main.screenPosition;
            Vector2 origin = tex.Size() / 2;
            
            if (projectile.scale < 0.85f)
            {
                float scaleFactor = (float)Math.Sin(1.57f * (projectile.scale / 0.85f));
                Vector2 scale = new Vector2(6 * scaleFactor, scaleFactor) * 0.8f;
                Color color = Color.White;
                color *= 1f - scaleFactor;
                spriteBatch.Draw(tex, position, tex.Frame(), color, projectile.rotation, origin, scale, 0, 0);
            }
            if (projectile.scale > 0.25f)
            {
                float scaleFactor = (float)Math.Sin(1.57f * ((projectile.scale - 0.15f) / 0.85f));
                Vector2 scale = new Vector2(6 * scaleFactor, scaleFactor) * 0.8f;
                Color color = Color.White;
                color *= 1f - scaleFactor;
                spriteBatch.Draw(tex, position, tex.Frame(), color, projectile.rotation, origin, scale, 0, 0);
            }
            return false;
        }
    }
}
