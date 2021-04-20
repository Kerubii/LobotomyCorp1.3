using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.NPCs.WhiteNight
{
    class PaleRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            projectile.damage = 100;
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.scale = 0;
            projectile.timeLeft = 300;
        }

        public override void AI()
        {
            projectile.scale += 0.03f;
            if (projectile.timeLeft < 60)
                projectile.alpha += 15;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 Pos = new Vector2(targetHitbox.X + targetHitbox.Width / 2, targetHitbox.Y + targetHitbox.Height / 2);
            float Distance = Vector2.Distance(Pos, projectile.Center);
            float length = 110 * projectile.scale;
            return Distance > length - 8 && Distance < length + 8;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle frame = new Rectangle(0, projectile.frame, texture.Width, texture.Height);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height - texture.Width/2);
            Vector2 pos = projectile.position - Main.screenPosition + projectile.Size / 2 + new Vector2(0, projectile.gfxOffY);
            spriteBatch.Draw(
                texture,
                pos,
                new Rectangle?(frame),
                Color.White * ((255 - (float)projectile.alpha)/255f),
                projectile.rotation,
                origin,
                projectile.scale,
                SpriteEffects.None,
                0f);
            return false;
        }
    }
}
