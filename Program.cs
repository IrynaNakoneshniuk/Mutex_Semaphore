namespace Mutex_Semaphore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[4];
            RandomNumbers randomNumbers = new RandomNumbers();
            PrimeNumbers primeNumbers = new PrimeNumbers();
            ReportFiles reportFiles = new ReportFiles();
            Mutex mutex = new Mutex();
            try
            {
                threads[0] = new Thread(randomNumbers.WtiteRandomDigitInFile);
                threads[1] = new Thread(primeNumbers.WritePrimeDigitInFile);
                //threads[2] = new Thread(primeNumbers.WriteFileLastDigitsSeven);
                //threads[3] = new Thread(reportFiles.Report);
                for (int i = 0; i<2;i++)
                {
                    mutex.WaitOne();
                    threads[i].Start();
                    mutex.ReleaseMutex();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}