namespace angularHeroes.Helpers
{
    public class StaticFunctions
    {
       public static double AddRandomNumberUpTo10Precent(double originalNumber) {
        
            Random random = new Random();
            
            double percentage = 0.10; // 10% expressed as a decimal
            if (originalNumber == 0) { originalNumber = 1; };
            double tenPercent = originalNumber * percentage;
            double randomNumber = tenPercent * random.NextDouble(); // Random number between 0 and 10% of the original number

            double result = originalNumber + randomNumber;

            /*Console.WriteLine("Original Number: " + originalNumber);
            Console.WriteLine("Random number between 0 and 10%: " + randomNumber);
            Console.WriteLine("Result after adding random number: " + result);*/

            return (double)Math.Round(result, 1);
        }
    }
}
