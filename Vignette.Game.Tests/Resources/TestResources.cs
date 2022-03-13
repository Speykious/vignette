// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.IO;
using osu.Framework.IO.Stores;

namespace Vignette.Game.Tests.Resources
{
    public static class TestResources
    {
        public static ResourceStore<byte[]> GetStore() => new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(TestResources).Assembly), "Resources");

        public static Stream GetStream(string name) => GetStore().GetStream(name);
    }
}
