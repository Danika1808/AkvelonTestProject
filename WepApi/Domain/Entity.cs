using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Priority { get; private set; }
        public string State { get; private set; }

        protected Entity()
        { }
        protected Entity(string name, string state, int priorityNum)
        {
            Name = name;
            State = state;
            UpdatePriority(priorityNum);
        }

        public abstract void UpdateState(int stateNum);

        public void UpdatePriority(int priorityNum)
        {
            var priority = (Priorities)priorityNum;
            Priority = priority.ToString();
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        protected void UpdateState(string state)
        {
            State = state;
        }

        public enum Priorities
        {
            Minor,
            Normal,
            Major,
            Critical
        }
    }
}
