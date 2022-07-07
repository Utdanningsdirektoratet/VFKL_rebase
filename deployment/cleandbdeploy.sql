-- we will organize our tables under 3 different schemas based on the altinn application. Userprofile is common to both the application
CREATE SCHEMA  invitations;
CREATE SCHEMA  assessment;
CREATE SCHEMA  userprofile;

CREATE TABLE public.assessmenttype(
    assessmenttype_id SERIAL PRIMARY KEY,
    name VARCHAR,
    description VARCHAR
);

INSERT INTO public.assessmenttype(name, description)
VALUES('Allefag','Allefag'),
      ('Engelsk','Engelsk'),
      ('Matte','Matte'),
	  ('Norsk','Norsk');

CREATE TABLE public.languagetype(
    language_id integer NOT NULL PRIMARY KEY,
    language_code VARCHAR NOT NULL,
    language_title varchar NOT NULL
    );
	
INSERT INTO public.languagetype(language_id, language_code, language_title)
VALUES(1044, 'nb-NO', 'Bokmål'),
      (2068,'nn-NO', 'Nynorsk'),
      (1083,'se-NO','Nordsamisk'),
	  (4155,'se-NO','Lulesamisk'),
	  (6203,'se-NO','Sørsamisk');

-- Create a new table called 'category'
CREATE TABLE assessment.category(
    category_id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    description varchar NOT NULL
    );

INSERT INTO assessment.category(name, description)
VALUES('design','design/utforming'),
      ('pedagogisk','Pedagogisk og didaktisk kvalitet'),
      ('læreplanverk','Bruk av læreplanverket'),
      ('føringer','Føringer fra læreplanverket'),
      ('kvalitet','Utforming og tekstlig kvalitet'),
      ('kobling','Kobling til læreplanverket');

-- Create a new table called 'answertype'
CREATE TABLE assessment.answertype(
    answertype_id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    description varchar NOT NULL
);

INSERT INTO assessment.answertype(name, description)
VALUES('heltenig','Helt enig'),
      ('delvisenig','Delvis enig'),
      ('delvisuenig','Delvis uenig'),
      ('heltuenig','Helt uenig'),
      ('ikkeaktuelt','ønsker ikke å svare/ikke aktuelt');

-- Create a new table called 'question'
CREATE TABLE assessment.question(
    question_id INTEGER PRIMARY KEY,
    question VARCHAR NOT NULL,
    question_id_inform varchar(4) NOT NULL,
    category_id_fk INTEGER NOT NULL,
    assessmenttype_id_fk INTEGER NOT NULL,
    CONSTRAINT fk_category
      FOREIGN KEY(category_id_fk) 
	  REFERENCES assessment.category(category_id),
    CONSTRAINT fk_question_assessmenttype 
        FOREIGN KEY (assessmenttype_id_fk)
        REFERENCES public.assessmenttype (assessmenttype_id)
);

