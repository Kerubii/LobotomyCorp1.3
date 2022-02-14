using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class ExuviaeMist: ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 60;
            projectile.alpha = 110;

            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0]++;
                projectile.frame += Main.rand.Next(2);
                projectile.rotation = Main.rand.NextFloat(3.14f);
            }
            if (projectile.timeLeft < 30)
            {
                projectile.alpha += 4;
            }
            projectile.rotation += 0.008f * Math.Sign(projectile.velocity.X);

            if (Main.rand.Next(15) == 0)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Moss_Green);
                Main.dust[d].noGravity = true;
                Main.dust[d].fadeIn = 1.2f;
            }
        }
    }
}
