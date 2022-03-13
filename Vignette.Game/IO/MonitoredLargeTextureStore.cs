// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.Threading.Tasks;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class MonitoredLargeTextureStore : MonitoredResourceStore
    {
        private LargeTextureStore largeTextureStore;

        public MonitoredLargeTextureStore(GameHost host, Storage underlyingStorage, IEnumerable<string> filters = null)
            : base(underlyingStorage, filters)
        {
            largeTextureStore = new LargeTextureStore(host.CreateTextureLoaderStore(UnderlyingStore));
        }

        public new Texture Get(string name)
            => largeTextureStore.Get(name);

        public new Task<Texture> GetAsync(string name)
            => largeTextureStore.GetAsync(name);
    }
}
