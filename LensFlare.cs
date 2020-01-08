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
    public class LensFlare : StoryboardObjectGenerator
    {
        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("");

            double startTime = 805;
            double endTime = 1715;
            var f1 = layer.CreateSprite("sb/flares/flare1.jpg", OsbOrigin.Centre, new Vector2(50, 150));
            f1.Additive(startTime, endTime);
            f1.FlipH(startTime, endTime);
            f1.Fade(startTime, 1);
            f1.Scale(startTime, 1);
            f1.Rotate(startTime, endTime, MathHelper.DegreesToRadians(-40), MathHelper.DegreesToRadians(-45));
            f1.Fade(endTime, 0);

            var l1 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(325, 100));
            l1.Additive(startTime, endTime);
            l1.Fade(startTime, endTime, 0, 0.2);
            l1.Scale(startTime, 0.3);

            startTime = endTime;
            endTime = 2624;
            var f2 = layer.CreateSprite("sb/flares/flare3.jpg", OsbOrigin.Centre, new Vector2(70, 70));
            f2.Additive(startTime, endTime);
            f2.Fade(startTime, 0.5);
            f2.Scale(startTime, Constants.screenScale);
            f2.Rotate(startTime, endTime, MathHelper.DegreesToRadians(-5), MathHelper.DegreesToRadians(0));
            f2.Fade(endTime, 0);

            startTime = endTime;
            endTime = 3533;
            var f3 = layer.CreateSprite("sb/flares/flare4.jpg", OsbOrigin.Centre, new Vector2(410, 190 - 40));
            f3.Additive(startTime, endTime);
            f3.Fade(startTime, 0.5);
            f3.Scale(startTime, Constants.screenScale);
            f3.Rotate(startTime, endTime, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(3));
            f3.Fade(endTime, 0);

            var l2 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(455, 232));
            l2.Color(startTime, Color4.White);
            l2.Additive(startTime, endTime);
            l2.Fade(startTime, endTime, 0, 0.2);
            l2.Scale(startTime, 0.8);

            startTime = endTime;
            endTime = 4442;
            var f4 = layer.CreateSprite("sb/flares/flare3.jpg", OsbOrigin.Centre, new Vector2(420 - 230 - 107, 190));
            f4.Additive(startTime, endTime);
            f4.Fade(startTime, 0.3);
            f4.Scale(startTime, Constants.screenScale);
            f4.Rotate(startTime, endTime, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-5));
            f4.Fade(endTime, 0);

            var l3 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(710, 250));
            l3.Color(startTime, Color4.White);
            l3.Additive(startTime, endTime);
            l3.Fade(startTime, endTime, 0, 0.2);
            l3.Scale(startTime, 0.8);

            startTime = endTime;
            endTime = 6260;
            var f5 = layer.CreateSprite("sb/flares/flare2.jpg", OsbOrigin.Centre, new Vector2(560, 215));
            f5.Additive(startTime, endTime);
            f5.Fade(startTime, 0.3);
            f5.Scale(startTime, Constants.screenScale);
            f5.Rotate(startTime, endTime, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(3));
            f5.Fade(endTime, 0);

            var l4 = layer.CreateSprite("sb/flares/light.jpg", OsbOrigin.Centre, new Vector2(409, 234));
            l4.Color(startTime, Color4.White);
            l4.Additive(startTime, endTime);
            l4.Fade(startTime, endTime, 0, 0.2);
            l4.Scale(startTime, 0.8);
        }
    }   
}
