namespace DungeonEscapeH1;

static class Utilities {
    //receive input from the user, check it is valid and return it
    public static string RecieveInput(string request) {
        while (true) {
            Console.WriteLine(request);
            string? result = Console.ReadLine();

            if (result == null) {
                Console.WriteLine(
                    "Det er ikke et gyldigt input\n" +
                    "tryk på en vilkårlig tast for at prøve igen."
                );
                Console.ReadKey();
                continue;
            }
            return result.ToLower();
        }
        
    }
}