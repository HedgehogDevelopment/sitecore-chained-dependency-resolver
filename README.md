Safe Dependency Injection with Sitecore
=======================================

This repo contains sample code for using a chained dependency resolver within Sitecore.
The code is discussed in the following blog post:-


Both MVC and WebApi chained resolvers can be found in the Common.Web project, with a Castle Windsor implementation in the other projects.

The TDS.Master project contains:-
 - Sample MVC layouts/views from my [Sample-Default-Sitecore-MVC repo](https://github.com/SaintSkeeta/Sample-Default-Sitecore-MVC)
 - a Sitecore Controller Rendering with the custom controller that gets an ILogger resolved
 - the Home item where the above are stored.

URLs to check it out
--------------------
There are a couple of URLs that could be directly hit to trigger both the MVC and WebApi custom controllers.

 - / (the Home item that has the renderings on it)
 - /api/sitecore/mycustom/myaction (explicit call to the MVC controller directly)
 - /api/Custom/MyCustomWebApi/MyAction (custom route to the WebApi controller)
 - /api/Custom/MyCustomWebApi/MyActionWithFilter (custom route to the WebApi controller, with a custom Action filter on it)
 


A Note about the Chained Resolvers
----------------------------------
The chained resolvers check for null returning from the inner resolver. We found this to be more performant than having the inner resolver throw and exception, and the chained resolver catching it.
If you would prefer for the inner resolver to throw, you can make the chained resolver adjustments accordingly.


Sean Holmesby
http://www.seanholmesby.com/
[@seanholmesby](http://twitter.com/seanholmesby)