INSERT INTO assessment.question(question_id,question, question_id_inform, category_id_fk,assessmenttype_id_fk)
VALUES(1,'Jeg ønsker ikke å svare på denne kategorien','0',1,1),
      (2,'Jeg ønsker ikke å svare på denne kategorien','0',2,1),
      (3,'Jeg ønsker ikke å svare på denne kategorien','0',3,1),
      (4,'Læremiddelet oppleves som engasjerende for elevene i målgruppen','1.1', 1,1),
      (5,'Læremiddelet er oversiktlig og intuitivt å bruke for elever og lærere','1.2', 1,1),
      (6,'Læremiddelet bruker et språk som er tilpasset fagets terminologi og elevene i målgruppen','1.3', 1,1),
      (7,'Læremiddelet har multimodalitet som sammen støtter læring','1.4', 1,1),
      (8,'Læremiddelet støtter lærerens arbeid med tilpasset opplæring','2.1', 2,1),
      (9,'Læremiddelet har rikt og variert innhold','2.2', 2,1),
      (10,'Læremiddelet legger til rette for at elevene skal lære å lære','2.3', 2,1),
      (11,'Læremiddelet støtter elevene i å utvikle kritisk tenkning og etisk bevissthet','2.4', 2,1),
      (12,'Læremiddelet inviterer elevene til utforsking i læringsarbeidet','2.5', 2,1),
      (13,'Læremiddelet legger til rette for samhandling i eller utenfor læremiddelet','2.6', 2,1),
      (14,'Læremiddelet fremmer inkludering og mangfold','3.1', 3,1),
      (15,'Læremiddelet bidrar til at elevene får innsikt i det samiske urfolkets historie, kultur, språk, samfunnsliv og rettigheter','3.2', 3,1),
      (16,'Læremiddelet ivaretar fagets relevans og sentrale verdier','3.3', 3,1),
      (17,'Læremiddelet gir mulighet til å arbeide med kjerneelementene i faget','3.4', 3,1),
      (18,'Læremiddelet legger til rette for å se sammenhenger i opplæringen','3.5', 3,1),
      (19,'Læremiddelet legger til rette for at elevene får utvikle og bruke grunnleggende ferdigheter i faget','3.6', 3,1),
      (20,'Læremiddelet har god progresjon i tråd med kompetansemålene i faget','3.7', 3,1),
      (21,'Læremiddelet støtter elevenes og lærerens arbeid med underveisvurdering','3.8', 3,1),
      (22,'Læremiddelet har et rikt og variert utvalg av autentiske og tilpassede tekster som er relevante for elevenes trinn og utdanningsprogram og som lar seg tilpasse elevenes faglige interesser og mestring','1.1', 4,2),
      (23,'Læremiddelet har et rikt og variert utvalg av narrative tekster i ulike sjangre, inkludert tekster relatert til barn og unges liv, erfaringer og interesser','1.2', 4,2),
      (24,'Læremiddelet har tekster, illustrasjoner og aktiviteter som fremmer interkulturell kompetanse','1.3', 4,2),
      (25,'Læremiddelet avspeiler det flerkulturelle og flerspråklige klasserommet','1.4', 4,2),
      (26,'Læremiddelet inviterer eleven til prøving og utforsking i læringsarbeidet','1.5', 4,2),
      (27,'Læremiddelet behandler tema fra ulike perspektiv, lar elevene fordype seg over tid og utfordrer elevene til å stille spørsmål, diskutere og reflektere','1.6', 4,2),
      (28,'Læremiddelet legger til rette for at elevene skal kunne skape noe nytt med kunnskapen som blir presentert og dermed overføre det de kan og har lært til nye og ukjente situasjoner','1.7', 4,2),
      (29,'Læremiddelet spør etter elevens opplevelser, syn, erfaringer og meninger','1.8', 4,2),
      (30,'Læremiddelet legger til rette for egenvurdering ved å formidle formålet med oppgaver og aktiviteter, og ved å stille konkrete spørsmål og invitere eleven til refleksjon over egen læring','1.9', 4,2),
      (31,'Læremiddelet opplyser om hvilke data det samler om eleven, til hvilke formål, og hvem som har tilgang til disse. Læremiddelet gjør rede for dette på en forståelig måte for elever og foresatte','1.10', 4,2),
      (32,'Læremiddelet bygger på et elev- og læringssyn som samsvarer med verdiene og prinsippene for læring i læreplanverket og i opplæringsloven','1.11', 4,2),
      (33,'Læremiddelet støtter opp under en kritisk tilnærming til tekst, også tekst formidlet av læremidler, og tematiserer hvordan mediet former kommunikasjonen','1.12', 4,2),
      (34,'Læremiddelet åpner for elevenes ulike tolkninger og opplevelser av litterære tekster og andre kulturuttrykk','2.1', 2,2),
      (35,'Læremiddelet fremmer en inkluderende holdning og aksept for ulike varianter av engelsk','2.2', 2,2),
      (36,'Læremiddelet formidler flerspråklighet som ressurs i læringsarbeidet','2.3', 2,2),
      (37,'Læremiddelet legger til rette for kommunikasjon og samhandling mellom elevene','2.4', 2,2),
      (38,'Læremiddelet støtter elevene i å velge ut og bevisst bruke kommunikasjonsstrategier','2.5', 2,2),
      (39,'Læremiddelet knytter arbeid med språkstrukturer til forskjellige tema, sammenhenger og kommunikative formål','2.6', 2,2),
      (40,'Læremiddelet åpner for en undrende og utforskende tilnærming til språklige strukturer','2.7', 2,2),
      (41,'Læremiddelet legger til rette for at elevene får lære språk i autentiske situasjoner','2.8', 2,2),
      (42,'Læremiddelet støtter elevene i utviklingen av et stadig bredere og mer nyansert ordforråd og i å bruke ord de har lært, i kjente og nye situasjoner','2.9', 2,2),
      (43,'Læremiddelet lar elevene reflektere over egen språkbruk og støtter utviklingen deres av språkbevissthet','2.10', 2,2),
      (44,'Læremiddelet inkluderer metatekst om teksttyper og sjangre og viser tekst i prosess','2.11', 2,2),
      (45,'Læremiddelet har tekster som gir gode modeller for elevenes egen tekstproduksjon og åpner for intertekstualitet som ressurs for tolking og tekstskaping','2.12', 2,2),
      (46,'Læremiddelet inviterer til bruk av flere sanser, til å bruke praktisk-estetiske arbeidsmåter og til bruk av fysiske aktiviteter i læringsarbeidet','2.13', 2,2),
      (47,'Læremiddelet legger opp til at eleven bearbeider og videreformidler lærestoff på ulike måter og ved bruk av ulike modaliteter','2.14', 2,2),
      (48,'Læremiddelet har et design som kan appellere til målgruppen','3.1', 5,2),
      (49,'Læremiddelet er oversiktlig og tydelig strukturert, og det er enkelt for læreren og elevene å navigere mellom delene i læremiddelet','3.2', 5,2),
      (50,'Læremiddelet bruker tydelige definisjoner, forklaringer og referanser, til støtte for lærere og foresatte med variert kompetanse i engelskfaget','3.3', 5,2),
      (51,'Læremiddelet er utformet slik at mediets affordanser tas i bruk og ulike modaliteter utfyller hverandre, gir ulike innganger til lærestoffet og bidrar til helhetlige og nyanserte framstillinger av tema','3.4', 5,2),
      (52,'Læremiddelet har en tiltalende design for målgruppen','1.1', 1,3),
      (53,'Læremiddelet har en struktur som gir oversikt og sammenheng i elevenes læring','1.2', 1,3),
      (54,'Læremiddelet har en tydelig og intuitiv meny for navigasjon','1.3', 1,3),
      (55,'Læremiddelet har kort vei fra innlogging til ønsket informasjon/ oppgave','1.4', 1,3),
      (56,'Læremiddelet presenterer data fra elevaktivitetene på en oversiktlig og forståelig måte for læreren','1.5', 1,3),
      (57,'Læremiddelet har informasjon som alle målgrupper kan oppfatte','1.6', 1,3),
      (58,'Læremiddelet har navigeringsfunksjoner som alle brukere kan betjene','1.7', 1,3),
      (59,'Læremiddelet har oppgaver som legger til rette for å bruke ulike kognitive prosesser','2.1', 2,3),
      (60,'Læremiddelet har oppgaver som stiller høye kognitive krav til elevene','2.2', 2,3),
      (61,'Læremiddelet bruker ulike representasjoner og forklarer overgangen mellom dem','2.3', 2,3),
      (62,'Læremiddelet har oppgaver som er egnet for samarbeid og diskusjon','2.4', 2,3),
      (63,'Læremiddelet legger opp til varierte arbeidsmåter','2.5', 2,3),
      (64,'Læremiddelet gir mulighet for å bruke varierte strategier og metoder for problemløsning','2.6', 2,3),
      (65,'Læremiddelet bidrar til at elevene får økt engasjement, skaperglede og utforsking','3.1', 6,3),
      (66,'Læremiddelet bidrar til at elevene får øvelse i kritisk tenkning','3.2', 6,3),
      (67,'Læremiddelet bidrar til at elevene lærer å lære','3.3', 6,3),
      (68,'Læremiddelet har gode eksempler som viser hvordan eleven kan bruke ulike problemløsningsstrategier','3.4', 6,3),
      (69,'Læremiddelet har med problemløsingsoppgaver gjennomgående og integrert i de ulike kunnskapsområdene','3.5', 6,3),
      (70,'Læremiddelet har med ulike praktiske eksempler og oppgaver hvor matematikk anvendes','3.6', 6,3),
      (71,'Læremiddelet synliggjør hvordan vi kan bruke matematisk modellering til å løse et praktisk problem','3.7', 6,3),
      (72,'Læremiddelet synliggjør de bærende ideene i de ulike resonnementene som gjøres','3.8', 6,3),
      (73,'Læremiddelet har oppgaver og utfordringer hvor elevene ikke bare må bruke regler og prosedyrer, men hvor de selv må finne resonnement','3.9', 6,3),
      (74,'Læremiddelet legger opp til at eleven må oversette mellom det matematiske symbolspråket, dagligspråk og mellom ulike representasjoner','3.10', 6,3),
      (75,'Læremiddelet legger opp til at elevene skal utvikle algebraisk tenkning. Det vil si at elevene selv skal kunne generalisere og finne sammenhenger ut fra mønstre og strukturer','3.11', 6,3),
      (76,'Læremiddelet har et variert utvalg av tekster på bokmål og nynorsk, fra norsk, nordisk og internasjonal litteratur fra fortid og nåtid som bidrar til å gi elevene felles referanserammer og historisk og kulturell forståelse','1.1',4,4),
      (77,'Læremiddelet representerer og verdsetter språklig, kulturelt og livssynsmessig mangfold og utnytter variasjonen i elevenes erfaringsbakgrunn','1.2',4,4),
      (78,'Læremiddelet støtter dybdelæring gjennom struktur, sammenheng mellom kunnskapsområder og utforskende oppgaver og aktiviteter','1.3',4,4),
      (79,'Læremiddelet legger til rette for refleksjon over egen læreprosess, og for egenvurdering','1.4',4,4),
      (80,'Læremiddelet lar elevene utforske sammenhengen mellom form, funksjon og innhold i ulike typer tekster og støtter elevens meningsskapende tekstarbeid','1.5',4,4),
      (81,'Læremiddelet legger opp til at elevene får arbeide kreativt, sammenlignende og utforskende med både sakprosa og skjønnlitteratur og både kortere tekster og hele verk','1.6',4,4),
      (82,'Læremiddelet støtter eleven i å ta stilling til tekst på en kunnskapsbasert, nyansert og kritisk måte','1.7',4,4),
      (83,'Læremiddelet legger til rette for god muntlig opplæring der læreren får støtte til å inkludere elevene i åpne samtale- og læringsfellesskap','1.8',4,4),
      (84,'Læremiddelet lar elevene få innta ulike skriveroller i meningsfylte og relevante skriveoppgaver, både individuelt og i fellesskap','1.9',4,4),
      (85,'Læremiddelet legger opp til sammenlignende, varierte og utforskende arbeid med språk der elevene inviteres til å ta i bruk et språk om språket (metaspråk)','1.10',4,4),
      (86,'Læremiddelet gir kunnskap om språksituasjonen i Norge og inviterer til å reflektere over og forstå egen og andres språklige situasjon','1.11',4,4),
      (87,'Læremiddelet gir god støtte i arbeidet med den første lese- og skriveopplæringen med utgangspunkt i elevens forutsetninger','1.12',4,4),
      (88,'Læremiddelet støtter opplæringen i og den videre utviklingen av de grunnleggende ferdighetene lesing og skriving, muntlige- og digitale ferdigheter','1.13',4,4),
      (89,'Læremiddelet bidrar til at elevene utvikler sin digitale dømmekraft, slik at de opptrer etisk og reflektert i kommunikasjon med andre','1.14',4,4),
      (90,'Læremiddelet tilbyr støtte til differensiering og tilpasset opplæring slik at elevene kan arbeide med tekster og oppgaver på ulike nivå','2.1',2,4),
      (91,'Læremiddelet har interessante og relevante innganger til fagstoffet og gir eleven hjelp til å forstå hva som er sentralt stoff i faget','2.2',2,4),
      (92,'Læremiddelet har relevante og varierte oppgaver på ulike nivå som kan løses både individuelt og i samarbeid og ved bruk av ulike modaliteter','2.3',2,4),
      (93,'Læremiddelet har digitale funksjoner som bygger på et læringssyn som er i tråd med verdigrunnlaget i læreplanen, og som presenterer arbeider og resultater fra elevaktivitetene på en oversiktlig og formålstjenlig måte for læreren og eleven selv','2.4',2,4),
      (94,'Digitale læremidler oppfyller krav til personvern og universell utforming','3.1',5,4),
      (95,'Læremiddelet oppfyller kravet til tekster på bokmål og nynorsk i tråd med kravet i opplæringsloven','3.2',5,4),
      (96,'Læremiddelet har en tiltalende og elevorientert utforming som utnytter samspillet mellom tekst, bilde og andre meningsskapende ressurser','3.3',5,4),
      (97,'Læremiddelet har en ryddig og logisk struktur som gir oversikt og sammenheng i framstillingen','3.4',5,4),
      (98,'Læremiddelet bruker et tilgjengelig og eksemplarisk språk som er tilpasset elevgruppen, og som kommuniserer med elevene','3.5',5,4);
      

