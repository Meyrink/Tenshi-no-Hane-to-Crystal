using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class MainBGYeet : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var yeet = GetLayer("Yeet");
            var yeetBg = yeet.CreateSprite("Meguru.jpg",OsbOrigin.Centre); 
            yeetBg.Fade(0, 0);

            var white = GetLayer("WhiteBG");
            var whiteBg = white.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/pixel.png");

            whiteBg.ScaleVec(0, 854.0 / bitmap.Width, 480.0 / bitmap.Height);

            whiteBg.Fade(0, 1);
            whiteBg.Fade(805, 0);

            whiteBg.Fade(6260, 1);
            whiteBg.Fade(7851, 0);

        }
    }
}
