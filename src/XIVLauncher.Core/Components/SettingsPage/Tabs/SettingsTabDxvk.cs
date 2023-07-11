using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using ImGuiNET;
using XIVLauncher.Common.Unix.Compatibility;
using XIVLauncher.Common.Util;
using XIVLauncher.Core.UnixCompatibility;

namespace XIVLauncher.Core.Components.SettingsPage.Tabs;

public class SettingsTabDxvk : SettingsTab
{
    private SettingsEntry<DxvkVersion> dxvkVersionSetting;
    private SettingsEntry<bool> wineD3DUseVk;
    private SettingsEntry<HudType> dxvkHudSetting;

    public SettingsTabDxvk()
    {
        Entries = new SettingsEntry[]
        {
            dxvkVersionSetting = new SettingsEntry<DxvkVersion>("DXVK Version", "Choose which version of DXVK to use.", () => Program.Config.DxvkVersion ?? DxvkVersion.v1_10_3, type => Program.Config.DxvkVersion = type)
            {
                CheckWarning = type =>
                {
                    if (new [] {DxvkVersion.v2_1, DxvkVersion.v2_2}.Contains(type))
                        return "May not work with older graphics cards. AMD users may need to use env variable RADV_PERFTEST=gpl";
                    return null;
                },
            },
            wineD3DUseVk = new SettingsEntry<bool>("Use WineD3D Vulkan renderer", "Use Vulkan instead of OpenGL for WineD3D. Unstable. May cause system to lock up.", () => Program.Config.WineD3DUseVK ?? false, b => Program.Config.WineD3DUseVK = b)
            {
                CheckVisibility = () => dxvkVersionSetting.Value == DxvkVersion.Disabled,
                CheckWarning = b =>
                {
                    if (b)
                        return "WARNING! This is very experimental, and may cause your system to crash or hang. If you still want to use it, disable Dalamud.";
                    return null;
                },
            },
            new SettingsEntry<bool>("Enable DXVK ASYNC", "Enable DXVK ASYNC patch.", () => Program.Config.DxvkAsyncEnabled ?? true, b => Program.Config.DxvkAsyncEnabled = b)
            {
                CheckVisibility = () => (new [] {DxvkVersion.v1_10_3, DxvkVersion.v2_0}.Contains(dxvkVersionSetting.Value)),
                CheckWarning = b =>
                {
                    if (!b && dxvkVersionSetting.Value == DxvkVersion.v2_0)
                        return "AMD users may need to use env variable RADV_PERFTEST=gpl";
                    return null;
                },
            },
            dxvkHudSetting = new SettingsEntry<HudType>("DXVK Overlay", "DXVK Hud is included with Dxvk. It doesn't work if Dxvk is disabled.\nMangoHud must be installed separately. Flatpak XIVLauncher needs flatpak MangoHud.", () => Program.Config.HudType, type => Program.Config.HudType = type)
            {
                CheckVisibility = () => dxvkVersionSetting.Value != DxvkVersion.Disabled || wineD3DUseVk.Value,
                CheckValidity = type =>
                {
                    if ((type == HudType.MangoHud || type == HudType.MangoHudCustom || type == HudType.MangoHudFull)
                        && (!File.Exists("/usr/lib/mangohud/libMangoHud.so") && !File.Exists("/usr/lib64/mangohud/libMangoHud.so") && !File.Exists("/usr/lib/extensions/vulkan/MangoHud/lib/x86_64-linux-gnu/libMangoHud.so")))
                        return "MangoHud not detected.";

                    return null;
                }
            },
            new SettingsEntry<string>("DXVK Hud Custom String", "Set a custom string for the built in DXVK Hud. Warning: If it's invalid, the game may hang.", () => Program.Config.DxvkHudCustom, s => Program.Config.DxvkHudCustom = s)
            {
                CheckVisibility = () => dxvkHudSetting.Value == HudType.Custom && dxvkVersionSetting.Value != DxvkVersion.Disabled,
                CheckWarning = s =>
                {
                    if(!HudManager.CheckDxvkHudString(s))
                        return "That's not a valid hud string";
                    return null;
                },
            },
            new SettingsEntry<string>("MangoHud Custom Path", "Set a custom path for MangoHud config file.", () => Program.Config.MangoHudCustom, s => Program.Config.MangoHudCustom = s)
            {
                CheckVisibility = () => dxvkHudSetting.Value == HudType.MangoHudCustom  && !(dxvkVersionSetting.Value == DxvkVersion.Disabled && !wineD3DUseVk.Value),
                CheckWarning = s =>
                {
                    if(!File.Exists(s))
                        return "That's not a valid file.";
                    return null;
                },
            },
            new NumericSettingsEntry("Frame Rate Limit", "Set a frame rate limit, and DXVK will try not exceed it. Use 0 for unlimited.", () => Program.Config.DxvkFrameRate ?? 0, i => Program.Config.DxvkFrameRate = i, 0, 1000)
            {
                CheckVisibility = () => dxvkVersionSetting.Value != DxvkVersion.Disabled,
            },
        };
    }

    public override SettingsEntry[] Entries { get; }

    public override bool IsUnixExclusive => true;

    public override string Title => "DXVK";

    public override void Draw()
    {
        ImGui.TextUnformatted("If you chose Proton in the Wine Tab, the version does not matter, except for Disabled.");
        ImGui.TextUnformatted("Choose any version of Dxvk, and set the rest of the options as normal. Disabled will attempt to use WineD3D. This probably won't work.");
        ImGui.Separator();
        
        base.Draw();
    }

    public override void Save()
    {
        base.Save();
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Program.CreateCompatToolsInstance();
    }
}
