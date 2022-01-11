using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Neuro;
using System.Windows.Forms;
using Neuro.Model;

namespace Neuro.Controller
{
    class Controller
    {
        public double[] inputs;
        public int[] hidden;
        int layers_at_all, outputs = 0, maximum;
        int[] biases;
        double highest, lenght, height, maximal_array_value, marv, count, delta, ch;
        double[] ax_X, hor_rects_inp, hor_rects_out;
        Layer hNodes = new Layer(0), last_h_Nodes;
        double[,] hor_rects_hidden;
        double vert_rects;
        char[] separators = new char[] { ' ', '\n' };
        float radius = 30;
        public bool flag = false;
        Pen pen, line_pen, line_v_pen;
        Brush brush, white_brush, yellow_brush, peachpuff_example;
        Bitmap bitmap, bitmap_layer;
        Graphics gr, gr_layer;
        Model.Net net;
        //Model.DeepInputOutputProgram net;

        public Controller()
        {
            pen = new Pen(Color.Black, 1.5f);
            line_pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
            line_v_pen = new Pen(Color.Red);
            white_brush = new SolidBrush(Color.White);
            yellow_brush = new SolidBrush(Color.Yellow);
            peachpuff_example = new SolidBrush(Color.PeachPuff);
        }

        public void Calculate()
        {
            try
            {
                net = new Net(inputs, hidden, outputs, biases);
            }
            catch (FormatException)
            {
                MessageBox.Show("Firstly build network");
                return;
            }

        }

