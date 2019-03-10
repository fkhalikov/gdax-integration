# GDAX Integration and Trade Simulation
## Brief intro into the layout of the application
Main entry points are:

1. GDAX.FeedReader - console application which periodically polls gdax for trade info saves it to database.
2. TradingSimulator - trading simulator which pulls data from a database and runs simulations to test chosen strategy.
3. Third party project gdax.netcore which facilitates communication with gdax API.
4. TestProject - runs unit tests.
5. Other projects are dependencies.
