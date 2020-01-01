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
    public class RoomClub : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("ClubRoom");

            var bg = layer.CreateSprite("sb/bg/clubRoom.jpg", OsbOrigin.Centre);
            var bgBlur = layer.CreateSprite("sb/bg/clubRoomB.jpg", OsbOrigin.Centre);

            bg.Scale(7851, Constants.screenScale);
            bg.Fade(7851, 1);
            bg.Fade(22169, 0);

            Helpers.bounceEffect(7851, bg);

            
            
            // Camera zooms on desk position
            var deskLocation = new Vector2(530f, 80f);
            var center = new Vector2(320f, 240f);
            var pos = Vector2.Subtract(center, deskLocation);
            bg.Move(21487, 22169, bg.PositionAt(21487).X, bg.PositionAt(21487).Y ,center.X + pos.X, center.Y + pos.Y);
            bg.Scale(21487, 22169, bg.ScaleAt(21487).X, 2);
            bg.Rotate(OsbEasing.InCirc, 21487, 22169, 0, MathHelper.DegreesToRadians(-40));
        }
    }
}
