// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsTextBox : SettingsExpandedControl<FluentTextBox, string>
    {
        protected override FluentTextBox CreateControl() => new FluentTextBox { RelativeSizeAxes = Axes.X };
    }
}
