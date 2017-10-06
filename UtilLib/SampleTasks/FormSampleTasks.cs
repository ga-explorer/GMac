using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UtilLib.SampleTasks
{
    public partial class FormSampleTasks : Form
    {
        //private readonly List<SampleTask> _sampleTasks;

        public FormSampleTasks(IEnumerable<SampleTask> tasks)
        {
            InitializeComponent();

            //_sampleTasks = new List<SampleTask>(tasks);

            foreach (var task in tasks)
                listBoxTasks.Items.Add(task);
        }


        private void buttonExecTask_Click(object sender, EventArgs e)
        {
            textBoxResults.Text = String.Empty;

            var task = listBoxTasks.SelectedItem as SampleTask;

            if (ReferenceEquals(task, null))
            {
                MessageBox.Show(
                    @"Please select a task to execute",
                    @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );

                return;
            }

            textBoxResults.Text = task.TaskAction();
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxResults.Text = String.Empty;
            textBoxTaskDescription.Text = String.Empty;

            var task = listBoxTasks.SelectedItem as SampleTask;

            if (ReferenceEquals(task, null))
                return;

            textBoxTaskDescription.Text = task.TaskDescription;
        }
    }
}
