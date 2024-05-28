using MetinLitBot.Handlers;
using OpenCvSharp;

namespace MetinLitBot.Models;

public class MetinStone
{
    private Point topLeft;
    private Point bottomRight;

    public MetinStone(Point topLeft, Point bottomRight)
    {
        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }
    
    public MetinStone(OpenCvSharp.Rect rect)
    {
        int width = rect.Width;
        int height = rect.Height;
        this.topLeft = rect.TopLeft;
        this.bottomRight = this.topLeft.Add(new Point(width, height));

    }

    public Point GetClickablePoint()
    {
        StoneHandler stoneHandler = StoneHandler.GetInstance();
        Mat metinImage = stoneHandler.SelectedStone;
        int height = metinImage.Height;
        Point point = new Point(topLeft.X + 15, topLeft.Y + height - 10);

        return point;
    }
    
    public Point TopLeft => topLeft;

    public Point BottomRight => bottomRight;
    
}