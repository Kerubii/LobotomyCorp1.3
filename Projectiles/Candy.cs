using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class Candy : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 60;

            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            projectile.frame = (int)projectile.ai[0];
            if (projectile.ai[1] == 0)
            {
                projectile.scale += Main.rand.NextFloat(0.5f);
                projectile.timeLeft = Main.rand.Next(60, 120);
                projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2);
            }
            projectile.ai[1]++;
            if (projectile.ai[1] > 15)
            {
                projectile.velocity.Y -= 0.04f;
                projectile.velocity.X *= 0.98f;
            }
            projectile.rotation += MathHelper.ToRadians(8) * projectile.velocity.X / 12f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item54, projectile.Center);
            Dust dust;
            float offset = Main.rand.NextFloat(1.57f);
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = projectile.Center + new Vector2(7, 0).RotatedBy(offset + MathHelper.ToRadians(120 * i));
                dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 33, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
            }
        }
    }
}
