using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class GreenStemArea : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Malice");
        }

		public override void SetDefaults() {
			projectile.width = 120;
			projectile.height = 120;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
            projectile.timeLeft = 60;
            
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
            projectile.hide = true;
		}

		public override void AI() {
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
            if (projOwner.channel)
            {
                if(projectile.ai[0] <= 1000000f)
                    projectile.ai[0]+= 2;
                    projectile.rotation += 0.005f;
            }
            else
            {
                projectile.ai[0] -= 1;
                if (projectile.ai[0] == 0)
                {
                    projectile.Kill();
                    return;
                }
            }
            projectile.timeLeft = 10;
            projectile.scale = projectile.ai[0] / 240f;
            projectile.localAI[0]+= 2;
            if (projectile.localAI[0] > 360)
                projectile.localAI[0] = 0;
            projectile.scale += 0.1f * (float) Math.Sin(MathHelper.ToRadians(projectile.localAI[0]));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
