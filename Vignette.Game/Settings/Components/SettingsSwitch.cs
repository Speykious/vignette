// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsSwitch : SettingsControl<FluentSwitch, bool>
    {
        protected override FluentSwitch CreateControl() => new FluentSwitch();
    }
}
