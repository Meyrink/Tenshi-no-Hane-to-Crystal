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
    public class InMyHeart : StoryboardObjectGenerator
    {
        [Configurable]
        public string fontName = "Gabriola";

        [Configurable]
        public int fontSize = 120;

        [Configurable]
        public float fontscale = 0.5f;

        [Configurable]
        public FontStyle fontstyle = FontStyle.Regular;

        [Configurable]
        public OsbEasing heartEasing = OsbEasing.InOutSine;

        [Configurable]
        public Color4 bgColor = Color4.White; //#FEBA98

        [Configurable]
        public Color4 fontColor = Color4.Black; //#D28369

        [Configurable]
        public Color4 heartColor1 = Color4.LightSkyBlue; //#FFF0BD

        [Configurable]
        public Color4 heartColor2 = Color4.Orange; //#B3DFFF

        StoryboardLayer layer;
        
        public override void Generate()
        {
		    layer = GetLayer("In My Heart");

            double startTime = 94442;
            double endTime = 97624;

            // Temporary Background
            var tempBG = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBG.ScaleVec(startTime, 854, 480);
            tempBG.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            tempBG.Fade(endTime, 0);
            tempBG.Color(startTime, bgColor);

            // Outer Heart
            startTime = 94669;
            endTime = 96942;
            var outHeart = layer.CreateSprite("sb/particles/heart.png", OsbOrigin.Centre);
            outHeart.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            outHeart.Scale(startTime - Constants.beatLength * 0.5, startTime, 0, Constants.screenScale * 0.8);
            outHeart.Scale(startTime, endTime, outHeart.ScaleAt(startTime).X, Constants.screenScale * 1.05);
            outHeart.Fade(endTime, 0);

            var inHeart = layer.CreateSprite("sb/particles/heart.png", OsbOrigin.Centre);
            inHeart.Color(startTime, bgColor);
            inHeart.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            inHeart.Scale(startTime - Constants.beatLength * 0.5, startTime, 0, Constants.screenScale * 0.7);
            inHeart.Scale(startTime, endTime, inHeart.ScaleAt(startTime).X, Constants.screenScale * 1.05);
            inHeart.Fade(endTime, 0);

            // Inner Heart
            heart(layer, 97169, Color4.White, false);
            heart(layer, 97624, heartColor2, false);
            heart(layer, 98078, heartColor1, true);

            // Particles
            generateParticles(layer, 94442, 97169, "sb/particles/star.png", 30, true);
            generateParticles(layer, 94442, 97169, "sb/particles/float.png", 30, false);

            // Lyrics
            var inMyHeart = LoadSubtitles("ass/heart.ass");
            FontGenerator font = LoadFont("sb/lyrics/enFont", new FontDescription() 
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
            generateLyrics(font, inMyHeart, layer); 
        }
        private void generateParticles(StoryboardLayer layer, double startTime, double endTime, string path, int particleNum, bool rotate)
        {
            Vector2 center = new Vector2(320, 240);
            for (int i = 0; i < particleNum; i++)
            {
                Vector2 randomCenter = new Vector2(320 + Random(-267, 267), 240 + Random(-120, 120));
                Vector2 distance = Vector2.Subtract(center, randomCenter);

                var particle = layer.CreateSprite(path, OsbOrigin.Centre);
                
                // Spreading stuff randomly, then expanding that random position by a distance
                particle.Scale(startTime, Random(0.2, 0.8));
                if (rotate) particle.Rotate(startTime, endTime, 0, Random(-Math.PI, Math.PI));
                particle.Fade(heartEasing, startTime, startTime + 227, 0, 1);
                particle.Move(heartEasing, startTime, startTime + 227, center, randomCenter);
                particle.Move(heartEasing, startTime + 227, endTime - 227, particle.PositionAt(startTime + 227), Vector2.Subtract(particle.PositionAt(startTime + 227), distance));
                
                // Moving stuff back to center
                distance = Vector2.Subtract(center, particle.PositionAt(endTime - 227));
                particle.Move(heartEasing, endTime - 227, endTime, particle.PositionAt(endTime - 227), Vector2.Add(particle.PositionAt(endTime - 227), distance));
                particle.Scale(heartEasing, endTime - 227, endTime, particle.ScaleAt(endTime - 227).X, 0.2);
                particle.Fade(endTime - 227, endTime, 1, 0);
            }
        }
        
        private void heart(StoryboardLayer layer, double startTime, Color4 color, bool end)
        {
            var heart = layer.CreateSprite("sb/particles/heart.png", OsbOrigin.Centre);
            heart.Color(startTime, color);
            heart.Scale(heartEasing, startTime, startTime + Constants.beatLength, 0, 1.8);
            heart.Fade(startTime, 1);
            if (end)
            {
                heart.Fade(startTime + Constants.beatLength * 1.5, 0);
            }else 
                heart.Fade(startTime + Constants.beatLength * 2, 0);  
        }

        private void generateLyrics(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer)
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

                            var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre);
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
