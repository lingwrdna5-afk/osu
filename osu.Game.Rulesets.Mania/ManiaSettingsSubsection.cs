// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterfaceV2;
using osu.Game.Localisation;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Mania.Configuration;
using osu.Game.Rulesets.Mania.UI;

namespace osu.Game.Rulesets.Mania
{
    public partial class ManiaSettingsSubsection : RulesetSettingsSubsection
    {
        public ManiaSettingsSubsection(ManiaRuleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (ManiaRulesetConfigManager)Config;

            Children = new Drawable[]
            {
                new SettingsItemV2(new FormEnumDropdown<ManiaScrollingDirection>
                {
                    Caption = RulesetSettingsStrings.ScrollingDirection,
                    Current = config.GetBindable<ManiaScrollingDirection>(ManiaRulesetSetting.ScrollDirection)
                }),
                new SettingsItemV2(new FormSliderBar<double>
                {
                    Caption = RulesetSettingsStrings.ScrollSpeed,
                    Current = config.GetBindable<double>(ManiaRulesetSetting.ScrollSpeed),
                    KeyboardStep = 1,
                    LabelFormat = v => RulesetSettingsStrings.ScrollSpeedTooltip((int)DrawableManiaRuleset.ComputeScrollTime(v), v),
                }),
                new SettingsItemV2(new FormCheckBox
                {
                    Caption = RulesetSettingsStrings.TimingBasedColouring,
                    Current = config.GetBindable<bool>(ManiaRulesetSetting.TimingBasedNoteColouring),
                })
                {
                    Keywords = new[] { "color" },
                },
            };

            Add(new SettingsItemV2(new FormCheckBox
            {
                Caption = RulesetSettingsStrings.TouchOverlay,
                Current = config.GetBindable<bool>(ManiaRulesetSetting.TouchOverlay)
            }));
            Add(new SettingsItemV2(new FormSliderBar<double>
            {
                Caption = "Touch area width",
                Current = config.GetBindable<double>(ManiaRulesetSetting.TouchOverlayWidth),
                KeyboardStep = 0.05f,
                LabelFormat = v => $"{v * 100:0}%",
            }));

            Add(new SettingsItemV2(new FormSliderBar<double>
            {
                Caption = "Touch area height",
                Current = config.GetBindable<double>(ManiaRulesetSetting.TouchOverlayHeight),
                KeyboardStep = 0.05f,
                LabelFormat = v => $"{v * 100:0}%",
            }));

            Add(new SettingsItemV2(new FormSliderBar<double>
            {
                Caption = "Touch area horizontal position (0% left, 100% right)",
                Current = config.GetBindable<double>(ManiaRulesetSetting.TouchOverlayPositionX),
                KeyboardStep = 0.05f,
                LabelFormat = v => $"{v * 100:0}%",
            }));

            Add(new SettingsItemV2(new FormSliderBar<double>
            {
                Caption = "Touch area vertical position (0% top, 100% bottom)",
                Current = config.GetBindable<double>(ManiaRulesetSetting.TouchOverlayPositionY),
                KeyboardStep = 0.05f,
                LabelFormat = v => $"{v * 100:0}%",
            }));
            if (RuntimeInfo.IsMobile)
            {
                Add(new SettingsItemV2(new FormEnumDropdown<ManiaMobileLayout>
                {
                    Caption = RulesetSettingsStrings.MobileLayout,
                    Current = config.GetBindable<ManiaMobileLayout>(ManiaRulesetSetting.MobileLayout),
#pragma warning disable CS0618 // Type or member is obsolete
                    Items = Enum.GetValues<ManiaMobileLayout>().Where(l => l != ManiaMobileLayout.LandscapeWithOverlay),
#pragma warning restore CS0618 // Type or member is obsolete
                }));
            }
        }
    }
}
