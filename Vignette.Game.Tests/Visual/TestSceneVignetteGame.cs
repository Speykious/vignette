// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.ComponentModel;
using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace Vignette.Game.Tests.Visual
{
    [Description("The full Vignette experience.")]
    public class TestSceneVignetteGame : VignetteTestScene
    {
        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            var game = new VignetteGame();
            game.SetHost(host);
            Add(game);
        }
    }
}
