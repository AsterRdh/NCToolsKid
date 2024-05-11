using NCToolsKidForJava.Forms;
using System;
using System.Windows.Forms;

namespace NCToolsKidForJava
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new BeanBuilderForm().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new PubSysBuilderForm().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new DefdocBuilderForm().Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new SQL2Table().Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new NccEnCoding().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new LanCaller().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AutoPack().Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            new VOBuilderForm().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new ConfigForm().Show();    
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new CreateCodeForm().Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            new EnumForm().Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new SqlForm().Show();
        }
    }
}
