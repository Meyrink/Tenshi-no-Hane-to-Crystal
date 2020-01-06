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
        public Color4 bgColor = Color4.White;

        [Configurable]
        public string fontName = "Gabriola";

        [Configurable]
        public int fontSize = 150;

        [Configurable]
        public FontStyle fontstyle = FontStyle.Regular;

        [Configurable]
        public Color4 fontColor = Color4.Black;

        [Configurable]
        public OsbOrigin origin = OsbOrigin.Centre;

        StoryboardLayer layer;

        public override void Generate()
        {
		    layer = GetLayer("In My Heart");
            
            double startTime = 94669;
            double endTime = 98760;

            // Temporary Background
            var tempBG = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBG.ScaleVec(startTime, 854, 480);
            tempBG.Fade(startTime - Constants.beatLength * 0.5, startTime, 0, 1);
            tempBG.Fade(endTime, 0);
            tempBG.Color(startTime, bgColor);

            // Outer Heart
            var outHeart = layer.CreateSprite("sb/particles/heartouter.png", OsbOrigin.Centre);

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

        public void generateLyrics(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer)
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
                        lineWidth += texture.BaseWidth * Constants.fontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * Constants.fontScale);
                    }

                    var letterX = 320 - lineWidth * 0.5f;
                    Vector2 center = new Vector2(320, 240);
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, (float)(letterY - lineHeight * 0.5)) // Moving Lyics To Y center
                                + texture.OffsetFor(origin) * Constants.fontScale;
                            
                            var distance = Vector2.Subtract(position, center); // Distance between each letter and center

                            var sprite = layer.CreateSprite(texture.Path, origin); 
                            sprite.MoveY(subtitleLine.StartTime, position.Y);
                            sprite.MoveX(subtitleLine.StartTime, subtitleLine.EndTime, position.X, position.X + distance.X * 0.25); // Move away from center
                            sprite.Scale(subtitleLine.StartTime, Constants.fontScale);
                            sprite.Fade(subtitleLine.StartTime, subtitleLine.StartTime + Constants.beatLength * 0.25, 0, 1);
                            sprite.Fade(subtitleLine.EndTime, 0);

                            // thanos(texture.Path, subtitleLine.EndTime, subtitleLine.EndTime + Constants.beatLength * 0.5, sprite.PositionAt(subtitleLine.EndTime));
                        }
                        letterX += texture.BaseWidth * Constants.fontScale;
                    }
                    letterY += lineHeight;
                }
            }
        }

        private void thanos(string texturePath, double startTime, double endTime, Vector2 letterPos)
        {
            Bitmap textBitmap = GetMapsetBitmap(texturePath);
            for (int x = 0; x < textBitmap.Width ; x += 2) 
            {
                for (int y = 0; y < textBitmap.Height ; y += 2) 
                {
                    Vector2 spritePos = new Vector2((float)x, (float)y - textBitmap.Height/2);
                    spritePos = Vector2.Multiply(spritePos, Constants.fontScale);
                    Vector2 center = new Vector2(320, 240);
                    Vector2 distance = Vector2.Subtract(center, (spritePos + letterPos));
                    Color pixelColor = textBitmap.GetPixel(x, y);

                    if (pixelColor.A > 0) 
                    {
                        var sprite = layer.CreateSprite("sb/particles/dot.png", OsbOrigin.Centre, spritePos + letterPos);
                        sprite.Scale(startTime, 0.075);
                        sprite.Fade(startTime, startTime, 0, 0.2);
                        sprite.Color(startTime, pixelColor);
                        sprite.Move(Random(startTime, endTime), Random(startTime, endTime), sprite.PositionAt(startTime), Vector2.Add(sprite.PositionAt(startTime), distance));
                    }
                }
            }
        }
    }
}
