// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.IO;
using osu.Framework.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDirectorySelectorParentDirectory : FluentDirectorySelectorDirectory
    {
        protected override IconUsage? DisplayIcon => SegoeFluent.Folder;

        public FluentDirectorySelectorParentDirectory(DirectoryInfo directory)
            : base(directory, "..")
        {
        }
    }
}
