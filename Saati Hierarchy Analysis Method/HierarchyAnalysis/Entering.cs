using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class Entering : Form
    {
        private HierarchyAnalyzer analyzer;
        private readonly int index;

        public Entering(HierarchyAnalyzer analyzer, string name, int index)
        {
            this.analyzer = analyzer;
            this.index = index;

            InitializeComponent();

            if (index < 0)
            {
                Text = "Отношения критериев";

                for (int i = 0; i < analyzer.n; i++)
                {
                    dataGridView1.Columns.Add(i.ToString(), name + (i + 1).ToString());
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].HeaderCell.Value = name + (i + 1).ToString();
                }
            }
            else
            {
                Text = "Отношения альтернатив по критерию " + (index + 1);

                for (int i = 0; i < analyzer.m; i++)
                {
                    dataGridView1.Columns.Add(i.ToString(), name + (i + 1).ToString());
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].HeaderCell.Value = name + (i + 1).ToString();
                }
            }

            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Height = dataGridView1.Rows[0].Height * dataGridView1.Rows.Count + 1;
            dataGridView1.Width = dataGridView1.Columns[0].Width * dataGridView1.Columns.Count + 52;

            Width = dataGridView1.Width + 40;
            Height = dataGridView1.Height + 100;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AllowUserToAddRows = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (i <= j)
                    {
                        dataGridView1[i, j].ReadOnly = true;
                        dataGridView1[i, j].Style.BackColor = Color.Gray;
                    }
                }
            }
        }

        private void dataGridView1_Validating(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (i > j)
                    {
                        string value = dataGridView1[i, j].Value.ToString();
                        if ((value != string.Empty) && int.TryParse(value, out int x) && (x > 0) && (x <= 9))
                        {
                            errorProvider1.Clear();
                            e.Cancel = false;
                        }
                        else
                        {
                            e.Cancel = true;
                            errorProvider1.SetError(dataGridView1, "Введите целые числа > 0, < 10");

                            for (int k = 0; k < dataGridView1.RowCount; k++)
                                for (int v = 0; v < dataGridView1.ColumnCount; v++)
                                    if (k > v)
                                        dataGridView1[k, v].Value = string.Empty;
                            return;
                        }
                    }
                }
            }
        }

        private void dataGridView1_Validated(object sender, EventArgs e)
        {
            Visible = false;

            if (index < 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (i > j)
                            analyzer.criteriaMatrix[j, i] = int.Parse(dataGridView1[i, j].Value.ToString());

                for (int i = 0; i < analyzer.criteriaMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < analyzer.criteriaMatrix.GetLength(1); j++)
                    {
                        if (i == j)
                            analyzer.criteriaMatrix[i, j] = 1;
                        else if (i > j)
                        {
                            analyzer.criteriaMatrix[i, j] = 1 / analyzer.criteriaMatrix[j, i];
                        }
                    }
                }

                new Output(analyzer, "K", -1).ShowDialog();
            }
            else
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (i > j)
                            analyzer.alternativesCube[index, j, i] = int.Parse(dataGridView1[i, j].Value.ToString());

                for (int i = 0; i < analyzer.m; i++)
                {
                    for (int j = 0; j < analyzer.m; j++)
                    {
                        if (i == j)
                            analyzer.alternativesCube[index, i, j] = 1;
                        else if (i > j)
                        {
                            analyzer.alternativesCube[index, i, j] = 1 / analyzer.alternativesCube[index, j, i];
                        }
                    }
                }

                new Output(analyzer, "B", index).ShowDialog();
            }
        }
    }
}
