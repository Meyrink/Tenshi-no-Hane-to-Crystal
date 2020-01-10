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
    public class RoomMeguru : StoryboardObjectGenerator
    {
        [Configurable]
        public float gradientOpacity = 0.7f;

        [Configurable]
        public float sdScale = 0.57f;
        public override void Generate()
        {
		    var layer = GetLayer("MeguRoom");
            
            // Original BG
            var bg = layer.CreateSprite("sb/bg/meguRoom.jpg", OsbOrigin.Centre);
            bg.Scale(127851, Constants.screenScale);
            Helpers.bounceEffect(127851, bg);
            bg.Fade(127851, 1); // Blur in
            bg.Fade(128533, 0);
            bg.Fade(141260, 1); // Blur out
            zoomFlipEffect(bg, 141942);
            bg.Fade(142624, 0);

            double startTime = 128305;
            double endTime = 141260;
            var bgB = layer.CreateSprite("sb/bg/meguRoomB.jpg", OsbOrigin.Centre);
            bgB.Scale(startTime, Constants.screenScale);
            bgB.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            bgB.Fade(endTime, endTime + Constants.beatLength * 0.5f, bgB.OpacityAt(endTime), 0);

            // Chibi Scenes
            fadeInOut(layer, "sb/sd/RoomSD1.jpg", 128987, sdScale);
            fadeInOut(layer, "sb/sd/RoomSD2.jpg", 130805, sdScale);
            fadeInOut(layer, "sb/sd/RoomSD3.jpg", 132624, sdScale);
            fadeInOut(layer, "sb/sd/RoomSD4.jpg", 134442, sdScale);
            fadeInOut(layer, "sb/sd/RoomSD5.jpg", 136260, sdScale);
            fadeInOut(layer, "sb/sd/RoomSD6.jpg", 138078, sdScale);
            var finalSD = fadeIn(layer, "sb/sd/RoomSD7.jpg", 139896, sdScale);
            finalSD.Fade(141033, 141260, 1, 0);

            // Bot Gradient
            var gradient = layer.CreateSprite("sb/gradient.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            gradient.Scale(startTime, Constants.screenScale);
            gradient.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, gradientOpacity);
            gradient.Fade(endTime, endTime + Constants.beatLength * 0.5f, gradient.OpacityAt(endTime), 0);

            // Top Gradient
            var gradient2 = layer.CreateSprite("sb/gradient.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            gradient2.FlipV(startTime, endTime + Constants.beatLength * 0.5f);
            gradient2.Scale(startTime, Constants.screenScale);
            gradient2.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, gradientOpacity);
            gradient2.Fade(endTime, endTime + Constants.beatLength * 0.5f, gradient2.OpacityAt(endTime), 0);

            // Radial Blur bg for zoom effect
            var bgR = layer.CreateSprite("sb/bg/meguRoomR.jpg", OsbOrigin.Centre);
            bgR.Scale(141942, Constants.screenScale);
            bgR.Fade(141942, 142624,0, 1);
            zoomFlipEffect(bgR, 141942);
        }

        private OsbSprite fadeInOut(StoryboardLayer layer, string path, double startTime, float scale)
        {
            var sprite = fadeIn(layer, path, startTime, scale);
            sprite.Fade(startTime + Constants.beatLength * 4, 0);
            return sprite;
        }

        private OsbSprite fadeIn(StoryboardLayer layer, string path, double startTime, float scale)
        {
            var sprite = layer.CreateSprite(path, OsbOrigin.Centre);
            sprite.Scale(startTime - Constants.beatLength * 0.5f, scale);
            sprite.Fade(startTime - Constants.beatLength * 0.5f, startTime, 0, 1);
            return sprite;
        }

        private void zoomFlipEffect(OsbSprite sprite, double startTime)
        {
            var endTime = startTime + Constants.beatLength * 1.5;
            sprite.Scale(startTime, endTime, sprite.ScaleAt(startTime).X, 1.6);
            sprite.Rotate(OsbEasing.InBack, startTime, endTime, 0, Math.PI);
        }
    }
}
