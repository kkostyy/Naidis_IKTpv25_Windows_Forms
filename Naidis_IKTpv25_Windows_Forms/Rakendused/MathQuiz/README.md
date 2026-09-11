# Matemaatiline äraarvamismäng (Math Quiz)

## Eesmärk ja kirjeldus

Rakendus kuvab neli matemaatilist ülesannet (liitmine, lahutamine, korrutamine, jagamine),
millele kasutaja peab vastama enne, kui aeg otsa saab. Pärast **"Start the quiz"** nupu
vajutamist genereeritakse uued ülesanded ja käivitub 30-sekundiline taimer. Kui aeg saab
läbi, kontrollitakse vastuseid ja kuvatakse tulemus (mitu ülesannet õigesti lahendati).

Rakendus järgib Microsofti ametlikku näidisõpetust "Create a math quiz WinForms app",
kuid kogu kasutajaliides on üles ehitatud käsitsi C# koodis - **Toolbox'i ei ole kasutatud**.

## Kasutatud tehnoloogiad

- C# (.NET Framework 4.7.2)
- Windows Forms (WinForms)
- `Label`, `TextBox`, `NumericUpDown`, `Button`, `System.Windows.Forms.Timer`

## Kuidas käivitada ja kasutada

1. Ava lahendus (`.slnx`) Visual Studios ja käivita projekt (F5).
2. Avavormil vajuta nuppu **"2. Math Quiz"**.
3. Rakenduse aknas vajuta **"Start the quiz"** - kuvatakse neli uut ülesannet
   ja algab 30-sekundiline pöördloendus.
4. Sisesta iga ülesande vastus vastavasse `NumericUpDown` väljale.
5. Kui aeg saab läbi, kuvatakse teade õigete vastuste arvuga. Uue mängu jaoks
   vajuta uuesti **"Start the quiz"**.

## Klasside ülesehitus (OOP)

- `MatemaatikaUlesanne` - vastutab ühe ülesande (kaks arvu, tehe, õige vastus) genereerimise
  ja kontrollimise eest. Vorm ise ei tea, kuidas ülesanne sisemiselt arvutatakse - see on
  klassi enda vastutus (*encapsulation*).
- `MathQuizForm` - loob kasutajaliidese, haldab taimerit ja kasutab `MatemaatikaUlesanne`
  objekte ülesannete kuvamiseks ja vastuste kontrollimiseks.

## Arendusideed (edasiarendus)

1. Lisa erinevad tehete tüübid ja rohkem ülesandeid (nt astendamine, protsendid) ning
   võimalus valida, mitu ülesannet ühes voorus lahendada.
2. Lisa punktiarvestus ja tulemuste ajalugu (nt parim tulemus salvestatakse faili või
   `Properties.Settings` alla).
3. Lisa raskusastme valik (nt `ComboBox` "Lihtne / Keskmine / Raske"), mis muudab
   arvude vahemikku ja ajapiirangut.
4. Lisa helisignaal või värviline tagasiside (roheline/punane) iga vastuse kohta kohe
   pärast ajapiirangu lõppu.
5. Lisa mitme kasutaja/mängija režiim, kus võrreldakse tulemusi omavahel.

## Eeldatav edasiareng

Rakendust võiks laiendada täieõiguslikuks õppetööriistaks, kus õpetaja saab valida
teemad ja raskusastme ning õpilaste tulemused salvestatakse ja neid saab hiljem
analüüsida (nt tabelina või graafikuna).
