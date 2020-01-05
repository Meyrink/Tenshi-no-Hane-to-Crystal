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
        public float chrousScale = Constants.fontScale;

        [Configurable]
        public int fineness = 2;

        [Configurable]
        public OsbOrigin origin = OsbOrigin.Centre;

        StoryboardLayer layer;

        public override void Generate()
        {
		    layer = GetLayer("In My Heart");

            // Temporary Background
            var tempBG = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            tempBG.ScaleVec(94669, 854, 480);
            tempBG.Fade(94442, 94669, 0, 1);
            tempBG.Fade(98760, 0);
            tempBG.Color(94669, bgColor);

            // Importing Subtitles File
            var inMyHeart = LoadSubtitles("ass/heart.ass");

            // Setup Font Style
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
            }
            );

            // Creating Lyrics
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
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(origin) * Constants.fontScale;

                            var sprite = layer.CreateSprite(texture.Path, origin);

                            float angle = 30;
                            float startPos = 8;

                            sprite.Move(subtitleLine.StartTime - 200, subtitleLine.StartTime, 
                            position.X + Random(-startPos, startPos), position.Y + + Random(-startPos, startPos), 
                            position.X, position.Y);
                            
                            sprite.Rotate(subtitleLine.StartTime - 200, subtitleLine.StartTime , 
                            Random(MathHelper.DegreesToRadians(-angle), Random(MathHelper.DegreesToRadians(angle))), 0);
                            
                            sprite.Scale(subtitleLine.StartTime, Constants.fontScale);
                            sprite.Fade(subtitleLine.StartTime, subtitleLine.StartTime + Constants.beatLength * 0.25, 0, 1);
                            sprite.Fade(subtitleLine.EndTime - Constants.beatLength * 0.5, subtitleLine.EndTime, 1, 0);
                        }
                        letterX += texture.BaseWidth * Constants.fontScale;
                    }
                    letterY += lineHeight;
                }
            }
        }

        private void thanosEffect(string path, double start, double end, Vector2 pos) 
        {
            Bitmap textBitmap = GetMapsetBitmap(path);
            double duration = end - start;
            double relativeStart;
            double timestep = Constants.beatLength/4;
            for (int x = 0; x < textBitmap.Width ; x += fineness) {
                for (int y = 0; y < textBitmap.Height ; y += fineness) {
                    Vector2 spritePos = new Vector2((float)x, (float)y - textBitmap.Height/2);
                    spritePos = Vector2.Multiply(spritePos, chrousScale);
                    Color pixelColor = textBitmap.GetPixel(x, y);
                    relativeStart = start + (x+y)*5;
                    if (pixelColor.A > 0) {
                        var sprite = layer.CreateSprite("sb/particles/dot.png", OsbOrigin.Centre, spritePos + pos);
                        sprite.Scale(OsbEasing.None, relativeStart, relativeStart + timestep, 0, 0.075);
                        sprite.Fade(relativeStart, 0.4);
                        sprite.Fade(end - Constants.beatLength, end, 1, 0);
                        sprite.Color(relativeStart, pixelColor);

                        double tmp = 0, inc = 9;
                        for (double i = relativeStart; i < relativeStart + duration - timestep; i += timestep) {
                            // y = sqrt(x * Random(inc))
                            sprite.Move(i, i + timestep, sprite.PositionAt(i), sprite.PositionAt(i).X + tmp, sprite.PositionAt(i).Y - Math.Sqrt(tmp * Random(10) / Random(5)));
                            tmp += inc;
                        }
                    }
                }
            }
        }
    }
}
