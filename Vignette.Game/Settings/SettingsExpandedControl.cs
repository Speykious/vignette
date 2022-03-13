// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Game.Settings
{
    public abstract class SettingsExpandedControl<TDrawable, TValue> : SettingsControl<TDrawable, TValue>
        where TDrawable : Drawable, IHasCurrentValue<TValue>
    {
        public SettingsExpandedControl()
        {
            Foreground.RelativeSizeAxes = Axes.X;
            Foreground.Height = 50;
            Foreground.Margin = new MarginPadding { Top = 50 };
        }

        protected override void InitializeControl()
        {
            Foreground.Add(Control = CreateControl().With(d =>
            {
                d.Anchor = Anchor.Centre;
                d.Origin = Anchor.Centre;
            }));
        }
    }
}
