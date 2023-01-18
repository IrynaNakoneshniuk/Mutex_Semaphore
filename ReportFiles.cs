using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex_Semaphore
{
    public class ReportFiles
    {
        private int CountOfNumbers(string fileName)
        {
            List<int> numbers = new List<int>();
            Mutex mutexmutex = new Mutex();
            mutexmutex.WaitOne();
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                mutexmutex.ReleaseMutex();
            }
            return numbers.Count;
        }
        private long CountByte(string fileName)
        {
            Mutex mutexmutex = new Mutex();
            mutexmutex.WaitOne();
            long bytesLength = new FileInfo(fileName).Length;
            mutexmutex.ReleaseMutex();
            return bytesLength;
        }
        private List<int> FileСontents(string fileName)
        {
            List<int> numbers = new List<int>();
            string name= new FileInfo(fileName).Name;
            Mutex mutexmutex = new Mutex();
            mutexmutex.WaitOne();
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                mutexmutex.ReleaseMutex();
            }
            return numbers;
        }
        public void GetReport(string fileName)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            List<string> report = new List<string>();
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { mutex.ReleaseMutex(); }   
        }
        public void Report(object? data)
        {

            GetReport("RandomDigit.bin");
            GetReport("PrimeDigit.bin");
            GetReport("LastDigitSeven.bin");
        }
    }
}
