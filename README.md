<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->



<!-- PROJECT LOGO -->
<br />
<h3 align="center">CinemaCritic</h3>

  <p align="center">
    This project is a web application for movie enjoyers who want to share their insights.
    <br />
    <a href=""><strong>Explore the docs » (to be implemented)</strong></a>
    <br />
    <br />
    <a href="https://github.com/KajusC/CinemaCritic/issues">Report Bug</a>
    ·
    <a href="https://github.com/KajusC/CinemaCritic/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

The project is a movie review website, where users will be able to write reviews and find new movies. It will offer
features such as: filtering by parametrs, adding movies to you favorites, browsing the top rated movies and much more.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* [![CSharp][Csharp.com]][Csharp-url]
* [![HTML][Html.com]][Html-url]
* [![Css][Css.com]][Css-url]
* [![.NET][dot.net]][dot.net-url]
* [![Bootstrap][Bootstrap.com]][Bootstrap-url]
* [![PostgreSQL][PostgreSQL.org]][PostgreSQL-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.

* Download PostgreSQL from the official website: https://www.postgresql.org/
* Create a database with the name "MovieReviewWebsite"

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/KajusC/CinemaCritic.git
   ```
2. Enter a connection string to the `appsettings.json` file in the API project in the "ConnectionStrings" section like this (replace username and password)
   ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost; Database=MovieReviewWebsite; Username=your_username; Password=your_password"
    },
   ```
3. In the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console) enter in a command to perform a database update
   ```sh
    Update-Database
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

This section will be updated as the program is developed more 

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTRIBUTING -->
## Contributing

A the moment the project is not accepting any contributors. However, any suggestions are welcomed in the issue tab: <a href="https://github.com/KajusC/CinemaCritic/issues">Request Feature</a>

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Erikas Rudokas - [LinkedIn](https://www.linkedin.com/in/erikasrudokas/)
<br/>
Kajus Černiauskas - [LinkedIn](https://www.linkedin.com/in/kajus-%C4%8Derniauskas-a68506205/)
<br/>
Matas Motiejūnas - [LinkedIn](https://www.linkedin.com/in/matas-motiej%C5%ABnas-09a050274/)
<br/>
Jonas Šileikis - [LinkedIn]()
<br/>

Project Link: [https://github.com/KajusC/CinemaCritic](https://github.com/KajusC/CinemaCritic)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [C# Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)
* [Gavin Lee's video on Blazor](https://www.youtube.com/watch?v=sHuuo9L3e5c&ab_channel=freeCodeCamp.org)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/github_username/repo_name.svg?style=for-the-badge
[contributors-url]: https://github.com/github_username/repo_name/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/github_username/repo_name.svg?style=for-the-badge
[forks-url]: https://github.com/github_username/repo_name/network/members
[stars-shield]: https://img.shields.io/github/stars/github_username/repo_name.svg?style=for-the-badge
[stars-url]: https://github.com/github_username/repo_name/stargazers
[issues-shield]: https://img.shields.io/github/issues/github_username/repo_name.svg?style=for-the-badge
[issues-url]: https://github.com/github_username/repo_name/issues
[license-shield]: https://img.shields.io/github/license/github_username/repo_name.svg?style=for-the-badge
[license-url]: https://github.com/github_username/repo_name/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/screenshot.png
[Csharp.com]: https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white
[Csharp-url]: https://learn.microsoft.com/en-us/dotnet/csharp/
[Html.com]:https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white
[Html-url]: https://developer.mozilla.org/en-US/docs/Web/HTML
[Css.com]:  https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white
[Css-url]:https://developer.mozilla.org/en-US/docs/Web/CSS
[dot.net]:  https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[dot.net-url]: https://dotnet.microsoft.com/en-us/
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com/
[PostgreSQL.org]: https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white
[PostgreSQL-url]: https://www.postgresql.org/
