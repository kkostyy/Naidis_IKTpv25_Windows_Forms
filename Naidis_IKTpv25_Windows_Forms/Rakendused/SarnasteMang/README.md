# Sarnaste piltide leidmise mäng (Matching Game)

## Eesmärk ja kirjeldus

Klassikaline "Memory"-tüüpi mäng: 4x4 ruudustik (16 nuppu) peidab endas 8 sümbolipaari.
Kasutaja klõpsab kahel nupul korraga:

- kui sümbolid ühtivad, jäävad kaardid nähtavaks (roheline taust) ja need eemaldatakse mängust,
- kui sümbolid ei ühti, peidetakse mõlemad kaardid lühikese viivituse järel uuesti.

Mäng lõpeb, kui kõik 8 paari on leitud.

Rakendus järgib Microsofti ametlikku näidisõpetust "Create a matching game WinForms app",
kuid kogu kasutajaliides on üles ehitatud käsitsi C# koodis - **Toolbox'i ei ole kasutatud**.
Sümbolid on kuvatud Wingdings fondi tähemärkidega.

## Kasutatud tehnoloogiad

- C# (.NET Framework 4.7.2)
- Windows Forms (WinForms)
- `Button` ruudustik, `System.Windows.Forms.Timer`, `System.Collections.Generic.List<T>`

## Kuidas käivitada ja kasutada

1. Ava lahendus (`.slnx`) Visual Studios ja käivita projekt (F5).
2. Avavormil vajuta nuppu **"3. Matching Game"**.
3. Klõpsa kahel ruudul, et paljastada nende taga peituvad sümbolid.
4. Kui sümbolid ühtivad, jäävad need nähtavaks. Kui ei ühti, peituvad need
   pärast lühikest pausi uuesti.
5. Leia kõik 8 paari, et mäng võita.

## Klasside ülesehitus (OOP)

- `Mangukaart` - seob kokku ühe nupu ja selle taga oleva sümboli ning teab, kas paar on juba
  leitud. Vorm ei pea teadma nupu sisemist olekut, vaid küsib seda `Mangukaart` objektilt
  (*encapsulation*).
- `MatchingGameForm` - loob mängulaua, segab kaardid juhuslikkuse alusel (Fisher-Yates
  algoritm) ja haldab mängu loogikat (klõpsu töötlus, taimeri abil peitmine, võidu tuvastus).

## Arendusideed (edasiarendus)

1. Lisa sümbolite asemele päris pildid (nt `PictureBox` nuppude peal või `Button.Image`),
   mitte ainult Wingdings tähemärgid.
2. Lisa taimer ja punktisüsteem - mida kiiremini ja vähemate klõpsudega mäng läbitakse,
   seda rohkem punkte.
3. Lisa tasemete valik (nt 4x4, 6x6 ruudustik) kasutajaliidese kaudu enne mängu algust.
4. Lisa helid õige/vale paari kohta ning animatsioon kaartide paljastamisel/peitmisel.
5. Lisa parimate tulemuste (leaderboard) salvestamine faili, et kasutaja saaks oma
   varasemate katsetega võrrelda.

## Eeldatav edasiareng

Mängu võiks laiendada mitmetasemeliseks õppemänguks (nt sõnade, valemite või
riikide lippude sobitamine), kus raskusaste ja teema on kasutaja poolt valitavad.
