using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex_Semaphore
{
    public class RandomNumbers
    {
       public void WtiteRandomDigitInFile(object? data)
       {
            Mutex mutexObj = new Mutex();
            List<int> list = new List<int>();
            Random rd= new Random();
            for(int i = 0; i < 100; i++)
            {
                list.Add(rd.Next(1,1000));
            }
            try
            {
                mutexObj.WaitOne();
                using (FileStream fs = File.OpenWrite("RandomDigit.bin"))
                {
                    byte[] buffer = list.SelectMany(i => BitConverter.GetBytes(i)).ToArray();
                    fs.Write(buffer, 0, buffer.Length);
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
    }
}
