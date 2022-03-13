// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneTextBox : UserInterfaceTestScene
    {
        public TestSceneTextBox()
        {
            Add(new FluentTextBox
            {
                Width = 200,
                PlaceholderText = "Simple",
            });

            Add(new TestLetterOnlyTextBox
            {
                Width = 200,
                PlaceholderText = "Letters Only",
            });

            Add(new FluentTextBox
            {
                Width = 200,
                ReadOnly = true,
                Text = @"Read Only",
            });

            Add(new FluentTextBox
            {
                Width = 200,
                PlaceholderText = "Disabled",
                Current = new Bindable<string>
                {
                    Disabled = true,
                },
            });

            Add(new FluentSearchBox
            {
                Width = 200,
            });
        }

        private class TestLetterOnlyTextBox : FluentTextBox
        {
            protected override TextInputContainer CreateTextBox()
                => new LetterOnlyInputBox();

            private class LetterOnlyInputBox : TextInputContainer
            {
                protected override bool CanAddCharacter(char character) => char.IsLetter(character);
            }
        }
    }
}
