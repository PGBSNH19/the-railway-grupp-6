# Dokumentation

## Planering

Vi utgick länge ifrån mind mapen och klass-korten vi gjorde i början och gjorde justeringar längs med vägen. Vi tog turer vid datorn samt att spionera och diskuterade oss fram till olika lösningar.
Vid starten tänkte vi kolla så att Tågen inte kan kollidera med hjälp av positioner, men tiden räckte inte till för att implementera det.

## Design

* Vi beslutade att skapa objekten utifrån Data-filerna
* Vi valde att använda en "WorldTimer" för aktuell tid i Programmet, denna "WorldTimer" används som angivelse för vart Tågen befinner sig, med hjälp utav TidTabellerna
* Vi beslutade även att ha en tråd var för sig i varje Train objekt som parallelkörs