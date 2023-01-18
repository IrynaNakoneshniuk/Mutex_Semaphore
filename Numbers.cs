using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex_Semaphore
{
    public class Numbers
    {
        private Mutex mutexObj= new Mutex();
        public void WritePrimeDigitInFile(object? data)
        {
              List<int> numbers = new List<int>();
              mutexObj.WaitOne();
            try
            {
                if(File.Exists("RandomDigit.bin"))
                {
                    byte[] buffer1 = File.ReadAllBytes("RandomDigit.bin");
                    foreach (byte b in buffer1)
                    {
                        numbers.Add(Convert.ToInt32(b));
                    }
                }

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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
        }

        private bool IsPrime(int digit)
        {
            for(int i =2; i < digit; i++) {

                if (digit % i == 7)
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsLastDigitSeven(int digit)
        {
            return (digit % 10 == 0)?true:false;
        }

        public void WriteFileLastDigitsSeven(object? data)
        {
            List<int> numbers = new List<int>();
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
                    fs.Write(buffer2);
                }
            }catch(Exception ex) { Console.WriteLine(ex.Message.ToString()); }    
            finally { mutexObj.ReleaseMutex(); }
        }
        public void WtiteRandomDigitInFile(object? data)
        {
            List<int> list = new List<int>();
            Random rd = new Random();
            for (int i = 0; i < 100; i++)
            {
                list.Add(rd.Next(1, 1000));
            }
            try
            {
                mutexObj.WaitOne();
                using (FileStream fs = File.OpenWrite("RandomDigit.bin"))
                {
                    byte[] buffer = list.SelectMany(i => BitConverter.GetBytes(i)).ToArray();
                    fs.Write(buffer);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
        }
        private int CountOfNumbers(string fileName)
        {
            List<int> numbers = new List<int>();
            mutexObj.WaitOne();
            try
            {
                if (File.Exists(fileName))
                {
                    byte[] bytes = File.ReadAllBytes(fileName);
                    foreach (byte b in bytes)
                    {
                        numbers.Add(Convert.ToInt32(b));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
            return numbers.Count;
        }
        private long CountByte(string fileName)
        {
            mutexObj.WaitOne();
            long bytesLength = new FileInfo(fileName).Length;
            mutexObj.ReleaseMutex();
            return bytesLength;
        }
        private List<int> FileСontents(string fileName)
        {
            List<int> numbers = new List<int>();
            string name = new FileInfo(fileName).Name;
            mutexObj.WaitOne();
            try
            {
                if (File.Exists(fileName))
                {
                    byte[] bytes = File.ReadAllBytes(fileName);
                    foreach (byte b in bytes)
                    {
                        numbers.Add(Convert.ToInt32(b));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
            return numbers;
        }
        public void GetReport(string fileName)
        {
            List<string> report = new List<string>();
            mutexObj.WaitOne();

            try
            {
               report.Add(fileName);
                report.Add("Content");
                foreach (int i in FileСontents(fileName))
                {
                    report.Add($" ,{i}");
                }
                report.Add("Size (bytes):");
                report.Add(CountByte(fileName).ToString());
                report.Add(CountOfNumbers(fileName).ToString());
                File.AppendAllLines("ReportFile.txt", report);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { mutexObj.ReleaseMutex(); }
        }
        public void Report(object? data)
        {

            GetReport("RandomDigit.bin");
            GetReport("PrimeDigit.bin");
            GetReport("LastDigitSeven.bin");
        }
    }
}
