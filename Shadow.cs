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
    public class Shadow : StoryboardObjectGenerator
    {
        [Configurable]
        public float opacity = 1;
        public override void Generate()
        {
            var layer = GetLayer("Shadow");
            var shadow = layer.CreateSprite("sb/damnae-vignette-854x480.png", OsbOrigin.Centre);            
            shadow.Fade(0, opacity);
            shadow.Fade(293668, 0);
        }
    }
}
