
--DROP DATABASE if EXISTS VfklDB;
--CREATE DATABASE if not EXISTS Vfkldb;

-- Drop the table if it already exists
DROP TABLE IF EXISTS answers;
DROP TABLE IF EXISTS assessment;
DROP TABLE IF EXISTS userprofile;
DROP TABLE IF EXISTS question;
DROP TABLE IF EXISTS answertype;
DROP TABLE IF EXISTS category;
DROP TABLE IF EXISTS invitations.invitations;

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

-- Create a new table called 'vurderingstype'
CREATE TABLE invitations.assessmenttype(
    assessmenttype_id SERIAL PRIMARY KEY,
    name VARCHAR,
    description VARCHAR
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
