// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Platform;

namespace Vignette.Game.Tests
{
    public class TemporaryNativeStorage : NativeStorage, IDisposable
    {
        public TemporaryNativeStorage(string path, GameHost host = null)
            : base(path, host)
        {
            GetFullPath("./", true);
        }

        public void Dispose()
        {
            DeleteDirectory(string.Empty);
        }
    }
}
