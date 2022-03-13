// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneFileBrowser : UserInterfaceTestScene
    {
        public TestSceneFileBrowser()
        {
            Add(new FluentFileSelector
            {
                Width = 600,
                Height = 150,
            });

        }
    }
}
