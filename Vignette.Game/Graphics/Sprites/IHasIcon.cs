// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Vignette.Game.Graphics.Sprites
{
    public interface IHasIcon : IDrawable
    {
        IconUsage Icon { get; set; }
    }
}
