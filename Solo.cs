using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

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
            slideShow(layer, "sb/bg/s5.jpg", 222624, 226260, OsbOrigin.CentreLeft, -20, true, true);

            generateParticles(211487, 214896, 100, 0.3);
            generateParticles(215124, 218533, 100, 0.3);
            generateParticles(218760, 220351, 50, 0.2);
            generateParticles(220578, 222396, 50, 0.2);
            generateParticles(222624, 225351, 100, 0.1);
        }

        private void generateParticles(double startTime, double endTime, int particleNum, double opacity)
        {
            double duration = endTime - startTime;
            double timeStep = duration / particleNum;
            double relativeStart = startTime;
            
            for (int i = 0; i < particleNum; i++)
            {
                var easing = OsbEasing.InOutSine;
                double moveDuration = timeStep * Random(20, 200);
                double scale = Random(1, 6);
                Vector2 rPos = new Vector2(Random(-107, 747), Random(0, 480));

                var particle = GetLayer("").CreateSprite("sb/particles/tinyfloat.png", OsbOrigin.Centre, rPos);
                particle.Additive(relativeStart, relativeStart + moveDuration);
                particle.Scale(relativeStart, scale/20);
                particle.Fade(easing, relativeStart, relativeStart + 227, 0, opacity);
                particle.Move(easing, relativeStart, relativeStart + moveDuration, particle.PositionAt(relativeStart), Random(-150, 750), Random(-10, 490));

                // Yeet if reach offscreen at X
                var p = particle.PositionAt(relativeStart + moveDuration);
                if(p.X > 747 || p.X < -107 || p.Y > 480 || p.Y < 0)
                {
                    particle.Fade(relativeStart + moveDuration, relativeStart + moveDuration + timeStep, 1, 0);
                }
                
                // Global Fadeout
                particle.Fade(endTime, endTime + Constants.beatLength * 0.5, particle.OpacityAt(endTime), 0); 
                
                relativeStart += timeStep;
            }
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
            else if (!fadeIn)
            {
                sprite.Fade(startTime , 1);
            }

            if (fadeOut) // Check if i want to have a slow fadeout or instant
            {
                sprite.Fade(endTime - Constants.beatLength * 3, endTime, 1, 0);
            }
            else if (!fadeOut)
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