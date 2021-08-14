using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class MeltyLove : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Gunk");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 300;

            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }

        public override void AI()
        {
            if (projectile.localAI[1]++ == 2)
            {
                Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 251, -projectile.velocity.X/2, -projectile.velocity.Y/2    )].noGravity = true;
                projectile.localAI[1] = 0;
            }
            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 251);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Slow"), 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(
                tex,
                projectile.Center - Main.screenPosition,
                tex.Frame(),
                lightColor,
                projectile.rotation,
                tex.Size() / 2 + new Vector2(4, 0),
                projectile.scale,
                0,
                0);
            return false;
        }
    }
}
