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
using System.Drawing;

namespace StorybrewScripts
{
    public class ShiawaseNano : StoryboardObjectGenerator
    {
        [Configurable]
        public string fontName = "MS UI Gothic";

        [Configurable]
        public int fontSize = 120;

        [Configurable]
        public float fontscale = 0.5f;

        [Configurable]
        public FontStyle fontstyle = FontStyle.Regular;

        [Configurable]
        public OsbEasing easing = OsbEasing.InOutSine;

        [Configurable]
        public Color4 bgColor = Color4.White;

        [Configurable]
        public Color4 fontColor = Color4.Black;

        public override void Generate()
        {
            double startTime = 178078;
            double endTime = 182396;

            // Temporary Background
            var tempBG = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBG.ScaleVec(startTime, 854, 480);
            tempBG.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            tempBG.Fade(endTime, 0);
            tempBG.Color(startTime, bgColor);

            // Star Flower
            startTime = 178305;
            endTime = 180578;
            var outStar = GetLayer("").CreateSprite("sb/particles/StarFlower.png", OsbOrigin.Centre);
            outStar.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            outStar.Rotate(startTime - Constants.beatLength * 0.5, endTime, MathHelper.DegreesToRadians(22.5), MathHelper.DegreesToRadians(15));
            outStar.Scale(startTime - Constants.beatLength * 0.5, startTime, 0, Constants.screenScale * 0.8);
            outStar.Scale(startTime, endTime, outStar.ScaleAt(startTime).X, Constants.screenScale * 1.05);
            outStar.Fade(endTime, 0);

            var inStar = GetLayer("").CreateSprite("sb/particles/StarFlower.png", OsbOrigin.Centre);
            inStar.Color(startTime - Constants.beatLength * 0.5, bgColor);
            inStar.Rotate(startTime - Constants.beatLength * 0.5, endTime, MathHelper.DegreesToRadians(22.5), MathHelper.DegreesToRadians(15));
            inStar.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            inStar.Scale(startTime - Constants.beatLength * 0.5, startTime, 0, Constants.screenScale * 0.7);
            inStar.Scale(startTime, endTime, inStar.ScaleAt(startTime).X, Constants.screenScale * 1.05);
            inStar.Fade(endTime, 0);

            // Particles and SD
            generateParticles(178078, 180805, "sb/particles/float.png", 40, false);
            generateCircularEffect(178078, 180805);
            sdScroll(180805, "sb/sd/BlushSD1.jpg", 454, OsbEasing.InCirc);
            sdScroll(181260, "sb/sd/BlushSD2.jpg", 454, OsbEasing.InCirc);
            sdScroll(181715, "sb/sd/BlushSD3.jpg", 454 * 1.5, OsbEasing.InBack);  
            sdScrollParticles(180805, 182396, "sb/particles/tinyFloat.png", 80);  

            // Lyrics
            var Happiness = LoadSubtitles("ass/happiness.ass");
            FontGenerator font = LoadFont("sb/lyrics/enFont2", new FontDescription()
            {
                FontPath = fontName,
                FontSize = fontSize,
                Color = fontColor,
                Padding = Vector2.Zero,
                FontStyle = fontstyle,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            });
            generateLyrics(font, Happiness);
        }

        private void sdScrollParticles(double startTime, double endTime, string path, int particleNum)
        {
            Vector2 center = new Vector2(320, 240);
            for (int i = 0; i < particleNum; i++)
            {
                Vector2 rPos = new Vector2(Random(-107, 747), Random(0, 480));

                Vector2 distance = Vector2.Subtract(center, rPos) * 0.4f;
                var particle = GetLayer("").CreateSprite(path, OsbOrigin.Centre);
                particle.Scale(startTime, Random(0.2, 0.8));
                particle.Fade(easing, startTime, startTime + 227, 0, 1);
                particle.Move(easing, startTime, startTime + 227, center, rPos);

                Vector2 nPos = Vector2.Subtract(particle.PositionAt(startTime + 227), distance);
                particle.Move(startTime + 227, endTime - 227, particle.PositionAt(startTime + 227), nPos);
                particle.Fade(endTime - 227, endTime, 1, 0);
            }
        }

        private void sdScroll(double startTime, string path, double duration, OsbEasing easing)
        {
            float sdScale = 0.65f;

            var sprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre);
            sprite.Fade(OsbEasing.InExpo, startTime, startTime + 114, 0, 1);
            sprite.MoveX(OsbEasing.OutCirc, startTime, startTime + 227, 1067, 320);
            sprite.Scale(OsbEasing.OutBack, startTime, startTime + 227, sdScale * 0.2, sdScale);
            sprite.MoveX(easing, startTime + 227, startTime + duration, 320, -427);
            sprite.Fade(startTime + duration, 0);
        }

