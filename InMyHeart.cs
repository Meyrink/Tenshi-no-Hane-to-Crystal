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
    public class InMyHeart : StoryboardObjectGenerator
    {
        [Configurable]
        public Color4 bg = Color4.White;
        public override void Generate()
        {
		    var layer = GetLayer("In My Heart");

            var tempBG = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBG.ScaleVec(94669, 854, 480);
            tempBG.Fade(94442, 94669, 0, 1);
            tempBG.Fade(98760, 0);
        }
    }
}
