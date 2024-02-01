namespace GPL2
{
    public partial class Form1 : Form
    {

        private CursorManager cursorManager;
        private TaskManager taskManager;
        private Parser parser;
        public string[] Commands { get; private set; }
        List<string> Wcommands;
        public Form1()
        {
            InitializeComponent();
            cursorManager = new CursorManager(pictureBox1);
            taskManager = new TaskManager(pictureBox1, cursorManager, Wcommands, conditionEval);
            parser = new Parser();
        }

        private bool conditionEval()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] commands = richTextBox1.Text.Split(new[] { '\n', '\v', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            parser.ParseCommands(commands);

            var validCommands = parser.GetDelayedExecutionCommands();
            try
            {
                taskManager.ExecuteCommands(validCommands);
            }
            catch (Exception ex) 
            {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}