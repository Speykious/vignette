// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;

namespace Vignette.Game.Configuration
{
    public class SessionConfigManager : InMemoryConfigManager<SessionSetting>
    {
        protected override void InitialiseDefaults() => Reset();

        public void Reset()
        {
            ensureDefault(SetDefault(SessionSetting.EditingBackground, false));
            ensureDefault(SetDefault(SessionSetting.EditingAvatar, false));
        }

        private static void ensureDefault<T>(Bindable<T> bindable) => bindable.SetDefault();
    }

    public enum SessionSetting
    {
        EditingBackground,
        EditingAvatar,
    }
}
