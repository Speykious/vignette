// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Graphics.Containers
{
    public class FluentContextMenuContainer : ContextMenuContainer
    {
        protected override Menu CreateMenu() => new FluentMenu(Direction.Vertical);
    }
}
