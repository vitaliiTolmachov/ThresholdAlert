# ThresholdAlert

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Features

- Create an API calls limit threshold notification for specific user consuming specific host
- Email send to user email (ser for nov in the DB, but the ser control endpoint will be added later)

## SetUp
1. Pull the code and build it
2. Set connection string to your database in dbSettings.json
3. Set DataAccess as startup project. Than in Package Management choose DataAccess as Default project and type:

```sh
Update-Database -v
```
4. Select multiple starup projects:
- AlertManagmentAPI (allows you to add a threshold for a particular user and web host it consumes)
- WeatherForecastAPI (simple API to imitate host workload)
- AlertSender (responsible to send email notification)

## License

MIT

**Free Software, Hell Yeah!**
