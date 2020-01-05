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
    public class Solo : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("Solo");
        }

        private static OsbSprite slideShow (string path, int startTime, StoryboardLayer layer, double beatMultiplier) 
        {
            Vector2 pos = new Vector2(-107, 240);
            OsbSprite sprite = layer.CreateSprite(path, OsbOrigin.CentreLeft, pos);
            var endTime = startTime + Constants.beatLength * beatMultiplier;
            sprite.Scale(startTime, Constants.screenScale);
            sprite.MoveX(startTime, endTime, sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).X - 20 * Constants.screenScale);
            sprite.Fade(startTime, endTime, 1 , 1);
            sprite.Fade(endTime, 0);
            return sprite;
        }
    }
}
