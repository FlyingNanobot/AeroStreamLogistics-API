# Run this script to trust the local development HTTPS certificate for .NET
# Usage: .\trust-devcert.ps1
Write-Host "Trusting dotnet dev-certs (you may be prompted to confirm)..."
dotnet dev-certs https --trust
Write-Host "Done. Restart browsers and Visual Studio if needed."