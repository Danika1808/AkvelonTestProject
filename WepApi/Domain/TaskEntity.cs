using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TaskEntity : Entity
    {
        public Guid? ProjectId { get; private set; }
        public Project Project { get; private set; }
        public string Description { get; private set; }

        TaskEntity() : base()
        { }

        public TaskEntity(string name, string state, int priorityNum, string description, Project project) : base(name, state, priorityNum)
        {
            Description = description;
            Project = project;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public override void UpdateState(int stateNum)
        {
            var state = (States)stateNum;
            UpdateState(state.ToString());
        }

        public enum States
        {
            ToDo,
            InProgress,
            Done
        }
    }
}
