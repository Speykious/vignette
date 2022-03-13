// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Testing;
using Vignette.Game.Tests.Resources;

namespace Vignette.Game.Tests
{
    public class VisualTestGame : VignetteGameBase
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(TestResources.GetStore());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(new TestBrowser("Vignette"));
        }
    }
}