-- Create a new table called 'userprofile'
CREATE TABLE userprofile.userprofile(
    user_id SERIAL PRIMARY KEY,
    feide_id VARCHAR,
    personnummer INTEGER,
    name varchar
);

-- Create a new table called 'invitations'
CREATE TABLE invitations.invitations
(
    user_id_fk integer NOT NULL,
    gv_id character varying COLLATE pg_catalog."default" NOT NULL,
    frist character varying COLLATE pg_catalog."default",
    laeremiddel character varying COLLATE pg_catalog."default",
    laereplan character varying COLLATE pg_catalog."default",
    mottaker_eposter character varying COLLATE pg_catalog."default",
    opprettet_dato character varying COLLATE pg_catalog."default",
    CONSTRAINT invitations_pkey PRIMARY KEY (gv_id)
);

-- Create a new table called 'assessment'
CREATE TABLE assessment.assessment(
    assessment_id SERIAL PRIMARY KEY,
    user_id_fk INTEGER NOT NULL,
    teachingaid VARCHAR (1000) NOT NULL,
    instance_id VARCHAR NOT NULL,
    groupassessment_id_fk VARCHAR NULL,
    created_datetime TIMESTAMP,
    CONSTRAINT fk_user
      FOREIGN KEY(user_id_fk) 
	  REFERENCES userprofile.userprofile(user_id),
    CONSTRAINT fk_groupid
        FOREIGN KEY(groupassessment_id_fk) 
	    REFERENCES invitations.invitations(gv_id)
);


