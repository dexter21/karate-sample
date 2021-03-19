# .Net Core + Karate + Docker
This is an example Docker configuration for API (.Net Core) and integration tests (Karate).

## Configuration
Karate requires a JSON file with configuration data (the file is not added to the repository).
It needs to be created in `./integrationtests/environment-config.json`
```
{
    "baseUrl": "http://host.docker.internal:5000",
    "login": "<<LOGIN>>",
    "password": "<<PASSWORD>>"
}
```