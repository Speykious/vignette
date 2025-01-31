// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Input.Bindings;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Configuration.Converters;

namespace Vignette.Game.Input
{
    public class VignetteKeyBindManager : JsonConfigManager
    {
        protected override string Filename => "keybinds.json";

        [JsonIgnore]
        public Action KeyBindsChanged;

        [JsonProperty]
        [JsonConverter(typeof(EnumKeyBindingConverter<GlobalAction>))]
        public List<IKeyBinding> Global { get; private set; } = GlobalActionContainer.GlobalKeyBindings.ToList();

        public VignetteKeyBindManager(Storage storage)
            : base(storage)
        {
        }

        public void SetKeyBind(GlobalAction action, params InputKey[] keys)
        {
            if (keys.Length == 0)
                return;

            var keybind = Global.FirstOrDefault(k => k.GetAction<GlobalAction>() == action);

            if (keybind == null)
                return;

            keybind.KeyCombination = keys;

            Save();
        }

        protected override bool PerformSave()
        {
            KeyBindsChanged?.Invoke();
            return base.PerformSave();
        }
    }
}
