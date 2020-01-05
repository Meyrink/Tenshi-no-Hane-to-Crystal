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
    public class SceneThighs : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Thighs");

            var thighs = layer.CreateSprite("sb/scenes/thighs.jpg", OsbOrigin.Centre);
            thighs.Scale(142624, 143078, 1, Constants.screenScale);
            thighs.Rotate(OsbEasing.OutBack, 142624, 143078, MathHelper.DegreesToRadians(-15), 0);
            thighs.Fade(142624, 1);
            thighs.Fade(182396, 0); // To fade for transition

            // Meguru Expressions
            Vector2 face = Helpers.locationHandler(299f, 199f);
            Helpers.moveScaleAndFade("sb/scenes/thighsLayer1.jpg", 150124, 156487, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/thighsLayer5.jpg", 156260, 160124, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/thighsLayer6.jpg", 159896, 163760, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/thighsLayer4.jpg", 163533, 167396, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/thighsLayer3.jpg", 167169, 171033, face.X, face.Y, layer);
            OsbSprite thighsfinal = Helpers.moveScaleAndFadeIn("sb/scenes/thighsLayer2.jpg", 170805, 170805, face.X, face.Y, layer);
            thighsfinal.Fade(182396, 0); // To fade for transition
        }
    }
}
