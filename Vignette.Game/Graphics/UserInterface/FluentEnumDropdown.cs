// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentEnumDropdown<T> : FluentDropdown<T>
        where T : struct, Enum
    {
        public FluentEnumDropdown()
        {
            Items = (T[])Enum.GetValues(typeof(T));
        }
    }
}
