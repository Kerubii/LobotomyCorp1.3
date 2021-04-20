using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class HOUSE : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("ROADA HOME DA");
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 13;
        }

        public override void SetDefaults()
        {
            projectile.width = 164;
            projectile.height = 128;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 2f;
            projectile.timeLeft = 300;

            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.extraUpdates = 3;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 600;
        }

        public override void AI()
        {
            if (projectile.timeLeft < 15)
                projectile.alpha -= 15;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.ai[0] == 0)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                projectile.ai[0]++;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/House_HouseBoom").WithVolume(0.25f));
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 position = projectile.Center - Main.screenPosition;
            float alpha = ((float)(255f - (float)projectile.alpha) / 255f);
                for (int i = 0; i < 4; i++)
                {
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
                    color * alpha, projectile.rotation, projectile.Size / 4, projectile.scale, SpriteEffects.None, 0f);
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
                lightColor * alpha, projectile.rotation, projectile.Size / 4, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            if (projectile.position.Y + projectile.height >= Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height - 10)
            {
                fallThrough = false;
            }
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
    }
}
