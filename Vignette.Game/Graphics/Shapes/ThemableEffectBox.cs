// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class ThemableEffectBox : ThemableContainer<Box>
    {
        public ThemableEffectBox()
        {
            Target.Add(new Box { RelativeSizeAxes = Axes.Both });
        }

        protected override void ThemeChanged(Theme theme)
        {
            base.ThemeChanged(theme);
            Target.Child.Colour = theme.Get(Colour);
        }
    }
}
