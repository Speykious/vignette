// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using Vignette.Game.Graphics.Sprites;

namespace Vignette.Game.Graphics.Containers
{
    public class ThemableTextFlowContainer : TextFlowContainer<CheapThemableSpriteText>
    {
        public ThemableTextFlowContainer(Action<CheapThemableSpriteText> creationParameters)
            : base((Action<osu.Framework.Graphics.Sprites.SpriteText>)creationParameters)
        {
        }

        public ThemableTextFlowContainer()
        {
        }

        protected override CheapThemableSpriteText CreateSpriteText() => new CheapThemableSpriteText();
    }
}
