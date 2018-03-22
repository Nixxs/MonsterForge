using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonsterForge
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameRoot : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // defining the class level holders for getters and setters
        public static GameRoot _instance;
        public static Viewport _viewport;
        public static Vector2 _screenSize;

        // return the instance of the main game object, this is required by other classes
        // sometimes like the Art class which needs to gain access to the content pipeline
        public static GameRoot Instance
        {
            get
            {
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        // provide access to the viewport of the gameroot
        public static Viewport Viewport
        {
            get
            {
                return Instance.GraphicsDevice.Viewport;
            }
        }

        // provide access to the screensize, this can be used for things like edge detection 
        // and what not for enemies or the player character
        public static Vector2 ScreenSize
        {
            get
            {
                return new Vector2(Viewport.Width, Viewport.Height);
            }
        }

        // the contructor for the main game object
        public GameRoot()
        {
            Instance = this;

            graphics = new GraphicsDeviceManager(this);
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
            base.Initialize();

            EntityManager.Add(PlayerChar.Instance);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Art.Load(Instance);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // TODO: Add your update logic here
            Input.Update();
            EntityManager.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
                EntityManager.Draw(spriteBatch);

                // START DEBUG
                string attackPressed;
                if (Input.WasLightAttackPressed())
                {
                    attackPressed = "True";
                } else
                {
                    attackPressed = "False";
                }

                string debugText = String.Format(
                    "Aim Direction: {0}, {1}\n" +
                    "Move Direction: {2}, {3}\n" +
                    "Attack Pressed: {4}",
                    Input.GetAimDirection().X,
                    Input.GetAimDirection().Y,
                    Input.GetMovementDirection().X,
                    Input.GetMovementDirection().Y,
                    attackPressed
                );
                spriteBatch.DrawString(Art.DebugFont, debugText, new Vector2(50, 50), Color.Yellow);
                // END DEBUG
            spriteBatch.End();
        }
    }
}
