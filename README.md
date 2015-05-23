# Orchard HTML Minifier
Module designed to reduce size of output HTML to bare minimum while maintaining same functionality for your Orchard website.

Size for simple pages gets reduced by 10% (gzipped) or 20% (uncompressed) respectively. As page gets more complex with more shapes and content, reduction ratio grows.

Minification makes full use of Output caching provided by Orchard.OutputCache module to prevent recurring minification for responses that were already recently minified.

**Supported versions:** Orchard 1.9
___

## Installation
### Automatic (Orchard Gallery)
1. Go to {YourOrchardDomain}/Packaging/Gallery/Modules?SearchText=HTML+Minifier and click `install`

### Manual #1
1. Download .nupkg package from releases section
2. Go to Admin Dashboard of your site > Modules > Installed > `Install a module from your computer`
3. Select downloaded .nupkg package and click `install`

### Manual #2
1. Download .zip package from releases section
2. Extract to modules folder
3. Go to Admin Dashboard of your site and enable `HTML Minifier` module

___

## Setup
You will not see difference in page source until you log out or go to settings and disable exclusion of authenticated users.
After successful installation you'll find new entry in your admin menu under Settings > HTML Minification. There you'll find various basic and advanced settings each described with a hint.

## Troubleshooting Notes
- Disabled by default for authenticated users, make sure you are either logged out, or disable exclusion of authenticated users in settings to see minified page source
- Minification won't take place if site runs in debug mode, check your sites web.config (there's also warning strip on module's settings page)
- Minification can under certain conditions affect page rendering, one such known scenario is presence of spaces between HTML elements and more aggresive whitespace minification mode.

___

## Source Compilation
Built using Visual Studio 2013, .NET 4.5.1. Has dependency on 1 NuGet package, make sure this dependency is acquired from the NuGet gallery.
