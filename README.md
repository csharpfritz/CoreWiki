# CoreWiki

A simple ASP.NET Core wiki that we are working on during live coding streams. It runs on Windows, Mac, Linux and Container. Core Wiki is an allusion to the Core App. This name was suggested by Shayne Boyer in the stream of the [27th/March](https://www.twitch.tv/videos/243463398). Initially this project is very basic and anyone who is learning ASP.NET Core 2.0 (Razor Pages) can use it to learn..

Jeff Fritz writes code live on video streams, and wants to give you a central place to ask questions, find samples, and links to projects and other materials referenced during the show.

## Watch LIVE

Jeff currently presents on the following services.  Choose the one that works for you:

* [Twitch](https://twitch.tv/csharpfritz)
* [Mixer](https://mixer.com/csharpfritz) - `[Not in use]`

You can find his current schedule on both services, and if you follow or subscribe to the channel you will be notified when the stream begins. 

## Get the Music!

Our friend Carl Franklin from [.NET Rocks](https://dotnetrocks.com) has graciously allowed us to play some of his [Music to Code By](http://mtcb.pwop.com)  during the stream.  Buy the [music](http://mtcb.pwop.com), or get a subscription with the mobile app at www.musictoflowby.com

## Ask Questions

If you want to know more about something or want to see a demo of something specific, you can ask Jeff by [opening an issue](https://github.com/csharpfritz/CoreWiki/issues/new) and adding the 'Question' label. 

The list of [currently outstanding questions](https://github.com/csharpfritz/CoreWiki/issues?q=is%3Aissue+is%3Aopen+label%3Aquestion+sort%3Acreated-asc) is available.  When questions are answered, they are closed and links are added to the wrap-up blog post for the stream they were answered in.

## Guests

I enjoy having guests join me for some pair-programming, because we're always going to learn something new together.

## Jeff's Setup

Jeff has written about how he has the hardware configured as well as the software to produce stream [on his blog](http://jeffreyfritz.com/2017/12/live-streaming-101-my-setup/).  
*  Jeff uses [Posh-Git](https://github.com/dahlbyk/posh-git) to make the Powershell prompt easier to navigate while working with Git repositories 
*  When coding with a guest, Jeff and the guest use [Visual Studio Live Share](https://github.com/MicrosoftDocs/live-share/blob/master/README.md) to work on code on screen at the same time.
*  Jeff uses a bunch of great Visual Studio extensions, and you can find that list on the [WebTools repository](https://github.com/csharpfritz/Ignite2017-WebTools). 

## Watch recordings

Archive of all shows from the stream can be found on [Jeff's YouTube Fritz and Friends](https://www.youtube.com/playlist?list=PLVMqA0_8O85zHkvIMHgG74eskQTO5nfWy)  playlist. To facilitate we're putting down the last streams:

Date | Topic
----|---
March 27, 2018 | [From DevIntersection in Orlando, ASP.NET Core with guest Shayne Boyer](https://www.twitch.tv/videos/243463398)
March 29, 2018 |  [Back in Philly working on our new Wiki project](https://www.twitch.tv/videos/244236019?t=52m50s) - _Start 00:52:50_
April 5, 2018 | [Building a Wiki with ASP.NET Core - Updating to Bootstrap 4](https://www.twitch.tv/videos/246900841)
April 10, 2018 | [Pair-programming with Jon Skeet, Handling Dates and Times](https://www.twitch.tv/videos/248778357)
April 12, 2018 | [Pair-programming with YOU!  Your .NET questions and pull-requests](https://www.twitch.tv/videos/249500947)
April 14, 2018 | [Coding ASP.NET Core: Building a Wiki](https://www.twitch.tv/videos/250258108)
April 19, 2018 | [Chill coding today.. good music, good code, and YOU!](https://www.twitch.tv/videos/252089112)

#  1) Functionality that have already been implemented

**HomePage** - _It's not a page or a feature. This is a default article that is presented as if it were the Home. When the user navigate to /Details, if topicName is not specified, the application redirect to default Article (HomePage)_

## Header Menu
 - [X] Allows the users navigate to the Default article (HomePage). `[Back to Home (CoreWiki)]`
 - [X] Allows the users navigate to LatestChanges articles feature.`[LatestChanges]`
 - [X] Allows the users navigate to Create new article feature. `[Create new article]`
 - [X] Allows the users navigate to All articles feature. `[All]`

## Details
 - [X] Allows the users see details of article. 
 - [X] Allows the users navigate to Edit feature. `[Edit]`
 - [X] Allows the users navigate to the Default Article (HomePage). `[Back to Home]`
 - [X] Allows the users see comments list
 - [X] Allows the users add new comment`[New Comment]`

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
- [X] Allows the users navigate to Details feature. `[Detail]`
- [X] Allows the users navigate to Create new article feature. `[New article]`
- [X] Allows the users navigate to the default article (HomePage).`[Back to Home]`

List All
-----------------------------
- [X] Allows the users see ALL articles. Ordered by Topic.
- [X] Allows the users navigate to Edit feature. `[Edit]`
- [X] Allows the users navigate to Delete feature. `[Delete]`
- [X] Allows the users navigate to Details feature. `[Detail]`
- [X] Allows the users navigate to Create new article feature. `[New article]`
- [X] Allows the users navigate to the default article (HomePage).`[Back to Home]`
- [X] Allows the users navigate to Latest Changes feature. `[Latest Changes]`

NotFound
-----------------------------
* Occurs, always which an page is not found. 

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
- [X] Create Branch (Master, Dev, project_VersionsAndRatings)
- [X] Create New Project
- [X] Add Issues to Project
- [X] Create/Merge/Close Pull Request
- [X] Create/Close Issues
- [X] Add tags to Issues

Lessons - Git Cli
--------
- [X] Commit
- [X] Push
- [X] Clone
- [X] Status
- [X] Checkout
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
  - [X] Create
- [X] Validation Error UI 
- [X] Server-Side Validation
- [ ] Client-Side Validation 
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
- [X] Data Access
  - [X] Pagination

Lessons - Entity Framework Core
--------

- [X] Code First Database 
- [X] Migrations
  - [X] Add-Migration
  - [X] Update-Database
- [X] Seed
- [X] CRUD (Create, Read, Update e Delete)
- [X] Update concurrency exception handling

Lessons - UI
-----------------------------
- [X] Bootstrap 4
  - [X] Cards
