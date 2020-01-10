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
        [Configurable]
        public double minSpeed = 50; // Lower, is faster

        [Configurable]
        public double maxSpeed = 300;

        [Configurable]
        public int particleAmount = 300;

        static Color4 Color1 = new Color4(215, 178, 211, 1);

        static Color4 Color2 = new Color4(90, 97, 151, 1);
        
        static Color4 Color3 = new Color4(153, 174, 201, 1);

        Color4[] Colors = { Color1, Color2, Color3 };

        public override void Generate()
        {
            upwardParticles(227851, 241033, particleAmount);
            sparkle(241942, 264215, 70, "sb/particles/circleb.png");
            glow(241942, 264215, 35, "sb/particles/light.png");
            flare(241487, 264215, new Vector2(800, 460));
            flare(241487, 264215, new Vector2(-200, 60));

            // Shadow
            var vig = GetLayer("").CreateSprite("sb/vig.png", OsbOrigin.Centre);
            float vigOpacity = 0.7f;
            vig.Fade(226942, 227624, 0, vigOpacity);
            vig.Scale(226942, 480.0 / 1080);
            vig.Fade(241033, 241487, vigOpacity, 0);
        }

        private void flare(double startTime, double endTime, Vector2 pos)
        {
            float flareOpacity = 0.5f;

            var sprite = GetLayer("").CreateSprite("sb/flares/light2.jpg", OsbOrigin.Centre, pos);
            sprite.Additive(startTime, endTime + Constants.beatLength);
            sprite.Scale(startTime, 0.8);
            sprite.Fade(startTime, startTime + Constants.beatLength, 0, flareOpacity);

            sprite.StartLoopGroup(startTime + Constants.beatLength * 2, 12);
                sprite.Fade(0, Constants.beatLength * 2, flareOpacity, 0.2);
                sprite.Fade(Constants.beatLength * 2, Constants.beatLength * 4, 0.2, flareOpacity);
            sprite.EndGroup();

            sprite.Fade(endTime, endTime + Constants.beatLength, sprite.OpacityAt(endTime), 0);
        }

        private void glow(double startTime, double endTime, int particleNum, string path)
        {
            double opacity = 0.1;
            double timeStep = (endTime - startTime) / particleNum;
            double relativeStart = startTime;
            double duration = Constants.beatLength * 8;
            
            for (int i = 0; i < particleNum; i++)
            {
                double scale = Random(2, 6);
                Vector2 position = new Vector2(Random(-107, 747), Random(0, 480));
                Vector2 newPosition = Vector2.Add(position , new Vector2(Random(-50, 50), Random(-50, 50)));
                
                var sprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre, position);
                sprite.Scale(relativeStart, scale/5);
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
        private void upwardParticles(double startTime, double endTime, int particleNum)
        {
            double duration = endTime - startTime;
            double timeStep = duration / particleNum;
            double relativeStart = startTime;
            

            for (int i = 0; i < particleNum; i++)
            {
                double moveDuration = timeStep * Random(minSpeed, maxSpeed);
                double scale = Random(1, 10);
                Vector2 initialPos = new Vector2(Random(-107, 747), Random(120 * 3, 480 + 10));

                var particle = GetLayer("").CreateSprite("sb/particles/tinyFloat.png", OsbOrigin.Centre, initialPos);
                particle.Additive(relativeStart, relativeStart + moveDuration);
                particle.Scale(relativeStart, scale/20);
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
