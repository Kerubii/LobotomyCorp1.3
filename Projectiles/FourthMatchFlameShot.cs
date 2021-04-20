using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class FourthMatchFlameShot : ModProjectile
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
            projectile.timeLeft = 300;

            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            projectile.rotation += 0.01f;
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FourthMatchFlameExplosion"), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }

    public class FourthMatchFlameExplosion : ModProjectile
    {
        public override string Texture => "LobotomyCorp/Projectiles/FourthMatchFlameShot";

        public override void SetDefaults()
        {
            projectile.width = 140;
            projectile.height = 140;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 2;
            projectile.alpha = 255;

            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }

        public override void AI()
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            projectile.rotation += 0.01f;
            Dust dust = new Dust();
            for (int i = 0; i < 20; i++)
            {
                dust = Main.dust[Dust.NewDust(projectile.Center, 1, 1, 31, 0, 0, 100, new Color(), 1.5f)];
                dust.velocity *= 1.4f;
            }
            for (int i = 0; i < 10; i++)
            {
                dust = Main.dust[Dust.NewDust(projectile.Center, 1, 1, 6, 0, 0, 100, new Color(), 2.5f)];
                dust.velocity *= 5f;
                dust.noGravity = true;
                dust = Main.dust[Dust.NewDust(projectile.Center, 1, 1, 6, 0, 0, 100, new Color(), 1.5f)];
                dust.velocity *= 3f;
            }
            Gore gore = Main.gore[Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64), 1f)];
            gore.velocity *= 0.4f;
            gore.velocity.X += 1f;
            gore.velocity.Y += 1f;
            gore = Main.gore[Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64), 1f)];
            gore.velocity *= 0.4f;
            gore.velocity.X -= 1f;
            gore.velocity.Y += 1f;
            gore = Main.gore[Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64), 1f)];
            gore.velocity *= 0.4f;
            gore.velocity.X += 1f;
            gore.velocity.Y -= 1f;
            gore = Main.gore[Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64), 1f)];
            gore.velocity *= 0.4f;
            gore.velocity.X -= 1f;
            gore.velocity.Y -= 1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
