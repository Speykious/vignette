// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Reflection;
using osu.Framework.Input;
using osu.Framework.Platform;
using Vignette.Game;

namespace Vignette.Desktop
{
    public class VignetteGameDesktop : VignetteGame
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            if (host.Window is not SDL2DesktopWindow window)
                return;

            window.ConfineMouseMode.Value = ConfineMouseMode.Never;
            window.DragDrop += f => FileDropped(new[] { f });
            window.Title = Name;

            string icon = IsInsidersBuild ? "vignette-insiders.ico" : "vignette.ico";
            window.SetIconFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), icon));
        }
    }
}
