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
            flashOut(805, 1260, true);
            flashOut(1715, 2169, true);
            flashOut(2624, 3078, true);
            flashOut(3533, 3987, true);
            flashOut(4442, 6260, true);
            flashOut(6260, 7169, true);

            // Inst 1
            flashOut(7851, 8305, true);

            // Verse 1
            flashOut(22169, 22396, true); 

            // Kiai 1
            flashIn(98533, 98760, Color4.LightSkyBlue);
            flashColor(98760, 100351, Color4.LightSkyBlue);
            flashIn(125805, 126033, Color4.LightSkyBlue);

            // Inst 2
            flashOut(127851, 128305, true);

            // Verse 3
            flashOut(142624, 143078, true);

            // Kiai 2
            flashOut(182396, 183533, true);
            flashIn(209442, 209669, Color4.SteelBlue);

            // Solo
            flashColor(211487, 212624, Color4.LightGoldenrodYellow);
            
            // Inst 3
            // flashOut(266033, 266715, true);
        }

        private void flashOut(double startTime, double endTime, bool additive) 
        {
            OsbSprite sprite = flash.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, endTime, startOpacity, 0);
            if (additive) sprite.Additive(startTime, endTime);
        }

        private void flashColor(double startTime, double endTime, Color4 color) 
        {
            OsbSprite sprite = flash.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Color(startTime, color);
            sprite.Fade(startTime, endTime, 0.4, 0);
            sprite.Additive(startTime, endTime);
        }

        private void flashIn(double startTime, double endTime, Color4 color)
        {
            var sprite = flash.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, endTime, 0, 0.3);
            sprite.Additive(startTime, endTime);
            sprite.Color(startTime, color);
        }
    }
}
