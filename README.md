# GDAX Integration and Trade Simulation
## Brief intro into the layout of the application
Main entry points are:

1. GDAX.FeedReader
Console application which depending on App.Config will poll periodically gdax for trade info and save to database.
2. TradingSimulator
Trading simulator which will pull data from database and run simulations to test chosen strategy.
3. Third party project gdax.netcore which facilitates communication with gdax API.
4. TestProject - runs unit tests.
5. Other projects are dependencies.
