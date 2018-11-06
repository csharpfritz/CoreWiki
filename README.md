# CoreWiki

A simple ASP.NET Core wiki that we are working on during live coding streams. It runs on Windows, Mac, Linux and Container. Core Wiki is an allusion to the Core App. This name was suggested by Shayne Boyer in the stream of the [27th/March](https://youtu.be/aXkeJmlPDI4). Initially this project is very basic and anyone who is learning ASP.NET Core 2.0 (Razor Pages) can use it to learn.

To learn more about Jeff's stream check his [Live Stream Repository](https://github.com/csharpfritz/Fritz.LiveStream).

### Table of contents
 - [Getting Started](#getting-started)
   - [Live Demo](#live-demo)
   - [Local Development](#develop)
   - [Deploy own instance](#deploy)
   - [FAQ](#faq)
     - [How do I get rid of the 'default administrator enabled' warning?](#default-admin-warning)
 - [Features](#features)
 - [Continuous Integration with Azure Pipelines](#continuous-integration)
 - [Streams Archive](#archive)

## <a id="getting-started">Getting Started</a>

### <a id="live-demo">Live Demo</a>
Explore a CoreWiki live demo at https://corewiki.info/

### <a id="develop">Local Development</a>
> _Note: You must have [nodejs](https://nodejs.org) with npm, and [.NET Core](https://www.microsoft.com/net/download) installed_

To run the latest version of CoreWiki on your local dev machine, open your favorite terminal on an operating system of your choice, and execute the following:

```bash
git clone https://github.com/csharpfritz/CoreWiki.git
cd CoreWiki\CoreWiki
npm install
dotnet run
```

:bulb: Tips: you can also use the watch command, it will rebuild CoreWiki when you do any code change `dotnet watch run`

### <a id="deploy">Deploy own instance</a>
Fastest way to ship your own instance is to try our Deploy to Azure button, but you can deploy to a cloud provider of your choice.

[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

### FAQ

#### I want to contribute:

**@csharpfritz** is inviting for new and old to learn together with team stream, and make a pull request.
A more descriptive contributing guide is written **here:** [contributing](contributing.md)
- To find task that has been discussed, search in the issues for **`help-wanted`** [here](https://github.com/csharpfritz/CoreWiki/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22)
- If you see **TODO** in code, or see small improvements in current functionality, you are also welcome to make a pull-request
- If you want to try adding a new feature, please open an issue so **@csharpfritz** can review the idea

#### <a id="default-admin-warning">How do I get rid of the 'default administrator enabled' warning?</a>
To create a new administrator and get rid of this warning, you must do the following:
1. Register a new account (it won't have administrator rights for obvious reasons).
2. Sign in as the default administrator (username: admin@corewiki.com, password: Admin@123), and go to the User Admin page from the main menu up top.
3. Scroll down, find the user you just registered, and give them the administrator role.
4. From the main menu again, click the email address near the Logout button to open the profile manager.
5. Click on the Personal Data sub-menu, and delete this default admin account.

## <a id="features">Features</a>
**HomePage** - _It's not a page or a feature. This is a default article that is presented as if it were the Home. When the user navigate to /Details, if topicName is not specified, the application redirect to default Article (HomePage)_

### Header Menu
 - [X] Allows the users navigate to the Default article (HomePage). `[Back to Home (CoreWiki)]`
 - [X] Allows the users navigate to LatestChanges articles feature.`[LatestChanges]`
 - [X] Allows the users navigate to Create new article feature. `[Create new article]`
 - [X] Allows the users navigate to All articles feature. `[All]`

### Details
 - [X] Allows the users see details of article. 
 - [X] Allows the users navigate to Edit feature. `[Edit]`
 - [X] Allows the users navigate to the Default Article (HomePage). `[Back to Home]`
 - [X] Allows the users see comments list
 - [X] Allows the users add new comment`[New Comment]`

### Create
- [X] Allows the users create a new article. When success, redirect the user to Details Feature. Otherwise, stay in the page and show error message. `[Create]`
- [X] Allows the users to navigate to the default article (HomePage).  `[Back to Home]`

### Edit
- [X] Allows the users change the article (Topic, Published and Content).
- [X] Allows the users navigate to the default article (HomePage).  `[Back to Home]`

### Delete
- [X] Allows the users delete the article (Topic, Published and Content).
- [X] Allows the users navigate to the default article (HomePage).  `[Back to Home]`

### LatestChanges
-----------------------------
- [X] Allows the users see the last 10 articles. Ordered by Published Date.
- [X] Allows the users navigate to Edit feature. `[Edit]`
- [X] Allows the users navigate to Delete feature. `[Delete]`
- [X] Allows the users navigate to Details feature. `[Detail]`
- [X] Allows the users navigate to Create new article feature. `[New article]`
- [X] Allows the users navigate to the default article (HomePage).`[Back to Home]`

### List All
-----------------------------
- [X] Allows the users see ALL articles. Ordered by Topic.
- [X] Allows the users navigate to Edit feature. `[Edit]`
- [X] Allows the users navigate to Delete feature. `[Delete]`
- [X] Allows the users navigate to Details feature. `[Detail]`
- [X] Allows the users navigate to Create new article feature. `[New article]`
- [X] Allows the users navigate to the default article (HomePage).`[Back to Home]`
- [X] Allows the users navigate to Latest Changes feature. `[Latest Changes]`

### Search engine friendly URL's
-----------------------------
`[Description]`

### NotFound
-----------------------------
* Occurs, always which an page is not found. 

### Error
-----------------------------
* When an exception occur, o user is redirected to error page. `[Not Simulated]`.

## <a id="continuous-integration">Continuous Integration with Azure Pipelines</a>

[![Build Status](https://dev.azure.com/FritzAndFriends/CoreWiki/_apis/build/status/CoreWiki-CI)](https://dev.azure.com/FritzAndFriends/CoreWiki/_build/latest?definitionId=4)

CoreWiki is built and tested continuously by Azure Pipelines. Shortly after you submit a pull request you can check the build status notification. All contributions encouraged!

## <a id="archive">Streams Archive</a>

Archive of all shows from the stream can be found on [Jeff's YouTube 'Building the CoreWiki'](https://www.youtube.com/playlist?list=PLVMqA0_8O85yC78I4Xj7z48ES48IQBa7p) playlist.

### Recordings Index

Date | Topic
:---|:---
March 27, 2018 | [From DevIntersection in Orlando, ASP.NET Core with guest Shayne Boyer](https://youtu.be/aXkeJmlPDI4)
March 29, 2018 |  [Back in Philly working on our new Wiki project](https://youtu.be/GrwhbK5KaGM?t=3170) - _Start 00:52:50_
April 5, 2018 | [Building a Wiki with ASP.NET Core - Updating to Bootstrap 4](https://youtu.be/-20TcnVo0Bc?t=1720) - _Start 00:28:40_
April 10, 2018 | [Pair-programming with Jon Skeet, Handling Dates and Times](https://youtu.be/mfsvh_IpGmw)
April 12, 2018 | [Pair-programming with YOU!  Your .NET questions and pull-requests](https://youtu.be/e-OO717psPI?t=1385) - _Start 00:23:05_
April 14, 2018 | [Coding ASP.NET Core: Building a Wiki](https://youtu.be/et8m8BPx8jI)
April 19, 2018 | [Chill coding today.. good music, good code, and YOU!](https://youtu.be/1JamllV1M4s)
May 3, 2018 | [Answering your questions, reviewing pull-requests, and May is for Macs continues!](https://youtu.be/gSncSG6aV3o?t=1375) - _Start 00:22:55_
May 5, 2018 | ASP.NET Core, live interactions with SignalR, and YOUR pull requests - _No Video Available_
May 22, 2018 | [May is for Macs - Reviewing GitHub scoreboard widget and working on our Wiki](https://youtu.be/kszoWHlSz4Q?t=4167) - _Start 01:09:27_
May 24, 2018 | [May is for Macs - Working with Gravatar and Updating to ASP.NET Core 2.1](https://youtu.be/pakUjHUm1Hk)
May 26, 2018 | [May is for Macs - Building an RSS Feed and adding some AI to our Bot](https://youtu.be/w3ccHbhIHdU?t=1127) - _Start 00:18:47_
May 29, 2018 | [May is for Macs - Reviewing Pull Requests and making the realtime web with SignalR](https://youtu.be/dgl1MJFdBbM)
May 31, 2018 | [The end of May is for Macs - Completing our realtime GitHub scoreboard SignalR](https://youtu.be/qEPEsyG5uh8) - _First hour_
June 2, 2018 | [Finishing the GitHub Scoreboard and merging YOUR pull-requests](https://youtu.be/8oNWFroOn8w) - _First hour_
June 7, 2018 | [Dotnet global tools and reviewing pull requests to the CoreWiki project](https://youtu.be/g4iGpL_OftQ)
June 9, 2018 | [DotNet Global Tools - Delivering Sample Code Easier than Ever](https://youtu.be/rqoFg7aCsGc?t=1968) - _Start 00:32:48_
June 12, 2018 | [Upgrading to ASP.NET Core 2.1.1 and finishing our .NET global tool](https://youtu.be/CskBccbe3rs?t=1447) - _Start 00:24:07_
June 14, 2018 | [Monitoring Applications with Isaac Levin](https://youtu.be/m4LW95T7TQE)
June 16, 2018 | [Authorization in C# and ASP.NET Applications, plus YOUR Pull Requests](https://youtu.be/yB79149ksk8?t=920) - _Start 00:15:20_
June 19, 2018 | [Notifications and Razor Pages in ASP.NET Core with Amanda Iverson](https://youtu.be/cSHeFgqH4zc)
June 23, 2018 | [Reviewing Pull Requests and talking about ASP.NET Performance](https://youtu.be/ggpUam985pU)
June 28, 2018 | [Completing historical data storage and reviewing your pull requests](https://youtu.be/modeWmv87K8)
June 30, 2018 | [Reviewing Pull Requests and Discussing Dependency Injection](https://youtu.be/lTK5dZLz0ak) - _Last 20 minutes_
July 3, 2018 | [Building a user-interface to compare wiki article versions](https://youtu.be/BB3v70xFKtU)
July 5, 2018 | [Day after Independence Day, building web pages with ASP.NET Core](https://youtu.be/Xx9UwxzicjE?t=2475) - _Start 00:41:15_
July 7, 2018 | [Refactoring and Pull requests \| C# \| ASP.NET Core](https://youtu.be/ae2N10F6PgA?t=1785) - _Start 00:29:45 for about 15 minutes_
July 10, 2018 | [Refactoring and the Repository Pattern \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=rpOvcRj64c0&t=0s)
July 12, 2018    | [Refactoring to support better unit tests \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=k-y6hCm-isI&t=0s)
July 14, 2018    | [Entity Framework Migrations \| C# \| Entity Framework \| ASP.NET Core](https://www.youtube.com/watch?v=RAkh7Rh6NPU&t=0s)
July 19, 2018    | [Authorization Policies and Security Enforcements \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=TnG8X5RZ-Ps&t=0s)
July 21, 2018    | [Architecture Review and Refactoring \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=ot1yfy8B1WY&t=0s)
July 21, 2018    | [Architecture Review and Refactoring \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=ot1yfy8B1WY&t=0s)	           
July 26, 2018    | [Application Extensibility \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=91YYDaVz1b4&t=0s)
July 27, 2018    | [More Architecture Review and Updates for CoreWiki \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=3pfEsEzLvqo&t=0s)
July 28, 2018    | [Simplifying Search Pages \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=CSyGYfQearM&t=0s)
August 3, 2018   | [ Pull Request Reviews and more Twitter Integration \| JavaScript \| C# \| ASP.NET](https://www.youtube.com/watch?v=1Cer4mZYLGc&t=0s)
August 4, 2018   | [ Writing code, Refactoring DTOs from Entity Framework \| C# \| ASP.NET](https://www.youtube.com/watch?v=zMjNkcs1Iy8&t=0s)
August 7, 2018   | [ Introducing Models to an Application \| C# \| ASP.NET](https://www.youtube.com/watch?v=sN4LM8qIF8w&t=0s)
August 9, 2018   | [ Refactoring to Domain Models in an Application \| C# \| ASP.NET](https://www.youtube.com/watch?v=Qm-fAiJdkj8&t=0s)
August 11, 2018  | [Introducing the CQRS Architecture Pattern with MediatR \| C# \| ASP.NET](https://www.youtube.com/watch?v=53SlSlBaQTM&t=0s)
August 14, 2018  | [More work with the CQRS Architecture Pattern and MediatR \| C# \| ASP.NET](https://www.youtube.com/watch?v=-WGCD1885s4&t=0s)
August 16, 2018  | [Unit Testing the CQRS Architecture Pattern \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=GNNSdL5AGmc&t=0s)
August 17, 2018  | [Building The CoreWiki - New Commands and Queries \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=ZeZxIi8fkPo&t=0s)
August 18, 2018  | [Introducing Automapper to CoreWiki \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=ERjmNxP4TtQ&t=0s)
August 25, 2018  | [Jeff is BACK! Talking about Twitch APIs and software architecture in CoreWiki \| C#](https://www.youtube.com/watch?v=MXt-KMujzWk&t=0s)
August 31, 2018  | [Fixing ASP.NET Core in a Container and YOUR Pull Requests \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=zTCr8E1nIUUt=0s)
September 1, 2018     | [Refactoring and Deploying our CoreWiki Application to Azure \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=tFuwM5e3pAA&t=0s)
September 6, 2018     | [Refactoring and Adding a 'Deploy to Azure' button to CoreWiki](https://www.youtube.com/watch?v=rmE3GTlQDDg&t=0s)
September 7, 2018 | [Adding Postgres to Corewiki and running Wordpress on .NET with PeachPie](https://www.youtube.com/watch?v=XhrG0kUHM_c&t=0s)
September 11, 2018    | [Talking about Real World ASP.NET Core with Javier Lozano \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=p4WpCw-uJWs&t=0s)
September 13, 2018    | [The Legacy of Code with Kathleen Dollard \| C# \| ASP.NET Core](https://www.youtube.com/watch?v=7mS08szgpJM&t=0s)
September 20, 2018	| 	[CoreWiki Meets Azure DevOps \| Azure DevOps \| ASP.NET Core](https://www.youtube.com/watch?v=3OmiwUyWhk4&t=0s)
September 20, 2018	| [CoreWiki Continuous Integration \| Azure DevOps \| ASP.NET Core](https://www.youtube.com/watch?v=KMdw_YnIuFI&t=0s)

#

Series | Topic
----|---
Architecture Workshop 1 of 7 | [Steve Smith shows us Clean Architecture](https://www.youtube.com/watch?v=k8cZUW4MS3I&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
Architecture Workshop 2 of 7 | [Julie Lerman introduces Domain Driven Design](https://www.youtube.com/watch?v=teuaPd8WwB8&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
Architecture Workshop 3 of 7 | [Jimmy Bogard, MediatR and the CQRS pattern](https://www.youtube.com/watch?v=w6_dCa2-Qrg&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
Architecture Workshop 4 of 7 | [Mark Miller talks about the Science of Great User Interfaces](https://www.youtube.com/watch?v=qNDlnYKTuCk&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
Architecture Workshop 5 of 7 | [Miguel Castro Makes our Application More Extensible](https://www.youtube.com/watch?v=jy-ZV7uEm7g&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
Architecture Workshop 6 of 7 | [Cecil Phillip Shows Azure Functions and Serverless Concepts](https://www.youtube.com/watch?v=DG12aX5gDs4&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
Architecture Workshop 7 of 7 | [Steve Lasker Shows Us the How and Why of Containers and Azure](https://www.youtube.com/watch?v=PcLpIW5s0AU&list=PLVMqA0_8O85x-aurj1KphxUeWTeTlYkGM&t=0s)
