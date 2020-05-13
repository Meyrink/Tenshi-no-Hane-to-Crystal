using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class LensFlare : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("");

            var Flare1 = layer.CreateSprite("sb/flares/flare1.jpg", OsbOrigin.Centre, new Vector2(50, 150));
            double startTime = 805;
            double endTime = 1715;
            Flare1.Fade(startTime, 1);
            Flare1.FlipH(startTime, endTime);
            Flare1.Additive(startTime, endTime);
            Flare1.Rotate(startTime, endTime, MathHelper.DegreesToRadians(-40), MathHelper.DegreesToRadians(-45));
            Flare1.Fade(endTime, 0);
            
            var Flare2 = layer.CreateSprite("sb/flares/flare2.jpg", OsbOrigin.Centre, new Vector2(410+70, 190 - 20));
            startTime = 2624;
            endTime = 3533;
            Flare2.Fade(startTime, 0.3);
            Flare2.Additive(startTime, endTime);
            Flare2.Rotate(startTime, endTime, 0, MathHelper.DegreesToRadians(3));
            Flare2.Fade(endTime, 0);
            
            startTime = endTime;
            endTime = 4442;
            var Light1 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(-107, 66));
            Light1.Additive(startTime, endTime);
            Light1.Fade(startTime, endTime, 0, 0.2);

            var Light2 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(710, 250));
            Light2.Additive(startTime, endTime);
            Light2.Fade(startTime, endTime, 0, 0.2);
            
            var Flare3 = layer.CreateSprite("sb/flares/flare2.jpg", OsbOrigin.Centre, new Vector2(560, 215));
            startTime = endTime;
            endTime = 6260;
            Flare3.Fade(startTime, 0.3);
            Flare3.Additive(startTime, endTime);
            Flare3.Rotate(startTime, endTime, 0, MathHelper.DegreesToRadians(3));
            Flare3.Fade(endTime, 0);

            var Light3 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(409, 234));
            Light3.Additive(startTime, endTime);
            Light3.Fade(startTime, endTime, 0, 0.2);
        }
    }   
}