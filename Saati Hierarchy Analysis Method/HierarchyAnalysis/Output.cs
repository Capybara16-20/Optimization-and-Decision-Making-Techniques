using System;
using System.Globalization;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class Output : Form
    {
        private HierarchyAnalyzer analyzer;
        private readonly int index;

        public Output(HierarchyAnalyzer analyzer, string name, int index)
        {
            this.analyzer = analyzer;
            this.index = index;

            InitializeComponent();

            if (index < 0)
            {
                Text = "Критерии";
                label1.Text = "Соотношение критериев";

                for (int i = 0; i < analyzer.n; i++)
                {
                    dataGridView1.Columns.Add(i.ToString(), name + (i + 1).ToString());
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].HeaderCell.Value = name + (i + 1).ToString();
                }
            }
            else
            {
                Text = "Альтернативы";
                label1.Text = "Соотношение альтернатив по критерию " + (index + 1).ToString();

                for (int i = 0; i < analyzer.m; i++)
                {
                    dataGridView1.Columns.Add(i.ToString(), name + (i + 1).ToString());
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].HeaderCell.Value = name + (i + 1).ToString();
                }
            }

            var column = new DataGridViewTextBoxColumn()
            {
                Name = "importance",
                HeaderText = "Важность"
            };
            dataGridView1.Columns.Add(column);

            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Height = dataGridView1.Rows[0].Height * dataGridView1.Rows.Count + 1;
            dataGridView1.Width = dataGridView1.Columns[0].Width * dataGridView1.Columns.Count + 52;

            Width = dataGridView1.Width + 50;
            Height = dataGridView1.Height + 110;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AllowUserToAddRows = false;

            if (index < 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                    {
                        dataGridView1[j, i].Value = analyzer.criteriaMatrix[i, j].ToString("0.###");
                        dataGridView1[j, i].ReadOnly = true;
                    }
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[dataGridView1.RowCount, i].Value = analyzer.GetImportance(analyzer.criteriaMatrix)[i].ToString("P", CultureInfo.InvariantCulture);
                    dataGridView1[dataGridView1.RowCount, i].ReadOnly = true;
                }

                label2.Text += analyzer.GetConsistencyIndex(analyzer.criteriaMatrix).ToString("0.###");
            }
            else
            {
                double[,] matrix = new double[analyzer.m, analyzer.m];
                for (int i = 0; i < analyzer.m; i++)
                {
                    for (int j = 0; j < analyzer.m; j++)
                    {
                        matrix[i, j] = analyzer.alternativesCube[index, i, j];
                    }
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                    {
                        dataGridView1[j, i].Value = matrix[i, j].ToString("0.###");
                        dataGridView1[j, i].ReadOnly = true;
                    }
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[dataGridView1.RowCount, i].Value = analyzer.GetImportance(matrix)[i].ToString("P", CultureInfo.InvariantCulture);
                    dataGridView1[dataGridView1.RowCount, i].ReadOnly = true;
                }

                label2.Text += analyzer.GetConsistencyIndex(matrix).ToString("0.###");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index < 0)
            {
                if (analyzer.GetConsistencyRelation(analyzer.criteriaMatrix) >= 0.1)
                {
                    string message = string.Format("Отношение согласованности: {0:0.###} > 0,1. Следует пересмотреть оценки критериев.", 
                        analyzer.GetConsistencyRelation(analyzer.criteriaMatrix));
                    MessageBox.Show(message);
                    Visible = false;
                    new Entering(analyzer, "K", -1).ShowDialog();
                }
                else
                {
                    string message = string.Format("Отношение согласованности: {0:0.###} < 0,1. Оценки согласованны.", 
                        analyzer.GetConsistencyRelation(analyzer.criteriaMatrix));
                    MessageBox.Show(message);

                    Visible = false;
                    new InputNumber(analyzer, "Введите количество альтернатив", 0).ShowDialog();
                }
            }
            else
            {
                double[,] matrix = new double[analyzer.m, analyzer.m];
                for (int i = 0; i < analyzer.m; i++)
                {
                    for (int j = 0; j < analyzer.m; j++)
                    {
                        matrix[i, j] = analyzer.alternativesCube[index, i, j];
                    }
                }

                if (analyzer.GetConsistencyRelation(matrix) >= 0.1)
                {
                    string message = string.Format("Отношение согласованности: {0:0.###} > 0,1. Следует пересмотреть оценки критериев.",
                        analyzer.GetConsistencyRelation(matrix));
                    MessageBox.Show(message);
                    Visible = false;
                    new Entering(analyzer, "B", index).ShowDialog();
                }
                else
                {
                    string message = string.Format("Отношение согласованности: {0:0.###} < 0,1. Оценки согласованны.",
                        analyzer.GetConsistencyRelation(matrix));
                    MessageBox.Show(message);

                    Visible = false;
                }
            }
        }
    }
}
