using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;
using ShiftingRectangles;

namespace ShiftingRectangleDemo.Windows
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		RectBackground rects;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Resolution.Init(ref graphics);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// Change Virtual Resolution
			Resolution.SetDesiredResolution(1280, 720);

			//set the desired resolution
			Resolution.SetScreenResolution(1024, 768, false);

			// TODO: Add your initialization logic here
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: use this.Content to load your game content here 

			rects = new RectBackground(Resolution.TitleSafeArea, Color.Black, Color.White);
			rects.LoadContent(GraphicsDevice);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			// TODO: Add your update logic here

			Vector2 vel = Vector2.Zero;
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				vel.Y -= 256.0f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				vel.Y += 256.0f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				vel.X -= 256.0f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				vel.X += 256.0f;
			}

			rects.Velocity = vel;
			rects.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear to Black
			graphics.GraphicsDevice.Clear(Color.Gray);

			// Calculate Proper Viewport according to Aspect Ratio
			Resolution.ResetViewport();

			spriteBatch.Begin(SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				null, null, null, null,
				Resolution.TransformationMatrix());

			rects.Draw(spriteBatch);

			spriteBatch.End();

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);
		}
	}
}
