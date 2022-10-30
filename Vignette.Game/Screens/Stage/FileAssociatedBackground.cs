// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using Vignette.Game.Configuration;
using Vignette.Game.IO;

namespace Vignette.Game.Screens.Stage
{
    public abstract class FileAssociatedBackground : CompositeDrawable, ICanAcceptFiles
    {
        public abstract IEnumerable<string> Extensions { get; }

        private Bindable<string> path;
        private Stream stream;

        [Resolved]
        private VignetteGameBase game { get; set; }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, IRenderer renderer)
        {
            path = config.GetBindable<string>(VignetteSetting.BackgroundPath);
            path.BindValueChanged(e => handlePathChange(renderer), true);
            game.RegisterFileHandler(this);
        }

        protected abstract Drawable CreateBackground(IRenderer renderer, Stream stream);

        private void handlePathChange(IRenderer renderer)
        {
            performCleanup();

            if (Extensions.Contains(Path.GetExtension(path.Value)) && File.Exists(path.Value))
            {
                stream = File.OpenRead(path.Value);
                InternalChild = CreateBackground(renderer, stream);
            }
        }

        public void FileDropped(IEnumerable<string> files)
        {
            Schedule(() => path.Value = files.FirstOrDefault());
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            game.UnregisterFileHandler(this);

            if (stream != null)
                Schedule(() => performCleanup());
        }

        private void performCleanup()
        {
            ClearInternal();
            stream?.Dispose();
            stream = null;
        }
    }
}
