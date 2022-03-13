// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Graphics.Containers
{
    /// <summary>
    /// An abstract implementation of a view that is a child of <see cref="ViewContainer"/>.
    /// </summary>
    public abstract class View : VisibilityContainer, IView
    {
        protected override bool StartHidden => true;

        protected override void PopIn() => this.FadeIn();

        protected override void PopOut() => this.FadeOut();

        public virtual void OnExiting(IView last)
        {
        }

        public virtual void OnEntering(IView last)
        {
        }
    }
}
