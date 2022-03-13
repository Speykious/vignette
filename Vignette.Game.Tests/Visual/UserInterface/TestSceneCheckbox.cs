// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneCheckbox : UserInterfaceTestScene
    {
        public TestSceneCheckbox()
        {
            Add(new FluentCheckbox
            {
                Text = @"Hello World",
            });

            Add(new FluentCheckbox
            {
                Text = @"Hello World",
                Current = new BindableBool
                {
                    Value = true
                }
            });

            Add(new FluentCheckbox
            {
                Text = @"Hello World",
                Current = new BindableBool
                {
                    Disabled = true
                }
            });

            Add(new FluentCheckbox
            {
                Text = @"Hello World",
                Current = new BindableBool
                {
                    Value = true,
                    Disabled = true
                }
            });

            Add(new FluentSwitch
            {
                Text = @"Hello World",
            });

            Add(new FluentSwitch
            {
                Text = @"Hello World",
                Current = new BindableBool
                {
                    Value = true
                }
            });

            Add(new FluentSwitch
            {
                Text = @"Hello World",
                Current = new BindableBool
                {
                    Disabled = true
                }
            });

            Add(new FluentSwitch
            {
                Text = @"Hello World",
                Current = new BindableBool
                {
                    Value = true,
                    Disabled = true
                }
            });
        }
    }
}
