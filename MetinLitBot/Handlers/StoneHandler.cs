using OpenCvSharp;

namespace MetinLitBot.Handlers;

public class StoneHandler
{
    private static StoneHandler instance;
    private Mat selectedStone;

    public static StoneHandler GetInstance()
    {
        if (instance == default)
        {
            instance = new StoneHandler();
        }

        return instance;
    }

    private StoneHandler() { }

    public void Initialize()
    {
        Mat metinImage = Cv2.ImRead(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\metinTop.png",
            ImreadModes.Unchanged);
        selectedStone = metinImage;
    }
    
    public Point[][] FindPoints(Mat result)
    {
        double threshold = 0.50;
            
        Mat binaryMask = new Mat();
        Cv2.Threshold(result, binaryMask, threshold, 255, ThresholdTypes.Binary);
        binaryMask.ConvertTo(binaryMask, MatType.CV_8UC1);

        Cv2.FindContours(binaryMask, out Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.CComp, ContourApproximationModes.ApproxSimple);
        return contours;
    }
    
    public Mat SelectedStone
    {
        get => selectedStone;
        set => selectedStone = value;
    }
}