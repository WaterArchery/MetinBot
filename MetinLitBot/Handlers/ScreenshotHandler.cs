using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using MetinLitBot.Libs;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace MetinLitBot.Handlers;

public class ScreenshotHandler
{
    private static ScreenshotHandler instance;
    private int proccessID;
    private string proccessName = "Farm2";

    public static ScreenshotHandler GetInstance()
    {
        if (instance == default)
        {
            instance = new ScreenshotHandler();
        }

        return instance;
    }

    private ScreenshotHandler()
    {
        proccessID = FindMetin2Proccess();
    }


    public Bitmap CaptureApplication()
    {
        var proc = Process.GetProcessById(proccessID);
        var rect = new User32.Rect();
        User32.GetWindowRect(proc.MainWindowHandle, ref rect);

        int width = rect.right - rect.left;
        int height = rect.bottom - rect.top;

        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using (Graphics graphics = Graphics.FromImage(bmp))
        {
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new System.Drawing.Size(width, height),
                CopyPixelOperation.SourceCopy);
        }

        return bmp;
    }

    public int FindMetin2Proccess()
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.MainWindowTitle.Contains(proccessName))
            {
                Console.WriteLine(process.MainWindowTitle + " Found!");
                return process.Id;
            }
        }

        return -1;
    }

    public Mat GetScreenShot()
    {
        Bitmap map = CaptureApplication();
        Mat mat = BitmapConverter.ToMat(map);

        return mat;
    }
}