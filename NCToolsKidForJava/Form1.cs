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

        BeanBuilderForm beanBuilderForm = null;
        AutoPack autoPackForm = null;
        PubSysBuilderForm pubSysBuilderForm = null;
        DefdocBuilderForm defdocBuilderForm = null;
        SQL2Table sql2Table = null;
        NccEnCoding nccEnCoding = null;
        LanCaller lanCaller = null;
        VOBuilderForm voBuilderForm = null;
        ConfigForm configForm = null;
        CreateCodeForm createCodeForm = null;
        EnumForm enumForm = null;
        SqlForm sqlForm = null;
        SqlFunctionBuilderForm sqlFunctionBuilderForm = null;


        //beanBuilderForm
        private void button4_Click(object sender, EventArgs e)
        {
            if (beanBuilderForm == null)
            {
                beanBuilderForm = new BeanBuilderForm();
                beanBuilderForm.Show();
            }
            else
            {
                beanBuilderForm.Activate();
            }
         
        }

        //PubSysBuilderForm
        private void button3_Click(object sender, EventArgs e)
        {
            if (pubSysBuilderForm == null)
            {
                pubSysBuilderForm = new PubSysBuilderForm();
                pubSysBuilderForm.Show();
            }
            else
            {
                pubSysBuilderForm.Activate();
            }
        }

        //DefdocBuilderForm
        private void button6_Click(object sender, EventArgs e)
        {
            if (defdocBuilderForm == null)
            {
                defdocBuilderForm = new DefdocBuilderForm();
                defdocBuilderForm.Show();
            }
            else
            {
                defdocBuilderForm.Activate();
            }
        }

        //SQL2Table
        private void button7_Click(object sender, EventArgs e)
        {
            if (sql2Table == null)
            {
                sql2Table = new SQL2Table();
                sql2Table.Show();
            }
            else
            {
                sql2Table.Activate();
            }
        }

        //NccEnCoding
        private void button8_Click(object sender, EventArgs e)
        {
            if (nccEnCoding == null)
            {
                nccEnCoding = new NccEnCoding();
                nccEnCoding.Show();
            }
            else
            {
                nccEnCoding.Activate();
            }
        }

        //LanCaller
        private void button5_Click(object sender, EventArgs e)
        {
            if(lanCaller == null)
            {
                lanCaller = new LanCaller();
                lanCaller.Show();
            }
            else
            {
                lanCaller.Activate();
            }
          
        }

        //AutoPack
        private void button1_Click(object sender, EventArgs e)
        {
            if (autoPackForm == null) { 
                autoPackForm = new AutoPack();
                autoPackForm.Show();
            }
            else
            {
                autoPackForm.Activate();
            }
           

        }

        //VOBuilderForm
        private void button10_Click(object sender, EventArgs e)
        {
            if(voBuilderForm == null)
            {
                voBuilderForm = new VOBuilderForm();    
                voBuilderForm.Show();

            }
            else
            {
                voBuilderForm.Activate();
            }
        }

        //ConfigForm
        private void button11_Click(object sender, EventArgs e)
        {
            if(configForm == null)
            {
                configForm = new ConfigForm();
                configForm.Show();
            }
            else
            {
                configForm.Activate();
            } 
        }

        //CreateCodeForm
        private void button12_Click(object sender, EventArgs e)
        {
            if(createCodeForm == null)
            {
                createCodeForm = new CreateCodeForm();
                createCodeForm.Show();
            }
            else
            {
                createCodeForm.Activate();
            }
        }

        //EnumForm
        private void button13_Click(object sender, EventArgs e)
        {
            if(enumForm == null)
            {
                enumForm = new EnumForm();
                enumForm.Show();
            }
            else
            {
                enumForm.Activate();
            }
        }

        //SqlForm
        private void button14_Click(object sender, EventArgs e)
        {
            if(sqlForm == null)
            {
                sqlForm = new SqlForm();
                sqlForm.Show();
            }
            else
            {
                sqlForm.Activate();
            }
        }

        //SqlFunctionBuilderForm
        private void button15_Click(object sender, EventArgs e)
        {
            if (sqlFunctionBuilderForm == null)
            {
                sqlFunctionBuilderForm = new SqlFunctionBuilderForm();
                sqlFunctionBuilderForm.Show();
            }
            else
            {
                sqlFunctionBuilderForm.Activate();
            }
        }
    }
}
