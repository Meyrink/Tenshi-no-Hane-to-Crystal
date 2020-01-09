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
        public static Color4 Color1 = Color4.LightSkyBlue;

        [Configurable]
        public static Color4 Color2 = Color4.LightSkyBlue;

        Color4[] Colors = { Color1,  Color2 };

        public override void Generate()
        {
            upwardParticles(227851, 241487, 200, 150, 20); //Slow and smal
            upwardParticles(227851, 241487, 200, 75, 10); // Fast and big
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
                Vector2 initialPos = new Vector2(Random(-107, 747), Random(240, 480));
                var particle = GetLayer("").CreateSprite("sb/particles/tinyFloat.png", OsbOrigin.Centre, initialPos);
                particle.Additive(relativeStart, relativeStart + moveDuration);
                particle.Scale(relativeStart, scale/scaling);
                particle.Fade(relativeStart, relativeStart + 227, 0, 1);
                particle.MoveY(relativeStart, relativeStart + moveDuration, particle.PositionAt(relativeStart).Y, -10);

                // Yeet if reach offscreen at Y
                if(particle.PositionAt(relativeStart + moveDuration).Y < -5 ) particle.Fade(relativeStart + moveDuration, relativeStart + moveDuration + timeStep, 1, 0);
                
                particle.Fade(endTime, endTime + Constants.beatLength, particle.OpacityAt(endTime), 0); // Global Fadeout
                
                relativeStart += timeStep;
            }
            
        }
    }
}
