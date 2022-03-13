// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class BackdropSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.Image;

        public override LocalisableString Label => "Backdrop";

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, SessionConfigManager session)
        {
            Children = new Drawable[]
            {
                new SettingsColourPicker
                {
                    Icon = SegoeFluent.ColorBackground,
                    Label = "Background Colour",
                    Current = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour),
                },
            };
        }
    }
}
