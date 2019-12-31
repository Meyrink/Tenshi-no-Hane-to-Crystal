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
    public class MainBGYeet : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Yeet");
            var yeetBg = layer.CreateSprite("Meguru.jpg",OsbOrigin.Centre); 
            yeetBg.Fade(0, 0);
        }
    }
}
