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
    public class Intro : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            slideShow("sb/bg/bg1.jpg", 805, 2);
            slideShow("sb/bg/bg2.jpg", 1715, 2);
            slideShow("sb/bg/bg3.jpg", 2624, 2);
            slideShow("sb/bg/bg4.jpg", 3533, 2);
            slideShow("sb/bg/bg5.jpg", 4442, 4);
        }

        private void slideShow(string path, double startTime, double beatMultiplier) 
        {
            Vector2 BottomLeft = new Vector2(-107, 240);
            var sprite = GetLayer("").CreateSprite(path, OsbOrigin.CentreLeft, BottomLeft);
            var endTime = startTime + Constants.beatLength * beatMultiplier;
            sprite.Scale(startTime, Constants.screenScale);
            sprite.MoveX(startTime, endTime, sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).X - 20 * Constants.screenScale);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);
        }
    }
}
