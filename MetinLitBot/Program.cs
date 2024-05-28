using MetinLitBot.Handlers;
using OpenCvSharp;

namespace MetinLitBot
{
    
    class Program
    {
        
        public static void Main()
        {
            // Initializing instances
            StoneHandler stoneHandler = StoneHandler.GetInstance();
            ScreenshotHandler screenshotHandler = ScreenshotHandler.GetInstance();
            DrawHandler drawHandler = DrawHandler.GetInstance();

            // Initializing ston
            stoneHandler.Initialize();
            using Mat selectedMetin = stoneHandler.SelectedStone;
            using Mat damageImage = Cv2.ImRead(AppDomain.CurrentDomain.BaseDirectory + "Images\\damage.png",
                ImreadModes.Unchanged);

            while (true)
            {
                // Taking screenshot of the screen
                Mat screenshot = screenshotHandler.GetScreenShot();

                // Detecting damage and Metin stone locations
                using Mat metinResult = screenshot.MatchTemplate(selectedMetin, TemplateMatchModes.CCoeffNormed);
                using Mat damageResult = screenshot.MatchTemplate(damageImage, TemplateMatchModes.CCoeffNormed);

                // Showing damage text if more than %34 percent sureness
                damageResult.MinMaxLoc(out double damageMinVal, out double damageMaxVal, out Point damageMinLoc,
                    out Point damageMaxLoc);
                if (damageMaxVal > 0.34)
                {
                    drawHandler.DrawRect(damageMaxLoc, damageImage.Width, damageImage.Height, screenshot);
                }

                // Finding points and drawing them on screenshot
                Point[][] points = stoneHandler.FindPoints(metinResult);
                drawHandler.DrawAll(points, screenshot);

                // Showing result
                Cv2.ImShow("Result", screenshot);
                Cv2.WaitKey(100);
            }
        }
        
    }
}