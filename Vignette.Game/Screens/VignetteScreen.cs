// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using Vignette.Game.Input;
using Vignette.Game.Settings;

namespace Vignette.Game.Screens
{
    public class VignetteScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        protected virtual bool AllowToggleSettings => true;

        protected virtual bool ShowSettingsOneEnter => false;

        [Resolved]
        private SettingsOverlay settings { get; set; }

        public override void OnEntering(ScreenTransitionEvent last)
        {
            if (settings.State.Value == Visibility.Visible && !AllowToggleSettings)
                settings.Hide();

            if (AllowToggleSettings && ShowSettingsOneEnter)
                settings.Show();

            base.OnEntering(last);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleSettings:
                    if (AllowToggleSettings)
                        settings.ToggleVisibility();
                    return true;

                default:
                    return false;
            }
        }

        public bool OnPressed(KeyBindingPressEvent<GlobalAction> e) => OnPressed(e.Action);

        public void OnReleased(GlobalAction action)
        {
        }

        public void OnReleased(KeyBindingReleaseEvent<GlobalAction> e) => OnReleased(e.Action);
    }
}
