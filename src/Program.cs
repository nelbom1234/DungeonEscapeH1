namespace DungeonEscapeH1;

class Program
{
    static int roomNum; //currently active room
    static string info; //info message to the player
    static bool trapped; // have we hit a trap
    static bool finish = false; // should we exit the game
    static void Main(string[] args)
    {
        Restart(); // initial setup of variables
        while (!finish) {
            Draw(roomNum); // show 
            if (trapped) { //if we've hit a trap, restart
                Restart();
                Console.ReadKey();
                continue;
            }
            string move = Utilities.RecieveInput("Vil du rykke (o)p, (n)ed, (h)øjre eller (v)enstre?"); //we only check the first character, so we highlight those characters in the message
            char square = ParseMove(move); // run the move logic and save the square we stepped on
            ParseSquare(square); // run the logic for which square we hit
        }
        //outside of the while loop, draw the room once again to display the last info message and then exit after button press
        Draw(roomNum);
        Console.WriteLine("tryk på en vilkårlig tast for at lukke spillet");
        Console.ReadKey();
    }

    // draw the various things that need drawing
    static void Draw(int roomNum) {
        Console.Clear();
        Map.Draw(roomNum);
        Player.Draw();
        DrawGuide();
        Console.SetCursorPosition(0,16);
        Console.WriteLine(info);
    }

    // guide for the player as to which squares mean what and how many keys they have
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

    // method to parse move commands
    static char ParseMove(string move) {
        char first = move != "" ? move[0] : ' '; // set the move to a space in case the input is just an empty string to avoid an exception
        switch (first) {
            case 'o': // up
                return Player.Move(roomNum, 0, -1);
            case 'n': // down
                return Player.Move(roomNum, 0, 1);
            case 'h': // right
                return Player.Move(roomNum, 1, 0);
            case 'v': // left
                return Player.Move(roomNum, -1, 0);
            case 'q': //exit the game
                finish = true;
                return ' ';
            default: //invalid input
                Console.WriteLine(
                    "Det er ikke en gyldig kommando\n" +
                    "tryk på en vilkårlig tast for at prøve igen" 
                );
                Console.ReadKey();
                return ' ';
        }
    }

    //parse the info message based on which square we stepped on
    static void ParseSquare(char square) {
        switch(square) {
            case 'X': //wall
                info = "du gik ind i en væg";
                break;
            case '#': //trap
                info = "du trådte i en fælde. tryk på en vilkårlig tast for at genstarte spillet";
                trapped = true;
                break;
            case 'K': //key
                info = "Du samlede en nøgle op";
                break;
            case 'd': //door, but we don't have a key
                info = "Døren er låst";
                break;
            case 'D': // door and we have a key
                info = "du låste døren op";
                break;
            case 'O': //stairs up
                info = "du gik op af trappen";
                roomNum++;
                break;
            case 'N': // stairs down
                info = "du gik ned af trappen";
                roomNum--;
                break;
            case 'G': //final goal
                info = "DU NÅEDE MÅLET!!! godt spillet";
                finish = true;
                break;
            default:
                info = "intet skete";
                break;
        }
    }

    //method to reset all the values to their base values and thus start the game over
    static void Restart() {
        roomNum = 0;
        info = "Velkommen til spillet";
        trapped = false;
        Player.Restart();
        Map.Restart();
    }
}
