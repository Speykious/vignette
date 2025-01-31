// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework;

namespace Vignette.Game.Tests
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using var host = Host.GetSuitableDesktopHost("vignette");
            host.Run(new VisualTestGame());
        }
    }
}
