# SPECIFICA DEL GIOCO - KNOWLEDGE

## 1. Panoramica del Gioco

**Titolo:** Knowledge  
**Genere:** RPG / Simulazione / Discovery / Crafting  
**Piattaforma:** PC (Unity 3D)  
**Target:** Giocatori amanti di crafting, RPG e simulazione

Il gioco si basa sulla **scoperta** - il giocatore accompagna l'evoluzione dell'umanità dalla preistoria fino alla conquista dello spazio esplorando, raccogliendo risorse, e scoprendo nuovi oggetti e strumenti attraverso la combinazione creativa di elementi. La conoscenza è il fulcro centrale del gioco.

---

## 2. Meccaniche Principali

### 2.1 Concetto Centrale: La Scoperta

Il gioco ruota attorno al concetto di **Knowledge** (Conoscenza). Ogni oggetto scoperto, ogni combinazione riuscita, ogni tecnologia sbloccata aumenta il patrimonio di conoscenza del giocatore.

- **Punti Conoscenza (KP):** Valore che rappresenta la conoscenza totale accumulata
- **Albero della Conoscenza:** Visualizzazione grafica di ciò che è stato scoperto
- **Progressione legata alla conoscenza:** L'accesso a nuove ere e zone dipende dalle scoperte effettuate

### 2.2 Sistema di Discovery/Crafting

- **Raccolta risorse:** Il giocatore esplora l'ambiente e raccoglie materie prime (legno, pietra, minerali, piante, etc.)
- **Editor di combinazione:** Interfaccia dove il giocatore posiziona e combina oggetti
- **Logica di scoperta:**
  - Combinazioni corrette sbloccano nuovi oggetti/strumenti
  - Alcune combinazioni richiedono conoscenze progressive (non si può scoprire il motore a razzo senza aver prima scoperto il metallo)
  - Il gioco tiene traccia delle scoperte in un "albero tecnologico"
- **Inventario:** Sistema di gestione oggetti con categorie (materie prime, strumenti, oggetti costruiti)

### 2.2 Progressione Temporale (Ere)

1. **Età della Pietra** - Raccogliere, cacciare, sopravvivere
2. **Età del Bronzo** - Metallurgia base, agricoltura
3. **Età del Ferro** - Armi avanzate, commercio
4. **Medioevo** - Castelli, tradizioni
5. **Rinascimento** - Esplorazione, scienza
6. **Era Industriale** - Fabbriche, treni
7. **Era Moderna** - Elettricità, comunicazioni
8. **Era Spaziale** - Razzi, colonizzazione

### 2.3 Sistema di Vita e Condizione Sociale

- **Statistiche del personaggio:**
  - Salute (fisica)
  - Fame/Sete
  - Energia
  - Felicità
  - **Condizione Sociale** (核心): influenza tutte le altre statistiche e le interazioni con NPC

- **Condizione Sociale:** Livello che rappresenta status nella società
  - Determina accesso a zone/negozi/NPC esclusivi
  - Influenza il prezzo degli oggetti
  - Maggiore condizione sociale = più missioni disponibili
  - Può diminuire se il giocatore compie azioni negative

- **Fattori che influenzano la vita:**
  - Abitazione (influisce su recupero energia)
  - Alimentazione (influisce su salute e fame)
  - Relazioni sociali (NPC, famigliari)
  - Lavoro/attività svolta

---

## 3. Struttura del Mondo

### 3.1 Mappa del Mondo

- **World Map:** Mappa globale con diverse regioni
- **Zone di gioco:** Ogni regione rappresenta un'era diversa
- **Transizioni:** Il giocatore può viaggiare tra le ere (normalmente in ordine progressivo)

### 3.2 Ambienti per Era

| Era | Ambiente Principale | Risorse Unique |
|-----|---------------------|----------------|
| Pietra | Foresta, caverna | Ossa, pietra grezza, legno |
| Bronzo | Villaggio, miniera | Rame, stagno, grano |
| Ferro | Città fortificata | Ferro, carbone, bestiame |
| Medievale | Castello, mercato | Lana, spezie, gioielli |
| Rinascimento | Porto, accademia | Vetro, spezie, mappe |
| Industriale | Fabbrica, città | Carbone, acciaio, vapore |
| Moderno | Città, laboratorio | Elettronica, plastica |
| Spaziale | Base spaziale, pianeti | Materiali esotici, isotopi |

---

