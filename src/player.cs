namespace DungeonEscapeH1;

class Player {
    static int x = 1;
    static int y = 1;
    static int keys = 0;

    public static void Draw() {
        Console.SetCursorPosition(2*x, y);
        Console.Write('P');
    }

    public static char Move(int roomNum, int dx, int dy) {
        char square = Map.Square(roomNum, x+dx, y+dy);
        if (square == 'X') return 'X';
        if (square == 'D' && keys < 1) return 'd';
        x += dx;
        y += dy;
        if (square == 'K') {
            keys++;
            Map.RemoveSquare(roomNum, x, y);
        };
        if (square == 'D') {
            keys--;
            Map.RemoveSquare(roomNum, x, y);
        }
        return square;
    }

    public static void Restart() {
        x = 1;
        y = 1;
        keys = 0;
    }

    public static int GetKeys() {
        return keys;
    }
}