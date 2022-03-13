// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneTextFlowContainer : UserInterfaceTestScene
    {
        private readonly ThemableTextFlowContainer flow;

        public TestSceneTextFlowContainer()
        {
            Add(flow = new ThemableTextFlowContainer
            {
                Margin = new MarginPadding(10),
                AutoSizeAxes = Axes.Both,
            });

            flow.AddText("Welcome to", t => t.Font = SegoeUI.Black.With(size: 24));
            flow.AddText(" Vignette", t => t.Font = SegoeUI.Black.With(size: 24));

            flow.NewParagraph();
            flow.NewParagraph();

            flow.AddText("The open-source toolkit for VTubers. Made with");
            flow.AddText(" <3 ", t => t.Font = SegoeUI.Black);
            flow.AddText("with osu!framework.");
        }
    }
}
