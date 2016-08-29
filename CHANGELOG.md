# Changelog

## v 2.0

+ Updated **WebMarkupMin.Core** library to **2.1.0**
    - Added 2 features:
        * `Preserve case` (Default: `False`)
        * `Preserve optional tags list` (Default: empty)	
	- Minification removes the byte order mark (BOM)
	- Fixed errors that occur when processing Angular 2, Aurelia and Polymer templates
	- Improved the safe whitespace minification mode
	- `rb` and `rtc` tags are now considered as optional end tags	
+ Improved compatibility with dev branch
+ Several other minor fixes and code improvements

## v 1.3

+ Updated **WebMarkupMin.Core** library to **1.0.1**
    - Fixed minor bugs
+ Bug fix: Now excludes all resposes that are not of content type `text/html', so dynamic XML should no longer be affected (Fix for issue #1)
+ Bug fix: Localized string for **Settings** in admin menu should no longer break (Fix for issue #2)

## v 1.2

+ **Updated to Orchard 1.9**
+ Added customizable Info Window for administrators that will display minification statistics on pages (see [documentation](https://github.com/JadeX/Orchard.HtmlMinifier/wiki/Statistics-info-window) on how to enable this feature)
+ Updated **WebMarkupMin.Core** library to **0.9.12**
    - Added support for KnockoutJS, Kendo UI MVVM and AngularJS templates minification
    - Added 4 advanced features:
        * `Processable script type list` (Default: empty)
        * `Minify Knockout binding expressions` (Default: `False`)
        * `Minify Angular binding expressions` (Default: `False`)
        * `Custom Angular directive list` (Default: empty)

+ Minification will now be disabled when site runs in **Debug mode**
+ Several other minor fixes and code improvements

## v 1.1

+ Updated WebMarkupMin.Core library to 0.8.21
+ Added option for ignoring urls
