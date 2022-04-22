// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mediapipe.Net.Calculators;
using Mediapipe.Net.Framework.Protobuf;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using SeeShark;
using SeeShark.Decode;
using SeeShark.Device;
using Vignette.Game.IO;
using ImageFormat = Mediapipe.Net.Framework.Format.ImageFormat;
using ImageFrame = Mediapipe.Net.Framework.Format.ImageFrame;

namespace Vignette.Game.Tracking
{
    public class TrackingComponent : Component
    {
        public IBindable<ImageFrame> CurrentFrame => currentFrame;
        private Bindable<ImageFrame> currentFrame = new Bindable<ImageFrame>();

        public IReadOnlyList<FaceData> Faces
        {
            get
            {
                lock (faces)
                    return faces.ToList();
            }
        }

        [Resolved]
        private MediapipeGraphStore graphStore { get; set; }

        public Bindable<Camera> Camera { get; set; } = new Bindable<Camera>();
        private FrameConverter converter;
        private readonly List<FaceData> faces = new List<FaceData>();

        private FaceMeshCpuCalculator calculator;

        private long timestampCounter = 0;

        [BackgroundDependencyLoader]
        private void load(Bindable<Camera> camera)
        {
#pragma warning disable CA1416
            calculator = new FaceMeshCpuCalculator();
            calculator.OnResult += handleLandmarks;
            calculator.Run();


            this.Camera.BindTo(camera);
            this.Camera.BindValueChanged(handleCamera, true);
        }

        private void handleLandmarks(object sender, List<NormalizedLandmarkList> landmarks)
        {
            lock (faces)
            {
                faces.Clear();
                foreach (var landmark in landmarks)
                    faces.Add(new FaceData(landmark));
            }
        }

        private void handleCamera(ValueChangedEvent<Camera> e)
        {
            if (e.OldValue != null)
            {
                e.OldValue.StopCapture();
                e.OldValue.OnFrame -= onFrameEventHandler;
            }

            if (e.NewValue != null)
            {
                e.NewValue.StartCapture();
                e.NewValue.OnFrame += onFrameEventHandler;
            }
        }

        private void onFrameEventHandler(object sender, SeeShark.FrameEventArgs e)
        {
            if (e.Status != DecodeStatus.NewFrame)
                return;

            Frame frame = e.Frame;

            converter ??= new FrameConverter(frame, PixelFormat.Rgba);
            Frame cFrame = converter.Convert(frame);

            timestampCounter++;

            using ImageFrame inputFrame = new ImageFrame(ImageFormat.Srgba, cFrame.Width, cFrame.Height, cFrame.WidthStep, cFrame.RawData);

            currentFrame.Value?.Dispose();
            currentFrame.Value = calculator.Send(inputFrame);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (Camera.Value != null)
                Camera.Value.OnFrame -= onFrameEventHandler;
        }
    }
}
