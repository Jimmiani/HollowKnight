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
    public static class SceneManager
    {
        private static Scene currentScene;

        private static GraphicsDevice graphicsDevice;
        private static ContentManager content;
        private static SpriteBatch spriteBatch;

        public static void Initialize(GraphicsDevice gd, ContentManager cm)
        {
            graphicsDevice = gd;
            content = cm;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public static void ChangeScene(Scene newScene)
        {
            currentScene?.UnloadContent();
            currentScene = newScene;
            currentScene.LoadContent();
        }

        public static void Update(GameTime gameTime)
        {
            currentScene?.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            currentScene?.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
