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
    public class Logo : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 6260;

        [Configurable]
        public double endTime = 7851;

        [Configurable]
        public double logoScale = 0.7;

        [Configurable]
        public double bounceSpeed = 2;

        [Configurable]
        public OsbEasing bounceEasing = OsbEasing.InSine;

        [Configurable]
        public Color4 particleColor = Color4.Yellow;

        [Configurable]
        public OsbEasing easing = OsbEasing.None;

        public override void Generate()
        {
		    var layer = GetLayer("Logo");

            // Temporary White BG
            var tempBg = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBg.ScaleVec(6260, 854, 480);
            tempBg.Fade(6260, 1);
            tempBg.Fade(7851, 0);
            
            // Background
            var logobg = layer.CreateSprite("sb/logoBg.png", OsbOrigin.Centre);
            scaleAndRotate(logobg, startTime, endTime, 0, 45);
            var logolayer = layer.CreateSprite("sb/logoLayer.png", OsbOrigin.Centre);
            scaleAndRotate(logolayer, startTime, endTime, 0, -45);        

            // In between (particles)
            generateParticles(layer, startTime, endTime, "sb/particles/star.png", 30, true);
            generateParticles(layer, startTime, endTime, "sb/particles/dot.png", 30, false);

            // Foreground
            var logo = layer.CreateSprite("sb/logo.png", OsbOrigin.Centre);
            logo.Fade(startTime, startTime + Constants.beatLength * 0.25f, 0 , 1);
            logo.Fade(endTime, 0);
            bounce(logo, startTime); 
            logo.ScaleVec(endTime - Constants.beatLength * 1.5f, endTime, logoScale, logoScale, 0.73, 0.73);
        }

        private void generateParticles(StoryboardLayer layer, double startTime, double endTime, string path, int particleNum, bool rotate)
        {
            Vector2 center = new Vector2(320, 240);
            var step = (endTime-startTime)/particleNum;
            for (int i = 0; i < particleNum; i++)
            {
                Vector2 randomCenter = new Vector2(320 + Random(-220, 220), 240 + Random(-100, 100));
                Vector2 distance = Vector2.Subtract(center, randomCenter);

                var particle = layer.CreateSprite(path, OsbOrigin.Centre);
                
                // Spreading stuff randomly, then expanding that random position by a distance
                particle.Scale(startTime, Random(0.2, 0.6));
                particle.Color(startTime, particleColor);
                if (rotate) particle.Rotate(startTime, endTime, 0, Random(-Math.PI, Math.PI));
                particle.Fade(easing, startTime, startTime + 227, 0, 1);
                particle.Move(easing, startTime, startTime + 227, center, randomCenter);
                particle.Move(easing, startTime + 227, endTime, particle.PositionAt(startTime + 227), Vector2.Subtract(particle.PositionAt(startTime + 227), distance));
                particle.Fade(endTime, 0);
                startTime += step;
            }
        }

        private void bounce(OsbSprite sprite, double startTime) 
        {
            var endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(bounceEasing, startTime, endTime, logoScale, logoScale, logoScale * 1.1, logoScale * 0.8);
            
            startTime = endTime;
            endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(bounceEasing, startTime, endTime, logoScale * 1.1, logoScale * 0.8, logoScale / 1.1, logoScale / 0.8);
            
            startTime = endTime;
            endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(bounceEasing, startTime, endTime, logoScale / 1.1, logoScale / 0.8, logoScale * 1.1, logoScale * 0.8);

            startTime = endTime;
            endTime = startTime + Constants.beatLength / bounceSpeed;
            sprite.ScaleVec(bounceEasing, startTime, endTime, logoScale * 1.1, logoScale * 0.8, logoScale, logoScale);
        }

        private void scaleAndRotate(OsbSprite sprite, double startTime, double endTime, int startAngle, int endAngle)
        {
            sprite.Fade(startTime, startTime + Constants.beatLength / 4, 0 , 1);
            sprite.Fade(endTime, 0);
            sprite.Scale(startTime, endTime, logoScale, 1);
            sprite.Rotate(startTime, endTime, MathHelper.DegreesToRadians(startAngle), MathHelper.DegreesToRadians(endAngle));
        }
    }
}
