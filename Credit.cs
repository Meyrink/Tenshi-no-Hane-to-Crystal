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
        public double logoScale = Constants.screenScale; //0.7 looks nice
        [Configurable]
        public double bounceSpeed = 2;
        [Configurable]
        public OsbEasing easing = OsbEasing.None; // Insine is apprently the best
        public override void Generate()
        {
		    var layer = GetLayer("Credits");

            // Game Logo
            double startTime = 6260;
            double endTime = 7851;

            var logobg = layer.CreateSprite("sb/logoBg.png", OsbOrigin.Centre);
            ScaleAndRotate(logobg, startTime, endTime, 0, 45);

            var logolayer = layer.CreateSprite("sb/logoLayer.png", OsbOrigin.Centre);
            ScaleAndRotate(logolayer, startTime, endTime, 0, -45);        

            var logo = layer.CreateSprite("sb/logo.png", OsbOrigin.Centre);
            logo.Fade(startTime, startTime + Constants.beatLength * 0.25f, 0 , 1);
            logo.Fade(endTime, 0);
            Bounce(logo, startTime); 
            logo.ScaleVec(endTime - Constants.beatLength * 1.5f, endTime, logoScale, logoScale, 0.73, 0.73);
            
            // Credit bar
            Vector2 CreditPos = Helpers.LocationHandler(649.5f, 639f);
            startTime = 9215;
            endTime = 14896;

            var creditBar = layer.CreateSprite("sb/creditBar.png", OsbOrigin.Centre, CreditPos);
            creditBar.ScaleVec(startTime, startTime + Constants.beatLength * 0.5f, 0, Constants.screenScale, Constants.screenScale, Constants.screenScale);
            creditBar.ScaleVec(endTime, endTime + Constants.beatLength * 0.5f, Constants.screenScale, Constants.screenScale, 0, Constants.screenScale);
            creditBar.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            creditBar.Fade(endTime, endTime + Constants.beatLength * 0.5f, 1, 0);

            // Sb & Map credits
            startTime = 9669;
            endTime = 12396;

            var credit = layer.CreateSprite("sb/credit.png", OsbOrigin.Centre, CreditPos);
            credit.Scale(startTime, Constants.screenScale);
            credit.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            credit.Fade(endTime, endTime + Constants.beatLength * 0.5f, 1, 0);

            // Gogo *:･٩(´・ω・)۶*:･
            startTime = 12851;
            endTime = 14669;

            var owo = layer.CreateSprite("sb/owo.png", OsbOrigin.Centre, CreditPos);
            owo.Scale(startTime, Constants.screenScale);
            owo.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            owo.Fade(endTime, endTime + Constants.beatLength * 0.5f, 1, 0);
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