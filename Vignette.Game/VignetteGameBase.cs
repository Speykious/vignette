// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using SeeShark.Device;
using SeeShark.FFmpeg;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Input;
using Vignette.Game.IO;
using Vignette.Game.Resources;
using Vignette.Game.Tracking;
using Vignette.Live2D.Resources;
using static SeeShark.FFmpeg.FFmpegManager;

namespace Vignette.Game
{
    public class VignetteGameBase : osu.Framework.Game, IKeyBindingHandler<FrameworkAction>
    {
        public bool IsDebugBuild { get; private set; }

        public virtual Version AssemblyVersion => Assembly.GetExecutingAssembly()?.GetName().Version ?? new Version();

        public bool IsDeployedBuild => AssemblyVersion.Major > 0;

        public static bool IsInsidersBuild => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion.Contains("insiders");

        public virtual string Version
        {
            get
            {
                if (!IsDeployedBuild)
                    return @"local " + (DebugUtils.IsDebugBuild ? @"debug" : @"release");

                var version = AssemblyVersion;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        protected Storage Storage;
        protected VignetteConfigManager LocalConfig;
        protected SessionConfigManager SessionConfig;

        private VignetteKeyBindManager keybindConfig;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private DependencyContainer dependencies;
        private IBindable<bool> resizable;
        private IBindable<bool> showFps;

        private CameraManager cameraManager;
        private Bindable<Camera> camera;
        private Container content;
        private TrackingComponent tracker;

        protected override Container<Drawable> Content => content;

        public VignetteGameBase()
        {
            IsDebugBuild = DebugUtils.IsDebugBuild;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            SetupFFmpeg(
                FFmpegLogLevel.Info,
                ConsoleColor.Yellow,
                AppDomain.CurrentDomain.BaseDirectory,
                "/usr/lib",
                "/usr/lib64"
            );

            Resources.AddStore(new DllResourceStore(VignetteResources.ResourceAssembly));
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), "Resources"));

            // Segoe UI
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-Italic");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-Bold");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-BoldItalic");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-Black");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-BlackItalic");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-Light");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-LightItalic");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-SemiBold");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-SemiBoldItalic");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-SemiLight");
            AddFont(Resources, @"Fonts/SegoeUI/SegoeUI-SemiLightItalic");

            // Spartan
            AddFont(Resources, @"Fonts/Spartan/Spartan-Bold");

            // Icons
            AddFont(Resources, @"Fonts/SegoeFluent/SegoeFluent-Regular");
            AddFont(Resources, @"Fonts/Vignette");

            dependencies.CacheAs(new MediapipeGraphStore(new NamespacedResourceStore<byte[]>(Resources, "Graphs")));
            dependencies.CacheAs(this);
            dependencies.CacheAs(LocalConfig);
            dependencies.CacheAs(SessionConfig = new SessionConfigManager());
            dependencies.CacheAs(tracker = new TrackingComponent());

            dependencies.CacheAs(cameraManager = new CameraManager());
            dependencies.CacheAs(camera = new Bindable<Camera>());

            showFps = LocalConfig.GetBindable<bool>(VignetteSetting.ShowFpsOverlay);
            showFps.BindValueChanged(e => FrameStatistics.Value = e.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None, true);

            resizable = LocalConfig.GetBindable<bool>(VignetteSetting.WindowResizable);

            keybindConfig = new VignetteKeyBindManager(Storage);
            dependencies.CacheAs(keybindConfig);

            base.Content.Add(new SafeAreaContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new DrawSizePreservingFillContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new ThemeManagingContainer(Storage)
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = content = new Container
                            {
                                RelativeSizeAxes = Axes.Both
                            },
                        },
                        new GlobalActionContainer(this, keybindConfig)
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                        new ConfigurationHandler(),
                        tracker,
                    },
                },
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
            LocalConfig ??= IsDebugBuild
                ? new VignetteDevelopmentConfigManager(Storage)
                : new VignetteConfigManager(Storage);
        }

        // Override framework level keybind actions as we're controlling bindables responsible to it
        /*
        bool IKeyBindingHandler<FrameworkAction>.OnPressed(FrameworkAction action)
        {
            switch (action)
            {
                case FrameworkAction.ToggleFullscreen:
                    return !resizable.Value && OnPressed(action);

                default:
                    return OnPressed(action);
            }
        }
        */

        protected override void Dispose(bool isDisposing)
        {
            LocalConfig?.Dispose();
            keybindConfig?.Dispose();
            camera?.Value?.StopCapture();
            camera?.Value?.Dispose();
            cameraManager?.Dispose();
            base.Dispose(isDisposing);
        }

        #region File Handling

        private readonly List<ICanAcceptFiles> fileHandlers = new List<ICanAcceptFiles>();

        public void RegisterFileHandler(ICanAcceptFiles handler) => fileHandlers.Add(handler);

        public void UnregisterFileHandler(ICanAcceptFiles handler) => fileHandlers.Remove(handler);

        protected void FileDropped(string[] paths)
        {
            if (paths.Length == 0)
                return;

            var filesByExtension = paths.GroupBy(p => Path.GetExtension(p).ToLowerInvariant());

            foreach (var group in filesByExtension)
            {
                foreach (var handler in fileHandlers)
                {
                    if (handler.Extensions.Contains(group.Key))
                        handler.FileDropped(group.ToArray());
                }
            }
        }

        #endregion
    }
}
