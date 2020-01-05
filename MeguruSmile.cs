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

            double endTime = startTime + Constants.beatLength * 4 * 2;
            double frameDelay = (endTime - startTime) / 47;

            var smile = layer.CreateAnimation("sb/outro/smile.jpg", frameCount, frameDelay, OsbLoopType.LoopOnce, OsbOrigin.Centre);
            smile.Scale(startTime, Constants.screenScale);
            smile.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            smile.Fade(endTime -  Constants.beatLength * 1, endTime, 1, 0);
        }
    }
}
