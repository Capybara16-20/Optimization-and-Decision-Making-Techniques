using System;
using System.ComponentModel;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class InputNumber : Form
    {
        private HierarchyAnalyzer analyzer;
        private int index;

        public InputNumber(HierarchyAnalyzer analyzer, string message, int index)
        {
            this.analyzer = analyzer;
            this.index = index;

            InitializeComponent();

            label1.Text = message;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int x) && (x > 1) && (x <= 10))
            {
                errorProvider1.Clear();
                if (index < 0)
                    analyzer.n = x;
                else
                    analyzer.m = x;
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                textBox1.Select(0, textBox1.Text.Length);
                errorProvider1.SetError(textBox1, "Введите целое число > 1, <= 10");
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            Visible = false;

            if (index < 0)
            {
                analyzer.criteriaMatrix = new double[analyzer.n, analyzer.n];

                index = 0;
                new Entering(analyzer, "K", -1).ShowDialog();
            }
            else
            {
                analyzer.alternativesCube = new double[analyzer.n, analyzer.m, analyzer.m];

                for (int i = 0; i < analyzer.n; i++)
                {
                    new Entering(analyzer, "B", i).ShowDialog();
                }

                new Result(analyzer).ShowDialog();

                Environment.Exit(0);
            }
        }
    }
}
