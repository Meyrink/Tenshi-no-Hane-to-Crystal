using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    public class RoomClub : StoryboardObjectGenerator
    {
        [Configurable]
        public Vector2 ZoomChange = new Vector2(530f, 80f);

        [Configurable]
        public float gradientOpacity = 0.7f;

        [Configurable]
        public float sdScale = 0.57f;
        
        public override void Generate()
        {
		    var layer = GetLayer("ClubRoom");
            
            // Original BG
            var bg = layer.CreateSprite("sb/bg/clubRoom.jpg", OsbOrigin.Centre);
            bg.Scale(7851, Constants.screenScale);
            Helpers.bounceEffect(7851, bg);
            bg.Fade(7851, 1); // Blur in
            bg.Fade(8533, 0);
            bg.Fade(21260, 1); // Blur out
            zoomEffect(bg, 21487);
            bg.Fade(22169, 0);

            double startTime = 8305;
            double endTime = 21260;
            var bgB = layer.CreateSprite("sb/bg/clubRoomB.jpg", OsbOrigin.Centre);
            bgB.Scale(startTime, Constants.screenScale);
            bgB.Fade(startTime, startTime + Constants.beatLength * 0.5f, 0, 1);
            bgB.Fade(endTime, endTime + Constants.beatLength * 0.5f, bgB.OpacityAt(endTime), 0);

            // Chibi Scenes
            fadeInOut(layer, "sb/sd/ClubSD1.jpg", 8987, sdScale);
            fadeInOut(layer, "sb/sd/ClubSD2.jpg", 10805, sdScale);
            fadeInOut(layer, "sb/sd/ClubSD3.jpg", 12624, sdScale);
            fadeInOut(layer, "sb/sd/ClubSD4.jpg", 14442, sdScale);
            fadeInOut(layer, "sb/sd/ClubSD5.jpg", 16260, sdScale);
            fadeInOut(layer, "sb/sd/ClubSD6.jpg", 18078, sdScale);
            var finalSD = fadeIn(layer, "sb/sd/ClubSD7.jpg", 19896, sdScale);
            finalSD.Fade(21033, 21260, 1, 0);

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
            var bgR = layer.CreateSprite("sb/bg/clubRoomR.jpg", OsbOrigin.Centre);
            bgR.Scale(21487, Constants.screenScale);
            bgR.Fade(21487, 21942, 0, 1);
            zoomEffect(bgR, 21487);
            bgR.Fade(22169, 0);
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

        private void zoomEffect(OsbSprite sprite, double startTime)
        {
            var center = new Vector2(320f, 240f);
            var endTime = startTime + Constants.beatLength * 1.5;
            var pos = Vector2.Subtract(center, ZoomChange);
            sprite.Move(startTime, endTime, sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y ,center.X + pos.X, center.Y + pos.Y);
            sprite.Scale(startTime, endTime, sprite.ScaleAt(startTime).X, 2.2);
            sprite.Rotate(OsbEasing.InCirc, startTime, endTime, 0, -Math.PI/4);
        }
    }
}