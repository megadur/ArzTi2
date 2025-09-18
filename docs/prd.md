# Sprint 1 Backlog

## User Stories
- As an ARZ system, I want to retrieve all new prescriptions for a client, so that I can process them for insurance mediation.

## Sprint 1 Tasks
- Add Basic Authentication to all endpoints.
	- Research ASP.NET Core Basic Authentication middleware.
	- Implement Basic Authentication handler.
	- Configure authentication in Program.cs.
	- Add authentication attributes to controllers.
	- Test authentication with valid and invalid credentials.
- Implement multitenancy: resolve ApoTi connection string from ArzSw per client.
	- Design a method to identify the client from each request (e.g., header, token).
	- Implement logic to fetch the connection string from ArzSw based on client ID.
	- Refactor DbContext creation to use the resolved connection string.
	- Add tests for multitenancy logic.
- Implement API endpoint to fetch new prescriptions (TransferArz=false) for all types.
	- Define API route and method signature.
	- Implement controller action to call service/repository.
	- Ensure filtering by TransferArz=false and correct client.
	- Map results to DTOs.
	- Handle paging and large result sets.
- Add error handling and logging for the fetch endpoint.
	- Add try/catch blocks in controller and service.
	- Log errors and important events.
	- Return appropriate HTTP status codes for errors.
- Write unit and integration tests for the fetch endpoint and authentication.
	- Write unit tests for authentication handler.
	- Write unit tests for service and repository fetch logic.
	- Write integration tests for the fetch endpoint (with and without authentication).
- Document the fetch endpoint and authentication usage.
	- Write OpenAPI/Swagger documentation for the fetch endpoint.
	- Document authentication requirements and example requests.
	- Add usage examples to the project README.

## Stretch Goals
- Optimize fetch endpoint for bulk operations and high volume.
- Begin implementation of the mark-as-read endpoint.
---

# Product Backlog

## User Stories
- As an ARZ system, I want to retrieve all new prescriptions for a client, so that I can process them for insurance mediation.
- As an ARZ system, I want to mark prescriptions as read after processing, so that they are not retrieved again.
- As an ARZ system, I want to update the status of prescriptions to ABGERECHNET after calculation, so that the workflow is complete.

## Technical Tasks
- Implement API endpoint to fetch new prescriptions (TransferArz=false) for all types.
- Implement API endpoint to mark prescriptions as read (TransferArz=true).
- Implement API endpoint to update prescription status to ABGERECHNET.
- Implement copayment data transfer logic.
- Implement multitenancy: resolve ApoTi connection string from ArzSw per client.
- Add Basic Authentication to all endpoints.
- Optimize all data access for bulk operations and high volume.
- Add error handling and logging.
- Write unit and integration tests for all endpoints and business logic.
- Document API endpoints and usage.

# Product Requirements Document (PRD)

## 1. Overview
ArzTi2 is a web service for "Apotheken-Rechen-Zentrums" to mediate insurance coverage for prescriptions and medication data. The system supports multiple clients (pharmacies) and handles three types of prescriptions: eMuster16, P-Rezept, and E-Rezept. The main database is ApoTi, and multitenancy is managed via the ArzSw database.

## 2. Goals & Objectives
- Use the given API for retrieving, transferring, and updating prescription data for insurance mediation.
- Support high data volumes (1,000,000+ records) with efficient, scalable operations.
- Ensure correct handling of all prescription types by technical and business keys.
- Enable multitenancy by dynamically resolving client-specific database connections.

## 3. Stakeholders
- Product Owner: ASW
- Developers: BMad
- QA: Bmad
- End Users: Apotheken-Rechen-Zentrums, pharmacies

## 4. User Stories / Use Cases
- As an ARZ system, I want to retrieve all new prescriptions for a client, so that I can process them for insurance mediation.
- As an ARZ system, I want to mark prescriptions as read after processing, so that they are not retrieved again.
- As an ARZ system, I want to update the status of prescriptions to ABGERECHNET after calculation, so that the workflow is complete.

## 5. Functional Requirements
- Retrieve all new prescriptions (TransferArz=false) for all types (eMuster16, P-Rezept, E-Rezept).
- Transfer prescription data for copayment mediation.
- Mark prescriptions as read (TransferArz=true) after data transfer.
- Update prescription status to RezeptStatus=ABGERECHNET after calculation.
- Support multitenancy by resolving the correct ApoTi connection string from ArzSw for each client.

## 6. Non-Functional Requirements
- Performance: Must efficiently handle 1,000,000+ records with bulk operations.
- Security: Ensure data privacy and secure API access.
- Security: Use Basic Authentication
- Scalability: Support multiple clients and high data throughput.
- Reliability: Ensure data consistency and error handling.

## 7. Acceptance Criteria
- API returns only new prescriptions (TransferArz=false) for the requesting client.
- After transfer, prescriptions are marked as read (TransferArz=true).
- After calculation, prescriptions have RezeptStatus=ABGERECHNET.
- System supports multiple clients with correct data isolation.

## 8. Out of Scope
- Direct user interface for pharmacies (API only).
- Manual data entry or editing by end users.

## 9. Open Questions / Risks
- How will authentication and authorization be managed for each client?
- Are there edge cases for prescription status transitions?
- What are the SLAs for data transfer and processing times?

## 10. Appendix / References
- See project-brief.md for detailed background.
- Database schema and API documentation.
