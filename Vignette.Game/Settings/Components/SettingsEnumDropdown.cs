// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsEnumDropdown<T> : SettingsDropdown<T>
        where T : struct, Enum
    {
        protected override FluentDropdown<T> CreateControl() => new FluentEnumDropdown<T> { Width = 125 };
    }
}
