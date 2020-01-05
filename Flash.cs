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
        [Configurable]
        public double startOpacity = 0.30;
        public override void Generate()
        {
		    var flash = GetLayer("Flash");
            int beatDivisor = 1;

            // Intro
            flashIt(805, flash, beatDivisor);
            flashIt(1715, flash, beatDivisor);          
            flashIt(2624, flash, beatDivisor);
            flashIt(3533, flash, beatDivisor);
            flashIt(4442, flash, beatDivisor * 0.25f);
            flashIt(6260, flash, beatDivisor * 0.5f);
            flashIt(7851, flash, beatDivisor);          // Inst 1
            flashIt(22169, flash, beatDivisor);         // Verse 1
            flashIt(98760, flash, beatDivisor);         // Kiai 1 
            flashIt(142624, flash, beatDivisor);        // Verse 3
            flashIt(182396, flash, beatDivisor);        // Kiai 2   
            flashIt(127851, flash, beatDivisor);        // Inst 2
            flashIt(211487, flash, beatDivisor);        // Solo
            flashIt(266033, flash, beatDivisor);        // Inst 3
        }

        private void flashIt(int startTime, StoryboardLayer layer, double beatDivisor) 
        {
            OsbSprite sprite = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var endTime = startTime + Constants.beatLength / beatDivisor;
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, endTime, startOpacity, 0);
            sprite.Additive(startTime, endTime);
        }
    }
}
