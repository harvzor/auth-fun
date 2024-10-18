# AuthFun

You read it - auth is fun.

## About

Using quick start guide from Duende: https://docs.duendesoftware.com/identityserver/v7/quickstarts/

## Projects

### AuthFun.IdentityServer

The OAuth 2.0 compliant server, uses Identity Server.

### AuthFun.Api

An API that needs as "access token" in order to make requests to it.

### AuthFun.ConsoleClient

Authenticates with IdentityServer to get an access token, then makes a request to AuthFun.Api.

### AuthFun.WebClient

Uses AuthFun.IdentityServer as the OIDC client in order to grant user access to certain pages.
