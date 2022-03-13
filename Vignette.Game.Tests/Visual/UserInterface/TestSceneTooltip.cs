// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneTooltip : UserInterfaceTestScene
    {
        public TestSceneTooltip()
        {
            Add(new TestBoxWithTooltip
            {
                Size = new Vector2(256),
                Colour = ThemeSlot.AccentPrimary,
            });
        }

        private class TestBoxWithTooltip : ThemableBox, IHasTooltip
        {
            public LocalisableString TooltipText => @"Hello World";
        }
    }
}
