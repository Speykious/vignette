// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A text input variant that displays a simple textbox.
    /// </summary>
    public class FluentTextBox : FluentTextInput
    {
        public FluentTextBox()
        {
            AddInternal(Input);
        }
    }
}
