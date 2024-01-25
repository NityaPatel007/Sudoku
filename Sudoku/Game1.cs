using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceCard
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Game1 self;
        private Song backgroundSong;

        public Game1()
        {
            self = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Utils.get().width;
            _graphics.PreferredBackBufferHeight = Utils.get().height;
            _graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Utils.get().batch = _spriteBatch;
            Utils.get().font12 = Content.Load<SpriteFont>("font12");
            Utils.get().font24 = Content.Load<SpriteFont>("font24");
            Utils.get().font32 = Content.Load<SpriteFont>("font32");
            Utils.get().font60 = Content.Load<SpriteFont>("font60");
            Utils.get().content = Content;
            Utils.get()._graphics = _graphics;
            // TODO: use this.Content to load your game content here

            Utils.get().currentGame = new MenuScreen(false);
            Utils.get().game = self;

            backgroundSong = Content.Load<Song>("bgm");

            // Play the background song on repeat
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundSong);
        }
        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            Utils.get().currentGame.update();

            // TODO: Add your update logic here
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            Utils.get().deltaTime = (float)delta;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);

            Utils.get().batch.Begin();

            Utils.get().currentGame.draw();

            Utils.get().batch.End();
        }
    }
}