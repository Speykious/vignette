// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

namespace Vignette.Live2D.Motion.Segments
{
    public interface ISegment
    {
        double ValueAt(double time);
    }
}