## 4. Personaggio

### 4.1 Creazione Personaggio

- Nome
- Aspetto fisico (personalizzazione base)
- Background (opzionale, influenza statistiche iniziali)

### 4.2 statistiche Base

- **Forza:** Danno in combattimento, capacità di trasporto
- **Destrezza:** Precisione, velocità
- **Intelligenza:** Efficienza crafting, scoperta combinazioni
- **Carisma:** Relazioni NPC, condizione sociale
- **Vitalità:** Salute massima, recupero

### 4.3 Progressione

- Esperienza da combattimento, crafting, missioni
- Level up: punti da distribuire
- Abilità speciali sbloccabili con progressione

---

## 5. Interazione con l'Ambiente e le Persone

### 5.1 Interazione con l'Ambiente

Il mondo di Knowledge è vivo e reattivo. Il giocatore può interagire con l'ambiente in molteplici modi:

- **Esplorazione attiva:** Ogni ambiente ha segreti, risorse nascoste e punti di interesse
- **Albero/piante:** Raccolta legno, frutti, corteccia; piantumazione e coltivazione
- **Rocce e minerali:** Estrazione, scavare cave
- **Corpi d'acqua:** Nuotare, pescare, raccogliere materiali acquatici
- **Costruzione:** Posizionare strutture, riparazioni, modificare l'ambiente
- **Osservazione:** Studiare fenomeni naturali per guadagnare Knowledge

### 5.2 Interazione con le Persone (NPC)

- **Relazioni:** Sistema di reputazione con fazioni e individui
- **Dialoghi:** Conversazioni con informazioni, missioni, scambi
- **Commercio:** Compravendita con NPC mercanti
- **Missioni:** Quest line principalie secondarie
- **Alleanze e conflitti:** Relazioni politiche tra fazioni
- **Matrimonio/famiglia:** Possibilità di formare una famiglia (ere avanzate)

### 5.3 Sistema di Reputazione

- **Fazioni:** Clan, tribù, regni, corporazioni
- **Livelli di reputazione:** Ostile → Neutrale → Amichevole → Alleato
- **Benefici:** Sconti, accesso a missioni esclusive, tecnologia condivisa
- **Conseguenze:** Azioni negative riducono la reputazione

---

## 6. Sistema di Minacce Naturali

### 6.1 Clima Dinamico

Il meteo influenza il gameplay e può rappresentare una minaccia:

| Clima | Effetti | Pericoli |
|-------|---------|----------|
| Pioggia | Visibilità ridotta, terreno scivoloso | Fulmini, alluvioni |
| Neve | Movimenti lenti, ipotermia | Valanghe, tempeste |
| Vento forte | Difficoltà di movimento | Tornado, caduta oggetti |
| Caldo estremo | Disidratazione, colpi di calore | Incendi, siccità |
| Nebbia | Visibilità molto ridotta | Agguati, smarrimento |
| Tempesta | Visibilità nulla, rumore | Fulmini, grandine |

### 6.2 Eventi Catastrofici

- **Fulmini:** Incendiano alberi e strutture, pericolo per il giocatore all'aperto
- **Frane:** Distruggono strutture, bloccano passaggi, pericolo in zone montane
- **Alluvioni:** Inondano aree, distruggono raccolti, pericolo in prossimità di fiumi
- **Incendi:** Distruggono foreste e strutture, propagazione rapida
- **Terremoti:** Distruggono edifici, creano crepe nel terreno
- **Eruzioni vulcaniche:** Cenere, lava, gas tossici (ere avanzate)

### 6.3 Sistema di Sopravvivenza alle Catastrofi

- **Previsioni:** Alcuni eventi possono essere previsti (nubi, vento)
- **Rifugi:** Costruire ripari per proteggersi
- **Assicurazioni:** Proteggere strutture e possessions
- **Recupero:** Dopo l'evento, pulizia e ricostruzione

---

## 7. Sistema di Biodiversità

### 7.1 Filosofia del Sistema

Il gioco presenta un ecosistema variegato ispirato alla biodiversità terrestre. Gli animali non sono semplici nemici, ma parte integrante dell'ecosistema con comportamenti, habitat e ruoli specifici.

### 7.2 Classificazione Animale per Era

