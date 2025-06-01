using System;
using System.Collections.Generic;
using System.Threading;

namespace AdapterExample
{
    public interface IWasher
    {
        void Wash(string cloth);
    }
    
    public class ManualWasher
    {
        public void ScrubWithBoard(string cloth)
        {
            Console.WriteLine($"[RĘCZNE] Szoruję ręcznie: {cloth}");
        }
    }
    
    public class ManualWasherAdapter : IWasher
    {
        private ManualWasher _manualWasher;

        public ManualWasherAdapter(ManualWasher washer)
        {
            _manualWasher = washer;
        }

        public void Wash(string cloth)
        {
            _manualWasher.ScrubWithBoard(cloth);
        }
    }
    
    public class WashingMachine : IWasher
    {
        public void Wash(string cloth)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"[PRALKA] [{time}] Start prania: {cloth}");
            Thread.Sleep(1000);
            string endTime = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"[PRALKA] [{endTime}] Koniec prania: {cloth}");
        }
    }

  
    public class LaundryService
    {
        private IWasher _washer;

        public LaundryService(IWasher washer)
        {
            _washer = washer;
        }

        public void WashAll(List<string> clothes)
        {
            foreach (var cloth in clothes)
            {
                _washer.Wash(cloth);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            List<string> clothes = new List<string>
            {
                "Koszulka",
                "Spodnie",
                "Skarpetki",
                "Bluza z kapturem",
                "Czapka zimowa"
            };

        
            Console.WriteLine(">>> Ręczne pranie:");
            var manualWasher = new ManualWasher();
            var manualService = new LaundryService(new ManualWasherAdapter(manualWasher));
            manualService.WashAll(clothes);
            
            Console.WriteLine("\n>>> Pralka automatyczna:");
            var washingMachine = new WashingMachine();
            var machineService = new LaundryService(washingMachine);

            Thread pralkaThread = new Thread(() => machineService.WashAll(clothes));
            pralkaThread.Start();
            pralkaThread.Join(); 

            Console.WriteLine("\n>>> Pranie zakończone.");
        }
    }
}