﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace LobotomyCorp.Tiles
{
	public class QlipothDeterence : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TEQlipothDeterence>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Qlipoth Detterence Field");
			AddMapEntry(new Color(190, 230, 190), name);
			dustType = 11;
			disableSmartCursor = true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<Items.QlipothDetterenceField>());
			ModContent.GetInstance<TEQlipothDeterence>().Kill(i, j);
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = tile.frameY == 36 ? 18 : 16;
			//Main.spriteBatch.Draw(mod.GetTexture("Tiles/ElementalPurge_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

        public override bool NewRightClick(int i, int j)
        {

			HitWire(i, j);
            return true;
        }

        public override void HitWire(int i, int j)
        {
			int x = i - Main.tile[i, j].frameX / 18 % 2;
			int y = j - Main.tile[i, j].frameY / 18 % 3;
			for (int l = x; l < x + 2; l++)
			{
				for (int m = y; m < y + 3; m++)
				{
					if (Main.tile[l, m] == null)
					{
						Main.tile[l, m] = new Tile();
					}
					if (Main.tile[l, m].active() && Main.tile[l, m].type == Type)
					{
						if (Main.tile[l, m].frameY < 56)
						{
							Main.tile[l, m].frameY += 56;
						}
						else
						{
							Main.tile[l, m].frameY -= 56;
						}
					}
				}
			}
			if (Wiring.running)
			{
				Wiring.SkipWire(x, y);
				Wiring.SkipWire(x, y + 1);
				Wiring.SkipWire(x, y + 2);
				Wiring.SkipWire(x + 1, y);
				Wiring.SkipWire(x + 1, y + 1);
				Wiring.SkipWire(x + 1, y + 2);
			}
			NetMessage.SendTileSquare(-1, x, y + 1, 3);
		}

        public override bool AutoSelect(int i, int j, Item item)
        {
            return base.AutoSelect(i, j, item);
        }

	}

	public class TEQlipothDeterence : ModTileEntity
	{
		private const int range = 320;

		public override void Update()
		{
			if (Main.tile[Position.X, Position.Y].frameY >= 56)
				for (int i = 0; i < 3; i++)
				{
					Vector2 dustPos = new Vector2(Position.X * 16, Position.Y * 16) + new Vector2(range, 0).RotateRandom(6.28f);
					Dust d = Dust.NewDustPerfect(dustPos, DustID.BlueTorch);
					d.noGravity = true;
					d.fadeIn = 1.4f;
				}
			//npc thing;
		}

		public override bool ValidTile(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			return tile.active() && tile.type == ModContent.TileType<QlipothDeterence>() && tile.frameX == 0 && (tile.frameY == 0 || tile.frameY == 56);
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			// i - 1 and j - 2 come from the fact that the origin of the tile is "new Point16(1, 2);", so we need to pass the coordinates back to the top left tile. If using a vanilla TileObjectData.Style, make sure you know the origin value.
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i - 1, j - 1, 3); // this is -1, -1, however, because -1, -1 places the 3 diameter square over all the tiles, which are sent to other clients as an update.
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i - 1, j - 2, Type, 0f, 0, 0, 0);
				return -1;
			}
			return Place(i - 1, j - 2);
		}
	}
}