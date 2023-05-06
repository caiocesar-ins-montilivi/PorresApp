# PORRA GIRONA - CAIO GOMES

## Descripció del projecte
Aquest projecte és una aplicació de porra de Girona desenvolupada en C# WPF que utilitza el patró d'arquitectura MVVM, una base de dades i la llibreria Material Design per a la interfície d'usuari. El projecte comença el 8 de maig i acaba el 31 de maig.

### Eines Utilitzades
Les següents eines s'han utilitzat en el desenvolupament d'aquest projecte:
* Visual Studio: IDE de desenvolupament per a la programació en C#.
* XAMPP: Paquet de programari que inclou un servidor web Apache, MySQL i PHP, utilitzat per crear una base de dades MySQL i un servidor web per a l'aplicació WPF.
* Material Design: Llibreria per a la interfície d'usuari que proporciona estils predefinits i elements de disseny.

### Arquitectura del Projecte
El projecte utilitzarà el patró de disseny MVVM, que significa Model-Vista-Vista Model. Aquest patró separa les dades, la lògica de negoci i la interfície d'usuari en diferents capes per millorar l'escalabilitat i el manteniment del codi.
L'aplicació es dividirà en les següents capes:
1. Capa de Model: aquesta capa contindrà la lògica de negoci i les dades de l'aplicació. La capa de Model es comunicarà amb la capa de dades i la capa de Vista Model.
2. Capa de Vista Model: aquesta capa es comunicarà amb la capa de Model i s'utilitzarà per actualitzar la interfície d'usuari. La capa de Vista Model es comunicarà amb la capa de Vista.
3. Capa de Vista: aquesta capa contindrà els elements de la interfície d'usuari, com ara finestres, botons, etc. La capa de Vista es comunicarà amb la capa de Vista Model.

### Objectius
L'objectiu d'aquest projecte és implementar la funcionalitat de la porra de Girona des del 8 de maig fins al 31 de maig utilitzant les eines i les llibreries esmentades anteriorment i el patró de disseny MVVM. S'haurà de crear una base de dades MySQL per emmagatzemar les dades de la porra.

### Execució
Per executar l'aplicació, s'ha d'obrir el projecte en Visual Studio i executar-lo des de l'IDE. La base de dades MySQL es pot configurar amb XAMPP.

### Contribució
Les contribucions són benvingudes. Si desitgeu contribuir al projecte, podeu fer una sol·licitud de tira aquesta branca.

### Llicència
Aquest projecte està sota llicència MIT. Podeu consultar el fitxer de llicència per obtenir més informació.
