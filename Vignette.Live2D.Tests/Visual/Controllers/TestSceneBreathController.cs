// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Live2D.Tests.Visual.Controllers
{
    public class TestSceneBreathController : CubismModelTestScene
    {
        protected override void ModelChanged()
        {
            base.ModelChanged();
            Model.Add(new CubismBreathController());
        }
    }
}
