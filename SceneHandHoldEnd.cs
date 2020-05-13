using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class SceneHandHoldEnd : StoryboardObjectGenerator
    {
        [Configurable]
        public int barAmount = 30;

        [Configurable]
        public OsbEasing easing = OsbEasing.None;

        [Configurable]
        public Color4 color = Color4.Black;
        
        public override void Generate()
        {
            double startTime = 126033;
            double endTime = 127624;

		    var layer = GetLayer("Kiai 1 End");

            var card1 = layer.CreateSprite("sb/card/card1.jpg", OsbOrigin.Centre);
            card1.Fade(startTime, 1);
            card1.Scale(startTime, Constants.screenScale);
            card1.Fade(endTime, 0);

            var mask = layer.CreateSprite("sb/card/cardMask.jpg", OsbOrigin.Centre);
            mask.Scale(startTime, Constants.screenScale);
            mask.Fade(startTime, startTime + Constants.beatLength * 2, 0.5f, 0);
            mask.Additive(startTime, startTime + Constants.beatLength * 2f);  

            transitionIn(layer, 126942, 127624);
        }

        private void transitionIn(StoryboardLayer layer, double startTime, double endTime)
        {
            double width = 854 / barAmount;
            double height = 480;
            double barDuration = (endTime - startTime) / barAmount;
            double relativeStart = startTime;

            for (int i = 1; i <= barAmount; i++)
            {
                var sprite = layer.CreateSprite("sb/pixel.png", OsbOrigin.BottomRight, new Vector2((float)(-107 + i * width), (float)480.0));
                sprite.Color(startTime, color); 
                sprite.Fade(relativeStart, 1);
                sprite.Fade(endTime, 0);
                sprite.ScaleVec(easing, relativeStart, relativeStart + barDuration, 0, height, width, height);
                relativeStart += barDuration;
            }
        }
    }
}