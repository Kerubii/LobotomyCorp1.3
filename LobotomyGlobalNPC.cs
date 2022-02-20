using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp
{
	public class LobotomyGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public int BeakTarget = 0;
		public bool BODExecute = false;

        public bool MatchstickBurn = false;
        public int MatchstickBurnTime = 0;

        public bool PleasureDebuff = false;

        public int QueenBeeSpore = 0;
        public bool QueenBeeLarva = true;

        public bool WingbeatFairyMeal = false;
        public int WingbeatFairyHeal = 0;
        public int WingbeatRotation = Main.rand.Next(360);
        public int WingbeatTarget = -1;
        public int WingbeatIndicator = 0;
        public int RiskLevel = 0;

        public bool WristCutterScars = false;

        public static LobotomyGlobalNPC LNPC (NPC npc)
        {
            return npc.GetGlobalNPC<LobotomyGlobalNPC>();
        }

        public override void ResetEffects(NPC npc)
        {
            if (BeakTarget > 0)
                BeakTarget--;

            MatchstickBurn = false;

            PleasureDebuff = false;

            if (QueenBeeSpore > 0)
                QueenBeeSpore--;

            if (!WingbeatFairyMeal)
            {
                WingbeatFairyHeal = 0;
                WingbeatRotation = Main.rand.Next(360);
            }
            if (WingbeatTarget > -1 && LobotomyModPlayer.ModPlayer(Main.player[WingbeatTarget]).RealizedWingbeatMeal != npc.whoAmI)
                WingbeatTarget = -1;
            WingbeatFairyMeal = false;

            WristCutterScars = false;
        }

		public override bool PreAI(NPC npc)
		{
			return !BODExecute;
		}

        public override void AI(NPC npc)
        {
            if (QueenBeeSpore > 0)
            {
                Dust.NewDust(npc.Center, 1, 1, mod.DustType("HornetDust"));
            }

            if (WingbeatFairyMeal)
            {
                WingbeatRotation += 3;
                WingbeatFairyHeal -= 3;
                if (WingbeatRotation > 360)
                    WingbeatRotation -= 360;
                if (Main.rand.Next(30) == 0)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 89)];
                    dust.velocity *= 0;
                    dust.noGravity = true;
                    dust.fadeIn = 1f;
                }
                if (WingbeatFairyHeal <= 0)
                {
                    WingbeatFairyHeal = 120;
                    npc.life += 10;
                    if (npc.life > npc.lifeMax)
                        npc.life = npc.lifeMax;
                    npc.HealEffect(10, true);
                    for (int i = 0; i < 10; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 89)];
                        dust.velocity *= 0;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                    }
                }
            }
            if (WingbeatTarget > -1 && WingbeatNear(npc))
            {
                if (WingbeatIndicator < 5)
                    WingbeatIndicator++;
            }
            else if (WingbeatIndicator > 0)
                WingbeatIndicator--;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (MatchstickBurn)
            {
                npc.lifeRegen -= MatchstickBurnTime * 2;
                damage += MatchstickBurnTime;
            }
            if (PleasureDebuff)
            {
                npc.lifeRegen -= 10;
                damage += 5;
            }
            if (QueenBeeSpore > 0)
            {
                npc.lifeRegen -= 30;
                damage += 10;
            }

            if (WristCutterScars)
            {
                npc.lifeRegen -= 10;
                damage += 2;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (BeakTarget > 0)
                drawColor = Color.Red;

            if (MatchstickBurn)
            {
                if (MatchstickBurnTime < 20)
                {
                    if (Main.rand.Next(25 - MatchstickBurnTime) == 0)
                        Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire);
                }
                else
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        float scale = 0.5f + MatchstickBurnTime / 40f;
                        if (scale > 4f)
                            scale = 4f;
                        Dust d = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire)];
                        d.scale = scale;
                        d.noGravity = true;
                    }
                }
            }
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (WingbeatTarget > -1 && WingbeatIndicator > 0)
            {
                Texture2D texture = mod.GetTexture("Projectiles/WingbeatTarget");
                Vector2 position = npc.Center - Main.screenPosition;
                Rectangle? frame = new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height));
                float rotation = (Main.player[WingbeatTarget].Center - npc.Center).ToRotation();
                Vector2 scale = new Vector2(((npc.width < npc.height ? npc.height : npc.width)/60f) , 0.5f);
                Color color = Color.White * (WingbeatIndicator / 5f);
                color.A = (byte) (color.A * 0.8f);
                spriteBatch.Draw(texture, position, frame, color, rotation, new Vector2(20, 15), scale, SpriteEffects.None, 0f);
            }
            if (WingbeatFairyMeal)
            {
                Texture2D texture = mod.GetTexture("Projectiles/WingbeatFairy");
                Vector2 position = npc.Center - Main.screenPosition + new Vector2(npc.width, 0).RotatedBy(MathHelper.ToRadians(WingbeatRotation));

                Rectangle? frame = new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, texture.Height / 6 * (int)(WingbeatRotation / 60f), texture.Width, texture.Height / 6));

                spriteBatch.Draw(texture, position, frame, drawColor, 0, new Vector2(15, 12), 1f, SpriteEffects.None, 0f);
            }
        }

        public override bool CheckDead(NPC npc)
        {
            if (HornetTarget(npc))
            {
                SpawnHornet(npc);
            }
            return true;
        }

        public override void NPCLoot(NPC npc)
        {
            if (!Main.expertMode)
            {
                if (npc.type == NPCID.QueenBee && Main.rand.Next(2) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Hornet"));
                }
                if (npc.type == NPCID.WallofFlesh && Main.rand.Next(4) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Censored"));
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant)
            {
                if (Main.bloodMoon)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("WristCutter"));
                    nextSlot++;
                }
            }
            if (type == NPCID.Dryad)
            {
                if (NPC.downedQueenBee || NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("Hypocrisy"));
                    shop.item[nextSlot].shopCustomPrice = 100000;
                    nextSlot++;
                }
            }
        }

        public bool WingbeatNear(NPC npc)
        {
             return Vector2.Distance(npc.Center, Main.player[WingbeatTarget].Center) - (npc.width < npc.height ? npc.height : npc.width) < 40;
        }

        public static bool HornetTarget(NPC npc)
        {
            return LNPC(npc).QueenBeeSpore > 0;
        }

        public static void SpawnHornet(NPC npc, int player = -1, int damage = 0, float knockBack = 0)
        {
            Mod mod = ModLoader.GetMod("LobotomyCorp");
            if (LNPC(npc).QueenBeeLarva)
            {
                if (player >= 0)
                {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("WorkerBee"), damage, knockBack, player);
                        for (int i = 0; i < 5; i++)
                        {
                            Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("HornetDust"));
                        }
                        LNPC(npc).QueenBeeLarva = false;
                }
                else
                {
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player p = Main.player[i];
                        if (p.active && !p.dead && p.HeldItem.type == mod.ItemType("RealizedHornet"))
                        {
                            Item item = p.HeldItem;
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("WorkerBee"), item.damage, item.knockBack, p.whoAmI);
                            for (int n = 0; n < 5; n++)
                            {
                                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("HornetDust"));
                            }
                            LNPC(npc).QueenBeeLarva = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}