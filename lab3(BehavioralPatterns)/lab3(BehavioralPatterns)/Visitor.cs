using System;
using System.Collections.Generic;
using System.Text;

namespace lab3_BehavioralPatterns_
{
    interface IVisitor
    {
       public void visit(Server component);
       public void visit(Cable component);
       public void visit(Workstation component);
    }

    class EstimateVisitor : IVisitor
    {
        public float m_estimate { get; private set; } = 0;
        public void visit(Server component)
        {
            m_estimate += component.m_estimateServer;
        }
        public void visit(Cable component)
        {
            m_estimate += component.m_estimateCable;
        }
        public void visit(Workstation component)
        {
            m_estimate += component.m_estimateWorkstation;
        }

    }
}
