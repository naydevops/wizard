<a id="readme-top"></a>

<div align="center">

  [![Contributors][contributors-shield]][contributors-url]
  [![Forks][forks-shield]][forks-url]
  [![Stargazers][stars-shield]][stars-url]
  [![Issues][issues-shield]][issues-url]
  [![MIT License][license-shield]][license-url]

  <div align="center">
    <img src="https://raw.githubusercontent.com/naydevops/wizard/refs/heads/main/.github/wizard.png" alt="Logo" width="640" height="320">
  </div>

  <h3 align="center">🪄 wizard - the little helper</h3>

  <p align="center">
    A little utility to pre-download all Wizard101 files.
    <br />
    <a href="https://github.com/naydevops/wizard/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/naydevops/wizard/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

## 🐹 About The Project

Wizard101 was released in September 2008 but, in all of its history, it has had no ability to pre-download every file to your system like other video games.

Pre-downloading your files allows:
* **Better Performance:** No delays or lag caused by downloading assets on the go.
* **No Data Limits:** Avoid internet usage spikes or data caps from streaming.
* **Faster Load Times:** Local files mean quicker loading and smoother transitions.

This project is early stages, the code is a prototype, terrible, no exception checking is made and only supports Windows for now. The application assumes your internet won't go down, and the patch server is always up, or you'll have to start again for now... This will be fixed in future versions. Sorry!

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## ⚠️ Warning

Read the <a href="https://www.wizard101.com/patchClient/termsofuse">Wizard101 Terms of Service</a> and <a href="https://github.com/naydevops/wizard/blob/main/LICENSE">project license</a> before using this software. I do not have any warranty or liability if you are sanctioned by Kingsisle Entertainment. Their terms of service state:

* You may not copy, modify, create derivative works of, publicly display or perform, republish, store, transmit or distribute any of the material on their site.
* You are prohibited from distributing, modifying, reverse-engineering, decompiling, or disassembling their software.
* You are responsible for complying with applicable laws when downloading their software, particularly with regard to U.S. export control laws.
* If downloading software from the Wizard101 site, you warrant that you are not located in any country subject to U.S. embargoes and that you comply with any requisite government authorizations.

This software may be requested to be taken down at any time by Kingsisle Entertainment in which I will happily comply if needed. This is an unofficial tool that is not endorsed by KingsIsle Entertainment. Wizard101 is a trademark of KingsIsle Entertainment.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## 🌍 Getting Started

Download the publish folder from the GitHub releases. Wizard101 is approximately 15GB as of September 2024, it may take up to 2 hours depending on your internet connection.

Run the `.exe` file to launch the tool. The tool will delete all previously downloaded files and download everything fresh.

This tool uses Microsoft .NET 8. If you are running into errors, download .NET 8 here: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## ⚙️ Compiling (Advanced)

If you're a little skeptical of downloading a random .exe online, I don't blame you. To prevent this, you're more than welcome to compile this yourself via Visual Studio, Rider or any other IDE. Open up the Microsoft .sln file, ensure you have .NET 8 installed and proceed to compile under the included 'Release' configuration. This will create your compiled .exe file.

You can also run the command `dotnet publish -r win-x64` in the root directory where `Wizard.sln` is kept in a terminal. This will output the .exe in `./src/Wizard.ConsoleInterface/bin/Release/net8.0/win-x64/publish/Wizard.ConsoleInterface.exe`

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contributing

Any contributions you make are **greatly appreciated**. If you have a suggestion that would make wizard better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

### ⚙️ Top contributors:

<a href="https://github.com/naydevops/wizard/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=naydevops/wizard" alt="contrib.rocks image" />
</a>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## 📝 License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<div align="center">
  <sub>Open source software should be free forever.</sub>

  <sub>Made with 💖 by <a href="https://github.com/naydevops">Nathan Hall</a>.</sub>

  <img height="30" src="https://cdn3.emoji.gg/emojis/6021_Cat.gif" href="#">
</div>

[contributors-shield]: https://img.shields.io/github/contributors/naydevops/wizard.svg?style=for-the-badge
[contributors-url]: https://github.com/naydevops/wizard/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/naydevops/wizard.svg?style=for-the-badge
[forks-url]: https://github.com/naydevops/wizard/network/members
[stars-shield]: https://img.shields.io/github/stars/naydevops/wizard.svg?style=for-the-badge
[stars-url]: https://github.com/naydevops/wizard/stargazers
[issues-shield]: https://img.shields.io/github/issues/naydevops/wizard.svg?style=for-the-badge
[issues-url]: https://github.com/naydevops/wizard/issues
[license-shield]: https://img.shields.io/github/license/naydevops/wizard.svg?style=for-the-badge
[license-url]: https://github.com/naydevops/wizard//blob/master/LICENSE.txt
