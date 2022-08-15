// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using System.Drawing;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using SeeShark.Device;
using Vignette.Game.Configuration;

namespace Vignette.Game
{
    public class ConfigurationHandler : Component
    {
        private Bindable<bool> resizable;
        private Bindable<Size> windowSize;
        private Bindable<WindowMode> windowMode;

        private Bindable<Size> frameworkResolution;
        private Bindable<Size> gameResolution;
        private readonly BindableSize resolution = new BindableSize();

        private Bindable<bool> isMuted;
        private readonly BindableDouble muteAdjustment = new BindableDouble();

        private Bindable<int> cameraIndex;
        private Bindable<Camera> camera;
        private CameraManager cameraManager;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, AudioManager audio, CameraManager cameraManager, Bindable<Camera> camera, GameHost host)
        {
            this.camera = camera;
            this.cameraManager = cameraManager;

            // We cannot disable the bindable obtained from framework config as it is internally modified.
            windowSize = frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize);
            windowMode = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode);

            frameworkResolution = frameworkConfig.GetBindable<Size>(FrameworkSetting.SizeFullscreen);
            gameResolution = gameConfig.GetBindable<Size>(VignetteSetting.WindowSize);

            resizable = gameConfig.GetBindable<bool>(VignetteSetting.WindowResizable);
            resizable.BindValueChanged(e =>
            {
                if (host.Window is SDL2DesktopWindow window)
                    window.Resizable = e.NewValue;

                resolution.TriggerChange();
            }, true);

            resolution.BindValueChanged(e =>
            {
                if (!resizable.Value && windowMode.Value == WindowMode.Windowed)
                    windowSize.Value = e.NewValue;
            });

            windowMode.BindValueChanged(e =>
            {
                resizable.Disabled = e.NewValue != WindowMode.Windowed;

                resolution.UnbindBindings();
                resolution.BindTo(e.NewValue != WindowMode.Windowed ? frameworkResolution : gameResolution);
            }, true);

            isMuted = gameConfig.GetBindable<bool>(VignetteSetting.SoundMuted);
            isMuted.BindValueChanged(e =>
            {
                if (isMuted.Value)
                    audio.AddAdjustment(AdjustableProperty.Volume, muteAdjustment);
                else
                    audio.RemoveAdjustment(AdjustableProperty.Volume, muteAdjustment);
            }, true);

            cameraIndex = gameConfig.GetBindable<int>(VignetteSetting.CameraIndex);
            cameraIndex.BindValueChanged(onCameraIndexChanged, true);
        }

        private static readonly SeeShark.VideoInputOptions ultraSpecificSpeykiousOptionsOfDoom = new SeeShark.VideoInputOptions
        {
            InputFormat = "mjpeg",
            VideoSize = (640, 480),
        };

        private void onCameraIndexChanged(ValueChangedEvent<int> e)
        {
            try
            {
                Console.WriteLine($"Getting camera {e.NewValue}");
                camera.Value = cameraManager.GetDevice(e.NewValue, e.NewValue == 1 ? ultraSpecificSpeykiousOptionsOfDoom : null);
                camera.Value.StartCapture();
                Console.WriteLine($"Got camera {camera.Value}");
            }
            catch (Exception ex)
            {
                camera.Value?.StopCapture();
                Console.Error.WriteLine($"Error: couldn't open camera at index {e.NewValue}\n{ex}");
            }
        }
    }
}
