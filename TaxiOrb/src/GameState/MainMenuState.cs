﻿namespace TaxiOrb.GameState
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	public class MainMenuState : GameState
	{
		private enum HighlightedButton
		{
			Play = 0,
			End = 1,
			Credits = 2
		}

		private HighlightedButton _currentButton;
		private KeyboardState _oldState;

		public MainMenuState(Game game) : base(game)
		{
			_currentButton = HighlightedButton.Play;
			_oldState = Keyboard.GetState();
		}

		public override void Update(GameTime gameTime)
		{
			var keystate = Keyboard.GetState();

			if ((_oldState.IsKeyUp(Keys.W) && keystate.IsKeyDown(Keys.W)) ||
			    (_oldState.IsKeyUp(Keys.Up) && keystate.IsKeyDown(Keys.Up)))
			{
				switch (_currentButton)
				{
					case HighlightedButton.Play:
						_currentButton = HighlightedButton.Credits;
						break;
					case HighlightedButton.End:
						_currentButton = HighlightedButton.Play;
						break;
					case HighlightedButton.Credits:
						_currentButton = HighlightedButton.End;
						break;
				}
			}

			if ((_oldState.IsKeyUp(Keys.S) && keystate.IsKeyDown(Keys.S)) ||
			    (_oldState.IsKeyUp(Keys.Down) && keystate.IsKeyDown(Keys.Down)))
			{
				switch (_currentButton)
				{
					case HighlightedButton.Play:
						_currentButton = HighlightedButton.End;
						break;
					case HighlightedButton.End:
						_currentButton = HighlightedButton.Credits;
						break;
					case HighlightedButton.Credits:
						_currentButton = HighlightedButton.Play;
						break;
				}
			}

			if ((_oldState.IsKeyDown(Keys.Enter) && keystate.IsKeyUp(Keys.Enter)) ||
			    (_oldState.IsKeyDown(Keys.Space) && keystate.IsKeyUp(Keys.Space)))
			{
				switch (_currentButton)
				{
					case HighlightedButton.Play:
						NextState = new PlayState(game);
						Finished = true;
						break;

					case HighlightedButton.End:
						game.Exit();
						break;

					case HighlightedButton.Credits:
                        NextState = new CreditState(game, this);
                        Updatable = false;

						break;
				}
			}

			_oldState = keystate;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			game.GraphicsDevice.Clear(Color.Gray);

			spriteBatch.Begin();

			DrawButton(spriteBatch, "Play", _currentButton == HighlightedButton.Play, new Vector2(20, 120));
			DrawButton(spriteBatch, "Exit", _currentButton == HighlightedButton.End, new Vector2(20, 220));
			DrawButton(spriteBatch, "Credits", _currentButton == HighlightedButton.Credits, new Vector2(20, 610));

			spriteBatch.DrawString(Resources.Font22, "TaxiOrb", new Vector2(game.GraphicsDevice.Viewport.Width/2f - Resources.Font22.MeasureString("TaxiOrb").X/2, 20).ToPoint().ToVector2(), Color.White);


			spriteBatch.DrawString(Resources.Font, "Collect as many Orbs as possible\n" +
			                                       "Be careful with the hot ones!\n" +
			                                       "\n" +
			                                       "\n" +
			                                       "Move with WASD or the arrow keys\n" +
			                                       "Space or Enter to accept\n" +
			                                       "Quickexit with Escape",
				new Vector2(800, 120), Color.White);

			spriteBatch.End();
		}

		private static void DrawButton(SpriteBatch spriteBatch, string text, bool isHighlighted, Vector2 position)
		{
			var destinationRectangle = new Rectangle(position.ToPoint(), new Point(360, 60));

			spriteBatch.Draw(Resources.Pixel, destinationRectangle, new Rectangle(0,0,1,1), isHighlighted ? Color.White : Color.Black);

			spriteBatch.Draw(Resources.Pixel, new Rectangle(position.ToPoint(), new Point(destinationRectangle.Width, 2)), isHighlighted ? Color.Black : Color.White);
			spriteBatch.Draw(Resources.Pixel, new Rectangle(position.ToPoint(), new Point(2, destinationRectangle.Height)), isHighlighted ? Color.Black : Color.White);

			spriteBatch.Draw(Resources.Pixel, new Rectangle(position.ToPoint() + new Point(0 ,destinationRectangle.Height), new Point(destinationRectangle.Width + 2, 2)), isHighlighted ? Color.Black : Color.White);
			spriteBatch.Draw(Resources.Pixel, new Rectangle(position.ToPoint() + new Point(destinationRectangle.Width, 0), new Point(2, destinationRectangle.Height + 2)), isHighlighted ? Color.Black : Color.White);

			var stringSize = Resources.Font.MeasureString(text);
			var textPosition = destinationRectangle.Center - new Point((int)(stringSize.X / 2f),(int)(stringSize.Y / 2f));
			spriteBatch.DrawString(Resources.Font, text, textPosition.ToVector2(), isHighlighted ? Color.Black : Color.White);
		}
	}
}
