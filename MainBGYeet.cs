using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class MainBGYeet : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("");
            var yeetBg = layer.CreateSprite("Meguru.jpg",OsbOrigin.Centre); 
            yeetBg.Fade(0, 0);

            var whiteBg = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            whiteBg.ScaleVec(0, 854, 480);
            whiteBg.Fade(0, 1);
            whiteBg.Fade(805, 0);
        }
    }
}
