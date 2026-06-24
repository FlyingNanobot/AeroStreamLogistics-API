AeroStreamLogistics‑API
AeroStreamLogistics‑API is the core backend service of the AeroStream Logistics platform — a distributed, event‑driven system for real‑time aircraft and cargo telemetry ingestion, processing, and retrieval.
It exposes REST endpoints consumed by the UI, Worker Service, and Processing Service.

The API is built with ASP.NET Core, integrates with PostgreSQL, Redis, OpenSearch, and streams data through Kafka.

🚀 Features
Real‑time Telemetry API  
Serves aircraft and cargo state data processed by the Processing Service.

Dashboard Endpoints  
Includes DashboardController (visible in your project tree) for UI dashboards and analytics.

Caching Layer  
Uses Redis for fast lookups and reducing load on Postgres/OpenSearch.

Search & Historical Queries  
Integrates with OpenSearch for querying past flight states.

Role‑based Authentication  
Secured via Microsoft Entra ID (Azure AD) using OAuth2/OIDC.

📁 Project Structure
Code
AeroStreamLogistics-API/
│
├── Controllers/
│   └── DashboardController.cs
│
├── Business/            # Domain logic
├── Repository/          # Data access (Postgres, Redis, OpenSearch)
├── Object/              # DTOs and shared models
│
├── appsettings.json
├── appsettings.Development.json
├── Dockerfile
└── Program.cs
(Based on the uploaded Solution Explorer:
“The API project contains Controllers, DashboardController.cs, appsettings.json, Dockerfile, Program.cs…”)

⚙️ Configuration
The API expects the following services to be reachable (from your container dashboard):

Service	Default Host	Port
PostgreSQL	postgres	5432
Redis	redis	6379
OpenSearch	opensearch	9200
Kafka	kafka	9092


Authentication uses Azure AD redirect URIs such as:

Code
https://api.localdev.me/signin-oidc
▶️ Running Locally
Start the infrastructure stack (Kafka, Redis, Postgres, OpenSearch).
(Your uploaded container list shows all of these running.)

Update appsettings.Development.json with correct connection strings.

Run the API:

Code
dotnet run
The API will be available at:

Code
https://api.localdev.me
🐳 Docker
Build and run:

Code
docker build -t aerostream-api .
docker run -p 8080:8080 aerostream-api
☸️ Kubernetes Deployment
The API is deployed via api-deployment in the aerostream namespace.

Ingress routes traffic through:

Code
api.localdev.me
Cluster status (from your logs) shows:

“api-deployment-79db994555-h86kw — Running, 1/1 Ready”

📝 License
Internal project — not licensed for external distribution.