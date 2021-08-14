using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace LobotomyCorp
{
    public enum RiskLevel
    {
        Zayin,
        Teth,
        He,
        Waw,
        Aleph,
        Unknown
    }
	public class LobotomyCorp : Mod
	{
        public static Texture2D ArcanaSlaveLaser = null;
        public static Texture2D ArcanaSlaveBackground = null;

        public static Texture2D RedShield = null;
        public static Texture2D WhiteShield = null;
        public static Texture2D BlackShield = null;
        public static Texture2D PaleShield = null;

        public static Color PositivePE => new Color(18,255,86);
        public static Color NegativePE => new Color(239, 77, 61);
        public static Color ZayinRarity => new Color(33, 249, 0);
        public static Color TethRarity => new Color(26, 161, 255);
        public static Color HeRarity => new Color(255, 250, 4);
        public static Color WawRarity => new Color(122, 48, 241);
        public static Color AlephRarity => new Color(255, 1, 0);

        public static ModHotKey SynchronizeEGO;
        public static ModHotKey PassiveShow;
        public static bool ExtraPassiveShow = false;

        public static void RiskLevelResist(ref int damage, RiskLevel ego, RiskLevel risk)
        {

        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                PassiveShow = RegisterHotKey("Extend Passive", "F");

                ExtraPassiveShow = false;

                ArcanaSlaveLaser = GetTexture("Projectiles/QueenLaser/Laser");
                PremultiplyTexture(ArcanaSlaveLaser);
                ArcanaSlaveBackground = GetTexture("Projectiles/QueenLaser/CircleBackground");
                PremultiplyTexture(ArcanaSlaveBackground);

                RedShield = GetTexture("Misc/BulletShield/RedShield");
                PremultiplyTexture(RedShield);
                WhiteShield = GetTexture("Misc/BulletShield/WhiteShield");
                PremultiplyTexture(WhiteShield);
                BlackShield = GetTexture("Misc/BulletShield/BlackShield");
                PremultiplyTexture(BlackShield);
                PaleShield = GetTexture("Misc/BulletShield/PaleShield");
                PremultiplyTexture(PaleShield);

                if (Main.netMode != NetmodeID.Server)
                {
                    Ref<Effect> punishingRef = new Ref<Effect>(GetEffect("Effects/PunishingBird"));
                    Ref<Effect> TrailRef = new Ref<Effect>(GetEffect("Effects/SwordTrail"));
                    Ref<Effect> ArcanaSlaveRef = new Ref<Effect>(GetEffect("Effects/ArcanaSlave"));

                    GameShaders.Misc["Punish"] = new MiscShaderData(punishingRef, "PunishingBird");

                    GameShaders.Misc["LobotomyCorp:Resize"] = new MiscShaderData(ArcanaSlaveRef, "ArcanaResize");

                    Texture2D blankTexture = TextureManager.BlankTexture;

                    TextureManager.BlankTexture = GetTexture("Projectiles/Help");
                    GameShaders.Misc["TrailingShader"] = new MiscShaderData(TrailRef, "Trail").UseImage("f");

                    TextureManager.BlankTexture = blankTexture;
                }
            }
        }

        public override void Unload()
        {
            ArcanaSlaveLaser = null;
            ArcanaSlaveBackground = null;

            RedShield = null;
            WhiteShield = null;
            BlackShield = null;
            PaleShield = null;

            /*PositivePE = new Color();
            NegativePE = new Color();
            ZayinRarity = new Color();
            TethRarity = new Color();
            HeRarity = new Color();
            WawRarity = new Color();
            AlephRarity = new Color();*/
            PassiveShow = null;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + "EvilPowder", new[]
            {
                (int)ItemID.ViciousPowder,
                (int)ItemID.VilePowder
            });
            RecipeGroup.RegisterGroup("LobotomyCorp:EvilPowder", rec);
        }

        public static bool LamentValid(NPC t, Projectile p)
        {
            bool valid = true;
            float health = (float)t.life/(float)t.lifeMax;
            foreach (NPC n in Main.npc)
            {
                float health2 = (float)t.life / (float)t.lifeMax;
                if (n.active && !n.dontTakeDamage && !n.friendly && n.life > 0 && n.whoAmI != t.whoAmI && health2 < health && n.chaseable && n.CanBeChasedBy(p) && n.realLife < 0)
                {
                    valid = false;
                    Main.NewText(n.type);
                    break;
                }
            }
            return valid;
        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public static float Lerp(float x, float x2, float progress, bool reverse = false)
        {
            if (progress < 0)
                progress = 0;
            if (progress > 1f)
                progress = 1f;
            if (reverse)
                return x2 * (1 - progress) + x * progress;
            else
                return x * (1 - progress) + x2 * progress;
        }
    }
}