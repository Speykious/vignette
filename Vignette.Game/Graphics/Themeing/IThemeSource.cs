// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Bindables;

namespace Vignette.Game.Graphics.Themeing
{
    /// <summary>
    /// An interface denoting that this is capable of providing a theme to <see cref="IThemable{T}"/>s.
    /// </summary>
    public interface IThemeSource
    {
        Action ThemeChanged { get; set; }

        Bindable<Theme> Current { get; }
    }
}