#### Età della Pietra
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Mammut | Tundra, pianura | Gregge, migratore | Carne, ossa, pelliccia |
| Tigre dai denti a sciabola | Caverne, foresta | Predatore, territoriale | Pelle, artigli |
| Megaceronte | Pianura | erbivoro, pacifico | Carne, corna |
| Uccello pterodattilo | Cieli | Predatore aereo | Piume, ossa |
| Lucertola gigante | Foresta, fiume | Acquatico/terrestre | Carne, pelle |
| Rettile Triceratopo | Foresta | erbivoro, difensivo | Carne, corna |
| Bisonte | Pianura | Gregge, migratore | Carne, lana, ossa |
| Orso delle caverne | Caverne | Predatore, ibernazione | Pelle, grasso |

#### Età del Bronzo / Ferro
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Lupo | Foresta | Branchi, caccia | Pelle, artigli |
| Cervo | Foresta, radura | erbivoro, fuggitivo | Carne, corna, pelle |
| Cinghiale | Foresta | Territoriale, aggressive | Carne, zanne |
| Aquila | Montagna, cieli | Predatore aereo | Piume, artigli |
| Serpente | Foresta, rocce | Predatore, velenoso | Veleno, pelle |
| Maiale domestico | Villaggio | Addomesticato | Carne, cuoio |
| Capra domestica | Montagna | Addomesticato | Latte, lana, carne |
| Cavallo | Pianura | Addomesticato, selvatico | Carne, latte, trasporto |

#### Medioevo
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Cavaliere destriero | Battaglia | Addomesticato da combattimento | Trasporto, battaglia |
| Falco | Cieli | Caccia, nobili | Piume, caccia |
| Lupo mannaro | Foresta | Leggenda, aggressivo | Raro, trofeo |
| Grifone | Montagna | Mitologico, territoriale | Piume, artigli |
| Maiale, pecora, bue | Fattoria | Allevamento | Carne, latte, lana, cuoio |
| Gatto | Villaggio | Cacciatore di topi | Compagnia, caccia |
| Cane da caccia | Foresta | Branchi, caccia | Compagnia, caccia |
| Cavaliere | Battaglia | Addomesticato | Combattimento |

#### Rinascimento
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Balena | Mare | Migratore, caccia commerciale | Olio, ambra grigia |
| Dromedario | Deserto | Viaggiatori, trasporto | Latte, trasporto |
| Rinoceronte | Savana | erbivoro, territoriale | Corno, pelle |
| Elefante | Savana, foresta | erbivoro, intelligente | Avorio, trasporto |
| Scimmia | Foresta tropicale | Gregge, intelligente | Pelliccia, studio |
| Pappagallo | Foresta | Colori, compagnia | Piume, compagnia |
| Tartaruga marina | Mare | Lenta, migratrice | Carne, guscio |

#### Era Industriale
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Mucca | Fattoria industriale | Allevamento | Carne, latte, cuoio |
| Pollo | Fattoria | Allevamento | Carne, uova |
| Piccione | Città | Comunicazione | Messaggi |
| Topo | Città, magazzini | Parassita, infestante | (Controllo infestazione) |
| Gabbiano | Costa, città | Onnivoro, opportunista | (Risorse minori) |
| Cavallo | Città, campagna | Trasporto, lavoro | Trasporto, lavoro |
| Bestia da soma | Miniere, fabbrica | Lavoro industriale | Lavoro |

#### Era Moderna
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Vaccaro robotico | Fattoria automatizzata | Lavoro automatizzato | (Tecnologia) |
| Drone esplorativo | Tutti | Sorveglianza, ricerca | (Tecnologia) |
| Animale da laboratorio | Laboratorio | Ricerca scientifica | (Ricerca) |
| Pesce allevato | Acquacoltura | Allevamento | Carne, proteine |
| Animale domestico avanzato | Casa | Compagnia | Felicity |

#### Era Spaziale
| Animale | Habitat | Comportamento | Risorse |
|---------|---------|---------------|---------|
| Creature aliene | Pianeti sconosciuti | Varie, da scoprire | Risorse aliene, studio |
| Cloni biologici | Laboratorio spaziale | Ricerca | Scienza |
| Robot biologici | Navi, stazioni | Manutenzione | Tecnologia |

### 7.3 Comportamenti Animali

- **Ciclo vitale:** Nascita, crescita, riproduzione, morte
- **Dieta:** Erbivori, carnivori, onnivori, detritivori
- **Habitat:** Zone specifiche dove vivono e cacciano
- **Predatori-preda:** Catena alimentare funzionante
- **Stagioni:** Comportamenti che cambiano con le stagioni
- **Riproduzione:** Gli animali possono riprodursi (gestione popolazione)
- **Migrazioni:** Alcuni animali migrano seasonally

