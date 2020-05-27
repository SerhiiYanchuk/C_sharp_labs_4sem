using System;
using System.Collections.Generic;
using System.Text;

namespace lab3_BehavioralPatterns_
{
    interface IComponent
    {
        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public void Add(IComponent component)
        {
            throw new NotImplementedException();
        }
        public void Remove(IComponent component)
        {
            throw new NotImplementedException();
        }

        public bool IsComposite()
        {
            return true;
        }
    }
    // Сервер ------------------------------------
    class Server: IComponent
        
    {
        public float m_estimateServer { get; private set; }

        private string m_serverName;
        private string m_serverType;
        private string m_chipset;
        private string m_CPU;

        private int m_powerSupplyUnitW;
        private int m_possibleConnectionCables;
        private int m_possibleConnectionWorkstations;

        public Server(float estimateServer, string serverName, string serverType, string chipset,
            string CPU, int powerSupplyUnitW, int possibleConnectionCables, int possibleConnectionWorkstations)
        {
            m_estimateServer = estimateServer;
            m_serverName = serverName;
            m_serverType = serverType;
            m_chipset = chipset;
            m_CPU = CPU;
            m_powerSupplyUnitW = powerSupplyUnitW;
            m_possibleConnectionCables = possibleConnectionCables;
            m_possibleConnectionWorkstations = possibleConnectionWorkstations;
        }

        protected List<IComponent> m_cables = new List<IComponent>();

        public void Add(IComponent component)
        {
            if (m_cables.Count >= m_possibleConnectionCables)
            {
                Console.WriteLine("Заполнен лимит кабелей");
                return;
            }

            int count = 0;
            foreach(Cable temp in m_cables)
            {
                count += temp.countingConnection();
            }
            if (count < m_possibleConnectionWorkstations)
                this.m_cables.Add(component);
            else
                Console.WriteLine("Заполнен лимит рабочих станций");
        }

        public void Remove(IComponent component)
        {
            this.m_cables.Remove(component);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.visit(this);
            foreach (IComponent temp in m_cables)
            {
                temp.Accept(visitor);
            }
        }
    }

    // Кабель ------------------------------------
    class Cable : IComponent
    {
        public float m_estimateCable { get; private set; }

        private int m_cableOrdinalNumber;
        private int m_possibleConnection;

        public Cable(float estimateCable, int cableOrdinalNumber, int possibleConnection)
        {
            m_estimateCable = estimateCable;
            m_cableOrdinalNumber = cableOrdinalNumber;
            m_possibleConnection = possibleConnection;
        }

        protected List<IComponent> m_workstations = new List<IComponent>();

        public int countingConnection()
        {
            return m_workstations.Count;
        }

        public void Add(IComponent component)
        {
            if (m_workstations.Count < m_possibleConnection)
                this.m_workstations.Add(component);
            else
                Console.WriteLine("Заполнен лимит рабочих станций");
        }

        public void Remove(IComponent component)
        {
            this.m_workstations.Remove(component);
        }
        public void Accept(IVisitor visitor)
        {
            visitor.visit(this);
            foreach (IComponent temp in m_workstations)
            {
                temp.Accept(visitor);
            }
        }
    }

    // Сервер ------------------------------------
    class Workstation : IComponent
    {
        public float m_estimateWorkstation { get; private set; }

        private int m_workstationOrdinalNumber;

        public Workstation(float estimateWorkstation, int workstationOrdinalNumber)
        {
            m_estimateWorkstation = estimateWorkstation;
            m_workstationOrdinalNumber = workstationOrdinalNumber;
        }
        public bool IsComposite()
        {
            return false;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
