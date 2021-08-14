﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Chat;
using LobotomyCorp;


namespace LobotomyCorp.NPCs.OneBadManyGood
{
    //[AutoloadBossHead]
	class OneBadManyGood : ModNPC
    {
        private bool SinsConfessed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("One Sin and Hundred of Good Deeds");
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 80;
            npc.lifeMax = 1200;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.damage = 0;
			npc.defense = 0;
            npc.aiStyle = -1;
            npc.knockBackResist = 0.0f;
			npc.HitSound = SoundID.NPCHit1;
            LobotomyGlobalNPC.LNPC(npc).RiskLevel = (int)RiskLevel.Zayin;
        }
		
		public override void AI()
		{
            npc.localAI[0] += MathHelper.ToRadians(1);
            npc.localAI[1] += MathHelper.ToRadians(2);
            npc.gfxOffY = 10 * (float)Math.Sin(npc.localAI[0]);
		}

        public override bool CanChat()
        {
            return true;
        }

        public override string GetChat()
        {
            // npc.SpawnedFromStatue value is kept when the NPC is transformed.
            switch (Main.rand.Next(2))
            {
                case 0:
                    return "A giant skull shows itself in your vicinity, its presence odd and terrifying.";
                default:
                    return "As you stand near the giant skull, your body feels slightly lighter.";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                Main.npcChatText = "You kneel and pray, nothing happens.";
            }
            else
            {
                if (!SinsConfessed)
                {
                    Main.npcChatText = "You confess your sins, Its mouth begins to quiver and your body feels lighter. It drops a mysterious material.";
                    Main.LocalPlayer.QuickSpawnItem(ItemID.Bone, 25);
                }
                else
                    Main.npcChatText = "You do not know what to confess, It seems to glow brighter.";
            }
            base.OnChatButtonClicked(firstButton, ref shop);
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Pray";
            button2 = "Confess";
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
            Texture2D texture = mod.GetTexture("NPCs/OneBadManyGood/OneBadManyGoodBack");
            Vector2 position = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY);
            Vector2 origin = texture.Size()/2;
            spriteBatch.Draw(texture, position, texture.Frame(), lightColor, npc.rotation, origin, npc.scale, 0, 0f);
            texture = mod.GetTexture("NPCs/OneBadManyGood/OneBadManyGoodJaw");
            origin = texture.Size() / 2 - new Vector2(0, 2 + 2 * (float)Math.Sin(npc.localAI[1]));
            spriteBatch.Draw(texture, position, texture.Frame(), lightColor, npc.rotation, origin, npc.scale, 0, 0f);
            texture = mod.GetTexture("NPCs/OneBadManyGood/OneBadManyGood");
            origin = texture.Size() / 2;
            spriteBatch.Draw(texture, position, texture.Frame(), lightColor, npc.rotation, origin, npc.scale, 0, 0f);
            return false;
		}
	}
}
