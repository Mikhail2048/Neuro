using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neuro.Controller;

namespace Neuro
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Bitmap bitmap;
        Controller.Controller controller;
        public int ticks_count = 0, pauser_count = 0;
        int weights_at_all;

        private double[] test_x_vector;
        private double[] test_y_vector;

        private double[] inputVector;
        private double[] expectedOutputVector;

        public static readonly Form1 instance = new Form1();

        public static Form1 Instance
        {
            get { return instance; }
        }

        public PictureBox PictureBox1
        {
            get { return instance.pictureBox1; }
        }
        

        private Form1()
        {
            InitializeComponent();
            controller = new Controller.Controller();
            this.bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.gr = Graphics.FromImage(bitmap);

            var objChart = this.chart.ChartAreas[0];
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            resizeChart(-95, 95, -95, 95);
        }

        private void addChartSeries()
        {
            chart.Series.Clear();

            chart.Series.Add("model");
            chart.Series["model"].Color = Color.Orange;
            chart.Series["model"].ChartArea = "ChartArea1";
            chart.Series["model"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            chart.Series.Add("fitness_function");
            chart.Series["fitness_function"].Color = Color.Blue;
            chart.Series["fitness_function"].ChartArea = "ChartArea1";
            chart.Series["fitness_function"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void resizeChart(double minY, double maxY, double minX, double maxX) {
            var objChart = this.chart.ChartAreas[0];

            objChart.AxisX.Minimum = Math.Min(objChart.AxisX.Minimum, Math.Round(minX + (minX * 0.15)));
            objChart.AxisX.Maximum = Math.Max(objChart.AxisX.Maximum, Math.Round(maxX + (maxX * 0.15)));

            objChart.AxisY.Minimum = Math.Min(objChart.AxisY.Minimum, Math.Round(minY + (minY * 0.15)));
            objChart.AxisY.Maximum = Math.Max(objChart.AxisY.Maximum, Math.Round(maxY + (maxY * 0.15)));

            objChart.AxisY.RoundAxisValues();
            objChart.AxisX.RoundAxisValues();
        }

        /**
         * 1) Add values to the grid
         */
        private void visualize_Click(object sender, EventArgs e)
        {
            addChartSeries();
            weights_at_all = vector_y.Text.Split(' ').Select(n => Convert.ToInt32(n)).ToArray().Length + 1;
            if (vector_x.Text == "" || vector_y.Text == "")
            {
                MessageBox.Show("Not all fields filled");
                return;
            }

            inputVector = vector_x.Text.Split(' ').Select(it => Convert.ToDouble(it)).ToArray();
            expectedOutputVector = vector_y.Text.Split(' ').Select(it => Convert.ToDouble(it)).ToArray();

            resizeChart(expectedOutputVector.Min(), expectedOutputVector.Max(), inputVector.Min(), inputVector.Max());

            controller.Initialize_NN(vector_x.Text, "3", Convert.ToString(expectedOutputVector.Length), vector_y.Text);

            bitmap = controller.Draw();
            pictureBox1.Image = bitmap;

            for (int i = 0; i < inputVector.Length; i++)
            {
                chart.Series["model"].Points.AddXY(inputVector[i], expectedOutputVector[i]);
            }
            controller.addValues(inputVector, expectedOutputVector);
        }

        private void model_fitness_Click(object sender, EventArgs e)
        {
            chart.Series["fitness_function"].Points.Clear();
            double predicted_y_first = controller.PredictY(chart.ChartAreas[0].AxisX.Minimum);
            double predicted_y_second = controller.PredictY(chart.ChartAreas[0].AxisX.Maximum);
            chart.Series["fitness_function"].Points.AddXY(chart.ChartAreas[0].AxisX.Minimum, predicted_y_first);
            chart.Series["fitness_function"].Points.AddXY(chart.ChartAreas[0].AxisX.Maximum, predicted_y_second);
        }

        private void model_add_values(object sender, EventArgs e)
        {
            double[] x_vector = predicted_x.Text.Trim().Split(' ').Select(str => Convert.ToDouble(str)).ToArray();
            double[] y_vector = predicted_y.Text.Trim().Split(' ').Select(str => Convert.ToDouble(str)).ToArray();

            test_x_vector = x_vector;
            test_y_vector = y_vector;

            chart.Series.Add("test");
            chart.Series["test"].Color = Color.Green;
            chart.Series["test"].ChartArea = "ChartArea1";
            chart.Series["test"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            resizeChart(y_vector.Min(), y_vector.Max(), x_vector.Min(), x_vector.Max());

            model_fitness_Click(null, null);

            for (int i = 0; i < x_vector.Length; i++)
            {
                chart.Series["test"].Points.AddXY(x_vector[i], y_vector[i]);
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void predicted_y_TextChanged(object sender, EventArgs e)
        {

        }

        private void vector_x_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void vector_y_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void model_evoluation_Click(object sender, EventArgs e)
        {
            string devianse_prefix = "devianse";
            double[] difference_array = new double[test_x_vector.Length];
            
            double maxYCandidate = Double.MinValue;
            double minYCandidate = Double.MaxValue;

            for (int i = 0; i < test_x_vector.Length; i++)
            {
                string deviance_chart_serices_name = devianse_prefix + i;
                double predicted_y = controller.PredictY(test_x_vector[i]);
                chart.Series.Add(deviance_chart_serices_name);
                chart.Series[deviance_chart_serices_name].Color = Color.Red;
                chart.Series[deviance_chart_serices_name].ChartArea = "ChartArea1";
                chart.Series[deviance_chart_serices_name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart.Series[deviance_chart_serices_name].IsVisibleInLegend = false;
                chart.Series[deviance_chart_serices_name].Points.AddXY(test_x_vector[i], test_y_vector[i]);
                chart.Series[deviance_chart_serices_name].Points.AddXY(test_x_vector[i], predicted_y);
                maxYCandidate = Math.Max(Math.Max(predicted_y, test_y_vector[i]), maxYCandidate);
                minYCandidate = Math.Min(Math.Min(predicted_y, test_y_vector[i]), minYCandidate);
                difference_array[i] = predicted_y - test_y_vector[i];
            }

            resizeChart(minYCandidate, maxYCandidate, test_x_vector.Min(), test_x_vector.Max());

            chart.Series.Add(devianse_prefix);
            chart.Series[devianse_prefix].Color = Color.Red;
            chart.Series[devianse_prefix].ChartArea = "ChartArea1";
            chart.Series[devianse_prefix].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            calculate_mean_error(difference_array);
        }

        private void calculate_mean_error(double[] difference_array)
        {
            double total_sum = 0; 
            for (int i = 0; i < difference_array.Length; i++)
            {
                total_sum += Math.Pow(difference_array[i], 2);
            }
            mean_squared_error_label.Text = "Mean Squared Error: " + Math.Round(Math.Sqrt(total_sum / difference_array.Length), 2);
            mean_squared_error_label.AutoSize = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}