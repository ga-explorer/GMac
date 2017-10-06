using System;

namespace UtilLib.SampleTasks
{
    public sealed class SampleTask
    {
        public string TaskName { get; private set; }

        public string TaskDescription { get; private set; }

        public Func<string> TaskAction { get; private set; }


        public SampleTask(string name, string description, Func<string> action)
        {
            TaskName = name;
            TaskDescription = description;
            TaskAction = action;
        }


        public override string ToString()
        {
            return TaskName;
        }
    }
}
