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
    public class WhiteBg : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("White");

            var bg = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/pixel.png");
            bg.ScaleVec(0, 854.0 / bitmap.Width, 480.0 / bitmap.Height);
            bg.Fade(0, 1);
            bg.Fade(293668, 1);

            var shadow = layer.CreateSprite("sb/shadow.png", OsbOrigin.Centre);            
            shadow.Fade(0, 1);
            shadow.Fade(293668, 1);
            
        }
    }
}
