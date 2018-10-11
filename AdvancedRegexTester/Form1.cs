using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedRegexTester
{
    public partial class Form1 : Form
    {
        private List<Match> matches;
        private Regex global;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            try
            {
                global=new Regex(textBox1.Text);
            }
            catch (Exception ex)
            {
                richTextBox2.AppendText(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            try
            {
                Match match = global.Match(richTextBox1.Text);
                matches = new List<Match>();
                matches.Add(match);
                //int count = 0;
                while (match.Success)
                {
                    match = match.NextMatch();
                    matches.Add(match);
                    //count++;
                }
                richTextBox2.AppendText("Found " + matches.Count + " matches.");
                // ReSharper disable HeuristicUnreachableCode
                comboBox1.Items.Clear();
                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = richTextBox1.BackColor;
                foreach (var match1 in matches)
                {
                    richTextBox1.Select(match1.Index, match1.Length);
                    richTextBox1.SelectionBackColor = button2.BackColor;
                    richTextBox1.Select(0,0);
                    comboBox1.Items.Add(match1.Value);
                }
            }
            catch (Exception ex)
            {
                richTextBox2.AppendText(ex.Message);

            }
// ReSharper restore HeuristicUnreachableCode
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        //    richTextBox1.Select(0, richTextBox2.Text.Length);
        //    richTextBox1.SelectionBackColor = richTextBox2.BackColor;
            //richTextBox1.Select(0, 0);

        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            richTextBox1.Select(0, richTextBox1.Text.Length);
            richTextBox1.SelectionBackColor = richTextBox2.BackColor;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = (comboBox1.SelectedIndex+1) + "/" + (comboBox1.Items.Count-1);
            int count = 0;
            Match needed=null;
            foreach (var match in matches)
            {
                richTextBox1.Select(match.Index, match.Length);
                if (count++ == comboBox1.SelectedIndex)
                {
                    richTextBox1.SelectionBackColor = Color.OrangeRed;
                    needed = match;
                }
                else

                    richTextBox1.SelectionBackColor = button2.BackColor;
                richTextBox1.Select(0, 0);

            }
            if (needed != null)
            {
                richTextBox1.SelectionStart=needed.Index;
                richTextBox1.ScrollToCaret();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.SelectedIndex--;
                comboBox1_SelectedIndexChanged(sender,e);
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.SelectedIndex++;
                comboBox1_SelectedIndexChanged(sender, e);
            }
            catch
            {
            }
        }
    }
}
