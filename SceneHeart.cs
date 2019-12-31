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

            // Some Mouvements ever 4beats, maybe i can make this into a shorter code? a loop maybe ?  
            floatingEffect(227851, movement, movement, heart);
            floatingEffect(229669, -movement, movement, heart);
            floatingEffect(231487, -movement, -movement, heart);
            floatingEffect(233305, movement, -movement, heart);

            floatingEffect(235124, movement, movement, heart);
            floatingEffect(236942, -movement, movement, heart);
            floatingEffect(238760, -movement, -movement, heart);

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
            sprite.Move(
            startTime, startTime + Constants.beatLength*4, 
            sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y, 
            sprite.PositionAt(startTime).X + moveX, sprite.PositionAt(startTime).Y + moveY
            );
            return sprite;
        }
    }
}
