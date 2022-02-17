using System;
using LobotomyCorp.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class ShaderTester : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 60;

            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }

        int timer = 15;

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];

            if (projOwner.HeldItem.type != ModContent.ItemType<Items.ShaderTesterSword>())
            {
                projectile.Kill();
                return;
            }
            projectile.timeLeft = 5;

            if (projOwner.itemAnimation == 15)
            {
                projectile.ai[0] = timer;
                projectile.spriteDirection = Main.rand.Next(2) == 0 ? -1 : 1;

            }

            if (projectile.ai[0] > 0)
            {
                projectile.ai[0]--;
                projectile.rotation = (-1.57f * projectile.spriteDirection + 4.71f * (float)Math.Sin(1.57f * (1f - projectile.ai[0] / timer))) * projectile.spriteDirection;
            }

            projectile.Center = projOwner.Center;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            /*Player projOwner = Main.player[projectile.owner];
            int angles = 32;
            Vector2[] position = new Vector2[angles + 1];
            float[] rotation = new float[angles + 1];
            for (int i = 0; i < angles + 1; i++)
            {
                rotation[i] = (1.57f + projectile.rotation - 6.28f * ((float)i / (float)angles) * projectile.spriteDirection);
                position[i] = projOwner.Center + new Vector2(80, 0).RotatedBy(rotation[i]);
            }


            SlashTrail trail = new SlashTrail(40, 1.57f);
            trail.color = Color.Cyan;
            trail.DrawSpecific(position, rotation, Vector2.Zero, shader);*/
            CustomShaderData shader = LobotomyCorp.LobcorpShaders["MimicrySlash"].UseOpacity(0.5f * (float)Math.Cos(3.15f * projectile.ai[0] / timer) + 0.5f);

            SlashTrail trail = new SlashTrail(40, 8, 0);
            //SlashTrail trail = new SlashTrail(40, 1.57f);
            trail.color = Color.Cyan;
            //trail.DrawCircle(Main.player[projectile.owner].Center, 1.57f + projectile.rotation, projectile.spriteDirection, 80, 32, shader);
            trail.DrawEllipse(projectile.Center, -3.14f, 0.52f, 1, 120, 80, 64, shader);

            return false;
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
