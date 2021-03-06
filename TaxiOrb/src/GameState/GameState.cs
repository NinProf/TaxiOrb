﻿namespace TaxiOrb.GameState
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public abstract class GameState
	{
		protected Game game;

		protected bool Drawable = true;
		protected bool Updatable = true;
		protected bool Finished = false;
		protected GameState NextState;

		//Constructor which takes the current Game-Object -> Game contains useful resources
		protected GameState(Game game)
		{
			this.game = game;

		}

		//Update and Draw get Called for each GameState depending on the properties
		public abstract void Update(GameTime gameTime);

		public abstract void Draw(SpriteBatch spriteBatch);


		//Returns whether Update or Draw should be called
		public virtual bool IsUpdatable() => Updatable;
		public virtual void SetUpdatable(bool newValue) { Updatable = newValue; }
		public virtual bool IsDrawable() => Drawable;
		public virtual void SetDrawable(bool newValue) { Drawable = newValue; }

		//Finished GameStates get removed
		public virtual bool IsFinished() => Finished;
		//If a NextState is set it will be put at the end of StateList -> will be drawn/updated on Top of others
		public virtual GameState GetNextState() => NextState;

		public void ResetNextState() { NextState = null; }
	}
}