### 7.4 Interazione con gli Animali

- **Caccia:** Risorse (carne, pelle, ossa)
- **Addomesticamento:** Alcuni animali possono essere addestrati
- **Allevamento:** Gestione di animali per risorse
- **Studio:** Osservazione per guadagnare Knowledge
- **Compagnia:** Animali domestici che seguono il giocatore

### 7.5 Equilibrio Ecosistemico

- Popolazioni animali si regolano automaticamente
- Sovraccacciare riduce le popolazioni
- Estinzione locale possibile se non gestito
- Biodiversità influisce sull'ambiente (es. pochi predatori = troppo erbivori)

---

## 8. Sistema di Combattimento e Sopravvivenza

### 8.1 Combattimento

- **Tipo:** In tempo reale con elementi tattici
- **Armi:** Spade, archi, armature (dipendenti dalle scoperte)
- **Nemici:** Animali selvatici (per caccia o difesa), banditi, creature

### 8.2 Combattimento vs Animali

- **Caccia attiva:** Il giocatore può cacciare animali per risorse
- **Difesa:** Animali aggressivi attaccano se minacciati o invaso il territorio
- **Tier di difficoltà:** Animali più grandi/predatori sono più difficili da abbattere

### 8.3 Sopravvivenza alle Minacce Naturali

- **Rifugi:** Costruire ripari per proteggersi da meteo e eventi catastrofici
- **Allerta meteo:** Alcuni eventi possono essere previsti
- **Resistenza:** Statistiche del personaggio influenzano la sopravvivenza
- **Kit sopravvivenza:** Oggetti specifici per affrontare pericoli naturali

---

## 9. Interfaccia Utente (UI)

- **HUD:** Salute, energia, fame, condizione sociale
- **Mappa:** Mini-mappa e mappa grande
- **Inventario:** Griglia oggetti con categorie
- **Editor Crafting:** Area di combinazione drag-and-drop
- **Menu Pausa:** Statistiche, opzioni, salvataggio

---

## 10. Tecnologie Unity Richieste

- **Rendering:** Universal Render Pipeline (URP)
- **Fisica:** Unity Physics
- **AI:** NavMesh per NPC
- **UI:** Unity UI (uGUI) o UI Toolkit
- **Audio:** Unity Audio System
- **Salvataggio:** PlayerPrefs o sistema custom JSON

### 10.1 Requisiti di Sistema

| Componente | Minimo | Consigliato |
|------------|--------|-------------|
| **OS** | Windows 10 (64-bit) | Windows 10/11 (64-bit) |
| **Processore** | Intel Core i5 / AMD Ryzen 5 | Intel Core i7 / AMD Ryzen 7 |
| **Memoria RAM** | 4 GB | 8 GB |
| **Memoria Video** | 2 GB | 4 GB |
| **Spazio su disco** | 10 GB | 15 GB |
| **Memoria massima allocabile** | 4 GB | 4 GB |

**Nota:** L'opzione **"Memory Size"** in Unity Player Settings deve essere impostata a **4096 MB (4 GB)** per garantire stabilità su sistemi con quantità di RAM limitata.

---

## 11. Milestone di Sviluppo

1. **Prototype:** Movimentazione personaggio, raccolta risorse base, primo sistema di combinazione, ecosistema animale base
2. **Alpha 1:** Sistema discovery funzionante con 20 combinazioni, sistema climatico base, fauna base per prima era
3. **Alpha 2:** Prima era (Pietra) completa con discovery tree, biodiversità fauna preistorica
4. **Beta 1:** Tutte le ere giocabili, sistema KP completo, fauna per tutte le ere
5. **Beta 2:** Sistema condizione sociale, relazioni NPCs, eventi catastrofici
6. **Release:** Polishing, bug fix, ottimizzazione

---

## 12. Note Aperte

- [ ] Definire lista completa combinazioni (albero tecnologico)
- [ ] Bilanciare sistema di combattimento e caccia
- [ ] Determinare struttura missioni
- [ ] Definire NPCs e quest line
- [ ] Dettagliare economia di gioco
- [ ] Definire algoritmo di equilibrio ecosistemico
- [ ] Dettagliare evento catastrofici e risposta del giocatore
