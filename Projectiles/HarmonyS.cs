using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class HarmonyS : ModProjectile
	{
		public override void SetDefaults() {
			projectile.width = 46;
			projectile.height = 46;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
            
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}

		public override void AI() {
            
            Player player = Main.player[projectile.owner];
            Vector2 mountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Main.myPlayer == projectile.owner)
            {
                projectile.velocity = Main.MouseWorld - mountedCenter;
            }
            Vector2 center = new Vector2(93, 0).RotatedBy(projectile.velocity.ToRotation());
            projectile.Center = mountedCenter + center;
            if (!player.channel)
            {
                if (projectile.ai[0] > 0)
                    projectile.ai[0] -= 2;
                if (projectile.ai[0] <= 0)
                    projectile.Kill();
            }
            else
            {
                if (projectile.ai[0] < 60)
                    projectile.ai[0] += 0.5f;
            }
            player.itemAnimation = 2;
            player.itemTime = 2;

            player.direction = center.X >= 0 ? 1 : -1;
            projectile.direction = player.direction;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * player.direction, projectile.velocity.X * player.direction);

            projectile.timeLeft = 60;
            projectile.rotation += (MathHelper.ToRadians(20) * (projectile.ai[0] / 60)) * player.direction;
            if (Main.rand.Next((int)(30 - 30 * (projectile.ai[0] / 60))) == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    float rotRand = Main.rand.NextFloat((float)Math.PI * 2);
                    Vector2 dustPos = new Vector2(23, 0).RotatedBy(rotRand);
                    Vector2 dustVel = new Vector2(0, 4).RotatedBy(rotRand);
                    Dust.NewDustPerfect(projectile.Center + dustPos, 5, dustVel);
                }
            }

            projectile.localAI[0]--;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY);
            Vector2 origin = new Vector2(projectile.direction > 1 ? 93 : 23, 23);
            float rot = projectile.velocity.ToRotation() + (projectile.direction > 1 ? 0 : 3.14f);

            spriteBatch.Draw(tex, position, tex.Frame(), lightColor, rot, origin, projectile.scale, (SpriteEffects)projectile.direction, 0);

            tex = mod.GetTexture("Projectiles/HarmonySHead");
            spriteBatch.Draw(tex, position, tex.Frame(), lightColor, projectile.rotation, origin, projectile.scale, (SpriteEffects)projectile.direction, 0);
            tex = mod.GetTexture("Projectiles/HarmonySString");
            spriteBatch.Draw(tex, position, tex.Frame(), lightColor, rot, origin, projectile.scale, (SpriteEffects)projectile.direction, 0);

            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            foreach (Player p in Main.player)
            {
                if (p.active && (p.whoAmI == projectile.owner || p.team == Main.player[projectile.owner].team) && !p.dead)
                {
                    LobotomyModPlayer.ModPlayer(p).HarmonyTime += 5;
                    if (LobotomyModPlayer.ModPlayer(p).HarmonyTime > 600)
                        LobotomyModPlayer.ModPlayer(p).HarmonyTime = 600;
                    p.AddBuff(mod.BuffType("MusicalAddiction"), LobotomyModPlayer.ModPlayer(p).HarmonyTime, true);
                }
            }
            target.immune[projectile.owner] = 15 - (int)(13 * (projectile.ai[0] / 60f));
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 5);
            }
            Vector2 spe = new Vector2(16f, 0).RotatedByRandom(6.28f);
            Main.item[Item.NewItem(target.getRect(), mod.ItemType("HarmonyNote"), 1, true, 0)].velocity = spe;

            if (projectile.localAI[0] <= 0)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Singing_Atk").WithVolume(0.5f).WithPitchVariance(0.3f), projectile.position);
                projectile.localAI[0] = 60 - 30 * (projectile.ai[0] / 60f);
            }

        }
    }
}
