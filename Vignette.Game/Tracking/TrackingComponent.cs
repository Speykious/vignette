// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Solutions;
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
        public IBindable<Frame> CurrentFrame => currentFrame;
        private Bindable<Frame> currentFrame = new Bindable<Frame>();

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

        private FaceMeshCpuSolution solution;

        [BackgroundDependencyLoader]
        private void load(Bindable<Camera> camera)
        {
#pragma warning disable CA1416
            solution = new FaceMeshCpuSolution();

            Camera.BindTo(camera);
            Camera.BindValueChanged(handleCamera, true);
        }

        private void handleLandmarks(object _, List<NormalizedLandmarkList> landmarks)
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

        private void onFrameEventHandler(object sender, FrameEventArgs e)
        {
            if (e.Status != DecodeStatus.NewFrame)
                return;

            Frame frame = e.Frame;

            converter ??= new FrameConverter(frame, PixelFormat.Rgba);
            Frame cFrame = converter.Convert(frame);

            using ImageFrame inputFrame = new ImageFrame(ImageFormat.Srgba, cFrame.Width, cFrame.Height, cFrame.WidthStep, cFrame.RawData);
            List<NormalizedLandmarkList> landmarks = solution.Compute(inputFrame);
            handleLandmarks(this, landmarks);
            currentFrame.Value = cFrame;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (Camera.Value != null)
                Camera.Value.OnFrame -= onFrameEventHandler;
        }
    }
}
