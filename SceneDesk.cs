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
            var deskBlur = layer.CreateSprite("sb/scenes/deskBlur.jpg", OsbOrigin.Centre); 

            desk.Fade(22169, 1);
            desk.Scale(OsbEasing.OutCirc, 22169, 22624, 1.65, Constants.screenScale);
            desk.Rotate(OsbEasing.OutCirc, 22169, 22624, MathHelper.DegreesToRadians(40), 0);
            desk.Fade(58533, 1);

            Helpers.bounceEffect(22624, desk);

            deskBlur.Fade(22169, 0.7);
            deskBlur.Fade(22169, 22851, desk.OpacityAt(22283), 0);
            deskBlur.Scale(OsbEasing.OutCirc, 22169, 22624, 1.65, Constants.screenScale);
            deskBlur.Rotate(OsbEasing.OutCirc, 22169, 22624, MathHelper.DegreesToRadians(40), 0);

            // Meguru Expressions
            Vector2 face = Helpers.faceLocationHandler(601f, 452f);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer1.jpg", 30124, 36487, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer2.jpg", 36260, 40124, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer3.jpg", 39896, 43533, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer2.jpg", 43305, 47396, face.X, face.Y, layer);
            Helpers.moveScaleAndFade("sb/scenes/deskLayer4.jpg", 47169, 51033, face.X, face.Y, layer);
            OsbSprite deskFinal = Helpers.moveScaleAndFadeIn("sb/scenes/deskLayer5.jpg", 50805, 58533, face.X, face.Y, layer);
            deskFinal.Fade(58533, 0);
            
            /* 
            // Attempt to scal both bg and face while keeping it a one picture
            Log(x + " " + y);
            Vector2 V1 = new Vector2(320f, 240f);
            Vector2 V2 = new Vector2(294.98058f, 295.6667f);
            Vector2 Distance = Vector2.Subtract(V2, V1);
            Vector2 Scale = Vector2.Multiply(Distance, 1.5f);
            desk.Scale(OsbEasing.InCirc, 57396, 58533, Constants.screenScale, 1.5f);
            deskFinal.Scale(OsbEasing.InCirc, 57396, 58533, Constants.screenScale, 1.5f); 
            deskFinal.Move(OsbEasing.InCirc, 57396, 58533, deskFinal.PositionAt(57396), x + Scale.X, y + Scale.Y);
            */
        }
    }
}
