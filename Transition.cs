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
    public class Transition : StoryboardObjectGenerator
    {
        [Configurable]
        public int barAmount = 30;

        [Configurable]
        public OsbEasing easing = OsbEasing.None;

        [Configurable]
        public Color4 color = Color4.Black;

        [Configurable]
        public float foldRatio = 0.5f;

        public override void Generate()
        {
		    var layer = GetLayer("Transition");
            transitionIn(layer, 57396, 58078);
            transitionOut(layer, 58078, 58760);
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
                sprite.ScaleVec(easing, relativeStart, relativeStart + barDuration, width * foldRatio, 0, width, height);
                relativeStart += barDuration;
            }
        }
        private void transitionOut(StoryboardLayer layer, double startTime, double endTime)
        {
            double width = 854 / barAmount;
            double height = 480;
            double barDuration = (endTime - startTime) / barAmount;
            double relativeStart = startTime;

            for (int i = 1; i <= barAmount; i++)
            {
                var sprite = layer.CreateSprite("sb/pixel.png", OsbOrigin.TopLeft, new Vector2((float)(-107 + i * width), (float)0));
                sprite.Color(startTime, color); 
                sprite.Fade(relativeStart, 1);
                sprite.Fade(endTime, 0);
                sprite.ScaleVec(easing, relativeStart, relativeStart + barDuration, width, height, width * foldRatio, 0);
                relativeStart += barDuration;
            }
        }
    }
}
