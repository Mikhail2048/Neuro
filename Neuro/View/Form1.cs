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

        public static readonly Form1 instance = new Form1();

        public static Form1 Instance
        {
            get { return instance; }
        }

        public PictureBox PictureBox1
        {
            get { return instance.pictureBox1; }
        }

        public Timer timer
        {
            get { return instance.timer1; }
        }

        public Timer Pauser
        {
            get { return instance.pauser; }
        }

        public ProgressBar ProgressBar
        {
            get { return instance.progressBar1; }
        }

        private Form1()
        {
            InitializeComponent();
            controller = new Controller.Controller();
            this.bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.gr = Graphics.FromImage(bitmap);
        }

        private void visualize_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            weights_at_all = hidden_layers.Text.Split(' ').Select(n => Convert.ToInt32(n)).ToArray().Length + 1;
            if (input_field.Text == "" || hidden_layers.Text == "" || number_of_output_neurons.Text == "")
            {
                MessageBox.Show("Not all fields filled");
                return;
            }
            controller.Initialize_NN(input_field.Text, hidden_layers.Text, number_of_output_neurons.Text, count_of_biases.Text);
            bitmap = controller.Draw();
            this.pictureBox1.Image = bitmap;
        }

        private void make_calculations_Click(object sender, EventArgs e)
        {
            controller.Calculate();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            if (ticks_count == weights_at_all + 1)
            {
                timer1.Stop();
                return;
            }

            controller.show_layer_calculation(ticks_count);
            //this.pictureBox1.Image = bitmap;

            ticks_count++;
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

        private void pauser_Tick(object sender, EventArgs e)
        {
            controller.light_neuron(pauser_count);
            pauser_count++;
        }


    }
}