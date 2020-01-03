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
        [Configurable]
        public float WhiteBGOpacity = 0.8f; 
        public override void Generate()
        {
		    var layer = GetLayer("White BG");
            var yeetBg = layer.CreateSprite("Meguru.jpg",OsbOrigin.Centre); 
            yeetBg.Fade(0, 0);

            var whiteBg = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/pixel.png");

            whiteBg.ScaleVec(0, 854.0 / bitmap.Width, 480.0 / bitmap.Height);

            whiteBg.Fade(0, WhiteBGOpacity);
            whiteBg.Fade(805, 0);

            whiteBg.Fade(6260, WhiteBGOpacity);
            whiteBg.Fade(7851, 0);
        }
    }
}
