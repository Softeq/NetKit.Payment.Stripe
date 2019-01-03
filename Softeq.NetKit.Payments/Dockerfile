FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
COPY Softeq.NetKit.Payments/Softeq.NetKit.Payments.csproj Softeq.NetKit.Payments/
RUN dotnet restore Softeq.NetKit.Payments/Softeq.NetKit.Payments.csproj
COPY . .
WORKDIR /Softeq.NetKit.Payments
RUN dotnet build Softeq.NetKit.Payments.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish Softeq.NetKit.Payments.csproj -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Softeq.NetKit.Payments.dll"]
