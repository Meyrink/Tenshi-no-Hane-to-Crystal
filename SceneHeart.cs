using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class SceneHeart : StoryboardObjectGenerator
    {
        [Configurable]
        public float movement = 0f;

        [Configurable]
        public int rotation = 0;
        
        [Configurable]
        public Vector2 heartLocation = new Vector2(345, 515);

        public override void Generate()
        {
		    var layer = GetLayer("Heart");

            var heart = layer.CreateSprite("sb/scenes/heart.jpg", OsbOrigin.Centre);
            heart.Fade(OsbEasing.InSine, 226033, 227851, 0, 0.5);
            heart.Scale(226033, 1.4);

            // Moving heart position to centre
            var center = new Vector2(320, 240);
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

            heart.Rotate(226033, 233305, 0, MathHelper.DegreesToRadians(rotation));
            heart.Rotate(233305, 242396, MathHelper.DegreesToRadians(rotation), 0);

            // Reset heart position then moving the whole background down to Yfloor
            heart.Move(OsbEasing.InOutCubic, 240578, 242396, heart.PositionAt(240578).X, heart.PositionAt(240578).Y, center.X , center.Y * (float)(846f/720f));
            heart.Scale(OsbEasing.InOutCubic, 240578, 242396, 1.4, Constants.screenScale);
            heart.Fade(OsbEasing.InOutCubic, 240578, 242396, heart.OpacityAt(240578), 1);
            heart.Fade(266033, 0);

            // Meguru Expressions
            Vector2 face = Helpers.locationHandler(610.5f, 347.5f);
            Helpers.moveScaleAndFade("sb/scenes/heartLayer1.jpg", 245351, 249215, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/heartLayer2.jpg", 248987, 256942, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/heartLayer3.jpg", 256715, 261942, face.X, face.Y, layer);
            OsbSprite heartFinal = Helpers.moveScaleAndFadeIn("sb/scenes/heartLayer4.jpg", 261715, 261715, face.X, face.Y, layer); 
            heartFinal.Fade(266033, 0);
        }

        private OsbSprite floatingEffect(double startTime, float moveX, float moveY, OsbSprite sprite) 
        {
            var endTime = startTime + 1818;
            sprite.Move(
            startTime, endTime, 
            sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y, 
            sprite.PositionAt(startTime).X + moveX, sprite.PositionAt(startTime).Y + moveY
            );
            return sprite;
        }
    }
}