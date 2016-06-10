using IndependentResolutionRendering;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameExample.Tools
{
    public class ResolutionSpriteBatch : SpriteBatch
    {
        public ResolutionSpriteBatch(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null)
        {
            base.Begin(sortMode, blendState, null, null, null, null, Resolution.getTransformationMatrix());

        }

    }
}
