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
        public float startOpacity = 0.30f;
        [Configurable]
        public float beatDivisor = 1.0f;

        public override void Generate()
        {
		    var layer = GetLayer("Flash");
            var flash = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/pixel.png");
            flash.ScaleVec(0, 854.0 / bitmap.Width, 480.0 / bitmap.Height);
            flash.Fade(0, 0);

            // Intro
            flashIt(805, flash, beatDivisor, startOpacity); // BG1
            flashIt(1715, flash, beatDivisor, startOpacity); // BG2
            flashIt(2624, flash, beatDivisor, startOpacity); // BG3
            flashIt(3533, flash, beatDivisor, startOpacity); // BG4
            flashIt(4442, flash, beatDivisor / 4, startOpacity); // BG5
            flashIt(6260, flash, beatDivisor / 2, startOpacity); // Credit

            flashIt(7851, flash, beatDivisor, startOpacity); // Inst 1
            flashIt(22169, flash, beatDivisor, startOpacity); // Verse 1
            flashIt(58533, flash, beatDivisor, startOpacity); // Verse 2
            flashIt(98760, flash, beatDivisor, startOpacity); // Kiai 1 
            flashIt(142624, flash, beatDivisor, startOpacity); // Verse 3
            flashIt(182396, flash, beatDivisor, startOpacity); // Kiai 2   
            flashIt(127851, flash, beatDivisor, startOpacity); // Inst 2
            flashIt(211487, flash, beatDivisor, startOpacity); // Solo
            flashIt(266033, flash, beatDivisor, startOpacity); // Inst 3
        }

        private static OsbSprite flashIt (int startTime, OsbSprite sprite, double beatDivisor, float startOpacity) 
        {
            var endTime = startTime + Constants.beatLength / beatDivisor;
            sprite.Fade(startTime, endTime, startOpacity, 0);
            sprite.Additive(startTime, endTime);
            return sprite;
        }
    }
}
