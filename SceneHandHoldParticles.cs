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
    public class SceneHandHoldParticles : StoryboardObjectGenerator
    {
        static Color4 Color1 = new Color4(202, 242, 250, 1);

        static Color4 Color2 = new Color4(154, 230, 165, 1);
        
        static Color4 Color3 = new Color4(252, 160, 137, 1);

        Color4[] Colors = { Color1, Color2, Color3 };
        public override void Generate()
        {
		    glow(98987, 125805, 30, "sb/particles/gawo.png");
            generateParticles(98760, 125805, 300);
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
