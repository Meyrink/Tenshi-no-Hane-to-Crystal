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
    public class Scenes : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("Meguru");

            var screenScale = 480.0 / 720; 

            // Main BG Yeeted
            var yeetBg = layer.CreateSprite("Meguru.jpg",OsbOrigin.Centre); 
            yeetBg.Fade(0, 0);

            // Club Desk Scene Verse 1
            var desk = layer.CreateSprite("sb/scenes/desk.jpg", OsbOrigin.Centre); 
            var deskBlur = layer.CreateSprite("sb/scenes/deskBlur.jpg", OsbOrigin.Centre); 

            desk.Fade(22169, 1);
            desk.Fade(58533, 1);
            desk.Scale(OsbEasing.OutCirc, 22169, 22624, 1.65, screenScale);
            desk.Rotate(OsbEasing.OutCirc, 22169, 22624, MathHelper.DegreesToRadians(40), MathHelper.DegreesToRadians(0));

            desk.Scale(OsbEasing.InCubic, 22624, 22737, screenScale, 0.68);
            desk.Scale(OsbEasing.OutCubic, 22737, 22851, 0.68, screenScale);

            deskBlur.Fade(22169, 0.7);
            deskBlur.Fade(22169, 22851, desk.OpacityAt(22283), 0);
            deskBlur.Scale(OsbEasing.OutCirc, 22169, 22624, 1.65, screenScale);
            deskBlur.Rotate(OsbEasing.OutCirc, 22169, 22624, MathHelper.DegreesToRadians(40), MathHelper.DegreesToRadians(0));

            // Club Desk Meguru face location
            var initialDeskFaceLocation = new Vector2(602.5f, 443.5f);
            var deskxRatio = initialDeskFaceLocation.X / 1280;
            var deskyRatio = initialDeskFaceLocation.Y / 720;
            var deskX = (854 * deskxRatio) + -107;
            var deskY = 480 * deskyRatio;

            // Meguru Expressions
            var deskLayer1 = layer.CreateSprite("sb/scenes/deskLayer1.jpg", OsbOrigin.Centre);
            var deskLayer2 = layer.CreateSprite("sb/scenes/deskLayer2.jpg", OsbOrigin.Centre);
            var deskLayer3 = layer.CreateSprite("sb/scenes/deskLayer3.jpg", OsbOrigin.Centre);
            var deskLayer4 = layer.CreateSprite("sb/scenes/deskLayer4.jpg", OsbOrigin.Centre);
            var deskLayer5 = layer.CreateSprite("sb/scenes/deskLayer5.jpg", OsbOrigin.Centre);

            deskLayer1.Move(30124, deskX, deskY);
            deskLayer1.Scale(30124, screenScale);
            deskLayer1.Fade(OsbEasing.OutSine, 30124, 30124 + 227, 0, 1);
            deskLayer1.Fade(36487, 36487 + 114, 1, 0);

            deskLayer2.Move(36260, deskX, deskY);
            deskLayer2.Scale(36260, screenScale);
            deskLayer2.Fade(OsbEasing.OutSine, 36260, 36260 + 227, 0, 1);
            deskLayer2.Fade(40124, 40124 + 114, 1, 0);

            deskLayer3.Move(39896, deskX, deskY);
            deskLayer3.Scale(39896, screenScale);
            deskLayer3.Fade(OsbEasing.OutSine, 39896, 39896 + 227, 0, 1);
            deskLayer3.Fade(43533, 43533 + 114, 1, 0);

            deskLayer2.Move(43305, deskX, deskY);
            deskLayer2.Scale(43305, screenScale);
            deskLayer2.Fade(OsbEasing.OutSine, 43305, 43305 + 227, 0, 1);
            deskLayer2.Fade(47396, 47396 + 114, 1, 0);

            deskLayer4.Move(47169, deskX, deskY);
            deskLayer4.Scale(47169, screenScale);
            deskLayer4.Fade(OsbEasing.OutSine, 47169, 47169 + 227, 0, 1);
            deskLayer4.Fade(51033, 51033 + 114, 1, 0);

            deskLayer5.Move(50805, deskX, deskY);
            deskLayer5.Scale(50805, screenScale);
            deskLayer5.Fade(OsbEasing.OutSine, 50805, 50805 + 227, 0, 1);
            deskLayer5.Fade(58533, 1);

            desk.Scale(OsbEasing.InCirc, 57396, 58533, screenScale, 1.5);
            deskLayer5.Scale(OsbEasing.InCirc, 57396, 58533, screenScale, 1.5); //Not working, need maths for this scaling, or just yeet it

            // Cinema scene Verse 2
            var cinema = layer.CreateSprite("sb/scenes/cinema.jpg", OsbOrigin.Centre);
            var cinemaHold = layer.CreateSprite("sb/scenes/cinemaHold.jpg", OsbOrigin.Centre);

            cinema.Scale(OsbEasing.OutCirc, 58533, 58987, 1.5, screenScale);
            cinema.Fade(58533, 1);
            cinema.Fade(73078, 1);

            cinemaHold.Scale(72851, screenScale);
            cinemaHold.Fade(OsbEasing.OutSine ,72851, 72851 + 227, 0, 1);
            cinemaHold.Fade(98760, 1); //To fade for "in my heart"

            cinema.Scale(OsbEasing.InCubic, 58987, 59101, screenScale, 0.68);
            cinema.Scale(OsbEasing.OutCubic, 59101, 59215, 0.68, screenScale);

            // Cinema Meguru face location
            var initialCinemaFaceLocation = new Vector2(974, 200);
            var cinemaxRatio = initialCinemaFaceLocation.X / 1280;
            var cinemayRatio = initialCinemaFaceLocation.Y / 720;
            var cinemaX = (854 * cinemaxRatio) + -107;
            var cinemaY = 480 * cinemayRatio;

            // Meguru Expressions
            var cinemaLayer1 = layer.CreateSprite("sb/scenes/cinemaLayer1.jpg", OsbOrigin.Centre);
            var cinemaLayer2 = layer.CreateSprite("sb/scenes/cinemaLayer2.jpg", OsbOrigin.Centre);
            var cinemaLayer3 = layer.CreateSprite("sb/scenes/cinemaLayer3.jpg", OsbOrigin.Centre);

            cinemaLayer1.Move(66487, cinemaX, cinemaY);
            cinemaLayer1.Scale(66487, screenScale);
            cinemaLayer1.Fade(OsbEasing.OutSine, 66487, 66487 + 227, 0, 1);
            cinemaLayer1.Fade(OsbEasing.OutSine, 73078, 73078 + 114, 1, 0);

            cinemaLayer2.Move(80124, cinemaX, cinemaY);
            cinemaLayer2.Scale(80124, screenScale);
            cinemaLayer2.Fade(OsbEasing.OutSine, 80124, 80124 + 227, 0, 1);
            cinemaLayer2.Fade(OsbEasing.OutSine, 87396, 87396 + 114, 1, 0);

            cinemaLayer3.Move(87169, cinemaX, cinemaY);
            cinemaLayer3.Scale(87169, screenScale);
            cinemaLayer3.Fade(OsbEasing.OutSine, 87169, 87169 + 227, 0, 1);
            cinemaLayer3.Fade(98760, 1); //To fade for "in my heart"

            // Hand Holding Kiai 1



            // Thighs Verse 2



            // Hug Scene Kiai 2
            var Hug = layer.CreateSprite("sb/scenes/hug.jpg" , OsbOrigin.Centre);

            Hug.Scale(182396, screenScale);
            Hug.Fade(182396, 1);
            Hug.Fade(211487, 1);

            // Hug Meguru face location
            var initialHugFaceLocation = new Vector2(456.5f, 195.5f);
            var HugxRatio = initialHugFaceLocation.X / 1280;
            var HugyRatio = initialHugFaceLocation.Y / 720;
            var HugX = (854 * HugxRatio) + -107;
            var HugY = 480 * HugyRatio;

            // Face changes (need to fix naming make it in order)
            var hugLayer1 = layer.CreateSprite("sb/scenes/hugLayer1.jpg", OsbOrigin.Centre);
            var hugLayer3 = layer.CreateSprite("sb/scenes/hugLayer3.jpg", OsbOrigin.Centre);
            var hugLayer5 = layer.CreateSprite("sb/scenes/hugLayer5.jpg", OsbOrigin.Centre);
            var hugLayer6 = layer.CreateSprite("sb/scenes/hugLayer6.jpg", OsbOrigin.Centre);
            var hugLayer4 = layer.CreateSprite("sb/scenes/hugLayer4.jpg", OsbOrigin.Centre);
            var hugLayer2 = layer.CreateSprite("sb/scenes/hugLayer2.jpg", OsbOrigin.Centre);

            hugLayer1.Move(185351, HugX, HugY);
            hugLayer1.Scale(185351, screenScale);
            hugLayer1.Fade(OsbEasing.OutSine, 185351, 185351 + 227, 0, 1);
            hugLayer1.Fade(OsbEasing.OutSine, 189782, 189782 + 114, 1, 0);

            hugLayer3.Move(189555, HugX, HugY);
            hugLayer3.Scale(189555, screenScale);
            hugLayer3.Fade(OsbEasing.OutSine, 189555, 189555 + 227, 0, 1);
            hugLayer3.Fade(OsbEasing.OutSine, 196714, 196714 + 114, 1, 0);

            hugLayer5.Move(196487, HugX, HugY);
            hugLayer5.Scale(196487, screenScale);
            hugLayer5.Fade(OsbEasing.OutSine, 196487, 196487 + 227, 0, 1);
            hugLayer5.Fade(OsbEasing.OutSine, 198532, 198532 + 114, 1, 0);

            hugLayer6.Move(198305, HugX, HugY);
            hugLayer6.Scale(198305, screenScale);
            hugLayer6.Fade(OsbEasing.OutSine, 198305, 198305 + 227, 0, 1);
            hugLayer6.Fade(OsbEasing.OutSine, 200351, 200351 + 114, 1, 0);

            hugLayer4.Move(200123, HugX, HugY);
            hugLayer4.Scale(200123, screenScale);
            hugLayer4.Fade(OsbEasing.OutSine, 200123, 200123 + 227, 0, 1);
            hugLayer4.Fade(OsbEasing.OutSine, 202510, 202510 + 114, 1, 0);

            hugLayer2.Move(202282, HugX, HugY);
            hugLayer2.Scale(202282, screenScale);
            hugLayer2.Fade(OsbEasing.OutSine, 202282, 202282 + 227, 0, 1);
            hugLayer2.Fade(OsbEasing.OutSine, 205578, 205578 + 227, 1, 0);

            hugLayer3.Move(205578, HugX, HugY);
            hugLayer3.Scale(205578, screenScale);
            hugLayer3.Fade(205578, 1);
            hugLayer3.Fade(OsbEasing.OutSine, 209669, 209669 + 227, 1, 0);
            
            // Heart Kiai 3

            

            // Flash (To be Moved to transisions script later)
            var flash = layer.CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/pixel.png");
            flash.ScaleVec(0, 854.0 / bitmap.Width, 480.0 / bitmap.Height);
            flash.Fade(0, 0);

            flash.Fade(22169, 22169 + 114, 0.7, 0);
            flash.Fade(58533, 58533 + 227, 0.7, 0);

            flash.Fade(182396, 182396 + 227, 0.7, 0);
        }
    }
}
