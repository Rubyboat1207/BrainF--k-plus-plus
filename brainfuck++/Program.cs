using System.IO;


internal class Program
{
    static void Main(string[] args)
    {
        new Interpreter(File.ReadAllText(args[0])).Run();
        
    }
}

public class Interpreter
{
    public byte[] tape;
    public int pointer = 30000;
    public char[] input;

    public Interpreter(string input)
    {
        this.input = input.ToCharArray();
        tape = new byte[60000];
    }

    public void Run()
    {
        var unmatchedBracketCounter = 0;
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '>':
                    pointer++;
                    break;
                case '<':
                    pointer--;
                    break;
                case '+':
                    tape[pointer]++;
                    break;
                case '-':
                    tape[pointer]--;
                    break;
                case '.':
                    Console.Write(Convert.ToChar(tape[pointer]));
                    break;
                case ',':
                    var key = Console.ReadKey();
                    tape[pointer] = (byte)key.KeyChar;
                    break;
                case '[':
                    if (tape[pointer] == 0)
                    {
                        unmatchedBracketCounter++;
                        while (input[i] != ']' || unmatchedBracketCounter != 0)
                        {
                            i++;

                            if (input[i] == '[')
                            {
                                unmatchedBracketCounter++;
                            }
                            else if (input[i] == ']')
                            {
                                unmatchedBracketCounter--;
                            }
                        }
                    }
                    break;
                case ']':
                    if (tape[pointer] != 0)
                    {
                        unmatchedBracketCounter++;
                        while (input[i] != '[' || unmatchedBracketCounter != 0)
                        {
                            i--;

                            if (input[i] == ']')
                            {
                                unmatchedBracketCounter++;
                            }
                            else if (input[i] == '[')
                            {
                                unmatchedBracketCounter--;
                            }
                        }
                    }
                    break;
                case '^':
                    tape[pointer] += tape[pointer - 1];
                break;
                case '!':
                    tape[pointer] -= tape[pointer - 1];
                break;
                case '\\':
                    tape[pointer] += tape[pointer + 1];
                    break;
                case '/':
                    tape[pointer] -= tape[pointer + 1];
                    break;
                case ':':
                    Console.Write(tape[pointer]);
                break;
            }
        }
    }
}