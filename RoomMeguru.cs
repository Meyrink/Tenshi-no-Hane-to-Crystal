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
        public float transientOpacity = 0.7f;

        [Configurable]
        public float sdbgScale = 0.9f;

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
            ZoomFlipEffect(bg, 141942);
            bg.Fade(142624, 0);

            // Radial Blur bg for zoom effect
            var bgR = layer.CreateSprite("sb/bg/meguRoomR.jpg", OsbOrigin.Centre);
            bgR.Scale(141942, Constants.screenScale);
            bgR.Fade(141942, 142624,0, 1);
            ZoomFlipEffect(bgR, 141942);


            // SD Scenes 
            double startTime = 128305;
            double endTime = 141260;

            var bgB = layer.CreateSprite("sb/bg/meguRoomB.jpg", OsbOrigin.Centre);
            bgB.Scale(startTime, Constants.screenScale);
            bgB.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            bgB.Fade(endTime, endTime + Constants.beatLength * 0.5f, bgB.OpacityAt(endTime), 0);

            // Orange Background for chibi scenes
            var sdbg = layer.CreateSprite("sb/sdbg.png", OsbOrigin.Centre);
            FoldInOut(sdbg, startTime, endTime, sdbgScale);

            // Chibi Scenes
            FadeInOut(layer, "sb/sd/RoomSD1.jpg", 128987, sdScale);
            FadeInOut(layer, "sb/sd/RoomSD2.jpg", 130805, sdScale);
            FadeInOut(layer, "sb/sd/RoomSD3.jpg", 132624, sdScale);
            FadeInOut(layer, "sb/sd/RoomSD4.jpg", 134442, sdScale);
            FadeInOut(layer, "sb/sd/RoomSD5.jpg", 136260, sdScale);
            FadeInOut(layer, "sb/sd/RoomSD6.jpg", 138078, sdScale);
            var finalSD = FadeIn(layer, "sb/sd/RoomSD7.jpg", 139896, sdScale);
            finalSD.Fade(141033, 141260, 1, 0);

            // Bot Gradient
            var transient = layer.CreateSprite("sb/transient.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            transient.Scale(startTime, Constants.screenScale);
            transient.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, transientOpacity);
            transient.Fade(endTime, endTime + Constants.beatLength * 0.5f, transient.OpacityAt(endTime), 0);

            // Top Gradient
            var transient2 = layer.CreateSprite("sb/transient.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            transient2.FlipV(startTime, endTime + Constants.beatLength * 0.5f);
            transient2.Scale(startTime, Constants.screenScale);
            transient2.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, transientOpacity);
            transient2.Fade(endTime, endTime + Constants.beatLength * 0.5f, transient2.OpacityAt(endTime), 0);
        }

        private OsbSprite FadeInOut(StoryboardLayer layer, string path, double startTime, float scale)
        {
            var sprite = FadeIn(layer, path, startTime, scale);
            sprite.Fade(startTime + Constants.beatLength * 4, 0);
            return sprite;
        }

        private OsbSprite FadeIn(StoryboardLayer layer, string path, double startTime, float scale)
        {
            var sprite = layer.CreateSprite(path, OsbOrigin.Centre);
            sprite.Scale(startTime - Constants.beatLength * 0.5f, scale);
            sprite.Fade(startTime - Constants.beatLength * 0.5f, startTime, 0, 1);
            return sprite;
        }

        private void FoldInOut(OsbSprite sprite, double startTime, double endTime, float scale)
        {
            sprite.ScaleVec(startTime, startTime + Constants.beatLength * 0.5f, scale, 0, scale, scale);
            sprite.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            sprite.ScaleVec(endTime, endTime + Constants.beatLength * 0.5f, scale, scale, scale , 0);
            sprite.Fade(endTime, endTime + Constants.beatLength * 0.5f, sprite.OpacityAt(endTime), 0);
        }

        private void ZoomFlipEffect(OsbSprite sprite, double startTime)
        {
            var endTime = startTime + Constants.beatLength * 1.5;
            sprite.Scale(startTime, endTime, sprite.ScaleAt(startTime).X, 1.6);
            sprite.Rotate(OsbEasing.InBack, startTime, endTime, 0, Math.PI);
        }
    }
}
