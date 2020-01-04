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
    public class RinkTransition : StoryboardObjectGenerator
    {
        [Configurable]
        public double transitionTime = 0;
        [Configurable]
        public double barWidth = 20;
        [Configurable]
        public double rotation = 30.0; // degrees
        [Configurable]
        public double transitionSpeed = 1000; // in ms

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("Transtion");
            
            double epsilon = 20;
            if (rotation % 360 >= 180 - epsilon && rotation % 360 <= 180 + epsilon) {
                // no solution, set default value
                rotation = 30;
            }

            rotation = rotation * Math.PI / 180.0; // radians
            double barOffset = barWidth * Math.Cos(rotation);
            double barHeight = Constants.height / Math.Cos(rotation) + barOffset;

            // y = m*(x + 107) + 0
            double m = 1.0 / Math.Tan(rotation);
            double b = m * (-Constants.xFloor + Constants.height * Math.Tan(rotation));
            double startingX = (0 - b) / m;

            double inc = barWidth / Math.Cos(rotation);
            double numBars = Math.Ceiling((Constants.xCeil - startingX) / inc);
            double buffer = 20;

            for (int i = 0; i < (int)numBars + 0.00005; i++) {
                OsbSprite bar = layer.CreateSprite("sb/pixel.png", OsbOrigin.BottomLeft, new Vector2((float)(startingX + i*inc), (float)480.0));
                
                double relativeIn = transitionTime - transitionSpeed - buffer * (numBars - i);
                bar.Fade(relativeIn, 1);
                bar.Rotate(relativeIn, rotation);
                bar.Color(relativeIn, Color4.Black);

                double fadeInMidTime = relativeIn + transitionSpeed/2;
                bar.ScaleVec(OsbEasing.Out, relativeIn, fadeInMidTime, barWidth, 0, barWidth, barHeight/2);
                bar.ScaleVec(OsbEasing.In, fadeInMidTime, relativeIn + transitionSpeed, barWidth, barHeight/2, barWidth, barHeight);


                double relativeOut = transitionTime + buffer * i;
                bar.ScaleVec(OsbEasing.OutCirc, relativeOut, relativeOut + transitionSpeed/2, bar.ScaleAt(relativeOut), barWidth, 0);
                bar.Move(OsbEasing.OutCirc, relativeOut, relativeOut + transitionSpeed/2, bar.PositionAt(relativeOut), bar.PositionAt(relativeOut).X + (Constants.height + barOffset) * Math.Tan(rotation), -barOffset);
                bar.Fade(relativeOut + transitionSpeed/2, 0);
            }
        }
    }
}
