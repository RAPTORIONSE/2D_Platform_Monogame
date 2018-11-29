using System;

namespace SuperMario
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
/*To do list:
FileManager Load overloads depend on what use.
MenuManager => static ?? 
MenuManager adapt into new setting
AStar adapt into new setting
ResourceManager make list for easy access
*/


/*Mål:
Att förstå och kunna tillämpa arv.
Att förstå abstrakta klasser.
Att förstå och tillämpa polymorfism.
Att kunna strukturera koden med hjälp av Handlers/Managers.
Att förstå och kunna tillämpa parallaxskrollning.
Att förstå och tillämpa pixelperfekt kollision.
Att kunna använda musik och ljudeffekter.

Plattformsspel - Super Mario
Genren plattformsspel uppstod under 80-talet, men är fortfarande populär. 
För att ett spel skall kallas för ett plattformsspel skall spelarens karaktär hoppa från plattform till plattform utan att hamna utanför. 
I den här examinationsuppgiften är det extra mycket utrymme att använda fantasi och kreativitet för att skapa ett intressant spel. 
Målet med uppgiften är att slutprodukten skall kännas som ett fullständigt spel även om funktionaliteten är begränsad.
     
Grundkrav för uppgiften: 
Se även ovan. 
Banan skall genereras från en fil som specificerar var plattformarna skall placeras, 
var fienden och andra objekt befinner sig samt spelarens start och slutposition för nivån. 
Spelaren skall röra sig och hoppa på plattformarna. 
Kollisioner hanteras på valfritt sätt, men pixelperfekt kollision skall förekomma. 
Lämpligen används Intersects och om en kollision inträffar görs en pixelperfekt kontroll. 
Koden ska dessutom vara skriven på ett snyggt sätt och uppdelad i klasser och metoder samt implementera arv med minst en abstrakt klass.
 

Exempel på extra funktionalitet:
Flera nivåer (~1-2p)
Meny (~1p)
Olika typer av bandelar t.ex. teleporter, stegar och fällor (~1-4p)
Kan döda fiender på flera olika sätt (~1p)
Level editor (~2-4p)
Bana som är större än skärmen (~1p)
Paralaxscrollning (~1p)

Struktur och kod: 
Stor vikt kommer att läggas på hur koden är strukturerad.
Korekt arv - D
Namngivning - C
Läsbarhet - C
Ingen hårdkodning - C
Blockstorlek - C
Klassindelning - A
Konsekvent - A

Betygsgränser: 
E: Grundkrav 
D: Grundkrav + 4 poäng + kodstruktur D
C: Grundkrav + 7 poäng + god kodstruktur D-C
B: Grundkrav + 10 poäng + god kodstruktur D-C
A: Grundkrav + 13 poäng + god kodstruktur D-A
För att betyget skall registreras krävs att du redovisar och lägger upp ditt projekt på Canvas.


Exempel på textfil för level
Värdena nedan är endast exempel!
50,300,50,50
1400
50,350,100,50;240,450,150,50;500,400,200,50;800,550,200,50;1100,400,200,50
550,360,40,42;1150,360,40,42
100,310,40,40;250,410,40,40
______________________________
Row 1 = Player start Pos (X,Y,Width,Height)
Row 2 = End of level Pos (X) Or if you want a platform as the level goal.
Row 3 = Platform Rectangles(PosX, PosY, Width, Height) ex: X0,Y0,Width0,Height0; X1,Y1,Width1,Height1; etc.
Row 4 = Enemy start Pos (X,Y,Width,Height)
Row 5 = BonusObjects Pos(X,Y,Width,Height)
______________________________
*/