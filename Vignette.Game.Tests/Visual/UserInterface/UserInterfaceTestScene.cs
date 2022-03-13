// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public abstract class UserInterfaceTestScene : ThemeProvidedTestScene
    {
        protected readonly FillFlowContainer FlowContent;

        protected override Container<Drawable> Content => FlowContent;

        public UserInterfaceTestScene()
        {
            base.Content.Add(new BasicScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(10),
                Child = FlowContent = new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 10),
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                },
            });
        }
    }
}
