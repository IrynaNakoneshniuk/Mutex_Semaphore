namespace Mutex_Semaphore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[4];
            Numbers primeNumbers = new Numbers();
            try
            {
                threads[0] = new Thread(primeNumbers.WtiteRandomDigitInFile);
                threads[1] = new Thread(primeNumbers.WritePrimeDigitInFile);
                threads[2] = new Thread(primeNumbers.WriteFileLastDigitsSeven);
                threads[3] = new Thread(primeNumbers.Report);
                for (int i = 0; i<2;i++)
                {
                    threads[i].Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}