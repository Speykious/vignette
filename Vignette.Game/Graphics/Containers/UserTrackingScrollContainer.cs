// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.Containers
{
    public class UserTrackingScrollContainer : UserTrackingScrollContainer<Drawable>
    {
        public UserTrackingScrollContainer()
        {
        }

        public UserTrackingScrollContainer(Direction direction)
            : base(direction)
        {
        }
    }

    public class UserTrackingScrollContainer<T> : FluentScrollContainer<T>
        where T : Drawable
    {
        public bool UserScrolling { get; private set; }

        public UserTrackingScrollContainer()
        {
        }

        public UserTrackingScrollContainer(Direction direction)
            : base(direction)
        {
        }

        public void CancelUserScroll() => UserScrolling = false;

        protected override void OnUserScroll(float value, bool animated = true, double? distanceDecay = default)
        {
            UserScrolling = true;
            base.OnUserScroll(value, animated, distanceDecay);
        }

        public new void ScrollIntoView(Drawable target, bool animated = true)
        {
            UserScrolling = false;
            base.ScrollIntoView(target, animated);
        }

        public new void ScrollTo(Drawable target, bool animated = true)
        {
            UserScrolling = false;
            base.ScrollTo(target, animated);
        }

        public new void ScrollTo(float value, bool animated = true, double? distanceDecay = null)
        {
            UserScrolling = false;
            base.ScrollTo(value, animated, distanceDecay);
        }

        public new void ScrollToEnd(bool animated = true, bool allowDuringDrag = false)
        {
            UserScrolling = false;
            base.ScrollToEnd(animated, allowDuringDrag);
        }
    }
}