        private void generateCircularEffect(double startTime, double endTime)
        {
            int barCount = 16;
            var angleInc = (Math.PI * 2) / barCount;
            double angle = 0;
            int radius = 240;

            for (int i = 0; i < barCount; i++)
            {
                Vector2 center = new Vector2(
                    (float)(320 + Math.Cos(angle) * 125),
                    (float)(240 + Math.Sin(angle) * 125)
                );

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius)
                );

                var bar = GetLayer("").CreateSprite("sb/particles/circleb.png", OsbOrigin.Centre);
                bar.Fade(startTime, startTime + 227, 0, 1);
                bar.Additive(startTime, endTime);
                bar.ScaleVec(startTime, endTime - 227, 0.1, 0.006, 0.2, 0.004);
                bar.Rotate(startTime, angle);
                bar.Move(easing, startTime, startTime + 227, center, position);
                // Expanding from center
                Vector2 distance = Vector2.Subtract(position, new Vector2(320, 240)) * 0.5f;
                bar.Move(OsbEasing.OutBack, startTime + 227, endTime - 227, position, Vector2.Add(position, distance));
                // Grouping at center
                distance = Vector2.Subtract(new Vector2(320, 240), bar.PositionAt(endTime - 227));
                bar.Move(endTime - 227, endTime, bar.PositionAt(endTime - 227), Vector2.Add(bar.PositionAt(endTime - 227), distance));
                bar.ScaleVec(endTime - 227, endTime, 0.2, 0.004, 0.004, 0.004);
                bar.Fade(endTime - 227, endTime, bar.OpacityAt(endTime - 227), 0);

                angle -= angleInc;
            } 
        }

        private void generateParticles(double startTime, double endTime, string path, int particleNum, bool rotate)
        {
            Vector2 center = new Vector2(320, 240);
            for (int i = 0; i < particleNum; i++)
            {
                Vector2 randomCenter = new Vector2(320 + Random(-267, 267), 240 + Random(-120, 120));
                Vector2 distance = Vector2.Subtract(center, randomCenter);

                var particle = GetLayer("").CreateSprite(path, OsbOrigin.Centre);
                // Expanding from center
                particle.Scale(startTime, Random(0.2, 0.8));
                if (rotate) particle.Rotate(startTime, endTime, 0, Random(-Math.PI, Math.PI));
                particle.Fade(easing, startTime, startTime + 227, 0, 1);
                particle.Move(easing, startTime, startTime + 227, center, randomCenter);
                particle.Move(easing, startTime + 227, endTime - 227, particle.PositionAt(startTime + 227), Vector2.Subtract(particle.PositionAt(startTime + 227), distance));
                // Grouping at center
                distance = Vector2.Subtract(center, particle.PositionAt(endTime - 227));
                particle.Move(easing, endTime - 227, endTime, particle.PositionAt(endTime - 227), Vector2.Add(particle.PositionAt(endTime - 227), distance));
                particle.Scale(easing, endTime - 227, endTime, particle.ScaleAt(endTime - 227).X, 0.2);
                particle.Fade(endTime - 227, endTime, 1, 0);
            }
        }
        
        private void generateLyrics(FontGenerator font, SubtitleSet subtitles)
        {
            foreach (var subtitleLine in subtitles.Lines)
            {
                var letterY = 240f;
                foreach (var line in subtitleLine.Text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * fontscale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontscale);
                    }

                    var letterX = 320 - lineWidth * 0.5f;
                    Vector2 center = new Vector2(320, 240);
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, (float)(letterY - lineHeight * 0.5)) // Moving Lyics To Y center
                                + texture.OffsetFor(OsbOrigin.Centre) * fontscale;
                            
                            var distance = Vector2.Subtract(position, center); // Distance between each letter and center

                            var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre);
                            // Move away from center
                            sprite.MoveY(subtitleLine.StartTime, position.Y);
                            sprite.MoveX(subtitleLine.StartTime, subtitleLine.EndTime, position.X, position.X + distance.X * 0.25); 
                            sprite.Scale(subtitleLine.StartTime, fontscale);
                            sprite.Fade(subtitleLine.StartTime, subtitleLine.StartTime + Constants.beatLength * 0.25, 0, 1);
                            // Move back to center
                            distance = Vector2.Subtract(sprite.PositionAt(subtitleLine.EndTime), center);
                            sprite.MoveX(subtitleLine.EndTime, subtitleLine.EndTime + Constants.beatLength * 0.25, sprite.PositionAt(subtitleLine.EndTime).X, Vector2.Subtract(sprite.PositionAt(subtitleLine.EndTime), distance).X); 
                            sprite.Fade(subtitleLine.EndTime, subtitleLine.EndTime + Constants.beatLength * 0.25, 1, 0);
                            sprite.Scale(subtitleLine.EndTime, subtitleLine.EndTime + Constants.beatLength * 0.25, fontscale, 0);
                        }
                        letterX += texture.BaseWidth * fontscale;
                    }
                    letterY += lineHeight;
                }
            }
        }
    }
}