-- Create a new table called 'answers'
CREATE TABLE assessment.answers(
    id SERIAL PRIMARY KEY,
    assessment_id_fk INTEGER NOT NULL,
    question_id_fk INTEGER NOT NULL,
    answertype_id_fk INTEGER NOT NULL,
    reason VARCHAR,
    CONSTRAINT fk_assessment
      FOREIGN KEY(assessment_id_fk) 
	  REFERENCES assessment.assessment(assessment_id),
    CONSTRAINT fk_question
      FOREIGN KEY(question_id_fk) 
	  REFERENCES assessment.question(question_id),
    CONSTRAINT fk_answertype
      FOREIGN KEY(answertype_id_fk) 
	  REFERENCES assessment.answertype(answertype_id)
);

CREATE TABLE assessment.categorytextresources(
    id SERIAL PRIMARY KEY,
    category_id_fk integer NOT NULL,
    name varchar NOT NULL,
	description varchar,
	language_id_fk integer not null,
CONSTRAINT fk_categoryid
      FOREIGN KEY(category_id_fk) 
	  REFERENCES assessment.category(category_id),
CONSTRAINT fk_language_category
      FOREIGN KEY(language_id_fk) 
	  REFERENCES public.languagetype(language_id)
    );

INSERT INTO assessment.categorytextresources(category_id_fk, name, description, language_id_fk)
VALUES(1,'design','Design/hábmen', 1083),
      (2,'pedagogalaš','Pedagogalaš ja didaktihkalaškvalitehta',1083),
      (3,'oahppoplánabuktos','Oahppoplánabuktos a geavaheapmi',1083),
	  (1,'design','Design/utforming',2068),
	  (2,'pedagogisk','Pedagogisk og didaktisk kvalitet',2068),
	  (3,'læreplanverk','Bruk av læreplanverket',2068),
	  (4,'føringer','Føringar frå læreplanverket',2068),
	  (5,'kvalitet','Utforming og tekstleg kvalitet',2068),
	  (6,'kopling','Kopling til læreplanverket',2068);

	  
CREATE TABLE assessment.answertypetextresources(
    id SERIAL PRIMARY KEY,
    answertype_id_fk integer NOT NULL,
    name varchar NOT NULL,
	description varchar,
	language_id_fk integer not null,
CONSTRAINT fk_answertypeid
      FOREIGN KEY(answertype_id_fk) 
	  REFERENCES assessment.answertype(answertype_id),
CONSTRAINT fk_language_answertype
      FOREIGN KEY(language_id_fk) 
	  REFERENCES public.languagetype(language_id)
    );

INSERT INTO assessment.answertypetextresources(answertype_id_fk, name, description, language_id_fk)
VALUES(1,'heltenig','Helt enig', 1083),
      (2,'delvisenig','Delvis enig',1083),
      (3,'delvisuenig','Delvis uenig',1083),
	  (4,'heltuenig','Helt uenig',1083),
	  (5,'ikkeaktuelt','ønsker ikke å svare/ikke aktuelt',1083)
	  (1,'heltenig','Heilt enig', 2068),
      (2,'delvisenig','Delvis enig',2068),
      (3,'delvisuenig','Delvis uenig',2068),
	  (4,'heltuenig','Heilt uenig',2068),
	  (5,'ikkeaktuelt','Ønsker ikkje å svara/Ikkje aktuelt',2068);
	  		  
