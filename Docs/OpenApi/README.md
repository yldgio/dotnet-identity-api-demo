# The Open Api Specification

## Open Api Specification

### Building the Open Api Specification

The Open Api Specification is built using the [Redocly CLI](https://www.npmjs.com/package/@redocly/cli).
The source file is `./OpenApi/openapi.yml` and the output file is `./Docs/OpenApi/openapi.yml`.
the specs can be built using the following command:

```bash
cd ./OpenApi
npx @redocly/cli bundle ./openapi.yml -o ../Docs/OpenApi/openapi.yml
```

To navigate the OpenApi Spec with redoc open the following file in a browser:
[redoc.html](redoc.html)
To navigate with Elements
[elements.html](elements.html)