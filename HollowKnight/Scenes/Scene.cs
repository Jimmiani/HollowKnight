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
    public abstract class Scene
    {
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;

        public bool IsLoaded { get; private set; }

        public Scene(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
        }

        public virtual void LoadContent()
        {
            IsLoaded = true;
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            IsLoaded = false;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
