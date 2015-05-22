# Changelog

## v 1.2

- **Updated to Orchard 1.9**
- Added customizable Info Window for administrators that will display minification statistics on pages (see [documentation](https://github.com/JadeX/Orchard.HtmlMinifier/wiki/Statistics-info-window) on how to enable this feature)
- Updated **WebMarkupMin.Core** library to **0.9.12**
    - Added support for KnockoutJS, Kendo UI MVVM and AngularJS templates minification
    - Added 4 advanced features:
        - `Processable script type list` (Default: empty)
        - `Minify Knockout binding expressions` (Default: `False`)
        - `Minify Angular binding expressions` (Default: `False`)
        - `Custom Angular directive list` (Default: empty)

- Minification will now be disabled when site runs in **Debug mode**
- Several bug fixes and code improvements

## v 1.1

- Updated WebMarkupMin.Core library to 0.8.21
- Added option for ignoring urls
