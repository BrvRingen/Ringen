## Bereiche

| Kürzel | Erläuterung                                                  |
| ------ | ------------------------------------------------------------ |
| TMA    | Turniermanager <br />(das ist die Lösung mit der man die Turnierabwicklung umsetzen kann) |
| CMS    | darin findet ihr u.U. auch den Ergebnisdienst und all das Zeuch, was auch bei Euch läuft |
| SETUP  | Zugriff auf das Core CMS<br />(d. h. ihr seht hier eine Native Installation ohne Wordpress, Joomla und ähnliches Zeuch) |



## API

Testserver: http://test.rdb.ringen-nrw.de/index.php

**Hinweis**: Api identifiziert sich jetzt als 1.0.2

Das ist bei Joomla über "view" parameter mit "com_rdb" oder so gelöst. 
Bei dem test System ist das der Service (sv) Parameter.
D. h. diese Url ist als Basis innerhalb Eurer Konfiguration irgendwie zu erfassen.
http://test.rdb.ringen-nrw.de/index.php?sv=json 

Übrigens: gut gepflegte RDB Installation stellen einen sog. "ICS"ervice  bereit, welcher euch diese Url auch ausliefert. D. h. eigentlich braucht ihr nur die Domäne und fragt da nach 'ics:jr'.

| URL Parameter                      |                                                              |
| ---------------------------------- | ------------------------------------------------------------ |
| 'jcb'                              | JsonP Notation im Response. (für ältere Browser erforderlich) |
| 'rpcid'                            | dann ist im Response dieser Parameter enthalten (für ältere Browser erforderlich) |
| tk                                 | task (**mandatorisch**)                                      |
| op                                 | operation<br />Mögliche Operationen: ls, gs, gso, ll, gt, lc, gc |
| xo                                 | xtended operation                                            |
|                                    |                                                              |
| tk=jr:cs                           | Json-Reader Service                                          |
| tk=jr:om                           | Damit ihr an die Pass Daten kommt -- den gibt es noch nicht auf dem Test Server |
|                                    |                                                              |
| sid                                | saison                                                       |
| lid                                | Liga                                                         |
| rid                                | Tabelle (Früher mal "Runde", war aber Käse)                  |
| cid                                | Competition / Mannschaftskampf<br />Dazu gibt es noch einen Parameter flags, mit dem man die Menge der gelieferten Daten steuern kann. ist eine Bitleiste und nicht jedes Bit ist immer mit Sinn belegt.  '63' ist daher eine gute Annahme :-) |
|                                    |                                                              |
| op=gcs<br />"getCompetitionScheme" | das liefert das Schema (oder die Kämpfe, wenn welche da sind) |
| op=gbl<br />"listBoutday"          | Kampftage der Tabelle                                        |
| op=gobl<br />"listOrgBoutday"      | Kampftage der Saison (genauer der Organisation)              |
|                                    |                                                              |

## Ergebnis ünermitteln
Die Felder habe ich wie folgt kategorisiert: 
1.	r-equired:  d.h. wenn diese fehlen dann gibt es ärger und der Upload schlägt fehl oder es wird versucht den Fehler zu kompensieren.
2.	q-euery: Felder die nur konsultiert werden, wenn die r-equired Felder fehlen 
3.	i-gnored: Felder die keine Bedeutung beim Upload haben und daher ignoriert werden
4.	s-et: Diese Felder werden beim Upload durch den Upload gefüllt. 
5.	w-anted: diese Felder werden eigentlich als Nutzdaten in die Datenbank geschrieben.
