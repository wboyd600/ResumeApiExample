
Instructions -

1. Install [wkhtmltopdf](https://wkhtmltopdf.org/).

2. Update the `appsettings.Development.json` to point to the `wkhtmltopdf` executable. On macOS, the default path is usually `/usr/local/bin/wkhtmltopdf`. Ensure your `appsettings.Development.json` includes this path:

    ```json
    {
         "wkhtmltopdfPath": "/usr/local/bin/wkhtmltopdf"
    }
    ```
