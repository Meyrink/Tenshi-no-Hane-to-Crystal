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
            TransitionIn(layer, 57396, 58078);
            TransitionOut(layer, 58078, 58760);
        }
        private void TransitionIn(StoryboardLayer layer, double startTime, double endTime)
        {
            double width = 854 / barAmount;
            double height = 480;
            double barDuration = (endTime - startTime) / barAmount;
            double relativeStart = startTime;

            for (int i = 0; i <= barAmount; i++)
            {
                Vector2 Pos = FindPosIn(i);
                var sprite = layer.CreateSprite("sb/pixel.png", OsbOrigin.BottomRight, Pos);
                sprite.Color(startTime, color); 
                sprite.Fade(startTime, 1);
                sprite.Fade(endTime, 1);
                sprite.ScaleVec(easing, relativeStart, relativeStart + barDuration, width * foldRatio, 0, width, height);
                relativeStart += barDuration;
            }
        }
        private void TransitionOut(StoryboardLayer layer, double startTime, double endTime)
        {
            double width = 854 / barAmount;
            double height = 480;
            double barDuration = (endTime - startTime) / barAmount;
            double relativeStart = startTime;

            for (int i = 0; i <= barAmount; i++)
            {
                Vector2 Pos = FindPosOut(i);
                var sprite = layer.CreateSprite("sb/pixel.png", OsbOrigin.TopLeft, Pos);
                sprite.Color(startTime, color); 
                sprite.Fade(startTime, 1);
                sprite.Fade(endTime, 1);
                sprite.ScaleVec(easing, relativeStart, relativeStart + barDuration, width, height, width * foldRatio, 0);
                relativeStart += barDuration;
            }
        }
        private Vector2 FindPosIn(double index) 
        {
            double width = 854 / barAmount;
            float x = (float) (-107 + index * width);
            float y = (float) (480);
            return new Vector2(x, y);
        }
        private Vector2 FindPosOut(double index) 
        {
            double width = 854 / barAmount;
            float x = (float) (-107 + index * width);
            float y = (float) (0);
            return new Vector2(x, y);
        }
    }
}
