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
    public class SceneHugParticles : StoryboardObjectGenerator
    {
        static Color4 Color1 = new Color4(189, 128, 144, 1);

        static Color4 Color2 = new Color4(255, 195, 145, 1);
        
        static Color4 Color3 = new Color4(248, 158, 150, 1);

        Color4[] Colors = { Color1, Color2, Color3 };
        public override void Generate()
        {
		    glow(182396, 209442, 30, "sb/particles/gawo.png");
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
    }
}
