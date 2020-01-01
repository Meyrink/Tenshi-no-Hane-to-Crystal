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
    public class SceneHeart : StoryboardObjectGenerator
    {
        [Configurable]
        public float movement = 0f;
        [Configurable]
        public int rotation = 0;

        public override void Generate()
        {
		    var layer = GetLayer("Heart");

            var heart = layer.CreateSprite("sb/scenes/heart.jpg", OsbOrigin.Centre);
            heart.Fade(OsbEasing.OutCirc, 226033, 227396, 0, 0.2);
            heart.Fade(OsbEasing.InCirc, 227396, 227851, heart.OpacityAt(227396), 0.5);
            heart.Scale(226033, 1.4);
            heart.Fade(266033, 0);

            // Moving heart position to centre
            var heartLocation = new Vector2(347f, 92f);
            var center = new Vector2(320f, 240f);
            var pos = Vector2.Subtract(center, heartLocation);
            heart.Move(226033, center.X + pos.X, center.Y + pos.Y);

            // BG Mouvements and rotatoin every 4 beats / 16 beats
            floatingEffect(226033, movement, movement, heart);
            floatingEffect(227851, -movement, movement, heart);
            floatingEffect(229669, -movement, -movement, heart);
            floatingEffect(231487, movement, -movement, heart);

            floatingEffect(233305, movement, movement, heart);
            floatingEffect(235124, -movement, movement, heart);
            floatingEffect(236942, -movement, -movement, heart);
            floatingEffect(238760, movement, -movement, heart);

            heart.Rotate(OsbEasing.InCubic, 226033, 233305, 0, MathHelper.DegreesToRadians(rotation));
            heart.Rotate(OsbEasing.InCubic, 233305, 242396, MathHelper.DegreesToRadians(rotation), 0);

            /* 
            //Something fucked at the end of the loop making the zoomout weird! so back to using manual input bove.
            Vector2 direction = new Vector2(movement, movement);
            for (double startTime = 227851; startTime < 240578; startTime += 1818)            {
                floatingEffect(startTime, direction.X, direction.Y, heart);
                Matrix2 directionChanger = new Matrix2(0, 1, -1, 0);
                Vector2 newDirection = multiply(direction, directionChanger);
                direction = newDirection;
            }
            */

            // Reset heart position then moving the whole background down to Yfloor 
            heart.Move(OsbEasing.InOutBack, 240578, 242396, heart.PositionAt(240578).X, heart.PositionAt(240578).Y, center.X , center.Y * 2);
            heart.Scale(OsbEasing.InOutCirc, 240578, 242396, heart.ScaleAt(240578).X, Constants.screenScale);
            heart.Fade(240578, 242396, heart.OpacityAt(240578), 1);

            // Meguru Expressions
            Vector2 face = Helpers.faceLocationHandler(610.5f, 347.5f);
            Helpers.moveScaleAndFade("sb/scenes/heartLayer1.jpg", 245351, 249215, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/heartLayer2.jpg", 248987, 256942, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/heartLayer3.jpg", 256715, 261942, face.X, face.Y, layer);
            OsbSprite heartFinal = Helpers.moveScaleAndFadeIn("sb/scenes/heartLayer4.jpg", 261715, 261715, face.X, face.Y, layer);
            heartFinal.Fade(266033, 0);
        }

        private OsbSprite floatingEffect(double startTime, float moveX, float moveY, OsbSprite sprite) {
            var endTime = startTime + 1818;
            sprite.Move(
            startTime, endTime, 
            sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y, 
            sprite.PositionAt(startTime).X + moveX, sprite.PositionAt(startTime).Y + moveY
            );
            return sprite;
        }

        private Vector2 multiply(Vector2 v, Matrix2 m) {
            return new Vector2 (
            m.M11 * v.X + m.M21 * v.Y, // x
            m.M12 * v.X + m.M22 * v.Y  // y
            );
        }
    }
}
