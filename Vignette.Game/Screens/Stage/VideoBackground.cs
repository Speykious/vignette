// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Video;

namespace Vignette.Game.Screens.Stage
{
    public class VideoBackground : FileAssociatedBackground
    {
        public override IEnumerable<string> Extensions => new[] { ".mp4" };

        protected override Drawable CreateBackground(IRenderer renderer, Stream stream) => new Video(stream)
        {
            Loop = true,
            FillMode = FillMode.Fit,
            RelativeSizeAxes = Axes.Both,
        };
    }
}
