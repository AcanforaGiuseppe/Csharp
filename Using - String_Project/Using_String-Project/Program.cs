/* Project that explains the use of "using" statement and "@" before a string */

// The "@" before the string tells us that it is splitted in major strings, subdivided by the fact that we go in the next row with the "Enter" keyboard key
string numbers = @"One
Two
Three
Four";

string letters = @"A
B
C
D";

// The using statement assigns a new variable/s as readonly, and the compiler let use them only in the brackets "{ }", not in other spaces
using (StringReader left = new StringReader(numbers), right = new StringReader(letters))
{
    string? item;
    do
    {
        // This loop iterates all the instances available in the variable item, and prints them on console
        item = left.ReadLine();
        Console.Write(item);
        Console.Write(" ");
        item = right.ReadLine();
        Console.WriteLine(item);
    } while (item != null);
}