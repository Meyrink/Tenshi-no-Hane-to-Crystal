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
    public class Flash : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Flash");
            var flash = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/pixel.png");
            flash.ScaleVec(0, 854.0 / bitmap.Width, 480.0 / bitmap.Height);
            flash.Fade(0, 0);

            double startTime = 22169;
            flash.Fade(startTime, startTime + Constants.beatLength/4, 0.7, 0); // Verse 1

            startTime = 58533;
            flash.Fade(startTime, startTime + Constants.beatLength/2, 0.7, 0); // Verse 2

            startTime = 98760;
            flash.Fade(startTime, startTime + Constants.beatLength/2, 0.7, 0); // Kiai 1 

            startTime = 142624;
            flash.Fade(startTime, startTime + Constants.beatLength/2, 0.7, 0); // Verse 3

            startTime = 182396;
            flash.Fade(startTime, startTime + Constants.beatLength/2, 0.7, 0); // Kiai 2       
        }
    }
}
