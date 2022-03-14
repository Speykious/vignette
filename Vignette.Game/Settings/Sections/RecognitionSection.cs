// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Localisation;
using osuTK;
using SeeShark;
using SeeShark.Decode;
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

        [Resolved]
        private CameraManager cameraManager { get; set; }

        private readonly BindableList<string> devices = new BindableList<string>();

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
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
                            Current = config.GetBindable<string>(VignetteSetting.CameraIndex),
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

            devices.AddRange(cameraManager.Devices.ToArray().Select((info) => info.ToString()));
            cameraManager.OnNewDevice += onNewCameraDevice;
            cameraManager.OnLostDevice += onLostCameraDevice;
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
            private void load(Bindable<Camera> camera)
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

                camera.BindValueChanged(handleCamera, true);
            }

            private void handleCamera(ValueChangedEvent<Camera> e)
            {
                Schedule(() => preview.Hide());

                texture?.Dispose();
                texture = null;

                if (e.OldValue != null)
                    e.OldValue.OnFrame -= onFrameEventHandler;


                if (e.NewValue != null)
                    e.NewValue.OnFrame += onFrameEventHandler;
            }

            private void onFrameEventHandler(object sender, SeeShark.FrameEventArgs e)
            {
                if (e.Status != DecodeStatus.NewFrame)
                    return;

                Frame frame = e.Frame;
                var pixelData = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(frame.RawData, frame.Width, frame.Height);

                texture ??= new Texture(frame.Width, frame.Height);
                texture.SetData(new TextureUpload(pixelData));

                preview.Texture = texture;
                Schedule(() => preview.Show());
            }
        }
    }
}
