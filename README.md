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
    This project is a web application for movie enjoyers who want to share their insights and discover new movies
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

The project is a movie review website, where users will be able share their insights, rate movies and find new movies. The project offers many features such as:
being able to review a movie, being able to track your own reviews, see reviews for other movies, find out more about a movie, the ability to favorite movies, filter based on specific
parameters such as genre, age rating or year of release.

The project aims to unite all movie enjoyers into a single website where all the functionality will be available and no other webpages will be needed. The main goal of the project is to create an environment where movie enthusiasts will be able to express themselves and find new and intriguing movies that maybe they did not think they liked.

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

### Prerequisites

To use this application in a development environment: 

* Download PostgreSQL from the official website: https://www.postgresql.org/
* Create a database with the name "MovieReviewWebsite"
* Install the latest .NET SDK from the official website: https://dotnet.microsoft.com/en-us/download
* Any IDE or text editor can be used, but Visual Studio is recommended: https://visualstudio.microsoft.com/downloads/

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
4. To run the project you will need to run both the API and the Blazor project simultaneously. To achieve this right click on the solution. In the opened up window click 
"Configure Startuo Projects...". In the window that opened select "Multiple startup projects:" and set the CinemaCritic.API and CinemaCritic.Web to "Start"
<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

When the application is first launched you will be met with a home screen. In the navigation bar you can see options of where you can navigate to. The home screen displays the name
of the website and a short description.
![home](https://github.com/KajusC/CinemaCritic/assets/144147405/a34086f1-dcfd-4a06-aa16-f5d1000d6c7c)

After running the program clicking on "Sign Up" will redirect to a signup page where the user will be asked to enter in their details. The user is requested to enter their first name, last name, email, username and a password. After entering all the details clicking "Sign Up" will create an account and a popup will appear
![signup](https://github.com/KajusC/CinemaCritic/assets/144147405/2ede475e-8587-483e-80bd-bcb1adafa7b1)

After signing up the user can now go to the "Log In" page via the navbar and login using their credentials. The user is asked to fill out two fields: username and password. Once the fields are filled out the user presses "Log In" and is now logged in.
![login](https://github.com/KajusC/CinemaCritic/assets/144147405/c8fcc77f-78a0-4090-ab61-ce277097be46)

After logging in many more navigation bar options appear.
![change_after_login](https://github.com/KajusC/CinemaCritic/assets/144147405/045c6072-af27-4382-a14e-5e794d9f1770)

Clicking on "Reviews" will redirect to all the logged in users' reviews. This page allows the user to see all the reviews that he has ever written, including the title, the movie for which the review is written, the rating, the comment and the date of the review. It also allows to edit or delete the review with the apporpriate buttons.
![user_reviews](https://github.com/KajusC/CinemaCritic/assets/144147405/d60d13a7-4951-4924-b23b-984c4eae14a9)

Clicking on "Profile" will redirect the user to his profile dashboard. This dashboard is used to alter the information that was provided during the signup process.
![edit_profile](https://github.com/KajusC/CinemaCritic/assets/144147405/c5ad6564-6e36-4a1a-b30d-7eaa81a500ce)

Click on "Movies" will redirect the user to the list of movies that are in the database. The webpage displays a movie poster and title. The page also offers the ability to filter movies and implements pagination.
![movie_view](https://github.com/KajusC/CinemaCritic/assets/144147405/f2af5c3f-61c3-430d-8238-d47d1604476e)

The user has the ability to click on a movie poster (this is indicated with a changed pointer and an outline). After clicking on the specific movie card the user is redirected to a detailed view of the movie with more information such as: title, description, release year, age rating, length, trailer and the reviews that were written for this movie by other users.
![detailed_view](https://github.com/KajusC/CinemaCritic/assets/144147405/3ff7a80d-f903-4982-b4f8-44b44c284288)

If the user wishes to logout from the website the navigation bar has an option to do so by clicking the "Log Out" button.

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
* [Gavin Lon's video on Blazor](https://www.youtube.com/watch?v=sHuuo9L3e5c&ab_channel=freeCodeCamp.org)

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
