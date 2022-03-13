// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings
{
    public class SettingsInteractableItem : SettingsItem
    {
        protected Action Action
        {
            get => button.Action;
            set => button.Action = value;
        }

        private SettingsInteractableItemBackground button;

        protected override Drawable CreateBackground() => button = new SettingsInteractableItemBackground();

        private class SettingsInteractableItemBackground : FluentButtonBase
        {
            private readonly ThemableBox background;

            public SettingsInteractableItemBackground()
            {
                RelativeSizeAxes = Axes.Both;

                InternalChild = background = new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                };

                BackgroundResting = ThemeSlot.Gray30;
                BackgroundPressed = ThemeSlot.Gray60;
                BackgroundHovered = ThemeSlot.Gray50;
                BackgroundDisabled = ThemeSlot.Gray30;
            }

            protected override void UpdateBackground(ThemeSlot slot) => background.Colour = slot;
        }
    }
}
