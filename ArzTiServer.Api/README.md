# ArzTiServer.Api - ASP.NET Core 6.0 Server

Kommunikation des Genossenschafts-Webservers mit den jeweiligen
Rechenzentren 

Bitte erst den Rezept-Status abfragen, ggf. weiter filtern und dann mit den
UUID die Rezeptdaten holen.

Weitere links:
- [Systembeschreibung](https://wiki.arz.software/de/Systembeschreibung/ArzTi_Verhalten_API)
- [Swagger](./openapi.yaml.txt)
- [History](./history.md)


Einzelne Rezepte sind Listen mit einem Element.

## Upgrade NuGet Packages

NuGet packages get frequently updated.

To upgrade this solution to the latest version of all NuGet packages, use the dotnet-outdated tool.


Install dotnet-outdated tool:

```
dotnet tool install --global dotnet-outdated-tool
```

Upgrade only to new minor versions of packages

```
dotnet outdated --upgrade --version-lock Major
```

Upgrade to all new versions of packages (more likely to include breaking API changes)

```
dotnet outdated --upgrade
```


## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```
