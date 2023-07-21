# HRworks API

C# Client für die HRworks API: [API von HRworks](https://www.hrworks.de/produkt/api/) / [Schnittstellenbeschreibung (V2)](https://developers.hrworks.de/2.0/general-information).

Folgende Funktionen sind verfügbar:

**Utility Endpoints**
- CreateToken (/v2/authentication)

**General Endpoints**
- Organization units (/v2/organization-units)
- Single organization unit (/v2/organization-units/{number})
- Currently present persons (/v2/organization-units/{number}/present-persons)
- Permanent establishments (/v2/permanent-establishments)
- Single permanent establishment (/v2/permanent-establishments/{id})
- Holidays (/v2/holidays)

**Absence Endpoints**
- Absences (/v2/absences)
- Absences types (/v2/absences/absence-types)
- Sick leaves (/v2/sick-leaves)
- Sick leaves types (/v2/sick-leaves/sick-leave-types)

**Persons Endpoints**
- Basic person data (/v2/persons)
- Working hour budgets (/v2/persons/available-working-hours)
- Full person data for multiple employees (/v2/persons/master-data)

## Lizenz

Der Quellcode der Seite steht unter der MIT-Lizenz, die Sie in der Datei
der Datei LICENSE.txt finden.
