using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Project : Entity
    {
        public DateTime StartDate { get; private set; }
        public DateTime CompletionDate { get; private set; }
        private readonly List<TaskEntity> _tasks = new();
        public IEnumerable<TaskEntity> Tasks => _tasks?.ToList();

        Project() : base()
        { }

        public Project(string name, string state, int priorityNum, DateTime startDate) : base(name, state, priorityNum)
        {
            StartDate = startDate;
        }

        public void UpdateCompletionDate(DateTime completionDate)
        {
            CompletionDate = completionDate;
        }

        public override void UpdateState(int stateNum)
        {
            var state = (States)stateNum;
            UpdateState(state.ToString());
        }

        public enum States
        {
            NotStarted,
            Active,
            Completed
        }
    }
}
