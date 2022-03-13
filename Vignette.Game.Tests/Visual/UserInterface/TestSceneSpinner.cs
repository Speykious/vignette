// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osuTK;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneSpinner : UserInterfaceTestScene
    {
        public TestSceneSpinner()
        {
            Add(new Spinner { Size = new Vector2(64) });
        }
    }
}
