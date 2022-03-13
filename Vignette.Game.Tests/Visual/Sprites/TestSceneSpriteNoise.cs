// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osuTK;
using Vignette.Game.Graphics.Sprites;

namespace Vignette.Game.Tests.Visual.Sprites
{
    public class TestSceneSpriteNoise : VignetteTestScene
    {
        public TestSceneSpriteNoise()
        {
            Add(new SpriteNoise
            {
                Size = new Vector2(512),
                Resolution = new Vector2(50),
            });
        }
    }
}
