// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;

namespace Vignette.Game.Graphics.UserInterface
{
    public class ScreenBreadcrumbControl : FluentBreadcrumbControl<IScreen>
    {
        public ScreenBreadcrumbControl(ScreenStack stack)
        {
            stack.ScreenPushed += screenPushed;
            stack.ScreenExited += screenExited;

            if (stack.CurrentScreen != null)
                screenPushed(null, stack.CurrentScreen);
        }

        protected override void SelectTab(TabItem<IScreen> tab)
            => tab.Value.MakeCurrent();

        private void screenPushed(IScreen lastScreen, IScreen newScreen)
        {
            AddItem(newScreen);
            Current.Value = newScreen;
        }

        private void screenExited(IScreen lastScreen, IScreen newScreen)
        {
            if (newScreen != null)
                Current.Value = newScreen;

            Items.ToList().SkipWhile(s => s != Current.Value).Skip(1).ForEach(RemoveItem);
        }
    }
}
