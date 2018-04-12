# CoreWiki
A simple ASP.NET Core wiki that we are working on during live coding streams. 

**NOTE:** This project is the result of some streams transmitted using [TWITCH](https://www.twitch.tv/csharpfritz) platform. To collaborate and understand the project, see the follows streams:

Date | Topic
----|---
March 27, 2018 | [From DevIntersection in Orlando, ASP.NET Core with guest Shayne Boyer](https://www.twitch.tv/videos/243463398)
March 29, 2018 |  [Back in Philly working on our new Wiki project](https://www.twitch.tv/videos/244236019?t=52m50s) - _Start 00:52:50_
April 5, 2018 | [Building a Wiki with ASP.NET Core - Updating to Bootstrap 4](https://www.twitch.tv/videos/246900841)
April 10, 2018 | [Pair-programming with Jon Skeet, Handling Dates and Times](https://www.twitch.tv/videos/248778357)

#  1) Implemented Feature List

**HomePage** - _It's not a page or a feature. This is a default article that is presented as if it were the Home. When the user navigate to /Details, if topicName is not specified, the application redirect to default Article (HomePage)_

## Header Menu
 - [X] Allows the users navigate to the Default Article (HomePage). `[Back to Home (CoreWiki)]`
 - [X] Allows the users navigate to LatestChanges feature.`[LatestChanges]`
 - [X] Allows the users navigate to Create new article feature. `[Create new article]`

## Details
 - [X] Allows the users see details of article. 
 - [X] Allows the users navigate to Edit feature. `[Edit]`
 - [X] Allows the users navigate to the Default Article (HomePage). `[Back to Home]`

## Create
- [X] Allows the users create a new article. When success, redirect the user to Details Feature. Otherwise, stay in the page and show error message. `[Create]`
- [X] Allows the users to navigate to the default article (HomePage).  `[Back to Home]`

## Edit
- [X] Allows the users change the article (Topic, Published and Content).
- [X] Allows the users navigate to the default article (HomePage).  `[Back to Home]`

## Delete
- [X] Allows the users delete the article (Topic, Published and Content).
- [X] Allows the users navigate to the default article (HomePage).  `[Back to Home]`

LatestChanges
-----------------------------
- [X] Allows the users see the last 10 articles. Ordered by Published Date.
- [X] Allows the users navigate to Edit feature. `[Edit]`
- [X] Allows the users navigate to Delete feature. `[Delete]`
- [ ] Allows the users navigate to Details feature. `[Detail]`
- [X] Allows the users navigate to Create new article feature. `[Create new article]`
- [X] Allows the users navigate to the default article (HomePage).`[Back to Home]`
 
NotFound
-----------------------------
* Occurs, always which default article (HomePage) is not found. To simulate page not found remove the article HomePage.

Error
-----------------------------
* When an exception occur, o user is redirected to error page. `[Not Simulated]`.

#  2) Topics covered in previous streams

Lessons - Visual Studio Live Sharing Extension
--------
- [X] Visual Studio Community x Visual Studio Code
- [X] Visual Studio Community x Visual Studio Community

Lessons - GitHub
--------
- [X] Create New Repository
- [X] Create Branch (Master, Dev)

Lessons - Git Cli
--------
- [X] Commit
- [X] Push
- [X] Clone
- `[Others ...]`

Lessons - Nuget
--------
- [X] Install Packages 

Lessons - DOTNET Cli
--------
- [X] Dotnet new globaljson --sdk-version 2.0.2

Lessons - ASP.NET Core
--------
- [X] Navigation
- [X] Razor Pages
  - [X] BindProperty
  - [X] RedirectPage
- [X] Routing
  - [X] Customization
  - [X] Constraint
- [X] Dependency Injection
- [X] Tag Helpers
  - [X] Install and Use External
- [X] Validation Error UI 
- [X] Server-Side Validation 
- [X] Configure Minification
- [X] Configure Bundling
- [X] Data Model
  - [X] Add Data Model to a Razor Pages
  - [X] Add Database connection string
  - [X] Register the database context
  - [X] Add database context class
  - [X] Scaffold the Model
  - [X] DataType Attributes
  - [X] ModelState Validation

Lessons - Entity Framework Core
--------

- [X] Code First Database 
- [X] Migrations
- [ ] Seed
- [X] CRUD (Create, Read, Update e Delete)
- [X] Update concurrency exception handling

Lessons - UI
-----------------------------
- [X] Bootstrap 4
  - [X] Cards
