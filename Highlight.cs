using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;


namespace StorybrewScripts
{
    public class Highlight : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            ring(98760); // Kiai 1
            ring(100351);
            ring(100578);
            ring(102169);
            ring(102396);
            ring(104215);
            ring(104669);
            ring(105124);
            ring(118533);
            ring(105578);
            ring(106033);
            ring(106715);
            ring(107624);
            ring(107851);
            ring(109442);
            ring(109669);
            ring(109896);
            ring(111260);
            ring(114896);
            ring(115124);
            ring(116715);
            ring(116942);
            ring(121260);
            ring(122396);
            ring(123078);
            ring(123987);
            ring(124215);

            ring(111487);
            ring(111942);
            ring(112396);
            ring(112851);
            ring(113305);
            light(111487);
            light(111942);
            light(112396);
            light(112851);
            light(113305);

            ring(118760);
            ring(119215);
            ring(119669);
            ring(120124);
            ring(120578);
            light(118760);
            light(119215);
            light(119669);
            light(120124);
            light(120578);

            ring(182396); // Kiai 2
            ring(183987);
            ring(184215);
            ring(185805);
            ring(186033);
            ring(187851);
            ring(188305);
            ring(188760);
            ring(194896);
            ring(189215);
            ring(189669);
            ring(190351);
            ring(191260);
            ring(191487);
            ring(193078);
            ring(193305);
            ring(193533);
            ring(202169);
            ring(198533);
            ring(198760);
            ring(200351);
            ring(200578);
            ring(204896);
            ring(206033);
            ring(206715);
            ring(207624);
            ring(207851);

            ring(195124);
            ring(195578);
            ring(196033);
            ring(196487);
            ring(196942);
            light(195124);
            light(195578);
            light(196033);
            light(196487);
            light(196942);

            ring(202396);
            ring(202851);
            ring(203305);
            ring(203760);
            ring(204215);
            light(202396);
            light(202851);
            light(203305);
            light(203760);
            light(204215);

            ring(242396); // Kiai 3
            ring(243533);
            ring(243987);
            ring(244215);
            ring(245805);
            ring(246033);
            ring(250351);
            ring(251487);
            ring(252169);
            ring(253078);
            ring(253305);
            ring(254896);
            ring(255124);
            ring(256942);
            ring(257624);
            ring(258078);
            ring(258533);
            ring(258760);
            ring(259442);
            ring(260578);
            ring(261260);
            ring(262169);
            ring(262396);
            ring(263078);
            ring(263533);
            ring(263987);
            ring(264215);
            ring(247624);
            ring(250805);

            ring(247851);
            ring(248305);
            ring(248760);
            ring(249215);
            ring(249669);
            light(247851);
            light(248305);
            light(248760);
            light(249215);
            light(249669);
        }
        
        private void ring(double startTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < startTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/particles/ring.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + Constants.beatLength, 1, 0);
                    sprite.Scale(hitobject.StartTime, hitobject.StartTime + Constants.beatLength, 0.4, 0.9);
                    sprite.Additive(hitobject.StartTime, hitobject.StartTime + Constants.beatLength);
                    sprite.Color(hitobject.StartTime, hitobject.Color);
                }
            }
        }

        private void light(double startTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < startTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/particles/light.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + Constants.beatLength, 0.7, 0);
                    sprite.Scale(hitobject.StartTime, hitobject.StartTime + Constants.beatLength, 1, 0.5);
                    sprite.Additive(hitobject.StartTime, hitobject.StartTime + Constants.beatLength);
                    sprite.Color(hitobject.StartTime, hitobject.Color);
                }
            }
        }
    }
}
