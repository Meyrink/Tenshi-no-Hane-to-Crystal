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
        public override void Generate()
        {
		    var layer = GetLayer("Credits");
            Vector2 CreditPos = Helpers.LocationHandler(649.5f, 639f);

            // Credit bar
            double startTime = 9215;
            double endTime = 14896;
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
    }
}