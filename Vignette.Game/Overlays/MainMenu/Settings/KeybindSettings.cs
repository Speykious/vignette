// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Input;
using Vignette.Game.Overlays.MainMenu.Settings.Components;
using Vignette.Game.Overlays.MainMenu.Settings.Sections;

namespace Vignette.Game.Overlays.MainMenu.Settings
{
    public class KeybindSettings : SettingsView
    {
        public override LocalisableString Title => "Keybinds";

        public override IconUsage Icon => FluentSystemIcons.Keyboard24;

        public KeybindSettings()
        {
            Add(new GlobalActionsSection());
        }

        private class GlobalActionsSection : SettingsSection
        {
            public override LocalisableString Header => "Global";

            [BackgroundDependencyLoader]
            private void load(VignetteKeyBindManager keybindManager)
            {
                foreach (var keybind in keybindManager.Global)
                    Add(new SettingsButtonKeybind(keybind));
            }
        }
    }
}
