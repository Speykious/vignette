// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsButton : SettingsItem
    {
        public Action Action
        {
            get => button.Action;
            set => button.Action = value;
        }

        public LocalisableString ButtonText
        {
            get => button.Text;
            set => button.Text = value;
        }

        private readonly FluentButton button;

        public SettingsButton()
        {
            Foreground.Add(button = new FluentButton
            {
                Width = 150,
                Style = ButtonStyle.Primary,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
            });
        }
    }
}
