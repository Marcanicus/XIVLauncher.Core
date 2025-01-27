﻿using System.Numerics;
using ImGuiNET;
using XIVLauncher.Core.Components.SettingsPage.Tabs;

namespace XIVLauncher.Core.Components.SettingsPage;

public class SettingsPage : Page
{
    private readonly SettingsTab[] tabs =
    {
        new SettingsTabGame(),
        new SettingsTabPatching(),
        new SettingsTabWine(),
        new SettingsTabDxvk(),
        new SettingsTabDalamud(),
        new SettingsTabAutoStart(),
        new SettingsTabAbout(),
        new SettingsTabDebug(),
        new SettingsTabTroubleshooting(),
    };

    private string searchInput = string.Empty;

    public SettingsPage(LauncherApp app)
        : base(app)
    {
    }

    public override void OnShow()
    {
        foreach (var settingsTab in this.tabs)
        {
            settingsTab.Load();
        }

        this.searchInput = string.Empty;

        base.OnShow();
    }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("###settingsTabs"))
        {
            if (string.IsNullOrEmpty(this.searchInput))
            {
                foreach (SettingsTab settingsTab in this.tabs)
                {
                    if (settingsTab.IsUnixExclusive && Environment.OSVersion.Platform != PlatformID.Unix)
                        continue;

                    if (ImGui.BeginTabItem(settingsTab.Title))
                    {
                        if (ImGui.BeginChild($"###settings_scrolling_{settingsTab.Title}", new Vector2(-1, -1), false))
                        {
                            settingsTab.Draw();
                        }

                        ImGui.EndChild();
                        ImGui.EndTabItem();
                    }
                }
            }
            else
            {
                if (ImGui.BeginTabItem("Search Results"))
                {
                    var any = false;

                    foreach (SettingsTab settingsTab in this.tabs)
                    {
                        if (settingsTab.IsUnixExclusive && Environment.OSVersion.Platform != PlatformID.Unix)
                            continue;

                        var eligible = settingsTab.Entries
                            .Where(x => x.Name.Contains(this.searchInput.Trim(),
                                StringComparison.InvariantCultureIgnoreCase));

                        if (!eligible.Any())
                            continue;

                        any = true;

                        if (ImGui.BeginChild("SearchResults"))
                        {
                            ImGui.TextColored(ImGuiColors.DalamudGrey, settingsTab.Title);
                            ImGui.Dummy(new Vector2(5) * ImGuiHelpers.GlobalScale);

                            foreach (SettingsEntry settingsTabEntry in eligible)
                            {
                                if (!settingsTabEntry.IsVisible)
                                    continue;

                                settingsTabEntry.Draw();
                            }

                            ImGui.Separator();

                            ImGui.Dummy(new Vector2(10) * ImGuiHelpers.GlobalScale);
                        }
                        ImGui.EndChild();
                    }

                    if (!any)
                        ImGui.TextColored(ImGuiColors.DalamudGrey, "No results found...");

                    ImGui.EndTabItem();
                }
            }
        }

        ImGui.SetCursorPos(ImGuiHelpers.ViewportSize - new Vector2(60) * ImGuiHelpers.GlobalScale);

        if (ImGui.BeginChild("###settingsFinishButton"))
        {
            ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 100f * ImGuiHelpers.GlobalScale);
            ImGui.PushFont(FontManager.IconFont);

            var invalid = this.tabs.Any(x => x.Entries.Any(y => !y.IsValid));
            if (invalid)
            {
                ImGui.BeginDisabled();
                ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.DalamudRed);
                ImGui.Button(FontAwesomeIcon.Ban.ToIconString(), new Vector2(40) * ImGuiHelpers.GlobalScale);
                ImGui.PopStyleColor();
                ImGui.EndDisabled();
            }
            else
            {
                if (ImGui.Button(FontAwesomeIcon.Check.ToIconString(), new Vector2(40) * ImGuiHelpers.GlobalScale))
                {
                    foreach (var settingsTab in this.tabs)
                    {
                        settingsTab.Save();
                    }

                    this.App.State = LauncherApp.LauncherState.Main;
                }
            }
        }

        ImGui.EndChild();

        ImGui.PopStyleVar();
        ImGui.PopFont();

        var vpSize = ImGuiHelpers.ViewportSize;
        ImGui.SetCursorPos(new Vector2(vpSize.X - 260 * ImGuiHelpers.GlobalScale, 4 * ImGuiHelpers.GlobalScale));
        ImGui.SetNextItemWidth(250 * ImGuiHelpers.GlobalScale);
        ImGui.InputTextWithHint("###searchInput", "Search for settings...", ref this.searchInput, 100);

        base.Draw();
    }
}