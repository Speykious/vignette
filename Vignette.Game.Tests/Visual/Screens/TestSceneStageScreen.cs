// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using Vignette.Game.Screens;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneStageScreen : ScreenTestScene
    {
        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load screen", () => LoadScreen(new StageScreen()));
        }
    }
}
