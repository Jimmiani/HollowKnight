using HollowKnight.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HollowKnight.Scenes
{
    public class ExampleScene : Scene
    {
        private Texture2D pixel;
        private Vector2 pos = new Vector2(100, 100);
        private float speed = 150f;

        public ExampleScene(GraphicsDevice graphicsDevice, ContentManager content)
            : base(graphicsDevice, content)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            Input.Update();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            pos.X += speed * dt;

            if (pos.X > 800 || pos.X < 0)
                speed *= -1;

            if (Input.IsRightClick())
                speed *= -1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, pos, null, Color.CornflowerBlue, 0f, Vector2.Zero, 50f, SpriteEffects.None, 0f);
        }
    }
}
