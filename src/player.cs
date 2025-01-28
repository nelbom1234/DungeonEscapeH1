namespace DungeonEscapeH1;

class Player {
    static int x = 1; // player x-position
    static int y = 1; // player y-position
    static int keys = 0; //number of keys held by player

    //replace the square at player position with the player character
    public static void Draw() {
        Console.SetCursorPosition(2*x, y);
        Console.Write('P');
    }

    // move the player position by dx,dy
    //logic for walls, doors and keys
    public static char Move(int roomNum, int dx, int dy) {
        char square = Map.Square(roomNum, x+dx, y+dy);
        if (square == 'X') return 'X'; //if we move into a wall
        if (square == 'D' && keys < 1) return 'd'; // if we hit a door without a key
        x += dx;
        y += dy;
        if (square == 'K') { //if we hit a key
            keys++;
            Map.RemoveSquare(roomNum, x, y);
        };
        if (square == 'D') { // if the prior door check didn't hit, we have at least 1 key
            keys--;
            Map.RemoveSquare(roomNum, x, y);
        }
        return square;
    }

    //reset to starting values
    public static void Restart() {
        x = 1;
        y = 1;
        keys = 0;
    }

    public static int GetKeys() {
        return keys;
    }
}