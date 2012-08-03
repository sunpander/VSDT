using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System;

namespace ImageResizer
{
    public partial class Resizer
    {
         
        //    private static BitmapFrame GetBitmapFrame(MemoryStream original, int width, int height, BitmapScalingMode mode)
        //    {
        //        BitmapDecoder photoDecoder = BitmapDecoder.Create(original, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
        //        BitmapFrame photo = photoDecoder.Frames[0];

        //        TransformedBitmap target = new TransformedBitmap(
        //            photo,
        //            new ScaleTransform(
        //                width / photo.Width * 96 / photo.DpiX,
        //                height / photo.Height * 96 / photo.DpiY,
        //                0, 0));
        //        BitmapFrame thumbnail = BitmapFrame.Create(target);
        //        BitmapFrame newPhoto = Resize(thumbnail, width, height, mode);

        //        return newPhoto;
        //    }

        //    private static BitmapFrame Resize(BitmapFrame photo, int width, int height, BitmapScalingMode scalingMode)
        //    {
        //        DrawingGroup group = new DrawingGroup();
        //        RenderOptions.SetBitmapScalingMode(group, scalingMode);
        //        group.Children.Add(new ImageDrawing(photo, new Rect(0, 0, width, height)));
        //        DrawingVisual targetVisual = new DrawingVisual();
        //        DrawingContext targetContext = targetVisual.RenderOpen();
        //        targetContext.DrawDrawing(group);
        //        RenderTargetBitmap target = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
        //        targetContext.Close();
        //        target.Render(targetVisual);
        //        BitmapFrame targetFrame = BitmapFrame.Create(target);
        //        return targetFrame;
        //    }

        //    private static MemoryStream GetPngStream(BitmapFrame photo)
        //    {
        //        MemoryStream result = new MemoryStream();

        //        PngBitmapEncoder targetEncoder = new PngBitmapEncoder();
        //        targetEncoder.Frames.Add(photo);
        //        targetEncoder.Save(result);

        //        return result;
        //    }

        //    private static MemoryStream GetJpegStream(BitmapFrame photo)
        //    {
        //        MemoryStream result = new MemoryStream();

        //        JpegBitmapEncoder targetEncoder = new JpegBitmapEncoder();
        //        targetEncoder.Frames.Add(photo);
        //        targetEncoder.Save(result);

        //        return result;
        //    }

        //    private static MemoryStream GetBmpStream(BitmapFrame photo)
        //    {
        //        MemoryStream result = new MemoryStream();

        //        BmpBitmapEncoder targetEncoder = new BmpBitmapEncoder();
        //        targetEncoder.Frames.Add(photo);
        //        targetEncoder.Save(result);

        //        return result;
        //    }
      

        //public static MemoryStream ResizePng(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.Unspecified);

        //    return GetPngStream(newphoto);
        //}

        //public static MemoryStream LowPng(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.LowQuality);

        //    return GetPngStream(newphoto);
        //}

        //public static MemoryStream HighPng(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.HighQuality);

        //    return GetPngStream(newphoto);
        //}

        //public static MemoryStream NearestNeighborPng(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.NearestNeighbor);

        //    return GetPngStream(newphoto);
        //}

        //public static MemoryStream ResizeJpeg(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.Unspecified);

        //    return GetJpegStream(newphoto);
        //}

        //public static MemoryStream LowJpeg(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.LowQuality);

        //    return GetJpegStream(newphoto);
        //}

        //public static MemoryStream HighJpeg(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.HighQuality);

        //    return GetJpegStream(newphoto);
        //}

        //public static MemoryStream NearestNeighborJpeg(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.NearestNeighbor);

        //    return GetJpegStream(newphoto);
        //}

        //public static MemoryStream ResizeBmp(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.Unspecified);

        //    return GetBmpStream(newphoto);
        //}

        //public static MemoryStream LowBmp(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.LowQuality);

        //    return GetBmpStream(newphoto);
        //}

        //public static MemoryStream HighBmp(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.HighQuality);

        //    return GetBmpStream(newphoto);
        //}

        //public static MemoryStream NearestNeighborBmp(MemoryStream original, int width, int height)
        //{
        //    BitmapFrame newphoto = GetBitmapFrame(original, width, height, BitmapScalingMode.NearestNeighbor);

        //    return GetBmpStream(newphoto);
        //}
    }


    //class Program
    //{
    //    static void Main(string[] args)
    //    {



    //        Console.Title = "Image Resizer for .NET";

    //        string filePath = @"D:\sunhaifeng\图片\图片\_n.jpg";

    //        Console.WriteLine("Enter the original image path:");
    //        string temp = Console.ReadLine();

    //        if (!string.IsNullOrEmpty(temp))
    //            filePath = temp;

    //        // Low quality PNG
    //        MemoryStream original = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
    //        MemoryStream resizedStreamLow = Resizer.LowPng(original, 400, 225);
    //        File.WriteAllBytes("resizedlow.png", resizedStreamLow.ToArray());

    //        // Low quality JPEG
    //        original = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
    //        resizedStreamLow = Resizer.LowJpeg(original, 400, 225);
    //        File.WriteAllBytes("resizedlow.jpg", resizedStreamLow.ToArray());

    //        // Low quality BMP
    //        original = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
    //        resizedStreamLow = Resizer.LowBmp(original, 400, 225);
    //        File.WriteAllBytes("resizedlow.bmp", resizedStreamLow.ToArray());

    //        Console.WriteLine("Done!");

    //        Console.ReadLine();
    //    }
    //}
}
