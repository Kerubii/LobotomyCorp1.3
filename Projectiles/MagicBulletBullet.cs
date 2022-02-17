using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class MagicBulletBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Bullet"); // The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 60; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0; // The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 8; // The width of projectile hitbox
			projectile.height = 8; // The height of projectile hitbox
			projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
			projectile.friendly = true; // Can the projectile deal damage to enemies?
			projectile.hostile = true; // Can the projectile deal damage to the player?
			projectile.ranged = true; // Is the projectile shoot by a ranged weapon?
			projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			projectile.light = 0.5f; // How much light emit around the projectile
			projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			projectile.tileCollide = false; // Can the projectile collide with tiles?
			projectile.extraUpdates = 3; // Set to above 0 if you want the projectile to update multiple time in a frame

			aiType = ProjectileID.Bullet; // Act exactly like default Bullet
		}

        public override void AI()
        {
			projectile.localAI[0]++;
			if (projectile.localAI[0] > 4)
			{
				projectile.localAI[0] = 0;
				int i = Dust.NewDust(projectile.position - projectile.getRect().Size(), projectile.width * 3, projectile.height * 3, mod.DustType("ElecDust"), 0.05f, 0.05f);
				Main.dust[i].velocity *= 0.01f;
			}
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = LobotomyCorp.MagicBulletBullet;

			// Redraw the projectile with the color not influenced by light
			Rectangle frame = texture.Frame();
			frame.Height /= 2;
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				if (k > 0)
					frame.Y = frame.Height;
				Vector2 drawPos = (projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(texture, drawPos, frame, color, projectile.rotation - 1.57f, drawOrigin, projectile.scale, SpriteEffects.None, 0);
			}

			return false;
		}
	}
}
