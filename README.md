![xlcore_sized](https://user-images.githubusercontent.com/16760685/197423373-b6082cdb-dc1f-46db-8768-3f507f182ba8.png)

# XIVLauncher.Core  [![Discord Shield](https://discordapp.com/api/guilds/581875019861328007/widget.png?style=shield)](https://discord.gg/3NMcUV5) RB Patched!
Cross-platform version of XIVLauncher, optimized for Steam Deck. Comes with a version of [WINE tuned for FFXIV](https://github.com/goatcorp/wine-xiv-git).

## Features

1. NEW: Added Proton as an option. Selecting Proton from the Installation Type in the Wine tab will reveal another menu with all the proton installs from Steam. This includes any GE-Proton versions you have in the compatibilitytools.d folder, and any extra proton versions (such as Proton Experimental) you have installed.

2. App Launcher is currently disabled; I need to do a bit of refactoring to get it to work with 1.0.4.

3. Added a button on the Wine tab for a Wine Explorer with WineD3D. Some apps may not like DXVK, so you can use this to launch them without DXVK.

4. Added a Download Now! button on the wine tab to download wine without trying to log in.

5. Added XL_PATH environment variable. This will let you specify an alternate directory for the xlcore folder. So instead of using the default ~/.xlcore, you can use XL_PATH=~/.local/share/xlcore, for example.

6. This will create a completely new folder, so you'll need to configure it to point to the correct ffxiv game and config folders. This may help with multiboxing, since you can run multiple copies of the game from different prefixes / wine installs.

7.Added a DXVK tab to Settings. This has all the DXVK settings now. Default version is 1.10.3, but you can try out 2.0, 2.1, and 2.2 versions, including the new gplasync patch for 2.1 & 2.2. Be aware that 8.

8.Reshade, GShade, and Dalamud may all have issues with 2.0+. If the game doesn't launch, or is glitchy, change this back to one of the 1.10 version.

9. Hud options and Async option have been moved to DXVK tab. Frame rate limit has also been added.

10.WineD3D can now be enabled by selecting Disabled as the Dxvk version in the DXVK tab.

11. Font size can be adjusted (requires restart).

## Using on Steam Deck
If you want to use XIVLauncher on your Steam Deck, feel free to [follow our guide in our FAQ](https://goatcorp.github.io/faq/steamdeck). If you're having trouble, you can [join our Discord server](https://discord.gg/3NMcUV5) - please don't use the GitHub issues for troubleshooting unless you're sure that your problem is an actual issue with XIVLauncher.

## Building & Contributing
1. Clone this repository with submodules
2. Make sure you have a recent(.NET6+) version of the .NET SDK installed
2. Run `dotnet build` or `dotnet publish`

Common components that are shared with the Windows version of XIVLauncher are linked as a submodule in the "lib" folder. XIVLauncher Core can run on Windows, but is by far not as polished as the original Windows version, as such we are not distributing it.

## Distribution
XIVLauncher Core has community packages for various Linux distributions. Please be aware that **only the Flathub version is official**, but the others are **packaged by trusted community members**.  The community packages may not always be up-to-date, or may have versions that are broken or contain features under testing (especially if labeled as unstable or git). We can't take any responsibility for their safety or reliability.

| Repo        | Status      |
| ----------- | ----------- |
| [AUR (RankynBass)](https://aur.archlinux.org/packages/xivlauncher-rb) | ![AUR version](https://img.shields.io/aur/version/xivlauncher-rb) |
| [AUR](https://aur.archlinux.org/packages/xivlauncher) | ![AUR version](https://img.shields.io/aur/version/xivlauncher) |
| [AUR (git)](https://aur.archlinux.org/packages/xivlauncher-git) | ![AUR version](https://img.shields.io/aur/version/xivlauncher-git) |
| [Copr (Fedora+openSuse+EL9)](https://copr.fedorainfracloud.org/coprs/rankyn/xivlauncher/) | ![COPR version](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Frankynbass%2FXIVLauncher4rpm%2Fmain%2Fbadge.json)|
| [GURU (Gentoo)](https://gitweb.gentoo.org/repo/proj/guru.git/tree/games-util/xivlauncher) | ![GURU version](https://repology.org/badge/version-for-repo/gentoo_ovl_guru/xivlauncher.svg?header=guru) |
| [MPR (Debian+Ubuntu)](https://mpr.makedeb.org/packages/xivlauncher)  | ![MPR package](https://repology.org/badge/version-for-repo/mpr/xivlauncher.svg?header=MPR) |
| [nixpkgs stable](https://search.nixos.org/packages?channel=22.11&from=0&size=50&sort=relevance&type=packages&query=xivlauncher) | ![nixpkgs stable version](https://repology.org/badge/version-for-repo/nix_stable_22_11/xivlauncher.svg?header=nixpkgs%2022.11) |
| [nixpkgs unstable](https://search.nixos.org/packages?channel=unstable&from=0&size=50&sort=relevance&type=packages&query=xivlauncher) | ![nixpkgs unstable version](https://repology.org/badge/version-for-repo/nix_unstable/xivlauncher.svg?header=nixpkgs%20unstable) |
| [PPA (Ubuntu)](https://launchpad.net/~linneris/+archive/ubuntu/xivlauncher-core-stable) | ![PPA version](https://img.shields.io/static/v1?label=PPA&message=1.0.3&color=brightgreen) |
