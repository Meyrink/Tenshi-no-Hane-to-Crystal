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
    public class PreOutro : StoryboardObjectGenerator
    {
        static Color4 Color1 = new Color4(215, 178, 211, 1);

        static Color4 Color2 = new Color4(90, 97, 151, 1);
        
        static Color4 Color3 = new Color4(153, 174, 201, 1);

        Color4[] Colors = { Color1, Color2, Color3 };
        public override void Generate()
        {
            var layer = GetLayer("");

            var final = layer.CreateSprite("sb/scenes/final.jpg", OsbOrigin.Centre);
            final.Scale(266033, Constants.screenScale);
            final.Fade(266033, 1);
            final.Fade(280578, 0);
            
            Vector2 face = Helpers.locationHandler(616, 267.5f);
            Helpers.moveScaleAndFade("sb/scenes/finalLayer1.jpg", 269215, 273533, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/finalLayer2.jpg", 273305, 277169, face.X, face.Y, layer);
            OsbSprite finalFinal = Helpers.moveScaleAndFadeIn("sb/scenes/finalLayer3.jpg", 276942, 276942, face.X, face.Y, layer);
            finalFinal.Fade(280578, 0); 
        }      
    }
}