CREATE TABLE assessment.questiontextresources(
    text_id SERIAL PRIMARY KEY,
    question_id_fk integer NOT NULL,
	question VARCHAR NOT NULL,
    language_id_fk integer NOT NULL,
    CONSTRAINT fk_language_question
      FOREIGN KEY(language_id_fk) 
	  REFERENCES public.languagetype(language_id)
    );
		  
INSERT INTO assessment.questiontextresources(question_id_fk, question, language_id_fk)
VALUES
      (1,'Jeg ønsker ikke å svare på denne kategorien',1083),
      (1,'Jeg ønsker ikke å svare på denne kategorien',6203),
      (1,'Jeg ønsker ikke å svare på denne kategorien',4155),
	  (1,'Ønsker ikkje å svare på denne kategorien',2068),
      (2,'Jeg ønsker ikke å svare på denne kategorien',1083),
      (2,'Jeg ønsker ikke å svare på denne kategorien',6203),
      (2,'Jeg ønsker ikke å svare på denne kategorien',4155),
	  (2,'Ønsker ikkje å svare på denne kategorien',2068),
      (3,'Jeg ønsker ikke å svare på denne kategorien',1083),
      (3,'Jeg ønsker ikke å svare på denne kategorien',6203),
      (3,'Jeg ønsker ikke å svare på denne kategorien',4155),
	  (3,'Ønsker ikkje å svare på denne kategorien',2068),                   
      (4,'Oahpponeavvu lea ulbmiljoavkku ohppiid mielas miellagiddevaš',1083),
	  (4,'Læremiddelet blir opplevd som engasjerande for elevane i målgruppa.',2068),
      (5,'Oahpponeavvu lea čorgat ja áddehahtti geavahit ohppiide ja oahpaheddjiide', 1083),
	  (5,'Læremiddelet er oversiktleg og intuitivt å bruke for elevar og lærarar.', 2068),
      (6,'Oahpponeavvus lea giella mii lea heivehuvvon fága terminologiijai ja ulbmiljoavkku ohppiide',1083),
	  (6,'Læremiddelet bruker eit språk som er tilpassa terminologien i faget og tilpassa elevane i målgruppa.',2068),
	  (7,'Oahpponeavvus lea multimodalitehta mii ovttas doarju oahppama',1083),
	  (7,'Læremiddelet har multimodalitet som saman støttar læring.',2068),
	  (8,'Oahpponeavvu doarju oahpaheddjiid barggu heivehuvvon oahpahusain',1083),
	  (8,'Læremiddelet støttar arbeidet til læraren med tilpassa opplæring.',2068),
	  (9,'Oahpponeavvus lea viiddes ja máŋggabealat sisdoallu',1083),
	  (9,'Læremiddelet har rikt og variert innhald.',2068),
	  (10,'Oahpponeavvu láhčá nu, ahte oahppit galget oahppat',1083),
	  (10,'Læremiddelet legg til rette for at elevane skal lære å lære.',2068),
	  (11,'Oahpponeavvu doarju ohppiid ovdánahttit kritihkalaš jurddašeami ja etihkalaš dihtomielalašvuođa', 1083),
	  (11,'Læremiddelet støttar elevane i å utvikle kritisk tenking og etisk medvit.', 2068),
	  (12,'Oahpponeavvu hástá ohppiid suokkardit oahpadettiin',1083),
	  (12,'Læremiddelet inviterer elevane til utforsking i læringsarbeidet.',2068),
	  (13,'Oahpponeavvu láhčá ovttasdoaibmamii oahpponeavvus dahje dan olggobealde',1083),
	  (13,'Læremiddelet legg til rette for samhandling i eller utanfor læremiddelet.',2068),
	  (14,'Oahpponeavvu ovddida searvadahttima ja girjáivuođa',1083),
	  (14,'Læremiddelet fremjar inkludering og mangfald.',2068),
	  (15,'Oahpponeavvu veahkeha ohppiid áddet sámi historjjá, kultuvrra, giela, servodateallima ja vuoigatvuođaid',1083),
	  (15,'Læremiddelet bidreg til at elevane får innsikt i historia, kulturen, språket, samfunnslivet og rettane til det samiske urfolket.',2068),
	  (16,'Oahpponeavvu fuolaha fága relevánssa ja guovddáš árvvuid',1083),
	  (16,'Læremiddelet tek vare på fagrelevans og sentrale verdiar.',2068),
	  (17,'Oahpponeavvu addá vejolašvuođa bargat fága guovddášelemeanttaiguin',1083),
	  (17,'Læremiddelet gir moglegheit til å arbeide med kjerneelementa i faget.',2068),
	  (18,'Oahpponeavvu láhčá oaidnit oktavuođaid oahpahusas',1083),
	  (18,'Læremiddelet legg til rette for å sjå samanhengar i opplæringa.',2068),
	  (19,'Oahpponeavvu láhčá nu, ahte oahppit besset ovdánahttit ja geavahit vuođđogálggaid fágas',1083),
	  (19,'Læremiddelet legg til rette for at elevane får utvikle og bruke grunnleggjande ferdigheiter i faget.',2068),
	  (20,'Oahpponeavvus lea buorre progrešuvdna fága gealbomihttomeriid olis',1083),
	  (20,'Læremiddelet har god progresjon i tråd med kompetansemåla i faget.',2068),
	  (21,'Oahpponeavvu doarju ohppiid ja oahpaheddjiid dađistaga árvvoštallama bargguin.',1083),
	  (21,'Læremiddelet støttar arbeidet med undervegsvurdering for elevane og læraren.',2068),
	  (22,'Læremiddelet har eit rikt og variert utval av autentiske og tilpassa tekstar som er relevante for årstrinn og utdanningsprogram, og som lar seg tilpasse faglege interesser og meistring.',2068),
	  (23,'Læremiddelet har eit rikt og variert utval av narrative tekstar i ulike sjangrar, inkludert tekstar som er relaterte til barn og unges liv, erfaringar og interesser.',2068),
	  (24,'Læremiddelet har tekstar, illustrasjonar og aktivitetar som fremmar interkulturell kompetanse.',2068),
	  (25,'Læremiddelet speglar det fleirkulturelle og fleirspråklege klasserommet.',2068),
	  (26,'Læremiddelet inviterer eleven til prøving og utforsking i læringsarbeidet.',2068),
	  (27,'Læremiddelet behandlar tema frå ulike perspektiv, lar elevane fordjupe seg over tid og utfordrar elevane til å stille spørsmål, diskutere og reflektere.',2068),
	  (28,'Læremiddelet legg til rette for at elevane skal kunne skape noko nytt med kunnskapen som blir presentert, og dermed overføre det dei kan og har lært, til nye og ukjende situasjonar.',2068),
	  (29,'Læremiddelet spør etter eleven sine opplevingar, syn, erfaringar og meiningar.',2068),
	  (30,'Læremiddelet legg til rette for eigenvurdering ved å formidle formålet med oppgåver og aktivitetar og ved å stille konkrete spørsmål og invitere eleven til refleksjon over eiga læring.',2068),
	  (31,'Læremiddelet opplyser om kva data det samlar om eleven, til kva formål, og kven som har tilgang til dei. Læremiddelet gjer greie for dette på ein forståeleg måte for elevar og føresette.',2068),
	  (32,'Læremiddelet bygger på eit elev- og læringssyn som samsvarer med verdiane og prinsippa for læring i læreplanverket og i opplæringslova.',2068),
	  (33,'Læremiddelet støttar opp under ei kritisk tilnærming til tekst, også tekst som er formidla av læremiddel, og tematiserer korleis mediet formar kommunikasjonen.',2068),
	  (34,'Læremiddelet opnar for ulike tolkingar og opplevingar frå elevane si side av litterære tekstar og andre kulturuttrykk.',2068),
	  (35,'Læremiddelet fremmar ei inkluderande haldning og aksept for ulike variantar av engelsk.',2068),
	  (36,'Læremiddelet formidlar fleirspråklegheit som ressurs i læringsarbeidet.',2068),
	  (37,'Læremiddelet legg til rette for kommunikasjon og samhandling mellom elevane.',2068),
	  (38,'Læremiddelet støttar elevane i å velje ut og bevisst bruke kommunikasjonsstrategiar.',2068),
	  (39,'Læremiddelet knyter arbeid med språkstrukturar til ulike tema, samanhengar og kommunikative formål.',2068),
	  (40,'Læremiddelet opnar for ei undrande og utforskande tilnærming til språklege strukturar.',2068),
	  (41,'Læremiddelet legg til rette for at elevane får lære språk i autentiske situasjonar.',2068),
	  (42,'Læremiddelet støttar elevane i utviklinga av eit stadig vidare og meir nyansert ordforråd, og i å bruke ord dei har lært, i kjende og nye situasjonar.',2068),
	  (43,'Læremiddelet lar elevane reflektere over eigen språkbruk og støttar utviklinga deira av språkbevisstheit.',2068),
	  (44,'Læremiddelet inkluderer metatekst om teksttypar og sjangrar og viser tekst i prosess.',2068),
	  (45,'Læremiddelet har tekstar som gir gode modellar for elevane sin eigen tekstproduksjon og opnar for intertekstualitet som ressurs for tolking og tekstskaping.',2068),
	  (46,'Læremiddelet inviterer til bruk av fleire sansar, til å bruke praktisk-estetiske arbeidsmåtar og til bruk av fysiske aktivitetar i læringsarbeidet.',2068),
	  (47,'Læremiddelet legg opp til at eleven bearbeider og vidareformidlar lærestoff på ulike måtar og ved bruk av ulike modalitetar.',2068),
	  (48,'Læremiddelet har eit design som kan appellere til målgruppa.',2068),
	  (49,'Læremiddelet er oversiktleg og tydeleg strukturert, og det er enkelt for læraren og elevane å navigere mellom delane i læremiddelet.',2068),
	  (50,'Læremiddelet bruker tydelege definisjonar, forklaringer og referansar, til støtte for lærarar og føresette med variert kompetanse i engelskfaget.',2068),
	  (51,'Læremiddelet er utforma slik at affordansane til mediet blir tatt i bruk og ulike modalitetar utfyller kvarandre, gir ulike inngangar til lærestoffet og bidrar til heilskaplege og nyanserte framstillingar av tema.',2068),
	  (52,'Læremiddelet har ein tiltalande design for målgruppa',2068),
	  (53,'Læremiddelet strukturerer fagstoffet på ein god måte, slik at elevane kan sjå samanhengar',2068),
	  (54,'Læremiddelet har ein tydeleg og intuitiv meny for navigasjon',2068),
	  (55,'Læremiddelet har kort veg frå innlogginga til informasjonen eller oppgåva du søker etter',2068),
	  (56,'Læremiddelet presenterer data frå elevaktivitetane på ein oversiktleg og forståeleg måte for læraren',2068),
	  (57,'Læremiddelet har informasjon som alle målgrupper kan oppfatte',2068),
	  (58,'Læremiddelet har navigeringsfunksjonar som alle brukarar kan betene',2068),
	  (59,'Læremiddelet har oppgåver som legg til rette for å bruke ulike kognitive prosessar',2068),
	  (60,'Læremiddelet har oppgåver som stiller høge kognitive krav til elevane',2068),
	  (61,'Læremiddelet bruker ulike representasjonar og forklarer overgangen mellom dei',2068),
	  (62,'Læremiddelet har oppgåver som eignar seg for samarbeid og diskusjon',2068),
	  (63,'Læremiddelet legg opp til varierte arbeidsmåtar',2068),
	  (64,'Læremiddelet gjer det mogleg å bruke varierte strategiar',2068),
	  (65,'Læremiddelet bidrar til større engasjement, skaparglede og utforskartrong hos elevane',2068),
	  (66,'Læremiddelet bidrar til at elevane får øve seg i kritisk tenking',2068),
	  (67,'Læremiddelet bidrar til at elevane lærer å lære',2068),
	  (68,'Læremiddelet har gode eksempler som viser hvordan eleven kan bruke ulike problemløsningsstrategier',2068),
	  (69,'Læremiddelet har med problemløysingsoppgåver gjennomgåande og integrerte i dei ulike kunnskapsområda',2068),
	  (70,'Læremiddelet har med ulike praktiske eksempel og oppgåver der matematikk blir brukt',2068),
	  (71,'Læremiddelet synleggjer korleis vi kan bruke matematisk modellering til å løyse eit praktisk problem',2068),
	  (72,'Læremiddelet synleggjer dei berande ideane i dei ulike resonnementa',2068),
	  (73,'Læremiddelet har oppgåver og utfordringar der elevane ikkje berre må bruke reglar og prosedyrar, men sjølve finne resonnement',2068),
	  (74,'Læremiddelet legg opp til at eleven må omsette mellom det matematiske symbolspråket og daglegspråket og mellom ulike representasjonar',2068),
	  (75,'Læremiddelet legg opp til at elevane skal utvikle algebraisk tenking. Det vil seie at elevane sjølve skal kunne generalisere og finne samanhengar ut frå mønster og strukturar',2068),
	  (76,'Læremiddelet har eit variert utval av tekstar på bokmål og nynorsk, frå norsk, nordisk og internasjonal litteratur frå fortid og notid som bidrar til å gi elevane felles referanserammer og historisk og kulturell forståing.',2068),
	  (77,'Læremiddelet representerer og verdset språkleg, kulturelt og livssynsmessig mangfald og utnyttar variasjonen i erfaringsbakgrunnen til elevane.',2068),
	  (78,'Læremiddelet støttar djupnelæring gjennom struktur, samanheng mellom kunnskapsområde og utforskande oppgåver og aktivitetar.',2068),
	  (79,'Læremiddelet legg til rette for refleksjon over eigen læreprosess, og for eigenvurdering.',2068),
	  (80,'Læremiddelet lar elevane utforske samanhengen mellom form, funksjon og innhald i ulike typar tekstar og støttar det meiningsskapande tekstarbeidet til eleven.',2068),
	  (81,'Læremiddelet legg opp til at elevane får arbeide kreativt, samanliknande og utforskande med både sakprosa og skjønnlitteratur og både kortare tekstar og heile verk.',2068),
	  (82,'Læremiddelet støttar eleven i å ta stilling til tekst på ein kunnskapsbasert, nyansert og kritisk måte.',2068),
	  (83,'Læremiddelet legg til rette for god munnleg opplæring der læraren får støtte til å inkludere elevane i opne samtale- og læringsfellesskapar.',2068),
	  (84,'Læremiddelet lar elevane få innta ulike skriveroller i meiningsfylte og relevante skriveoppgåver, både individuelt og i fellesskap.',2068),
	  (85,'Læremiddelet legg opp til samanliknande, varierte og utforskande arbeid med språk der elevane blir inviterte til å ta i bruk eit språk om språket (metaspråk).',2068),
	  (86,'Læremiddelet gir kunnskap om språksituasjonen i Noreg og inviterer til å reflektere over og forstå eigen og andres språklege situasjon.',2068),
	  (87,'Læremiddelet gir god støtte i arbeidet med den første lese- og skriveopplæringa med utgangspunkt i føresetnadene til eleven.',2068),
	  (88,'Læremiddelet støttar opplæringa i og den vidare utviklinga av dei grunnleggande ferdigheitene lesing og skriving, munnlege og digitale ferdigheter.',2068),
	  (89,'Læremiddelet bidrar til at elevane utviklar si digitale dømmekraft, slik at dei opptrer etisk og reflektert i kommunikasjon med andre.',2068),
	  (90,'Læremiddelet tilbyr støtte til differensiering og tilpassa opplæring, slik at elevane kan arbeide med tekstar og oppgåver på ulike nivå.',2068),
	  (91,'Læremiddelet har interessante og relevante inngangar til fagstoffet og gir eleven hjelp til å forstå kva som er sentralt stoff i faget.',2068),
	  (92,'Læremiddelet har relevante og varierte oppgåver på ulike nivå som kan løysast både individuelt og i samarbeid og ved bruk av ulike modalitetar.',2068),
	  (93,'Læremiddelet har digitale funksjonar som bygger på eit læringssyn som er i tråd med verdigrunnlaget i læreplanen, og som presenterer arbeid og resultat frå elevaktivitetane på ein oversiktleg og formålstenleg måte for læraren og eleven sjølv.',2068),
	  (94,'Digitale læremiddel oppfyller krav til personvern og universell utforming.',2068),
	  (95,'Læremiddelet oppfyller kravet til tekstar på bokmål og nynorsk i tråd med kravet i opplæringslova.',2068),
	  (96,'Læremiddelet har ei tiltalande og elevorientert utforming som utnyttar samspelet mellom tekst, bilete og andre meiningsskapande ressursar.',2068),
	  (97,'Læremiddelet har ein ryddig og logisk struktur som gir oversikt og samanheng i framstillinga.',2068),
	  (98,'Læremiddelet bruker eit tilgjengeleg og eksemplarisk språk som er tilpassa elevgruppa, og som kommuniserer med elevane.',2068),
	  (4,'Learohkh ulmiedåehkesne tuhtjieh learoevierhtie lea ïedtjije',6203),
      (5,'Learoevierhtie lea tjyölkehke jïh aelhkie guarkedh jïh nuhtjedh learoehkidie jïh lohkehtæjjide', 6203),
      (6,'Learoevierhtie gïelem nuhtjie mij lea sjïehtedamme faagen terminologijese jïh learoehkidie ulmiedåehkesne',6203),
	  (7,'Learoevierhtien lea multimodaliteete mij ektesne lïeremem dåarjohte',6203),
	  (8,'Learoevierhtie lohkehtæjjan barkoem sjïehtedamme lïerehtimmine dåarjohte',6203),
	  (9,'Learoevierhtien lea ræjhkoes jïh jeereldihkie sisvege',6203),
	  (10,'Learoevierhtie sjïehteladta ihke learohkh edtjieh lïeredh lïeredh',6203),
	  (11,'Learoevierhtie learoehkidie dåarjohte laejhtehks ussjedimmiem jïh etihkeles voerkesvoetem evtiedidh', 6203),
	  (12,'Learoevierhtie learoehkidie böörede liemmebarkosne goerehtidh',6203),
	  (13,'Learoevierhtieh sjïehteladta aktivyöki barkose learoevierhtesne jallh learoevierhtien ålkolen',6203),
	  (14,'Learoevierhtie feerhmemem jïh gellievoetem eevtjie',6203),
	  (15,'Learoevierhtie viehkehte guktie learohkh daajroem åadtjoeh saemien aalkoeåålmegen histovrijen, kultuvren, gïelen, seabradahkejieleden jïh reaktaj bïjre.',6203),
	  (16,'Learoevierhtie faagen relevaansem jïh vihkeles aarvoeh gorrede',6203),
	  (17,'Learoevierhtie nuepiem vadta jarngebiehkiejgujmie faagesne barkedh',6203),
	  (18,'Learoevierhtie sjïehteladta ektiedimmieh vuejnedh lïerehtimmesne',6203),
	  (19,'Learoevierhtie sjïehteladta ihke learohkh åadtjoeh vihkeles tjiehpiesvoeth faagesne evtiedidh jïh nuhtjedh',6203),
	  (20,'Learoevierhtie hijven progresjovnem åtna maahtoeulmiej mietie faagesne',6203),
	  (21,'Learoevierhtie learohki jïh lohkehtæjjan barkoem jaabnan vuarjasjimmine dåarjohte',6203),
	  (4,'Oahpponævvo la dakkir mij båktå berustimev ulmmejuohkusa oahppij gaskan',4155),
      (5,'Álgge l gávnnat majt galggá ja oahpponævvo la intuitijvva oahppijda ja åhpadiddjijda', 4155),
      (6,'Oahpponævvo adná gielav mij la hiebadum fáhka terminologijjaj ja ulmmejuohkusa oahppijda',4155),
	  (7,'Oahpponævon la multimodalitiehtta mij aktan oahppamav doarjju',4155),
	  (8,'Oahpponævvo doarjju åhpadiddje bargov hiebadam åhpadimijn',4155),
	  (9,'Oahpponævon la valjes ja målsudahkes sisadno',4155),
	  (10,'Oahpponævvo dilev láhtjá váj oahppe galggi oahppat oahppat',4155),
	  (11,'Oahpponævvo doarjju oahppijt váj sijá lájttális ájádallam ja etalasj diedulasjvuohta nanniduvvá', 4155),
	  (12,'Oahpponævvo gåhttju oahppijt åtsådit oahppambargon',4155),
	  (13,'Oahpponævvo dilev láhtjá aktisasjbargguj oahpponævon jali dan ålggolin',4155),
	  (14,'Oahpponævvo åvdet sebradahttemav ja moattebelakvuodav',4155),
	  (15,'Oahppopládna la viehkken váj oahppe bessi oahpásmuvvat sáme álggoálmmuga histåvrråj, kultuvrraj, giellaj, sebrudakiellemij ja riektájda',4155),
	  (16,'Oahpponævvo várajda válldá fága relevánsav ja guovdásj árvojt',4155),
	  (17,'Oahpponævon bæssá fága guovdásj elementaj barggat',4155),
	  (18,'Oahpponævo baktu vuojnná tjanástagájt åhpadimen',4155),
	  (19,'Oahppopládna dilev láhtjá váj oahppe bessi adnet ja åvddånahttet fága vuodulasj tjehpudagájt',4155),
	  (20,'Oahpponævon la buorre progresjåvnnå fága máhtudakulmij gáktuj',4155),
	  (21,'Oahpponævvo doarjju oahppe ja åhpadiddje bargov åhpadattijn árvustallamijn',4155);