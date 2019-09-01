# Certificates used for token validation

It’s possible to create self-signed certificates for testing this out with the **makecert** and **pvk2pfx** command line tools (which should be on the path in a Visual Studio Developer Command prompt).

    makecert -n "CN=AuthSample" -a sha256 -sv IdentityServer4Auth.pvk -r IdentityServer4Auth.cer

This will create a new self-signed test certificate with its public key in `IdentityServer4Auth.cer` and it’s private key in `IdentityServer4Auth.pvk`.

    pvk2pfx -pvk IdentityServer4Auth.pvk -spc IdentityServer4Auth.cer -pfx IdentityServer4Auth.pfx

If pvk2fx is not found, it can be downloaded from [here](https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk)

This will combine the pvk and cer files into a single pfx file containing both the public and private keys for the certificate. Our app will use the private key from the pfx to sign tokens. Make sure to protect this file. The .cer file can be shared with other services for the purpose of signature validation.