        async public void show_layer_calculation(int layer_number)
        {
            maximal_array_value = inputs.Max();
            for (int neuron = 0; neuron < hor_rects_inp.Length; neuron++)
            {
                try
                {
                    gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * inputs[neuron] / maximal_array_value), 0, 0, 255)), (float)(ax_X[0] - radius), (float)(hor_rects_inp[neuron] - radius), 2 * radius, 2 * radius);
                }
                catch (IndexOutOfRangeException)
                {
                    gr.FillEllipse(yellow_brush, (float)(ax_X[0] - radius), (float)(hor_rects_inp[neuron] - radius), 2 * radius, 2 * radius);
                }

                Form1.instance.PictureBox1.Image = bitmap;
                await Task.Delay(150);
            }

            for (int layer = 0; layer < hidden.Length; layer++)
            {
                double maxx = net.hidden[layer].Max();
                last_h_Nodes = new Layer(hNodes.Length);
                last_h_Nodes = hNodes;
                hNodes = new Layer(net.hidden[layer].Length);
                Array.Copy(net.hidden[layer].layer, 0, hNodes.layer, 0, net.hidden[layer].Length);
                count = 1;
                delta = 0.2;
                ch = 1.1;
                for (int el = 0; el < hNodes.Length; el++)
                {
                    if (hNodes.layer[el].Activated_value != maxx)
                    {
                        hNodes.layer[el].Activated_value = hNodes.layer[el].Activated_value / count;
                        count *= ch;
                        ch += delta;
                    }

                }

                for (int neuron = 0; neuron < hidden[layer]; neuron++)
                {
                    if (layer == 0)
                    {
                        for (int old_neuron = 0; old_neuron < hor_rects_inp.Length; old_neuron++)
                        {
                            gr.DrawLine(line_v_pen, (float)ax_X[0], (float)hor_rects_inp[old_neuron], (float)ax_X[layer + 1], (float)hor_rects_hidden[neuron, layer]);     //рисование линий от предыдущего слоя до нынешнего

                            gr.FillEllipse(white_brush, (float)(ax_X[0] - radius), (float)(hor_rects_inp[old_neuron] - radius), 2 * radius, 2 * radius);
                            gr.FillEllipse(brush, (float)(ax_X[0] - radius), (float)(hor_rects_inp[old_neuron] - radius), 2 * radius, 2 * radius);
                            try
                            {
                                gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * inputs[old_neuron] / maximal_array_value), 0, 0, 255)), (float)(ax_X[0] - radius), (float)(hor_rects_inp[old_neuron] - radius), 2 * radius, 2 * radius);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                gr.FillEllipse(yellow_brush, (float)(ax_X[0] - radius), (float)(hor_rects_inp[old_neuron] - radius), 2 * radius, 2 * radius);
                            }


                            gr.FillEllipse(white_brush, (float)(ax_X[1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                            gr.FillEllipse(brush, (float)(ax_X[1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                            try
                            {
                                gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * inputs[old_neuron] / maximal_array_value), 255, 0, 0)), (float)(ax_X[1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                gr.FillEllipse(yellow_brush, (float)(ax_X[1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);

                            }
                            Form1.instance.PictureBox1.Image = bitmap;
                        }

                    }
                    else
                    {
                        marv = last_h_Nodes.Max();
                        for (int old_neuron = 0; old_neuron < maximum; old_neuron++)
                        {
                            if (hor_rects_hidden[old_neuron, layer - 1] == 0)
                            {
                                break;
                            }
                            else
                            {
                                gr.DrawLine(line_v_pen, (float)ax_X[layer], (float)hor_rects_hidden[old_neuron, layer - 1], (float)ax_X[layer + 1], (float)hor_rects_hidden[neuron, layer]);     //рисование линий от предыдущего слоя до нынешнего

                                //gr.FillEllipse(white_brush, (float)(ax_X[layer] - radius), (float)(hor_rects_hidden[old_neuron, layer - 1] - radius), 2 * radius, 2 * radius);
                                gr.FillEllipse(brush, (float)(ax_X[layer] - radius), (float)(hor_rects_hidden[old_neuron, layer - 1] - radius), 2 * radius, 2 * radius);
                                if (hor_rects_hidden[old_neuron + 1, layer - 1] == 0 && Array.Exists(this.biases, x => x == layer))
                                    gr.FillEllipse(yellow_brush, (float)(ax_X[layer] - radius), (float)(hor_rects_hidden[old_neuron, layer - 1] - radius), 2 * radius, 2 * radius);
                                else
                                    gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * last_h_Nodes.layer[old_neuron].Activated_value / marv), 255, 0, 0)), (float)(ax_X[layer] - radius), (float)(hor_rects_hidden[old_neuron, layer - 1] - radius), 2 * radius, 2 * radius);

                                //gr.FillEllipse(white_brush, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                                gr.FillEllipse(brush, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                                if (hor_rects_hidden[old_neuron + 1, layer - 1] == 0 && Array.Exists(this.biases, x => x == layer))
                                    gr.FillEllipse(yellow_brush, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                                else
                                    gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * last_h_Nodes.layer[old_neuron].Activated_value / maximal_array_value), 255, 0, 0)), (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);


                                //gr.FillEllipse(red_example, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);

                                Form1.instance.PictureBox1.Image = bitmap;
                            }
                        }

                    }

                    gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * hNodes.layer[neuron].Activated_value / maxx), 255, 0, 0)), (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                    await Task.Delay(150);
                }
                foreach (var y in hor_rects_out)
                {
                    gr.DrawEllipse(pen, (float)(ax_X[ax_X.Length - 1] - radius), (float)(y - radius), 2 * radius, 2 * radius);
                    gr.FillEllipse(white_brush, (float)(ax_X[ax_X.Length - 1] - radius), (float)(y - radius), 2 * radius, 2 * radius);
                }

            }

            maximal_array_value = net.output.Max();
            marv = hNodes.Max();
            count = 15;
            ch = 20;
            for (int el = 0; el < net.output.layer.Length - 1; el++)
            {
                if (net.output.layer[el].Activated_value != maximal_array_value)
                {
                    net.output.layer[el].Activated_value = net.output.layer[el].Activated_value / count;
                    count *= ch;
                }

            }
            for (int neuron = 0; neuron < hor_rects_out.Length; neuron++)
            {
                int len;
                bool flag = false;
                if (Array.Exists(this.biases, x => x == hidden.Length))
                {
                    len = hidden[hidden.Length - 1] + 1;
                    flag = true;
                }
                else
                    len = hidden[hidden.Length - 1];
                for (int old_neuron = 0; old_neuron < len; old_neuron++)
                {
                    gr.DrawLine(line_v_pen, (float)ax_X[ax_X.Length - 1], (float)hor_rects_out[neuron], (float)ax_X[ax_X.Length - 2], (float)hor_rects_hidden[old_neuron, hidden.Length - 1]);     //рисование линий от предыдущего слоя до нынешнего
                    if (flag && old_neuron == len - 1)
                        gr.FillEllipse(yellow_brush, (float)(ax_X[ax_X.Length - 2] - radius), (float)(hor_rects_hidden[old_neuron, hidden.Length - 1] - radius), 2 * radius, 2 * radius);
                    else
                    {
                        gr.FillEllipse(brush, (float)(ax_X[ax_X.Length - 2] - radius), (float)(hor_rects_hidden[old_neuron, hidden.Length - 1] - radius), 2 * radius, 2 * radius);
                        gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * hNodes.layer[old_neuron].Activated_value / marv), 255, 0, 0)), (float)(ax_X[ax_X.Length - 2] - radius), (float)(hor_rects_hidden[old_neuron, hidden.Length - 1] - radius), 2 * radius, 2 * radius);
                    }
                    gr.FillEllipse(brush, (float)(ax_X[ax_X.Length - 1] - radius), (float)(hor_rects_out[neuron] - radius), 2 * radius, 2 * radius);
                    gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * net.output.layer[neuron].Activated_value / maximal_array_value), 0, 255, 0)), (float)(ax_X[ax_X.Length - 1] - radius), (float)(hor_rects_out[neuron] - radius), 2 * radius, 2 * radius);
                    Form1.instance.PictureBox1.Image = bitmap;
                }
                gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * net.output.layer[neuron].Activated_value / maximal_array_value), 0, 255, 0)), (float)(ax_X[ax_X.Length - 1] - radius), (float)(hor_rects_out[neuron] - radius), 2 * radius, 2 * radius);
                await Task.Delay(150);
                Form1.instance.PictureBox1.Image = bitmap;
            }


        }

        public void light_neuron(int neuron)
        {
            try
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(255 * inputs[neuron] / maximal_array_value), 255, 0, 0)), (float)(ax_X[0] - radius), (float)(hor_rects_inp[neuron] - radius), 2 * radius, 2 * radius);
            }
            catch (System.IndexOutOfRangeException)
            {
                Form1.instance.pauser_count = -1;
                maximal_array_value = 0;
                return;
            }
            Form1.instance.PictureBox1.Image = bitmap;
        }

        public void Initialize_NN(string input, string h_l, string outs, string biases)
        {
            inputs = Array.ConvertAll(input.Split(separators), Double.Parse);
            hidden = h_l.Split(separators).Select(n => Convert.ToInt32(n)).ToArray();       //массив с количеством нейронов в каждом слое
            outputs = Convert.ToInt32(outs);
            try { this.biases = biases.Split(separators).Select(n => Convert.ToInt32(n)).ToArray(); } catch (FormatException) { this.biases = new int[0]; }
            lenght = Form1.instance.PictureBox1.Width;
            height = Form1.instance.PictureBox1.Height;
            layers_at_all = hidden.Length + 2;
            vert_rects = lenght / layers_at_all;
            ax_X = new double[layers_at_all];

            hor_rects_out = new double[outputs];

            int count = 0;
            for (double i = vert_rects / 2; i < lenght; i = i + vert_rects)
            {
                ax_X[count] = i;
                count++;
            }

            double highest;
            count = 0;
            if (Array.Exists(this.biases, x => x == 0))
            {
                hor_rects_inp = new double[inputs.Length + 1];
                highest = height / (inputs.Length + 1);
            }
            else
            {
                hor_rects_inp = new double[inputs.Length];
                highest = height / inputs.Length;
            }

            for (double i = highest / 2; i < height; i = i + highest)       //y для входного слоя
            {
                hor_rects_inp[count] = i;
                count++;
            }

            maximum = Math.Max(hidden.Max(), Math.Max(inputs.Length, outputs)) + 1;
            hor_rects_hidden = new double[maximum + 1, hidden.Length];
            int neuron;
            for (int layer = 0; layer < hidden.Length; layer++)
            {

                if (Array.Exists(this.biases, x => x == layer + 1))
                {
                    highest = height / (hidden[layer] + 1);
                }
                else
                {
                    highest = height / hidden[layer];
                }

                for (neuron = 0; neuron < hidden[layer]; neuron++)      //ось y для скрытых слоёв
                {
                    hor_rects_hidden[neuron, layer] = highest / 2 + highest * neuron;
                }

                if (Array.Exists(this.biases, x => x == layer + 1))
                {
                    hor_rects_hidden[neuron, layer] = highest / 2 + highest * neuron;
                }
            }

            count = 0;
            highest = height / outputs;
            for (double i = highest / 2; i < height; i = i + highest)
            {
                hor_rects_out[count] = i;
                count++;
            }

        }

        public Bitmap Draw()
        {
            bitmap = new Bitmap(Form1.instance.PictureBox1.Width, Form1.instance.PictureBox1.Width);
            gr = Graphics.FromImage(bitmap);


            foreach (var y in hor_rects_inp)    //визуализация входного слоя
            {
                gr.DrawEllipse(pen, (float)(ax_X[0] - radius), (float)(y - radius), 2 * radius, 2 * radius);
                gr.FillEllipse(brush, (float)(ax_X[0] - radius), (float)(y - radius), 2 * radius, 2 * radius);
            }

            int neuron, old_neuron, layer;
            for (layer = 0; layer < hidden.Length; layer++)
            {
                for (neuron = 0; neuron < hidden[layer]; neuron++)
                {
                    gr.DrawEllipse(pen, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                    gr.FillEllipse(brush, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                    if (layer == 0)
                    {
                        for (old_neuron = 0; old_neuron < hor_rects_inp.Length; old_neuron++)
                            gr.DrawLine(line_pen, (float)ax_X[0], (float)hor_rects_inp[old_neuron], (float)ax_X[layer + 1], (float)hor_rects_hidden[neuron, layer]);     //рисование линий от предыдущего слоя до нынешнего
                    }
                    else
                    {
                        for (old_neuron = 0; old_neuron < maximum; old_neuron++)
                            if (hor_rects_hidden[old_neuron, layer - 1] == 0) { break; }
                            else
                            {
                                gr.DrawLine(line_pen, (float)ax_X[layer], (float)hor_rects_hidden[old_neuron, layer - 1], (float)ax_X[layer + 1], (float)hor_rects_hidden[neuron, layer]);     //рисование линий от предыдущего слоя до нынешнего
                            }
                    }

                    if (layer == 1)
                    {
                        bool a = true;
                    }

                    if (Array.Exists(this.biases, x => x == layer))
                    {
                        if (layer == 0)
                            gr.DrawLine(line_pen, (float)ax_X[0], (float)hor_rects_inp[old_neuron - 1], (float)ax_X[1], (float)hor_rects_hidden[neuron, layer]);     //рисование линий от предыдущего слоя до 
                        else
                            gr.DrawLine(line_pen, (float)ax_X[layer], (float)hor_rects_hidden[old_neuron - 1, layer - 1], (float)ax_X[layer + 1], (float)hor_rects_hidden[neuron, layer]);     //рисование линий от предыдущего слоя до нынешнего

                    }
                }

                try
                {
                    if (Array.Exists(this.biases, x => x == layer + 1))
                    {
                        gr.DrawEllipse(pen, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                        gr.FillEllipse(brush, (float)(ax_X[layer + 1] - radius), (float)(hor_rects_hidden[neuron, layer] - radius), 2 * radius, 2 * radius);
                    }
                }
                catch (System.IndexOutOfRangeException) { }

            }

            foreach (var y in hor_rects_out)
            {
                int amount;
                if (Array.Exists(this.biases, x => x == layer))
                    amount = hidden[hidden.Length - 1] + 1;
                else
                    amount = hidden[hidden.Length - 1];
                for (old_neuron = 0; old_neuron < amount; old_neuron++)
                    gr.DrawLine(line_pen, (float)ax_X[ax_X.Length - 2], (float)hor_rects_hidden[old_neuron, hidden.Length - 1], (float)ax_X[ax_X.Length - 1], (float)y);     //рисование линий от предыдущего слоя до нынешнего
                gr.DrawEllipse(pen, (float)(ax_X[ax_X.Length - 1] - radius), (float)(y - radius), 2 * radius, 2 * radius);
                gr.FillEllipse(white_brush, (float)(ax_X[ax_X.Length - 1] - radius), (float)(y - radius), 2 * radius, 2 * radius);
            }

            return bitmap;
        }

    }
}
