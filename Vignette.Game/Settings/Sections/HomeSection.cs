// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class HomeSection : SettingsSection
    {
        public override IconUsage Icon => VignetteFont.Logo;

        public HomeSection()
        {
            Child = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Spacing = new Vector2(5),
                Children = new Drawable[]
                {
                    new HomeSectionLink(SegoeFluent.Heart, "https://github.com/sponsors/vignetteapp"),
                    new HomeSectionLink(FontAwesome.Brands.Github, "https://github.com/vignetteapp"),
                    new HomeSectionLink(SegoeFluent.Globe, "https://vignetteapp.org"),
                    new HomeSectionLink(FontAwesome.Brands.Discord, "https://discord.vignetteapp.org"),
                    new HomeSectionLink(FontAwesome.Brands.Twitter, "https://twitter.com/vignette_org"),
                }
            };
        }

        private class HomeSectionLink : OpenExternalLinkButton
        {
            protected override IconUsage? RightIcon => null;

            public HomeSectionLink(IconUsage icon, string url)
                : base(url)
            {
                Icon = icon;
                IconDrawable.Margin = new MarginPadding();
                IconDrawable.Anchor = Anchor.Centre;
                IconDrawable.Origin = Anchor.Centre;

                RelativeSizeAxes = Axes.None;

                Width = 50;
                Anchor = Anchor.TopCentre;
                Origin = Anchor.TopCentre;
            }
        }
    }
}
