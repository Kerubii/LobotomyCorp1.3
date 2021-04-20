using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LobotomyCorp;


namespace LobotomyCorp.NPCs.WhiteNight
{
    //[AutoloadBossHead]
	class WhiteNight : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WhiteNight");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 56;
            npc.lifeMax = 1200;
            npc.boss = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.damage = 25;
			npc.defense = 0;
            npc.aiStyle = -1;
            npc.knockBackResist = 0.0f;
            npc.buffImmune[BuffID.Cursed] = true;
			npc.HitSound = SoundID.NPCHit1;
            LobotomyGlobalNPC.LNPC(npc).RiskLevel = (int)RiskLevel.Aleph;
        }
		
		public override void AI()
		{
            npc.localAI[3] += MathHelper.ToRadians(1);
            if (npc.localAI[3] > MathHelper.ToRadians(360))
                npc.localAI[3] = 0;
            npc.gfxOffY = 8 + 8 * (float)Math.Sin(npc.localAI[3]);
		}
		
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 15)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > frameHeight * 3)
                    npc.frame.Y = 0;
            }
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByItem(player, item, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void NPCLoot()
		{
			Item.NewItem((int) npc.position.X, (int) npc.position.Y, npc.width, npc.height, mod.ItemType("Paradise Lost"));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
            Texture2D texture = mod.GetTexture("NPCs/WhiteNight/PaleRing");
            Vector2 position = npc.position - Main.screenPosition + new Vector2(8, 41 + npc.gfxOffY);
            Vector2 origin = new Vector2(128, 128);
            float rotation = npc.localAI[3];
            spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?
                        (
                            new Rectangle
                            (
                                0, 0, 256, 256
                            )
                        ),
                        Color.White, MathHelper.ToRadians(rotation), origin, 0.7f, SpriteEffects.None, 0f);

            texture = mod.GetTexture("NPCs/WhiteNight/WhiteNightWingBack");
            for (int i = 0; i < 8; i++)
            {
                int length = i % 2 == 0 ? 4 : 10;
                int height = 36 + (i > 1 ? 6 : 0);
                position = npc.position - Main.screenPosition + new Vector2( length, height + npc.gfxOffY);
                origin = new Vector2(84, 70);
                if (i % 2 == 1)
                    origin = new Vector2(6, 70);
                rotation = 13 + 8 * (float)Math.Sin(npc.localAI[3]);
                float scale = 0.8f;
                if (i > 1)
                {
                    rotation = 0 + 14 * (float)Math.Sin(npc.localAI[3]);
                    scale = 1f;
                }
                if (i > 3)
                {
                    rotation = -35 + 12 * (float)Math.Sin(npc.localAI[3]);
                    scale = 0.9f;
                }
                if (i > 5)
                {
                    rotation = -95 + 8 * (float)Math.Sin(npc.localAI[3]);
                }
                if (i % 2 == 1)
                    rotation *= -1;
                SpriteEffects spriteeffect = SpriteEffects.None;
                if (i % 2 == 1)
                    spriteeffect = SpriteEffects.FlipHorizontally;

                spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?
                                                    (
                                                        new Rectangle
                                                        (
                                                            0, 0, 90, 74
                                                        )
                                                    ),
                                lightColor, MathHelper.ToRadians(rotation), origin, scale, spriteeffect, 0f);
            }   

            texture = mod.GetTexture("NPCs/WhiteNight/WhiteNightWingFrontR");
            position = npc.position - Main.screenPosition + new Vector2(14, 44 + npc.gfxOffY);
            origin = new Vector2(6, 18);
            rotation = 8 * (float)Math.Sin(npc.localAI[3]);
            spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, 62, 68
                                        )
                                    ),
                lightColor, MathHelper.ToRadians(rotation), origin, npc.scale, SpriteEffects.None, 0f);

            texture = mod.GetTexture("NPCs/WhiteNight/WhiteNightWingFrontL");
            position = npc.position - Main.screenPosition + new Vector2(2, 44 + npc.gfxOffY);
            origin = new Vector2(52, 16);
            rotation = -8 * (float)Math.Sin(npc.localAI[3]);
            spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, 64, 76
                                        )
                                    ),
                lightColor, MathHelper.ToRadians(rotation), origin, npc.scale, SpriteEffects.None, 0f);
            return true;
		}
	}
}
