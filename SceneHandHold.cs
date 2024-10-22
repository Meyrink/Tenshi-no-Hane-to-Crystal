using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class SceneHandHold : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("HandHold");

            var handHold = layer.CreateSprite("sb/scenes/handhold.jpg", OsbOrigin.Centre);
            handHold.Scale(98760, Constants.screenScale);
            handHold.Fade(98760, 1);
            handHold.Fade(126033, 0);
            
            // Meguru Expressions
            Vector2 face = Helpers.locationHandler(930f, 177f);
            Helpers.moveScaleAndFade("sb/scenes/handholdLayer1.jpg", 105805, 113078, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/handholdLayer2.jpg", 112851, 121942, face.X, face.Y, layer);
            OsbSprite handHoldfinal = Helpers.moveScaleAndFadeIn("sb/scenes/handholdLayer3.jpg", 121715, 121715, face.X, face.Y, layer);
            handHoldfinal.Fade(126033, 0);
        }
    }
}