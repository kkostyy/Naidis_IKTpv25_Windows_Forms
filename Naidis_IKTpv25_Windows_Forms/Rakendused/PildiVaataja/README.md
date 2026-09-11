# Pildi vaatamise programm (Picture Viewer)

## Eesmärk ja kirjeldus

Lihtne pildivaataja rakendus, mis võimaldab kasutajal:

- avada ja kuvada pildifaili (`.jpg`, `.jpeg`, `.png`, `.bmp`, `.gif`) oma arvutist,
- muuta pildiala taustavärvi,
- kuvada pilti mustvalgena ("Sketch" märkeruut),
- kustutada kuvatava pildi.

Rakendus järgib Microsofti ametlikku näidisõpetust "Create a picture viewer Windows Forms app",
kuid kogu kasutajaliides on üles ehitatud käsitsi C# koodis - **Toolbox'i ei ole kasutatud**.

## Kasutatud tehnoloogiad

- C# (.NET Framework 4.7.2)
- Windows Forms (WinForms)
- `PictureBox`, `OpenFileDialog`, `ColorDialog`, `CheckBox`, `Button`, `Panel`

## Kuidas käivitada ja kasutada

1. Ava lahendus (`.slnx`) Visual Studios ja käivita projekt (F5).
2. Avavormil vajuta nuppu **"1. Pildi vaataja"**.
3. Rakenduse aknas:
   - **Show a picture** - ava failidialoog ja vali pildifail.
   - **Set the background color** - vali pildiala taustavärv.
   - **Sketch** (märkeruut) - näitab valitud pilti mustvalgena.
   - **Clear the picture** - eemaldab kuvatava pildi.
   - **Close** - sulgeb akna.

## Klasside ülesehitus (OOP)

- `PildiVaatajaForm` - vastutab kasutajaliidese ja sündmuste käsitlemise eest.
- `PildiTootlus` - staatiline abiklass, mis vastutab ainult pildi töötlemise (halltoonideks
  muutmise) eest. Eraldi klass hoiab vormi koodi puhtana (*single responsibility* põhimõte).

## Arendusideed (edasiarendus)

1. Lisa slaidishow funktsioon, mis vahetab pilte automaatselt kindla ajavahemiku tagant
   (nt `Timer` komponendiga), koos "eelmine/järgmine" nuppudega.
2. Lisa võimalus salvestada muudetud pilt (nt mustvalgena) uude faili teises formaadis
   (`Image.Save`, kasutades `SaveFileDialog`-i).
3. Lisa lihtne pildi pööramise ja peegeldamise võimalus (`RotateFlip` meetod),
   samuti suumimise liugur (`TrackBar`).
4. Asenda praegune aeglane piksel-haaval halltoonide arvutus kiirema `ColorMatrix`
   põhise lahendusega, et suured pildid ei aeglustaks rakendust.
5. Lisa miniatuuride (thumbnail) riba, mis näitab viimati avatud pilte kiireks
   vahetamiseks.

## Eeldatav edasiareng

Pikemas perspektiivis võiks rakendus areneda lihtsaks pildihalduri/galerii-tüüpi
tööriistaks: kaustade sirvimine, miniatuurid, lihtsad pilditöötlusfiltrid ning
piltide järjestamine slaidishow jaoks.
