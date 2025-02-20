using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class PreOutro : StoryboardObjectGenerator
    {
        static Color4 Color1 = new Color4(202, 242, 250, 1);

        static Color4 Color2 = new Color4(154, 230, 165, 1);
        
        static Color4 Color3 = new Color4(252, 160, 137, 1);

        Color4[] Colors = { Color1, Color2, Color3 };
        
        public override void Generate()
        {
            var layer = GetLayer("");

            var final = layer.CreateSprite("sb/scenes/final.jpg", OsbOrigin.Centre);
            final.Scale(266033, Constants.screenScale);
            final.Fade(266033, 1);
            final.Fade(280578, 0);
            
            Vector2 face = Helpers.locationHandler(616, 267.5f);
            Helpers.moveScaleAndFade("sb/scenes/finalLayer1.jpg", 269215, 273533, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/finalLayer2.jpg", 273305, 277169, face.X, face.Y, layer);
            OsbSprite finalFinal = Helpers.moveScaleAndFadeIn("sb/scenes/finalLayer3.jpg", 276942, 276942, face.X, face.Y, layer);
            finalFinal.Fade(280578, 0);

            glow(266033, 280124, 30, "sb/particles/gawo.png");
            generateParticles(266260, 279896, 350);
        }

        private void generateParticles(double startTime, double endTime, int particleNum)
        {
            double duration = endTime - startTime;
            double timeStep = duration / particleNum;
            double relativeStart = startTime;
            
            for (int i = 0; i < particleNum; i++)
            {
                var easing = (OsbEasing)Random(1, 10);
                double moveDuration = timeStep * Random(20, 200);
                double scale = Random(1, 6);
                Vector2 rPos = new Vector2(Random(-107, 747), Random(0, 480));

                var particle = GetLayer("").CreateSprite("sb/particles/tinyfloat.png", OsbOrigin.Centre, rPos);
                particle.Additive(relativeStart, relativeStart + moveDuration);
                particle.Scale(relativeStart, scale/20);
                particle.Color(relativeStart, Colors[Random(Colors.Length)]);
                particle.Fade(easing, relativeStart, relativeStart + 227, 0, 0.3);
                particle.Move(easing, relativeStart, relativeStart + moveDuration, particle.PositionAt(relativeStart), 760, Random(-10, 490));

                // Yeet if reach offscreen at X
                if(particle.PositionAt(relativeStart + moveDuration).X > 750 )
                {
                    particle.Fade(relativeStart + moveDuration, relativeStart + moveDuration + timeStep, 1, 0);
                }
                
                // Global Fadeout
                particle.Fade(endTime, endTime + Constants.beatLength, particle.OpacityAt(endTime), 0); 
                
                relativeStart += timeStep;
            }
        }

        private void glow(double startTime, double endTime, int particleNum, string path)
        {
            double opacity = 0.15;
            double timeStep = (endTime - startTime) / particleNum;
            double relativeStart = startTime;
            double duration = Constants.beatLength * 8;
            
            for (int i = 0; i < particleNum; i++)
            {
                double scale = Random(2, 6);
                Vector2 position = new Vector2(Random(-107, 747), Random(0, 480));
                Vector2 newPosition = Vector2.Add(position , new Vector2(Random(-50, 50), Random(-50, 50)));
                
                var sprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre, position);
                sprite.Scale(relativeStart, scale/8);
                sprite.Color(relativeStart, Colors[Random(Colors.Length)]);
                sprite.Additive(relativeStart, relativeStart + duration);

                sprite.Fade(relativeStart, relativeStart + 454, 0, opacity);
                sprite.Move(relativeStart, relativeStart + duration, position, newPosition);
                sprite.Fade(relativeStart + duration - 454, relativeStart + duration, opacity, 0);

                // Global Fade out
                if (sprite.OpacityAt(endTime) > 0)
                {
                    sprite.Fade(endTime, endTime + Constants.beatLength, sprite.OpacityAt(endTime), 0);
                    break;
                }
                relativeStart += timeStep;
            }
        }   
    }
}