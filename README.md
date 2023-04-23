# Questionnaire Api

An api to generate questionnaires about diferents kind of topic such as `Enginering`, `Culture`, `health` and many other topics.

This app was developed by modules and with a simple software design approach.

## Get started

For starting this project first no need to have `.net` version `6.*` or `7.*` and Microsoft SQL Server in your machine or Docker.

If you have SQlServer just need to create a database and change the url connection in the project.

Here you can do it.

`appsettings.json` : 
```json

ConnectionStrings: {
    "Default": "Server=localhost,1433;Database=master;User=sa;Password=Questionnaire@ssW0rd!;TrustServerCertificate=True;Encrypt=false;"
},

```
And in Tests dir on Global: `Tests/Global.cs`

```C#
public static class Globals
{
    public static string MyConnection { get; private set; } = 
        "Server=localhost,1433;Database=master;User=sa;Password=Questionnaire@ssW0rd!;TrustServerCertificate=True;Encrypt=false;";
}
```

if you have docker and docker-compose installed just need to do this.

```shell
docker-compose up --build
```

After doing all of this you're ready to start.

Run the commands bellow.


