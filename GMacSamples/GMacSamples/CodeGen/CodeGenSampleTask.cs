using System;

namespace GMacSamples.CodeGen
{
    public sealed class CodeGenSampleTask
    {
        public string TaskName { get; }

        public string TaskDescription { get; private set; }

        public Func<string> TaskAction { get; private set; }


        public CodeGenSampleTask(string name, string description, Func<string> action)
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
