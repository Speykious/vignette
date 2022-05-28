// Copyright (c) The Vignette Authors
// This file is part of Vignette.
// Vignette is licensed under the GPL v3 License (With SDK Exception). See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using osu.Framework.Extensions.ImageExtensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Vignette.Game.Graphics
{
    public static class Utils
    {
        public static byte[] ConvertRaw<TPixelIn, TPixelOut>(byte[] data, int width, int height)
        where TPixelIn : unmanaged, IPixel<TPixelIn>
        where TPixelOut : unmanaged, IPixel<TPixelOut>
        {
            Image<TPixelIn> start = Image.LoadPixelData<TPixelIn>(data, width, height);

            ReadOnlyPixelSpan<TPixelIn> pixelSpan = start.CreateReadOnlyPixelSpan();
            ReadOnlySpan<TPixelIn> pixels = pixelSpan.Span;

            TPixelOut[] dest = new TPixelOut[pixels.Length];
            Span<TPixelOut> destination = new Span<TPixelOut>(dest);
            PixelOperations<TPixelIn>.Instance.To(new SixLabors.ImageSharp.Configuration(), pixels, destination);
            start.Dispose();
            return MemoryMarshal.AsBytes(destination).ToArray();
        }
    }
}
