using StorybrewCommon.Storyboarding;
using OpenTK;

namespace StorybrewScripts
{
    public class Constants 
    {
        public static double beatLength = 454;
        public static float xFloor = -107;
        public static float xCeil = 747;
        public static float width = 854;

        public static float height = 480;

        public static double screenScale = 480.0 / 720;
        public static int glowRadius = 5;
        public static string jpFont = "Source Han Sans JP";
        public static string enFont = "a";
        public static int fontSize = 30;
        public static float fontScale = 0.5f;
    }

    public class Helpers 
    {
        public static OsbSprite moveScaleAndFade(string path, double startTime, double endTime, double x, double y, StoryboardLayer layer) 
        {
            OsbSprite sprite = moveScaleAndFadeIn(path, startTime, endTime, x, y, layer);
            sprite.Fade(endTime, endTime + Constants.beatLength/4, 1, 0);
            return sprite;
        }
        
        public static OsbSprite moveScaleAndFadeIn(string path, double startTime, double endTime, double x, double y, StoryboardLayer layer) 
        {
            OsbSprite sprite = layer.CreateSprite(path, OsbOrigin.Centre);
            sprite.Move(startTime, x, y);
            sprite.Scale(startTime, Constants.screenScale);
            sprite.Fade(OsbEasing.OutSine, startTime, startTime + Constants.beatLength/2, 0, 1);
            return sprite;
        }
        
        public static Vector2 locationHandler(float InitialX, float InitialY) 
        {
            float xRatio = InitialX / 1280;
            float yRatio = InitialY / 720;
            float x = (854 * xRatio) + -107;
            float y = 480 * yRatio;           
            return new Vector2(x, y);
        }

        public static OsbSprite bounceEffect(double startTime, OsbSprite sprite)
        {
            sprite.Scale(OsbEasing.InCubic, startTime, startTime + Constants.beatLength / 4, Constants.screenScale, 0.69);
            startTime = startTime + Constants.beatLength / 4;
            sprite.Scale(OsbEasing.OutCubic, startTime, startTime + Constants.beatLength / 4, 0.69, Constants.screenScale);
            return sprite;
        } 
    }
}