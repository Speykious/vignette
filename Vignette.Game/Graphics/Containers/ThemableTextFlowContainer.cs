// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.Sprites;

namespace Vignette.Game.Graphics.Containers
{
    public class ThemableTextFlowContainer : TextFlowContainer
    {
        public ThemableTextFlowContainer(Action<CheapThemableSpriteText> creationParameters)
            : base((s) => creationParameters((CheapThemableSpriteText)s))
        {
        }

        public ThemableTextFlowContainer()
        {
        }

        protected override CheapThemableSpriteText CreateSpriteText() => new CheapThemableSpriteText();
    }
}
