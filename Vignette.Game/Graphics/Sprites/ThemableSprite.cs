// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Sprites
{
    public class ThemableSprite : ThemableDrawable<Sprite>
    {
        public Texture Texture
        {
            get => Target.Texture;
            set => Target.Texture = value;
        }

        public ThemableSprite(bool attached = true)
            : base(attached)
        {
        }

        protected override Sprite CreateDrawable() => new Sprite();
    }
}
