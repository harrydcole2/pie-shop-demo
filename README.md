# pie-shop-demo

This is a C# website aimed at gaining experience with the ASP.NET Core platform, and the different technologies that sit atop it: MVC, Razor, Blazor, and Web API. I have annotated this project with some of the basics of .NET, especially as far as dependency injection and the MVC pattern are concerned. I've worked with C# in the past, but it was great to understand what is going on in the background when you use .NET and I thought the site was really rewarding to build.

You can navigate to the site if it is still live with the following URL: [https://bethanyspieshop-demo.azurewebsites.net/](url) 


**What's included in this demo**
- A pie shop website that supports sorting, searching, viewing, and buying fake pies using components, forms, tagHelpers, and more!
- A heavily annotated program.cs file which establishes additions to the DI container and middleware
- Model-View-Controller endpoints for a series of cshtml views, a few razor pages, and returns of data from an API
- Entity framework classes and migrations for a code-first approach to database management
- Use of ASP.NET Core Identity for scaffolding and to establish SQL tables for authorization and authentication

**What is not included**
- CSS styling native to the app
- Any dependencies that would be found in libman, such as bootstrap
- The actual connection string and appsettings used in my Azure app service
