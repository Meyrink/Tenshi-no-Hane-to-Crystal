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

        StoryboardLayer flash;
        public override void Generate()
        {
		    flash = GetLayer("Flash");

            // Intro
            flashIt(805, 1260);
            flashIt(1715, 2169);
            flashIt(2624, 3078);
            flashIt(3533, 3987);
            flashIt(4442, 6260);
            flashIt(6260, 7169);
            // Inst 1
            flashIt(7851, 8305);
            // Verse 1
            flashIt(22169, 22624);     
            // Kiai 1
            flashIt(98760, 100578);
            // Inst 2
            flashIt(127851, 128305);
            // Verse 3
            flashIt(142624, 143078);
            // Kiai 2
            flashIt(182396, 183533);
            // Solo
            flashIt(211487, 212624);
            // Inst 3
            flashIt(266033, 266715);
        }

        private void flashIt(double startTime, double endTime) 
        {
            OsbSprite sprite = flash.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, endTime, startOpacity, 0);
            sprite.Additive(startTime, endTime);
        }
    }
}
