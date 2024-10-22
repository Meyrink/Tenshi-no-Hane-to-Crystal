using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class SceneCinema : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Cinema");

            var cinema = layer.CreateSprite("sb/scenes/cinema.jpg", OsbOrigin.Centre);
            cinema.Scale(OsbEasing.OutCirc, 58078, 58987, 1, Constants.screenScale);
            cinema.Fade(58078, 1);
            cinema.Fade(73078, 0);
            
            Helpers.bounceEffect(58987, cinema);

            var cinemaHold = layer.CreateSprite("sb/scenes/cinemaHold.jpg", OsbOrigin.Centre);
            cinemaHold.Scale(72851, Constants.screenScale);
            cinemaHold.Fade(OsbEasing.OutSine ,72851, 72851 + 227, 0, 1);
            cinemaHold.Fade(94442, 0); 

            // Meguru Expressions
            Vector2 face = Helpers.locationHandler(974f, 200f);
            Helpers.moveScaleAndFade("sb/scenes/cinemaLayer1.jpg", 66487, 73078, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/cinemaLayer2.jpg", 80124, 87396, face.X, face.Y, layer);
            OsbSprite cinemaFinal = Helpers.moveScaleAndFadeIn("sb/scenes/cinemaLayer3.jpg", 87169, 87169, face.X, face.Y, layer);
            cinemaFinal.Fade(94442, 0);
        }
    }
}