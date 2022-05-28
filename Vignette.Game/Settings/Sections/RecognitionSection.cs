// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using System.Linq;
using Mediapipe.Net.Framework.Format;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Localisation;
using osuTK;
using SeeShark.Device;
using SixLabors.ImageSharp.PixelFormats;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Settings.Components;
using Vignette.Game.Tracking;

namespace Vignette.Game.Settings.Sections
{
    public class RecognitionSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.EyeShow;

        public override LocalisableString Label => "Recognition";

        public event Action CalibrateAction;

        private readonly BindableList<string> devices = new BindableList<string>();
        private Bindable<string> currentDevice;
        private Bindable<int> cameraIndex;

        [Resolved(canBeNull: true)]
        private TrackingComponent tracker { get; set; }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, CameraManager cameraManager)
        {
            devices.AddRange(cameraManager.Devices.ToArray().Select((info) => info.ToString()));
            cameraIndex = config.GetBindable<int>(VignetteSetting.CameraIndex);

            try
            {
                currentDevice = new Bindable<string>(devices[cameraIndex.Value]);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Error.WriteLine("Warning: No cameras? \\:v");
                currentDevice = new Bindable<string>("No Camera :(");
            }

            currentDevice.ValueChanged += onNewCameraSelected;
            cameraManager.OnNewDevice += onNewCameraDevice;
            cameraManager.OnLostDevice += onLostCameraDevice;

            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Label = "Camera",
                    Children = new Drawable[]
                    {
                        new CameraTrackingPreview(),
                        new SettingsDropdown<string>
                        {
                            Icon = SegoeFluent.Camera,
                            Label = "Device",
                            ItemSource = devices,
                            Current = currentDevice,
                        },
                    }
                },
                new ThemableTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.TopLeft,
                    Text = "To properlay calibrate, please open your mouth and eyes wide first!",
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5, 0),
                    Child = new FluentButton
                    {
                        Text = "Calibrate",
                        Width = 90,
                        Style = ButtonStyle.Secondary,
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Action = () => CalibrateAction?.Invoke(),
                    },
                },
            };
        }

        private void onNewCameraSelected(ValueChangedEvent<string> e)
        {
            cameraIndex.Value = devices.IndexOf(e.NewValue);
        }

        private void onNewCameraDevice(CameraInfo info)
        {
            if (!devices.Contains(info.ToString()))
                devices.Add(info.ToString());
        }

        private void onLostCameraDevice(CameraInfo info)
        {
            if (devices.Contains(info.ToString()))
                devices.Remove(info.ToString());
        }

        private class CameraTrackingPreview : Container
        {
            [Resolved(canBeNull: true)]
            private TrackingComponent tracker { get; set; }

            private Sprite preview;
            private Texture texture;

            [BackgroundDependencyLoader]
            private void load()
            {
                Size = new Vector2(250, 140);
                Masking = true;
                CornerRadius = 5;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Black,
                    },
                    preview = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                };

                tracker.CurrentFrame.BindValueChanged(handleImageFrame, true);
            }

            private void handleImageFrame(ValueChangedEvent<ImageFrame> e)
            {
                ImageFrame image = e.NewValue;
                if (image == null || image.IsDisposed)
                    return;

                lock (image)
                {
                    byte[] pixelBytes = e.NewValue.CopyToByteBuffer(image.PixelDataSize);
                    var pixelData = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelBytes, image.Width, image.Height);

                    texture ??= new Texture(image.Width, image.Height);
                    texture.SetData(new TextureUpload(pixelData));
                }

                preview.Texture = texture;
                Schedule(() => preview.Show());
            }
        }
    }
}
