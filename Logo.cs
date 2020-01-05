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
        public int numSprites = 30;
        [Configurable]
        public double startTime = 6260;
        [Configurable]
        public double endTime = 7851;
        [Configurable]
        public double logoScale = 0.7; //0.7 looks nice
        [Configurable]
        public double bounceSpeed = 2;
        [Configurable]
        public OsbEasing bounceEasing = OsbEasing.InSine; // InSine seems the best
        [Configurable]
        public Color4 particleColor = Color4.Yellow;
        [Configurable]
        public Vector2 Center = new Vector2(320, 240);
        public override void Generate()
        {
		    var layer = GetLayer("Logo");
            
            // Background
            var logobg = layer.CreateSprite("sb/logoBg.png", OsbOrigin.Centre);
            scaleAndRotate(logobg, startTime, endTime, 0, 45);
            var logolayer = layer.CreateSprite("sb/logoLayer.png", OsbOrigin.Centre);
            scaleAndRotate(logolayer, startTime, endTime, 0, -45);        

            // In between (particles)
            generateParticles(layer, startTime, endTime, "sb/star.png");

            // Foreground
            var logo = layer.CreateSprite("sb/logo.png", OsbOrigin.Centre);
            logo.Fade(startTime, startTime + Constants.beatLength * 0.25f, 0 , 1);
            logo.Fade(endTime, 0);
            bounce(logo, startTime); 
            logo.ScaleVec(endTime - Constants.beatLength * 1.5f, endTime, logoScale, logoScale, 0.73, 0.73);
        }

        private void generateParticles(StoryboardLayer layer, double startTime, double endTime, string path)
        {
            for (int i = 0; i < numSprites; i++)
            {
                var sprite = layer.CreateSprite(path, OsbOrigin.Centre);
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
