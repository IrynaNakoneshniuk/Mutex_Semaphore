using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex_Semaphore
{
    public class PrimeNumbers
    {
        public void WritePrimeDigitInFile(object? data)
        {
            List<int> numbers = new List<int>();
            Mutex mutexObj = new Mutex();
            try
            {
                mutexObj.WaitOne();
                if (File.Exists("RandomDigit.bin"))
                {
                    byte[] buffer1 = File.ReadAllBytes("RandomDigit.bin");
                    foreach (byte b in buffer1)
                    {
                        numbers.Add(Convert.ToInt32(b));
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
         
            try
            {
                mutexObj.WaitOne();
                using (FileStream fs = File.OpenWrite("PrimeDigit.bin"))
                {
                    int j = 0;
                    byte[] buffer2 = new byte[sizeof(int) * numbers.Count];
                    foreach (int i in numbers)
                    {
                        if (IsPrime(i))
                        {
                            buffer2[j++] = Convert.ToByte(i);
                        }
                    }
                    fs.Write(buffer2, 0, buffer2.Length);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
        }

        private bool IsPrime(int digit)
        {
            for(int i =2; i < digit; i++) {

                if (digit % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsLastDigitSeven(int digit)
        {
            return (digit % 10 == 0)?true:false;
        }

        public void WriteFileLastDigitsSeven(object? data)
        {
            List<int> numbers = new List<int>();
            Mutex mutexObj = new Mutex();
            mutexObj.WaitOne();
            try
            {
                if (File.Exists("PrimeDigit.bin"))
                {
                    byte[] buffer1 = File.ReadAllBytes("PrimeDigit.bin");
                    foreach (byte b in buffer1)
                    {
                        numbers.Add(Convert.ToInt32(b));
                    }
                }
            }catch(Exception e) { Console.WriteLine(e.Message.ToString()); }
            finally { mutexObj.ReleaseMutex(); }
        
            mutexObj.WaitOne();
            try
            {
                using (FileStream fs = File.OpenWrite("LastDigitSeven.bin"))
                {
                    int j = 0;
                    byte[] buffer2 = new byte[sizeof(int) * numbers.Count];
                    foreach (int i in numbers)
                    {
                        if (IsLastDigitSeven(i))
                        {
                            buffer2[j++] = Convert.ToByte(i);
                        }
                    }
                    fs.Write(buffer2, 0, buffer2.Length);
                }
            }catch(Exception ex) { Console.WriteLine(ex.Message.ToString()); }    
            finally { mutexObj.ReleaseMutex(); }
        }
    }
}
