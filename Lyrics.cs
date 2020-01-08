using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;

namespace StorybrewScripts
{
    public class Lyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public string FontName = "MS UI Gothic";

        [Configurable]
        public string FontName2 = "Sawarabi Mincho";

        [Configurable]
        public int FontSize = 50;

        [Configurable]
        public int FontSize2 = 50;

        [Configurable]
        public float mainFontScale = 0.5f;

        [Configurable]
        public float sideFontScale = 0.8f;

        [Configurable]
        public float SubtitleY = 400f;

        [Configurable]
        public Color4 GlowColor = Color4.Black;

        [Configurable]
        public Color4 GlowColor2 = Color4.Black;

        [Configurable]
        public Color4 OutlineColor = Color4.Orange;

        public override void Generate()
        {
            var font = LoadFont("sb/lyrics/jpFont", new FontDescription()
            {
                FontPath = FontName,
                FontSize = FontSize,
                Color = Color4.White,
                FontStyle = FontStyle.Bold,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow()
            {
                Radius = 5,
                Color = GlowColor,
            },
            new FontOutline()
            {
                Thickness = 1,
                Color = OutlineColor,
            });

            var font2 = LoadFont("sb/lyrics/jpFont2", new FontDescription()
            {
                FontPath = FontName2,
                FontSize = FontSize2,
                Color = Color4.White,
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow()
            {
                Radius = 8,
                Color = GlowColor2,
            });

            var main = LoadSubtitles("ass/main.ass");
            var suki = LoadSubtitles("ass/suki.ass");

            generateLyrics(font, main, "");
            generateSukiLyrics(font2, suki, "");
        }

        public void generateLyrics(FontGenerator font, SubtitleSet subtitles, string layerName)
        {
            var layer = GetLayer(layerName);
            foreach (var subtitleLine in subtitles.Lines)
            {
                float letterY = SubtitleY;
                float whiteSpace = 3f;
                foreach (var line in subtitleLine.Text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * mainFontScale + whiteSpace;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * mainFontScale);
                    }
                    var letterX = 320 - lineWidth * 0.5f;
                    double relativeTime = subtitleLine.StartTime;
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(OsbOrigin.Centre) * mainFontScale;
                            Vector2 offscreen = Vector2.Subtract(new Vector2(-200, position.Y), position);
                            Vector2 screenright = new Vector2(600, position.Y);


                            var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);
                            sprite.Scale(relativeTime, mainFontScale);
                            sprite.Fade(relativeTime - 227, relativeTime, 0, 1);
                            sprite.Move(subtitleLine.EndTime - 227, subtitleLine.EndTime, position, Vector2.Add(position, new Vector2(Random(-5, 5), Random(-20, 20))));
                            sprite.Scale(subtitleLine.EndTime - 227, subtitleLine.EndTime, mainFontScale, 0);
                            sprite.Fade(subtitleLine.EndTime - 227, subtitleLine.EndTime, 1, 0);
                        }
                        letterX += texture.BaseWidth * mainFontScale + whiteSpace;
                        relativeTime += 114/2;
                    }
                    letterY += lineHeight;
                }
            }
        }

        public void generateSukiLyrics(FontGenerator font, SubtitleSet subtitles, string layerName)
        {
            var layer = GetLayer(layerName);
            foreach (var subtitleLine in subtitles.Lines)
            {
                var letterY = 240f;
                float whiteSpace = 6f;
                foreach (var line in subtitleLine.Text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += (texture.BaseWidth + whiteSpace) * sideFontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * sideFontScale);
                    }
                    var letterX = 320 - lineWidth * 0.5f;
                    Vector2 center = new Vector2(320, 240);
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, (float)(letterY - lineHeight * 0.5))
                                + texture.OffsetFor(OsbOrigin.Centre) * sideFontScale;

                            var distance = Vector2.Subtract(position, center) * 0.3f;

                            var sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre);
                            sprite.MoveX(subtitleLine.StartTime - 227, subtitleLine.StartTime, 320, position.X);
                            sprite.MoveX(subtitleLine.StartTime, subtitleLine.EndTime, sprite.PositionAt(subtitleLine.StartTime).X, Vector2.Add(sprite.PositionAt(subtitleLine.StartTime), distance).X);
                            sprite.Scale(subtitleLine.StartTime, sideFontScale);
                            sprite.Fade(subtitleLine.StartTime - 227, subtitleLine.StartTime, 0, 1);
                            sprite.Fade(subtitleLine.EndTime - 227, subtitleLine.EndTime, 1, 0);
                            sprite.Additive(subtitleLine.StartTime - 227, subtitleLine.EndTime);
                        }
                        letterX += (texture.BaseWidth + whiteSpace) * sideFontScale;
                    }
                    letterY += lineHeight;
                }
            }
        }
    }
}
