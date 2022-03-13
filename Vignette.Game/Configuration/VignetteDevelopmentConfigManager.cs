// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Platform;

namespace Vignette.Game.Configuration
{
    public class VignetteDevelopmentConfigManager : VignetteConfigManager
    {
        protected override string Filename => "config.dev.ini";

        public VignetteDevelopmentConfigManager(Storage storage, IDictionary<VignetteSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }
    }
}
