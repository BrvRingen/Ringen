http://test.rdb.ringen-nrw.de/index.php


- TMA ~ Turniermanager (das ist die Lösung mit der man die Turnierabwicklung umsetzen kann
- CMS ~ darin findet ihr u.U. auch den Ergebnisdienst und all das Zeuch, was auch bei Euch läuft  (falls ihr immer schon mal wissen wolltet, das die RDB eigentlich so alles umfasst und (wie wenig) ihr davon nutzt ...
- SETUP ~ Zugriff auf das Core CMS (d.h. ihr seht hier eine Native Installation ohne Wordpress, Joomla und ähnliches Zeuch)




## Zur Api:  
•	ihr braucht immer eine BASE url, das ist eine Url mit dem ihr den Controller der RDB (Komponente) erreicht.  
Das ist bei Joomla über "view" parameter mit "com_rdb" oder so gelöst. Bei dem test System ist das der Service (sv) Parameter.  
D.h. diese Url ist als Basis innerhalb Eurer Konfiguration iwie zu erfassen.
http://test.rdb.ringen-nrw.de/index.php?sv=json 

Übrigens: gut gepflegte RDB Installation stellen einen sog. "ICS"ervice  bereit, welcher euch diese Url auch ausliefert. D.h. eigentlich braucht ihr nur die Domäne und fragt da nach 'ics:jr'.  Aber das für später, weil ist halt nicht gut gepflegt 😉🤷‍♂‍ 

Hinweise: 
- wird der Url der Parameter 'jcb' übergeben, dann wird ein JsonP Notation im Response übergeben. (für ältere Browser erforderlich) 
- wird der Url der Parameter 'rpcid' übergeben, dann ist im Response dieser Parameter enthalten (für ältere Browser erforderlich) 

- Um eine RDB Funktion anzusprechen gibt es 3 Query Parameter die man zu der Base Url setzen kann (oder muss): Diese 3 Parameter sind bei allen RDB Modulen (bzw. WBW Komponenten) gleich:  
  - tk ~ task
  - op ~ operation 
  - xo ~ xtended operation 
 
- tk ist mandatorisch, wenn nicht gesetzt wird dieser aus dem Navigationskontext gewonnen.  Ist das nicht machbar gibt es eine schöne "hau ab" Anzeige.
- Für den Json-Reader Service braucht ihr die Task tk=jr:cs und zur Steuerung der gewünschten Werte die Oeration 'op'.  Hinweis: Ich werde Euch auch einen jr:om Jreader noch bereitstellen (Damit ihr an die Pass Daten kommt -- den gibt es noch nicht auf dem Test Server) 
- fehlt 'op', dann ist das System so nett und gibt Euch eine HIlfe: zu geben: Eigentlich gibt es z.Z: nur die folgenden Operationen: ls, gs, gso, ll, gt, lc und gc
- Die Operationen brauchen z.T: Parameter, damit diese sinnvoll arbeiten können. Fehlen die Parameter, dann macht der Jreader einfach Annahmen, die meistens falsch sind. Die gemachten Annahmen werden aber im Response Objekt zurück geschickt.  
  - sid - saison, 
  - lid - Liga, 
  - rid - Tabelle (Früher mal "Runde", war aber Käse)
  - cid - Competition / Mannschaftskampf
Dazu gibt es noch einen Parameter flags, mit dem man die Menge der gelieferten Daten steuern kann.  ist eine Bitleiste und nicht jedes Bit ist immer mit Sinn belegt.  '63' ist daher eine gute Annahme :-)  



## Frage wie man an ein "Schema" für einen Kampf herankommt?
(=> Schema ist der RDB Begriff für die VORLAGE zum Erstellen der Einzelkämpfe eines Mannschaftskampfes) 
Wenn ja ist das machbar.  Hinweis: das ist aber eine Funktion des 'KAMPFTAGES" einer "TABELLE", wobei ein KAMPFTAG einer "TABELLE" was anderes als als ein "SAISONKAMPFTAG"  
Ich suche Euch gleich mal was raus, was das Darstellt. ist eigentlich ganz einfach, man muss ich nur vom Begriff "KAMPFTAG" lösen und in einem Mapping von "Saisonkampftag" und "Tabellenkampftag" arbeiten.  
