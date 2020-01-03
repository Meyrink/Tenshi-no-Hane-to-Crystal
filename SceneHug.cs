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
    public class SceneHug : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Hug");
            
            var Hug = layer.CreateSprite("sb/scenes/hug.jpg" , OsbOrigin.Centre);
            Hug.Scale(182396, Constants.screenScale);
            Hug.Fade(182396, 1);
            Hug.Fade(211487, 0);

            // Meguru Expressions
            Vector2 face = Helpers.LocationHandler(456.5f, 195.5f);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer1.jpg", 185351, 189782, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer3.jpg", 189555, 196714, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer5.jpg", 196487, 198532, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer6.jpg", 198305, 200351, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer4.jpg", 200123, 202510, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer2.jpg", 202282, 205578, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/hugLayer3.jpg", 205578, 209669, face.X, face.Y, layer);
        }
    }
}
