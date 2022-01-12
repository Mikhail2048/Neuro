using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro.Model
{

    //class DeepNetProgramm
    //{

    //void Main(string[] args)
    //{
    //[] inputs = new double[] { 1, 2, 3, 7, 6, 10 };
    //int[] neurons_in_layers = new int[] { 10, 4, 5, 6, 7, 8, 1 };
    //int num_outputs = 5;
    //int[] biases_in_layers = new int[] { };
    //Net net = new Net(inputs, neurons_in_layers, num_outputs, biases_in_layers);
    //net.print_net();
    //Console.ReadKey();
    //}

    //}

    class Net
    {
        public int num_outputs;
        public int[] neurons_in_layers, biases_in_layers;
        public double[] inputs;
        public Input_layer input;
        public Layer[] hidden;
        public Layer output;
        public Weights IH_weights;
        public Weights[] H_weights;

        public double[] xVals, yVals;
        private static int xIndex;
        private static int yIndex;

        public double standardDevX, standardDevY, correlation, slope, interception;

        public Net(double[] inputs, int[] neurons_in_layers, int num_outputs, int[] biases_in_layers)
        {
            this.inputs = inputs;
            this.neurons_in_layers = neurons_in_layers;
            this.num_outputs = num_outputs;
            this.biases_in_layers = biases_in_layers;

            int amount_of_layers = neurons_in_layers.Length + 2;
            int amount_of_weights = amount_of_layers - 1;
            int num_hidden;

            input = new Input_layer(inputs);
            hidden = new Layer[neurons_in_layers.Length];
            output = new Layer(num_outputs, "out");


            IH_weights = new Weights(input.Length, neurons_in_layers[0]);
            H_weights = new Weights[amount_of_weights - 1];
            for (int i = 0; i < H_weights.Length; i++)
            {
                try
                {
                    H_weights[i] = new Weights(neurons_in_layers[i] + 1, neurons_in_layers[i + 1]);
                }
                catch (IndexOutOfRangeException)
                {
                    H_weights[i] = new Weights(neurons_in_layers[i] + 1, num_outputs);

                }
            }


            for (int i = 0; i < hidden.Length + 1; i++)
            {
                try
                {
                    hidden[i] = hidden[i - 1] * H_weights[i - 1];

                }
                catch (IndexOutOfRangeException)
                {
                    if (hidden.Length != i)
                    {
                        hidden[0] = input * IH_weights;
                    }
                }
                if (Array.Exists(biases_in_layers, x => x == i))
                {
                    try
                    {
                        hidden[i - 1].set_bias(true);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        input.set_bias(true);
                    }

                }
            }


            output = hidden[hidden.Length - 1] * H_weights[H_weights.Length - 1];
            output.out_l = true;

            xVals = new double[100];
            yVals = new double[100];
            xIndex = 0;
            yIndex = 0;
        }

        public void print_net()
        {
            input.print();
            IH_weights.print();
            for (int i = 0; i < hidden.Length; i++)
            {
                hidden[i].print();
                H_weights[i].print();
            }

            output.print();
        }

        public void addValues(double[] xValuesToAdd, double[] yValuesToAdd)
        {
            int i = 0;
            for (; xIndex < xVals.Length && i < xValuesToAdd.Length; xIndex++)
            {
                this.xVals[xIndex] = xValuesToAdd[i];
                i++;
            }

            i = 0;
            for (; yIndex < yVals.Length && i < yValuesToAdd.Length; yIndex++)
            {
                this.yVals[yIndex] = yValuesToAdd[i];
                i++;
            }
            standardDevX = GetStandardDeviation(xVals, xIndex);
            standardDevY = GetStandardDeviation(yVals, yIndex);
            correlation = GetCorrelation(xVals, yVals);
            slope = correlation * standardDevY / standardDevX;
            interception = GetArrMean(yVals, yIndex) - slope * GetArrMean(xVals, xIndex);
        }

        public double PredictY(double x)
        {
            return interception + slope * x;
        }

        private static double GetArrMean(double[] arr, int borderIndex)
        {
            return arr.Sum() / borderIndex;
        }

        private static double GetStandardDeviation(double[] arr, int borderIndex)
        {
            double mean = GetArrMean(arr, borderIndex);
            double[] devs = new double[borderIndex];
            for (int i = 0; i < borderIndex; i++)
            {
                devs[i] = Math.Pow(arr[i] - mean, 2);
            }
            return Math.Sqrt(devs.Sum() / (devs.Length - 1));
        }

        private static double GetCorrelation(double[] X, double[] Y)
        {
            double XMean = X.Sum() / xIndex;
            double YMean = Y.Sum() / yIndex;
            double[] x = new double[xIndex];
            double[] y = new double[yIndex];

            for (int i = 0; i < xIndex; i++)
            {
                x[i] = X[i] - XMean;
                y[i] = Y[i] - YMean;
            }

            double[] xy = new double[xIndex];
            for (int i = 0; i < xIndex; i++)
            {
                xy[i] = x[i] * y[i];
            }

            double[] xPowed = new double[xIndex];
            double[] yPowed = new double[yIndex];

            for (int i = 0; i < xIndex; i++)
            {
                xPowed[i] = Math.Pow(x[i], 2);
                yPowed[i] = Math.Pow(y[i], 2);
            }

            return xy.Sum() / Math.Sqrt(xPowed.Sum() * yPowed.Sum());
        }

    }

    class Bias_Neuron
    {
        protected double val;

        public double Val
        {
            get { return val; }
            set { val = value; }
        }

        public Bias_Neuron(bool exists)
        {
            switch (exists)
            {
                case true:
                    val = 1;
                    break;
                default:
                    val = 0;
                    break;
            }
        }


        public Bias_Neuron() { val = 0; }
    }   //примитивный нейрон смещения со значением 0 или 1

    class Input_Neuron : Bias_Neuron
    {

        public Input_Neuron(double val)
        {
            this.val = val;
        }

        public Input_Neuron() { }

    }   //входной нейрон, может значение  какое угодно значение

    class Neuron : Input_Neuron
    {
        double activated_value;

        public double Activated_value
        {
            get { return activated_value; }
            set { activated_value = value; }
        }

        public Neuron() { }
        public Neuron(double val) : base(val) { }

        public void activate()
        {
            activated_value = 1 / (1 + Math.Exp(-val));
        }
    }   //обычный нейрон, может применить активироваться с помощью сигмоиды. activated_value - значение нейрона после активации

    abstract class Pre_layer
    {
        protected Bias_Neuron bias_neuron;
        public Pre_layer(bool bias)
        {
            bias_neuron = new Bias_Neuron(bias);
        } //первая перегрузка конструктора. Без входных значений, просто нейрон смещения.
        public Pre_layer() { }

        public abstract void print();

        protected Input_Neuron ConverTo_Input_neuron(Bias_Neuron b)
        {
            return new Input_Neuron(b.Val);
        }
        protected Neuron ConverTo_Neuron(Bias_Neuron b)
        {
            return new Neuron(b.Val);
        }

    }

    class Input_layer : Pre_layer
    {
        public Input_Neuron[] layer;

        public int Length
        {
            get { return layer.Length; }
        }

        public Input_layer(bool bias = false) : base(bias) { }
        public Input_layer(double[] inputs, bool bias = false) : base(bias)// вторая перегрузка. С входными значениями - заполнение слоя
        {
            layer = new Input_Neuron[inputs.Length + 1];
            layer[layer.Length - 1] = ConverTo_Input_neuron(bias_neuron);
            for (int i = 0; i < inputs.Length; i++)
            {
                layer[i] = new Input_Neuron(inputs[i]);
            }
        } // вторая перегрузка. С входными значениями - заполнение слоя

        public void set_bias(bool bias)
        {
            layer[layer.Length - 1] = ConverTo_Input_neuron(new Bias_Neuron(bias));
        }

        public static Layer operator *(Input_layer I_l, Weights w)
        {
            Layer l = new Layer(w.Weight_matrix[0].Length - 1);
            if (I_l.layer.Length != w.Weight_matrix.Length)
            {
                throw new Exception("Размерность слоя и матрицы весов не совпадают");
            }
            else
            {
                for (int str = 0; str < w.Weight_matrix.Length; str++)
                {
                    for (int el = 0; el < w.Weight_matrix[str].Length - 1; el++)
                    {
                        l.A_Layer[el].Val += I_l.layer[str].Val * w.Weight_matrix[str][el];
                    }
                }
            }
            l.activate_layer();
            return l;
        }

        public override void print()
        {
            if (layer == null)
            {
                Console.WriteLine("no neuron values");
                return;
            }

            for (int i = 0; i < layer.Length; i++)
            {
                Console.Write(layer[i].Val + " ");
            }
            Console.Write("\n");

        }
        public void set_layer(double[] inputs)  //заполнение слоя
        {
            layer = new Input_Neuron[inputs.Length];
            layer[layer.Length - 1] = ConverTo_Input_neuron(bias_neuron);
            for (int num = 0; num < inputs.Length; num++)
            {
                layer[num] = new Input_Neuron(inputs[num]);
            }
        }
    }

    class Layer : Pre_layer
    {
        public Neuron[] layer;
        public bool out_l = false;

        public Neuron[] A_Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public int Length
        {
            get { return layer.Length; }
        }

        public Layer(int number_of_neurons, bool bias = false) : base(bias)
        {
            layer = new Neuron[number_of_neurons + 1];
            layer[layer.Length - 1] = ConverTo_Neuron(bias_neuron);
            for (int i = 0; i < layer.Length - 1; i++)
            {
                layer[i] = new Neuron();
            }
        }

        public Layer(int number_of_neurons, string output, bool bias = false) : base(bias)
        {
            if (output == "out")
            {
                out_l = true;
                layer = new Neuron[number_of_neurons];
                for (int i = 0; i < layer.Length - 1; i++)
                {
                    layer[i] = new Neuron();
                }
            }
            else
                throw new Exception($"not {"out"} marker");
        }

        public void set_bias(bool bias)
        {
            layer[layer.Length - 1] = ConverTo_Neuron(new Bias_Neuron(bias));
            layer[layer.Length - 1].Activated_value = layer[layer.Length - 1].Val;
        }

        public static Layer operator *(Layer l, Weights w)
        {
            Layer next_layer = new Layer(w.Weight_matrix[0].Length - 1);
            if (l.layer.Length != w.Weight_matrix.Length)
            {
                throw new Exception("Размерность слоя и матрицы весов не совпадают");
            }
            else
            {
                for (int str = 0; str < w.Weight_matrix.Length; str++)
                {
                    for (int el = 0; el < w.Weight_matrix[str].Length - 1; el++)
                    {
                        next_layer.A_Layer[el].Val += l.layer[str].Activated_value * w.Weight_matrix[str][el];
                    }
                }
            }
            next_layer.activate_layer();
            return next_layer;
        }

        public double Max()
        {
            double maxx = double.MinValue;
            foreach (var neuron in layer)
            {
                if (maxx < neuron.Activated_value)
                {
                    maxx = neuron.Activated_value;
                }
            }
            return maxx;
        }

        public override void print()
        {
            if (layer == null)
            {
                Console.WriteLine("no neuron values");
                return;
            }

            Console.Write("\n");
            if (out_l)
            {
                for (int i = 0; i < layer.Length - 1; i++)
                {
                    Console.Write(layer[i].Activated_value + " ");
                }
                Console.Write("\n");
            }
            else
                for (int i = 0; i < layer.Length; i++)
                {
                    Console.Write(layer[i].Activated_value + " ");
                }
            Console.Write("\n");

        }
        public void activate_layer()
        {
            for (int num = 0; num < layer.Length - 1; num++)
            {
                layer[num].activate();
            }
            layer[layer.Length - 1].Activated_value = layer[layer.Length - 1].Val;
        }

    }

    class Weights
    {
        Random rand = new Random();
        double[][] weight_matrix;

        public double[][] Weight_matrix
        {
            get { return weight_matrix; }
        }

        public Weights(int num_of_strings, int length_strings)
        {
            weight_matrix = new double[num_of_strings][];
            for (int i = 0; i < num_of_strings; i++)
            {
                weight_matrix[i] = new double[length_strings + 1];
                //weight_matrix[i] = new double[length_strings];

            }
            set_randow_weights();
        }

        public void set_randow_weights()
        {
            foreach (var str in weight_matrix)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    str[i] = rand.NextDouble();
                }
            }
        }

        public void print()
        {
            Console.Write("\n");
            foreach (var str in weight_matrix)
            {
                for (int i = 0; i < str.Length - 1; i++)
                {
                    Console.Write(str[i] + " ");
                }
                Console.Write("\n");
            }
        }
    }
}