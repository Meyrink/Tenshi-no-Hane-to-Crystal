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
    public class Logo : StoryboardObjectGenerator
    {
        [Configurable]
        public double logoScale = 0.7; //0.7 looks nice
        [Configurable]
        public double bounceSpeed = 2;
        [Configurable]
        public OsbEasing easing = OsbEasing.InSine; // Insine is apprently the best
        public override void Generate()
        {
		    var layer = GetLayer("Logo");
            double startTime = 6260;
            double endTime = 7851;

            // Background
            var logobg = layer.CreateSprite("sb/logoBg.png", OsbOrigin.Centre);
            ScaleAndRotate(logobg, startTime, endTime, 0, 45);
            var logolayer = layer.CreateSprite("sb/logoLayer.png", OsbOrigin.Centre);
            ScaleAndRotate(logolayer, startTime, endTime, 0, -45);        

            // In between

            // Foreground
            var logo = layer.CreateSprite("sb/logo.png", OsbOrigin.Centre);
            logo.Fade(startTime, startTime + Constants.beatLength * 0.25f, 0 , 1);
            logo.Fade(endTime, 0);
            Bounce(logo, startTime); 
            logo.ScaleVec(endTime - Constants.beatLength * 1.5f, endTime, logoScale, logoScale, 0.73, 0.73);
        }

        private void Bounce(OsbSprite sprite, double startTime) 
        {
            var endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(easing, startTime, endTime, logoScale, logoScale, logoScale * 1.1, logoScale * 0.8);
            
            startTime = endTime;
            endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(easing, startTime, endTime, logoScale * 1.1, logoScale * 0.8, logoScale / 1.1, logoScale / 0.8);
            
            startTime = endTime;
            endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(easing, startTime, endTime, logoScale / 1.1, logoScale / 0.8, logoScale * 1.1, logoScale * 0.8);

            startTime = endTime;
            endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(easing, startTime, endTime, logoScale * 1.1, logoScale * 0.8, logoScale, logoScale);
        }

        private void ScaleAndRotate(OsbSprite sprite, double startTime, double endTime, int startAngle, int endAngle)
        {
            sprite.Fade(startTime, startTime + Constants.beatLength / 4, 0 , 1);
            sprite.Fade(endTime, 0);
            sprite.Scale(startTime, endTime, logoScale, 1);
            sprite.Rotate(startTime, endTime, MathHelper.DegreesToRadians(startAngle), MathHelper.DegreesToRadians(endAngle));
        }
    }
}
