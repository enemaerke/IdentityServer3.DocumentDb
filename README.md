# IdentityServer3.DocumentDb

Persistence layer for IdentityServer3 on Azure DocumentDb

## Typical usage

A sample usage is given in WebHost sample application. Examine: Startup.cs and Factory.cs.

For the moment, ToConnectionSettings() is internal, so you can provide your own mapper from DocumentDbServiceOptions to ConnectionSettings which is rather trivial as you can find in DocumenDBServiceOptions internal ConnectionSettings ToConnectionSettings().
