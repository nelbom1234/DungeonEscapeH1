namespace DungeonEscapeH1;

class Program
{
    static int roomNum;
    static string info;
    static bool trapped;
    static bool finish = false;

    static void Main(string[] args)
    {
        Restart();
        while (!finish) {
            Draw(roomNum);
            if (trapped) {
                Restart();
                Console.ReadKey();
                continue;
            }
            string move = Utilities.RecieveInput("Vil du rykke (o)p, (n)ed, (h)øjre eller (v)enstre?");
            char square = ParseMove(move);
            ParseSquare(square);
        }
        Draw(roomNum);
        Console.WriteLine("tryk på en vilkårlig tast for at lukke spillet");
        Console.ReadKey();
    }

    static void Draw(int roomNum) {
        Console.Clear();
        Map.Draw(roomNum);
        Player.Draw();
        DrawGuide();
        Console.SetCursorPosition(0,16);
        Console.WriteLine(info);
    }

    static void DrawGuide() {
        Console.SetCursorPosition(32, 0);
        Console.Write("P: dig");
        Console.SetCursorPosition(32, 2);
        Console.Write("X: væg");
        Console.SetCursorPosition(32, 4);
        Console.Write("K: nøgle");
        Console.SetCursorPosition(32, 6);
        Console.Write("D: dør");
        Console.SetCursorPosition(32, 8);
        Console.Write("O: trappe op");
        Console.SetCursorPosition(32, 10);
        Console.Write("N: trappe ned");
        Console.SetCursorPosition(32, 12);
        Console.Write("G: mål");
        Console.SetCursorPosition(32, 14);
        Console.Write($"Nøgler: {Player.GetKeys()}");
    }

    static char ParseMove(string move) {
        char first = move != "" ? move[0] : ' ';
        switch (first) {
            case 'o':
                return Player.Move(roomNum, 0, -1);
            case 'n':
                return Player.Move(roomNum, 0, 1);
            case 'h':
                return Player.Move(roomNum, 1, 0);
            case 'v':
                return Player.Move(roomNum, -1, 0);
            case 'q':
                finish = true;
                return ' ';
            default:
                Console.WriteLine(
                    "Det er ikke en gyldig kommando\n" +
                    "tryk på en vilkårlig tast for at prøve igen" 
                );
                Console.ReadKey();
                return ' ';
        }
    }

    static void ParseSquare(char square) {
        switch(square) {
            case 'X':
                info = "du gik ind i en væg";
                break;
            case '#':
                info = "du trådte i en fælde. tryk på en vilkårlig tast for at genstarte spillet";
                trapped = true;
                break;
            case 'K': 
                info = "Du samlede en nøgle op";
                break;
            case 'd':
                info = "Døren er låst";
                break;
            case 'D':
                info = "du låste døren op";
                break;
            case 'O':
                info = "du gik op af trappen";
                roomNum++;
                break;
            case 'N':
                info = "du gik ned af trappen";
                roomNum--;
                break;
            case 'G':
                info = "DU NÅEDE MÅLET!!! godt spillet";
                finish = true;
                break;
            default:
                info = "intet skete";
                break;
        }
    }

    static void Restart() {
        roomNum = 0;
        info = "Velkommen til spillet";
        trapped = false;
        Player.Restart();
        Map.Restart();
    }
}
