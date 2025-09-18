Project Brief: ArzTi2 Webservice

- Purpose: Web service for "Apotheken-Rechen-Zentrums" to mediate coverage for prescriptions and medication data.
- Main database: ApoTi (stores prescription and medication data).
- Prescription types:
	- eMuster16: Digital equivalent of German paper-based medication requests (business key: Muster16Id)
	- P-Rezept: Parenterale-Rezepte, mixtures by pharmacists (business key: transaktionsnummer)
	- E-Rezept: Contains encrypted FHIR data (business key: eRezeptId)
- Each prescription has a technical key (UUID) and a business key.
- API requirements:
	1. Retrieve all new prescriptions where TransferArz=false.
	2. Transfer the data for copayment mediation.
	3. Mark prescriptions as read (TransferArz=true) after data transfer.
	4. After calculation, set their status to RezeptStatus=ABGERECHNET.
- Multitenancy: The ArzSw database provides the connection string for each client.