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
    public class Credit : StoryboardObjectGenerator
    {
        [Configurable]
        public double logoScale = Constants.screenScale;
        [Configurable]
        public double bounceSpeed = 2;
        [Configurable]
        public OsbEasing easing = OsbEasing.None;
        public override void Generate()
        {
		    var layer = GetLayer("Credits");

            var logobg = layer.CreateSprite("sb/logoBg.png", OsbOrigin.Centre);
            ScaleAndRotate(logobg, 6260, 7851, 0, 45);

            var logolayer = layer.CreateSprite("sb/logoLayer.png", OsbOrigin.Centre);
            ScaleAndRotate(logolayer, 6260, 7851, 0, -45);        

            var logo = layer.CreateSprite("sb/logo.png", OsbOrigin.Centre);
            logo.Fade(6260, 6374, 0 , 1);
            logo.Fade(7851, 0);
            Bounce(logo, 6260); 
            logo.ScaleVec(7169, 7851, logoScale, logoScale, 0.73, 0.73);

            // Credit for sp and map
            Vector2 CreditPos = Helpers.LocationHandler(649.5f, 639f);

            var creditBar = layer.CreateSprite("sb/creditBar.png", OsbOrigin.Centre, CreditPos);
            creditBar.ScaleVec(9215, 9215 + Constants.beatLength * 0.5f, 0, Constants.screenScale, Constants.screenScale, Constants.screenScale);
            creditBar.ScaleVec(14896, 14896 + Constants.beatLength * 0.5f, Constants.screenScale, Constants.screenScale, 0, Constants.screenScale);
            creditBar.Fade(9215, 9215 + Constants.beatLength * 0.5f, 0, 1);
            creditBar.Fade(14896, 14896 + Constants.beatLength * 0.5f, 1, 0);

            var credit = layer.CreateSprite("sb/credit.png", OsbOrigin.Centre, CreditPos);
            credit.Scale(9669, Constants.screenScale);
            credit.Fade(9669, 9669 + Constants.beatLength * 0.5f, 0, 1);
            credit.Fade(12396, 12396 + Constants.beatLength * 0.5f, 1, 0);

            var owo = layer.CreateSprite("sb/owo.png", OsbOrigin.Centre, CreditPos);
            owo.Scale(12851, Constants.screenScale);
            owo.Fade(12851, 12851 + Constants.beatLength * 0.5f, 0, 1);
            owo.Fade(14669, 14669 + Constants.beatLength * 0.5f, 1, 0);
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

        private void ScaleAndRotate(OsbSprite sprite, int startTime, int endTime, int startAngle, int endAngle)
        {
            sprite.Fade(startTime, startTime + Constants.beatLength / 4, 0 , 1);
            sprite.Fade(endTime, 0);
            sprite.Scale(startTime, endTime, logoScale, 1);
            sprite.Rotate(startTime, endTime, MathHelper.DegreesToRadians(startAngle), MathHelper.DegreesToRadians(endAngle));
        }
    }
}