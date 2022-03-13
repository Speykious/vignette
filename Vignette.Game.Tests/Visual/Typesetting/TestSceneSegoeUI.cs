// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Tests.Visual.Typesetting
{
    public class TestSceneSegoeUI : FontUsageTestScene
    {
        public TestSceneSegoeUI()
        {
            AddText(SegoeUI.Light);
            AddText(SegoeUI.SemiLight);
            AddText(SegoeUI.Regular);
            AddText(SegoeUI.SemiBold);
            AddText(SegoeUI.Bold);
            AddText(SegoeUI.Black);
        }
    }
}
