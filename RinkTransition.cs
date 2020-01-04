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
            
            if (rotation % 90 < 80) {
                // no solution, set default value
                rotation = 30;
            }

            rotation = rotation * Math.PI / 180.0; // radians
            double barOffset = barWidth * Math.Tan(rotation);
            double barHeight = Constants.height / Math.Cos(rotation) + barOffset;

            // y = m*(x + 107) + 0
            double m = 1.0 / Math.Tan(rotation);
            double b = m * (-Constants.xFloor + Constants.height * Math.Tan(rotation));
            double startingX = (0 - b) / m;

            double inc = barWidth / Math.Cos(rotation);
            double numBars = Math.Ceiling((Constants.xCeil - startingX) / inc);
            double buffer = transitionSpeed / numBars;

            for (int i = 0; i < (int)numBars + 0.00005; i++) {
                OsbSprite bar = layer.CreateSprite("sb/pixel.png", OsbOrigin.BottomLeft, new Vector2((float)(startingX + i*inc), (float)480.0));
                
                double relativeIn = transitionTime - transitionSpeed - buffer * (numBars - i);
                double fadeInMidTime = relativeIn + transitionSpeed/2;
                bar.Rotate(relativeIn, rotation);
                bar.Color(relativeIn, Color4.Black);
                bar.ScaleVec(OsbEasing.Out, relativeIn, fadeInMidTime, barWidth, 0, barWidth, barHeight/2);
                bar.ScaleVec(OsbEasing.In, fadeInMidTime, relativeIn + transitionSpeed, barWidth, barHeight/2, barWidth, barHeight);

                double relativeOut = transitionTime + buffer * i;
                double fadeOutMidTime = relativeOut + transitionSpeed/2;
                bar.ScaleVec(OsbEasing.In, relativeOut, fadeOutMidTime, bar.ScaleAt(relativeOut), barWidth, barHeight/2);
                bar.ScaleVec(OsbEasing.Out, fadeOutMidTime, relativeOut + transitionSpeed, bar.ScaleAt(relativeOut), barWidth, 0);
                bar.Move(OsbEasing.In, relativeOut, fadeOutMidTime, bar.PositionAt(relativeOut), bar.PositionAt(relativeOut).X + (Constants.height/2) * Math.Tan(rotation), Constants.height/2);
                bar.Move(OsbEasing.Out, fadeOutMidTime, relativeOut + transitionSpeed, bar.PositionAt(fadeOutMidTime), bar.PositionAt(relativeOut).X + (Constants.height + barOffset) * Math.Tan(rotation), -barOffset);
            }
        }
    }
}