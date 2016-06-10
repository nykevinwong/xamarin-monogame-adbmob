using IndependentResolutionRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameExample
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            // below two lines fixed the blue screen issue when launching screen in portrait orientation.
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.ApplyChanges();

            Components.Add(new GamerServicesComponent(this));
            Content.RootDirectory = "Content";

            Resolution.Init(ref graphics);

            int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int max = (width > height) ? width : height;
            int min = (height > width) ? height : width;

            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(800,600);
            Resolution.SetResolution(width, height, false);
            this.Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //            graphics.GraphicsDevice.Clear(Color.YellowGreen);

            Resolution.BeginDraw();
            // TODO: Add your drawing code here
           
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Draw(gameTime);
        }


    }
}
