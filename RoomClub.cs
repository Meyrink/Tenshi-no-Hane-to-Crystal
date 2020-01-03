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
    public class RoomClub : StoryboardObjectGenerator
    {
        [Configurable]
        public Vector2 ZoomChange = new Vector2(530f, 80f);

        [Configurable]
        public float transientOpacity = 0.7f;

        [Configurable]
        public float sdbgScale = 0.9f;

        [Configurable]
        public float sdScale = 0.57f;
        public override void Generate()
        {
		    var layer = GetLayer("ClubRoom");
            
            // Original BG
            var bg = layer.CreateSprite("sb/bg/clubRoom.jpg", OsbOrigin.Centre);
            bg.Scale(7851, Constants.screenScale);
            Helpers.bounceEffect(7851, bg);

            bg.Fade(7851, 1);
            bg.Fade(8533, 0);

            bg.Fade(20578, 1);
            ZoomEffect(bg, 21487);
            bg.Fade(22169, 0);

            // Radial Blur bg for zoom effect
            var bgR = layer.CreateSprite("sb/bg/clubRoomR.jpg", OsbOrigin.Centre);
            bgR.Scale(21487, Constants.screenScale);
            bgR.Fade(OsbEasing.OutCirc, 21487, 22169, 0, 1);
            ZoomEffect(bgR, 21487);

            // Blur BG
            var bgB = layer.CreateSprite("sb/bg/clubRoomB.jpg", OsbOrigin.Centre);
            bgB.Scale(8305, Constants.screenScale);
            bgB.Fade(8305, 8533, 0, 1);
            bgB.Fade(21260, 21487, bgB.OpacityAt(20578), 0);

            // Orange Background for chibi scenes
            var sdbg = layer.CreateSprite("sb/sdbg.png", OsbOrigin.Centre);
            FoldInOut(sdbg, 8305, 21260, sdbgScale);

            // Chibi Scenes
            FadeInOut(layer, "sb/sd/ClubSD1.jpg", 8987, sdScale);
            FadeInOut(layer, "sb/sd/ClubSD2.jpg", 10805, sdScale);
            FadeInOut(layer, "sb/sd/ClubSD3.jpg", 12624, sdScale);
            FadeInOut(layer, "sb/sd/ClubSD4.jpg", 14442, sdScale);
            FadeInOut(layer, "sb/sd/ClubSD5.jpg", 16260, sdScale);
            FadeInOut(layer, "sb/sd/ClubSD6.jpg", 18078, sdScale);
            var finalSD = FadeIn(layer, "sb/sd/ClubSD7.jpg", 19896, sdScale);
            finalSD.Fade(21033, 21260, 1, 0);

            // Bot transient
            var transient = layer.CreateSprite("sb/transient.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            transient.Scale(8305, Constants.screenScale);
            transient.Fade(8305, 8533, 0, transientOpacity);
            transient.Fade(21260, 21487, transient.OpacityAt(21260), 0);

            // Top transient
            var transient2 = layer.CreateSprite("sb/transient.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            transient2.FlipV(8305, 21487);
            transient2.Scale(8305, Constants.screenScale);
            transient2.Fade(8305, 8533, 0, transientOpacity);
            transient2.Fade(21260, 21487, transient2.OpacityAt(21260), 0);
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

        private void ZoomEffect(OsbSprite sprite, double startTime)
        {
            var center = new Vector2(320f, 240f);
            var endTime = startTime + Constants.beatLength * 1.5;
            var pos = Vector2.Subtract(center, ZoomChange);
            sprite.Move(startTime, endTime, sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y ,center.X + pos.X, center.Y + pos.Y);
            sprite.Scale(startTime, endTime, sprite.ScaleAt(startTime).X, 2.2);
            sprite.Rotate(OsbEasing.InCirc, startTime, endTime, 0, MathHelper.DegreesToRadians(-40));
        }
        
        private void FoldInOut(OsbSprite sprite, double startTime, double endTime, float scale)
        {
            sprite.ScaleVec(startTime, startTime + Constants.beatLength * 0.5f, scale, 0, scale, scale);
            sprite.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            sprite.ScaleVec(endTime, endTime + Constants.beatLength * 0.5f, scale, scale, scale , 0);
            sprite.Fade(endTime, endTime + Constants.beatLength * 0.5f, sprite.OpacityAt(endTime), 0);
        }
    }
}
