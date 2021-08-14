using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
    public class FeatherOfHonor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 320;

            projectile.tileCollide = false;
            projectile.minion = true;
            projectile.friendly = true;
        }

        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

            //projectile.ai[0]++;

            if (projectile.timeLeft > 230)
            {
                projectile.position = ownerMountedCenter + projectile.velocity * projectile.ai[1];
                if (projectile.timeLeft > 260)
                    projectile.ai[1] += 0.001f;// * (projectile.ai[0] > 20 ? 1f - ((projectile.ai[0] - 20f) / 40f) : 1f);
            }
            else if (projectile.timeLeft == 230 - projectile.ai[0])
            {
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 targetPos = Main.MouseWorld + new Vector2(Main.rand.Next(32), Main.rand.Next(32));
                    projectile.velocity = Vector2.Normalize(targetPos - projectile.Center) * 12f;
                    projectile.rotation = projectile.velocity.ToRotation();
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.timeLeft < 230 - projectile.ai[0])
                for (int i = 0; i < 5; i++)
                {
                    Vector2 Pos = projectile.oldPos[i] + projectile.Size / 2 - Main.screenPosition;
                    Color color = lightColor * (1f - ((float)(i + 1f) / 5f));

                    spriteBatch.Draw(
                    Main.projectileTexture[projectile.type],
                    Pos,
                    Main.projectileTexture[projectile.type].Frame(),
                    lightColor,
                    projectile.oldRot[i],
                    Main.projectileTexture[projectile.type].Size() / 2,
                    projectile.scale,
                    0f, 0f);
                }
            spriteBatch.Draw(
                Main.projectileTexture[projectile.type],
                projectile.Center - Main.screenPosition,
                Main.projectileTexture[projectile.type].Frame(),
                lightColor,
                projectile.rotation,
                Main.projectileTexture[projectile.type].Size() / 2,
                projectile.scale,
                0f, 0f);
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return (projectile.timeLeft < 230 - projectile.ai[0]);
        }
    }
}
