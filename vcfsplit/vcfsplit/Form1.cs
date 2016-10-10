using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace vcfSplit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string fileName = openFileDialog1.FileName;
                MessageBox.Show("Choose save directory in next step!");
                saveInFile(fileName);
            }
        }

        void saveInFile(string fileName)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;
                string readContents;
                using (StreamReader streamReader = new StreamReader(fileName))
                    readContents = streamReader.ReadToEnd();
                int BeginIndexInString=readContents.IndexOf("BEGIN:VCARD");
                while (BeginIndexInString != -1)
                {
                    int EndIndexInString = readContents.IndexOf("END:VCARD", BeginIndexInString);
                    string OneVcf = readContents.Substring(BeginIndexInString, EndIndexInString - BeginIndexInString);
                    int b = OneVcf.IndexOf("FN:");
                    int a=OneVcf.LastIndexOf("N:",b);
                    string VcfName = OneVcf.Substring(a,  b - a);
                    VcfName = Regex.Replace(VcfName, @"N:|;|:|\r|\n", "");
                    File.WriteAllText(folderName + "\\" + VcfName + ".vcf", OneVcf,Encoding.Default);
                    BeginIndexInString = readContents.IndexOf("BEGIN:VCARD", EndIndexInString);
                }
                MessageBox.Show("Done!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Donation send to paypal address: amadej.pevec@gmail.com");
        }
    }
}
