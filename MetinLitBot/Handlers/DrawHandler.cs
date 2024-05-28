using MetinLitBot.Models;
using OpenCvSharp;

namespace MetinLitBot.Handlers;

public class DrawHandler
{
    private static DrawHandler instance;

    public static DrawHandler GetInstance()
    {
        if (instance == default)
        {
            instance = new DrawHandler();
        }

        return instance;
    }

    public void DrawRect(Point topLeft, int width, int height, Mat screenImage)
    {
        Point bottomRight = new Point(topLeft.X + width, topLeft.Y + height);
        Cv2.Rectangle(screenImage, topLeft, bottomRight, Scalar.Black, 2, 0);
    }

    public void DrawAll(Point[][] points, Mat screenshot)
    {
        StoneHandler stoneHandler = StoneHandler.GetInstance();
        Mat selectedMetin = stoneHandler.SelectedStone;

        foreach (Point[] contour in points)
        {
            OpenCvSharp.Rect boundingRect = Cv2.BoundingRect(contour);
            MetinStone stone = new MetinStone(boundingRect);

            int width = selectedMetin.Width;
            int height = selectedMetin.Height;
            Cv2.Rectangle(screenshot, boundingRect.TopLeft, boundingRect.BottomRight.Add(new Point(width, height)),
                Scalar.Red, 2);

            Point point = stone.GetClickablePoint();
            Cv2.Rectangle(screenshot, point, point, Scalar.Aqua, 2);
        }
    }
}