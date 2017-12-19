<img src="https://www.hhog.com/-/media/PublicImages/Hedgehog/Hedgehog-logo-4color-275x46.jpg" alt="Hedgehog Development" border="0"> 

Safe Dependency Injection with Sitecore
=======================================

This repo contains sample code for using a chained dependency resolver within Sitecore.
The code is discussed in detail in the following blog post:-

[Safe Dependency Injection for MVC and WebApi within Sitecore](http://www.seanholmesby.com/safe-dependency-injection-for-mvc-and-webapi-within-sitecore/)


Both MVC and WebApi chained resolvers can be found in the Common.Web project, with a Castle Windsor implementation in the other projects.

The TDS.Master project contains:-

 - Sample MVC layouts/views from my [Sample-Default-Sitecore-MVC repo](https://github.com/SaintSkeeta/Sample-Default-Sitecore-MVC)
 - a Sitecore Controller Rendering with the custom controller that gets an `ILogger` resolved
 - the Home item where the above are stored.

Nuget Packages and Versions
---------------------------
The versions of the Nuget packages in this project correspond to Sitecore 8.0 Update-5 (rev. 150812). For other Sitecore versions, either update the packages to the correct versions, or view other branches on this repository (if available).

The `SitecoreKernel` package is referenced here, but will need to be restored from your own local repository (as we cannot share Sitecore DLLs).
This package was generated with an older, preferred version of the [Sitecore Nuget Package Generator](https://bitbucket.org/sitecoresupport/sitecore-nuget-packages-generator/wiki/Home), which can be found here:-

[https://bitbucket.org/seanholmesby/sitecore-nuget-packages-generator/](https://bitbucket.org/seanholmesby/sitecore-nuget-packages-generator/)

URLs to test with
-----------------
There are a couple of URLs that could be directly hit to trigger both the MVC and WebApi custom controllers.

 - / (the Home item that has the renderings on it)
 - /api/sitecore/mycustom/myaction (explicit call to the MVC controller directly)
 - /api/Custom/MyCustomWebApi/MyAction (custom route to the WebApi controller)
 - /api/Custom/MyCustomWebApi/MyActionWithFilter (custom route to the WebApi controller, with a custom Action filter on it)
 


A Note about the Chained Resolvers
----------------------------------
The chained resolvers check for null returning from the inner resolver. We found this to be more performant than having the inner resolver throw and exception, and the chained resolver catching it.
If you would prefer for the inner resolver to throw, you can make the chained resolver adjustments accordingly.

Thanks to Charlie Turano and Petko Kamburov for their help with this implementation.

Sean Holmesby
[http://www.seanholmesby.com/](http://www.seanholmesby.com/)
[@seanholmesby](http://twitter.com/seanholmesby)