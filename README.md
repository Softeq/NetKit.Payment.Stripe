# Softeq.NetKit.Payment.Stripe

Softeq.NetKit.Payment.Stripe is a RESTful microservice that enables payment management around Stripe payment gateway. 
API is written in ```Asp.Net Core 2.0``` and protected with ```OAuth2``` protocol. 
```Swashbuckle``` is enabled to provide API consumers with the documentation.

### APIs

Service exposes the following APIs:

1. ```Customers``` API to create new Stripe customer or get customer key;
2. ```Cards``` API to manage customer's cards;
3. ```Subscriptions``` API to manage customer's subscriptions;
4. ```Subscription Plans``` API to manage customer's subscription plans;
5. ```Invoices``` API to manage customer's invoices;
6. ```Charge``` API to create or get customer's charges.

### Configuration

Update ```appsettings.json``` by specifying the following settings: 
1. Set MS SQL database connection string in ```ConnectionStrings:DefaultConnection```;
2. Set Stripe Api key in ```Stripe:ApiKey```;
3. Set Stripe Publish key in ```Stripe:PublishKey```;
4. (Optional) Set Azure AppInsights Instrumentatinon key in ```ApplicationInsights:InstrumentationKey```.

## About

This project is maintained by Softeq Development Corp.

We specialize in .NET core applications.

## Contributing

We welcome any contributions.

## License

The Query Utils project is available for free use, as described by the [LICENSE](/LICENSE) (MIT).