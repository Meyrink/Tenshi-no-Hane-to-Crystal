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
    public class SceneHugEnd : StoryboardObjectGenerator
    {
        [Configurable]
        public int barAmount = 30;

        [Configurable]
        public OsbEasing easing = OsbEasing.None;

        [Configurable]
        public Color4 color = Color4.Black;
        
        public override void Generate()
        {
            double startTime = 209669;
            double endTime = 211260;

		    var layer = GetLayer("Kiai 2 End");
           
            var card2 = layer.CreateSprite("sb/card/card2.jpg", OsbOrigin.Centre);
            card2.Fade(startTime, 1);
            card2.Scale(startTime, Constants.screenScale);
            card2.Fade(endTime, 0);

            var mask = layer.CreateSprite("sb/card/cardMask.jpg", OsbOrigin.Centre);
            mask.Scale(startTime, Constants.screenScale);
            mask.Fade(startTime, startTime + Constants.beatLength * 2, 0.5f, 0);
            mask.Additive(startTime, startTime + Constants.beatLength * 2f); 

            transitionIn(layer, 210578, 211260);
        }

        private void transitionIn(StoryboardLayer layer, double startTime, double endTime)
        {
            double width = 854 / barAmount;
            double height = 480;
            double barDuration = (endTime - startTime) / barAmount;
            double relativeStart = startTime;

            for (int i = 1; i <= barAmount; i++)
            {
                var sprite = layer.CreateSprite("sb/pixel.png", OsbOrigin.BottomRight, new Vector2((float)(-107 + i * width), (float)480.0));
                sprite.Color(startTime, color); 
                sprite.Fade(relativeStart, 1);
                sprite.Fade(endTime, 0);
                sprite.ScaleVec(easing, relativeStart, relativeStart + barDuration, 0, height, width, height);
                relativeStart += barDuration;
            }
        }
    }
}
