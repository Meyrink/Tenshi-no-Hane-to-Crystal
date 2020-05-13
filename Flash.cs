using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Flash : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // Intro
            flashOut(805, 1260);
            flashOut(1715, 2169);
            flashOut(2624, 3078);
            flashOut(3533, 3987);
            flashOut(4442, 6260);
            flashOut(6260, 7169);

            // Inst 1
            flashOut(7851, 8305);

            // Verse 1
            flashOut(22169, 22396); 

            // Kiai 1
            flashInColor(0.3f, 98533, 98760, Color4.LightSkyBlue);
            flashOutColor(98760, 100351, Color4.LightSkyBlue);
            flashInColor(0.3f, 125805, 126033, Color4.LightSkyBlue);
            cinematicColor(113305, Color4.LightSkyBlue, 4);

            // Inst 2
            flashOut(127851, 128305);

            // Verse 3
            flashOut(142624, 143078);

            // Kiai 2
            flashInColor(0.3f, 182169, 182396, Color4.SteelBlue);
            flashOut(182396, 183533);
            flashInColor(0.3f, 209442, 209669, Color4.SteelBlue);
            cinematicColor(196942, Color4.SteelBlue, 4);

            // Solo
            flashOutColor(211487, 212624, Color4.LightGoldenrodYellow);
            cinematicColor(213305, Color4.LightGoldenrodYellow, 3);
            cinematicColor(216942, Color4.LightGoldenrodYellow, 3);
            cinematicColor(219896, Color4.AliceBlue, 2);
            cinematicColor(222624, Color4.CadetBlue, 3);
            
            // Kiaot 3 to Inst 3
            flashInColor(0.3f, 265351, 266033, Color4.CadetBlue);
            flashOutColor(266033, 266715, Color4.LightSkyBlue);
            cinematicColor(274669, Color4.LightSkyBlue, 4);
        }

        private void flashOut(double startTime, double endTime) 
        {
            var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, endTime, 0.3, 0);
            sprite.Additive(startTime, endTime);
        }

        private void flashOutColor(double startTime, double endTime, Color4 color) 
        {
            var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Color(startTime, color);
            sprite.Fade(startTime, endTime, 0.3, 0);
            sprite.Additive(startTime, endTime);
        }

        private void flashInColor(float opacity, double startTime, double endTime, Color4 color)
        {
            var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, endTime, 0, opacity);
            sprite.Additive(startTime, endTime);
            sprite.Color(startTime, color);
        }

        private void cinematicColor(double startTime, Color4 color, double duration)
        {
            var fadeTime = startTime + Constants.beatLength * duration;
            var endTime = fadeTime + Constants.beatLength * duration;
            double opacity = 0.1;
            var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Additive(startTime, endTime);
            sprite.Color(startTime, color);
            sprite.Fade(startTime, fadeTime, 0, opacity);
            sprite.Fade(fadeTime, endTime, opacity, 0);
        }
    }
}