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
    public class Solo : StoryboardObjectGenerator
    {
        [Configurable]
        public double fadeby = 0.30;
        [Configurable]
        public OsbEasing easing = OsbEasing.None;
        public override void Generate()
        {
            var layer = GetLayer("Solo");
            slideShow(layer, "sb/bg/s1.jpg", 211487, 215124, OsbOrigin.CentreLeft, -20, false, false);
            zoomIn(layer, "sb/bg/s2.jpg", 215124, 218760, 1.02);
            slideShow(layer, "sb/bg/s3.jpg", 218760, 220578, OsbOrigin.CentreLeft, -10, true, false);
            slideShow(layer, "sb/bg/s4.jpg", 220578, 222624, OsbOrigin.CentreLeft, -10, true, false);
            slideShow(layer, "sb/bg/s5.jpg", 222624, 226033, OsbOrigin.CentreLeft, -20, true, true);
        }

        private void slideShow(StoryboardLayer layer, string path, double startTime, double endTime, OsbOrigin origin, double moveBy, bool fadeIn, bool fadeOut)
        {
            Vector2 pos = new Vector2();

            if (origin == OsbOrigin.CentreLeft) // Check if i want to slide to left
            {
                pos = new Vector2(-107, 240);
            }
            else if (origin == OsbOrigin.CentreRight) // Check if i want to slide to right
            {
                pos = new Vector2(747, 240);
            }

            var sprite = layer.CreateSprite(path, origin, pos);
            sprite.Scale(startTime, Constants.screenScale);
            sprite.MoveX(startTime, endTime, sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).X + moveBy * Constants.screenScale);

            if (fadeIn) // Check if i want to have a slow fade or instant
            {
                sprite.Fade(easing, startTime - Constants.beatLength * fadeby, startTime, 0, 1);
            }
            else
            {
                sprite.Fade(startTime , 1);
            }

            if (fadeOut) // Check if i want to have a slow fadeout or instant
            {
                sprite.Fade(endTime - Constants.beatLength * 3, endTime, 1, 0.3);
            }
            else
            {
                sprite.Fade(endTime, 0);
            }
        }
        private void zoomIn(StoryboardLayer layer, string path, double startTime, double endTime, double zoomBy)
        {
            var sprite = layer.CreateSprite(path, OsbOrigin.Centre);
            sprite.Fade(easing, startTime - Constants.beatLength * fadeby, startTime, 0 , 1);
            sprite.Scale(startTime - Constants.beatLength * fadeby, endTime, Constants.screenScale, Constants.screenScale * zoomBy);
            sprite.Fade(endTime, 0);
        }
    }
}
