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
    public class ParticlesInst : StoryboardObjectGenerator
    {
        [Configurable]
        public static Color4 Color1 = Color4.LightPink;

        [Configurable]
        public static Color4 Color2 = Color4.LightSkyBlue;

        [Configurable]
        public double timeStep = 500;

        Color4[] Colors = { Color1,  Color2 };
        public override void Generate()
        {
            // Inst 1
            generateWave("sb/particles/dotGlow.png", 50, 8533, 21260, 120, 40, 50);
            generateWave("sb/particles/dotGlow.png", 50, 8533, 21260, 120, 40, 480-50);
            // Inst 2
            generateWave("sb/particles/dotGlow.png", 50, 128533, 141260, 120, 40, 50);
            generateWave("sb/particles/dotGlow.png", 50, 128533, 141260, 120, 40, 480-50);
        }

        private void generateWave(string path, int numParticles, double startTime, double endTime, double waveLength, double waveHeight, double yPos)
        {
            for (int i = 0; i < numParticles; i++) 
            {
                // Y = A * Sin(X) + OriginOffset
                double vX = Random(waveLength) - waveLength/2; 
                double vY = Random(waveHeight) - waveHeight/2;
                double scale = Random(2, 6);

                float x = (float) Random(Constants.xFloor - 150, Constants.xCeil - 10);
                float y = (float) (waveHeight * (Math.Sin( (x + vX) / waveHeight)) + vY + yPos); // Sine Equation

                var sprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre, new Vector2(x, y)); 

                sprite.Fade(startTime, startTime + timeStep, 0, 0.4);
                sprite.Additive(startTime, endTime);
                sprite.Scale(startTime, scale/10);
                sprite.Color(startTime, Colors[Random(Colors.Length)]);
                

                for (double j = startTime; j < endTime - timeStep; j += timeStep)
                {
                    x += (float) (waveLength/20);
                    y =  (float) (waveHeight * (Math.Sin( (x + vX) / waveHeight)) + vY + yPos); // Sine Equation

                    sprite.Move(j, j + timeStep, sprite.PositionAt(j), x, y);

                    if (x > Constants.xCeil + 5) // Yeeting the particle when x > 747 offscreen
                    {
                        sprite.Fade(j, j + timeStep, sprite.OpacityAt(j), 0);
                        break;
                    }
                }
                sprite.Fade(endTime - timeStep, endTime, sprite.OpacityAt(endTime - timeStep), 0);
            }
        }
    }
}
