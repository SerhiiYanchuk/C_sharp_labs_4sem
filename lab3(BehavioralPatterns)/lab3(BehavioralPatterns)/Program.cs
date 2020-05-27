using System;

namespace lab3_BehavioralPatterns_
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(500, "DEPO Storm 1420K4", "Коммутационный сервер", "Intel® C242", "Intel® Xeon® E-2100/2200", 920, 3, 10);
            Cable cable1 = new Cable(50, 1, 5);
            Cable cable2 = new Cable(30, 2, 3);
            Cable cable3 = new Cable(20, 3, 2);
            Cable extraCable = new Cable(20, 3, 2);
            server.Add(cable1);
            server.Add(cable2);
            server.Add(cable3);
            server.Add(extraCable);
            Workstation workstation1 = new Workstation(100, 1);
            Workstation workstation2 = new Workstation(100, 2);
            Workstation workstation3 = new Workstation(100, 3);
            Workstation workstation4 = new Workstation(100, 4);
            Workstation workstation5 = new Workstation(100, 5);
            Workstation extraWorkstation = new Workstation(100, 500);
            cable1.Add(workstation1);
            cable1.Add(workstation2);
            cable1.Add(workstation3);
            cable1.Add(workstation4);
            cable1.Add(workstation5);
            cable1.Add(extraWorkstation);
            Workstation workstation6 = new Workstation(150, 6);
            Workstation workstation7 = new Workstation(150, 7);
            Workstation workstation8 = new Workstation(150, 8);
            cable2.Add(workstation6);
            cable2.Add(workstation7);
            cable2.Add(workstation8);
            Workstation workstation9 = new Workstation(200, 9);
            Workstation workstation10 = new Workstation(200, 10);
            cable3.Add(workstation9);
            cable3.Add(workstation10);

            EstimateVisitor visitor = new EstimateVisitor();
            server.Accept(visitor);
            Console.WriteLine($"Estimate of network per month: {visitor.m_estimate}");
            Console.ReadKey();
        }
    }
}
