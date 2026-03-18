namespace FizzBuzz;

public class FizzBuzzer
{
    public string Go(int number)
    {
        if (number % 3 == 0)
        {
            return "Fizz";
        }
        return null;
    }
}
