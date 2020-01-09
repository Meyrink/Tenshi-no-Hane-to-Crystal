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
    public class SceneHeartParticles : StoryboardObjectGenerator
    {
        static Color4 Color1 = new Color4(215, 178, 211, 1);

        static Color4 Color2 = new Color4(90, 97, 151, 1);
        
        static Color4 Color3 = new Color4(153, 174, 201, 1);

        Color4[] Colors = { Color1, Color2, Color3 };

        public override void Generate()
        {
            upwardParticles(227851, 241033, 200, 150, 20);
            upwardParticles(227851, 241033, 100, 75, 10);

            sparkle(241942, 264215, 70, "sb/particles/circleb.png");
        }

        private void sparkle(double startTime, double endTime, int particleNum, string path)
        {
            double opacity = 0.3;
            double timeStep = (endTime - startTime) / particleNum;
            double relativeStart = startTime;
            double duration = Constants.beatLength * 6;
            
            for (int i = 0; i < particleNum; i++)
            {
                double scale = Random(2, 6);
                Vector2 position = new Vector2(Random(-107, 747), Random(0, 480));
                Vector2 newPosition = Vector2.Add(position , new Vector2(Random(-20, 20), Random(-20, 20)));
                
                var sprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre, position);
                sprite.Scale(relativeStart, scale/50);
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
        private void upwardParticles(double startTime, double endTime, int particleNum, int speed, double scaling)
        {
            double duration = endTime - startTime;
            double timeStep = duration / particleNum;
            double relativeStart = startTime;
            double moveDuration = timeStep * speed;

            for (int i = 0; i < particleNum; i++)
            {
                double scale = Random(2, 4);
                Vector2 initialPos = new Vector2(Random(-107, 747), Random(120 * 3, 480 + 10));

                var particle = GetLayer("").CreateSprite("sb/particles/tinyFloat.png", OsbOrigin.Centre, initialPos);
                particle.Additive(relativeStart, relativeStart + moveDuration);
                particle.Scale(relativeStart, scale/scaling);
                particle.Fade(relativeStart, relativeStart + 227, 0, 1);
                particle.MoveY(relativeStart, relativeStart + moveDuration, particle.PositionAt(relativeStart).Y, -10);

                // Yeet if reach offscreen at Y
                if(particle.PositionAt(relativeStart + moveDuration).Y < -5 )
                {
                    particle.Fade(relativeStart + moveDuration, relativeStart + moveDuration + timeStep, 1, 0);
                }
                
                // Global Fadeout
                particle.Fade(endTime, endTime + Constants.beatLength, particle.OpacityAt(endTime), 0); 
                
                relativeStart += timeStep;
            }
        }
    }
}
