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
    public class SceneDesk : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("Desk");

            var desk = layer.CreateSprite("sb/scenes/desk.jpg", OsbOrigin.Centre); 
            desk.Fade(22169, 1);
            desk.Scale(OsbEasing.OutCirc, 22169, 22624, 1.65, Constants.screenScale);
            desk.Rotate(OsbEasing.OutCirc, 22169, 22624, Math.PI/4, 0);
            desk.Fade(58078, 0);

            Helpers.bounceEffect(22624, desk);

            var deskR = layer.CreateSprite("sb/scenes/deskR.jpg", OsbOrigin.Centre); 
            deskR.Fade(22169, 0.7);
            deskR.Fade(22169, 22624, desk.OpacityAt(22283), 0);
            deskR.Scale(OsbEasing.OutCirc, 22169, 22624, 1.65, Constants.screenScale);
            deskR.Rotate(OsbEasing.OutCirc, 22169, 22624, Math.PI/4, 0);

            // Meguru Expressions
            Vector2 face = Helpers.locationHandler(601f, 452f);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer1.jpg", 30124, 36487, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer2.jpg", 36260, 40124, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer3.jpg", 39896, 43533, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer2.jpg", 43305, 47396, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer4.jpg", 47169, 51033, face.X, face.Y, layer);
            OsbSprite deskFinal = Helpers.moveScaleAndFadeIn("sb/scenes/deskLayer5.jpg", 50805, 58533, face.X, face.Y, layer);
            deskFinal.Fade(58078, 0);
        }
    }
}
