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
    public class MeguruSmile : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 281715;
        [Configurable]
        public int frameCount = 47;
        public override void Generate()
        {
		    var layer = GetLayer("Outro");

            var tempBg = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBg.ScaleVec(282851, 854, 480);
            
            tempBg.Fade(280124, 280578, 0, 1);
            tempBg.Fade(280805, 0);

            tempBg.Fade(282851, 1);
            tempBg.Fade(293668, 0);

            double endTime = startTime + Constants.beatLength * 6;
            double frameDelay = (endTime - startTime) / 47;

            var smile = layer.CreateAnimation("sb/outro/smile.jpg", frameCount, frameDelay, OsbLoopType.LoopOnce, OsbOrigin.Centre);
            smile.Scale(startTime, Constants.screenScale);
            smile.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            smile.Fade(endTime -  Constants.beatLength * 1, endTime, 1, 0);

            var fin = layer.CreateSprite("sb/end.png", OsbOrigin.Centre);
            fin.Fade(283987, 284442, 0, 1);
            fin.Scale(284442, Constants.screenScale);
            fin.Fade(287851, 288078, 1, 0);
        }
    }
}
