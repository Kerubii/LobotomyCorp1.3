using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class CherryBlossomsPetal : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 120;

            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            if (projectile.ai[0] == 0)
                projectile.rotation = Main.rand.NextFloat(MathHelper.ToRadians(360));
            projectile.ai[0]++;
            if (projectile.ai[0] > 10)
                projectile.velocity *= 0.98f;
            projectile.rotation += MathHelper.ToRadians(4) * ((float)projectile.velocity.Length() / 14f);
            if (projectile.timeLeft < 50)
                projectile.alpha += 5;

            if (Main.rand.Next(5) == 0)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 205, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 205, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
            }
        }
    }
}
