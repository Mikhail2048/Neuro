
namespace Neuro
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.visualize = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.predicted_x = new System.Windows.Forms.TextBox();
            this.vector_y = new System.Windows.Forms.TextBox();
            this.vector_x = new System.Windows.Forms.TextBox();
            this.model_fitness = new System.Windows.Forms.Button();
            this.model_prediction = new System.Windows.Forms.Button();
            this.predicted_y = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.model_evoluation = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(635, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(727, 563);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "1. Input and Output ";
            this.label5.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(142, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "x = ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(142, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "y = ";
            // 
            // visualize
            // 
            this.visualize.BackColor = System.Drawing.Color.Bisque;
            this.visualize.Location = new System.Drawing.Point(451, 65);
            this.visualize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.visualize.Name = "visualize";
            this.visualize.Size = new System.Drawing.Size(178, 39);
            this.visualize.TabIndex = 6;
            this.visualize.Text = "Построить модель";
            this.visualize.UseVisualStyleBackColor = false;
            this.visualize.Click += new System.EventHandler(this.visualize_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "2. ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // predicted_x
            // 
            this.predicted_x.Location = new System.Drawing.Point(223, 334);
            this.predicted_x.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.predicted_x.Name = "predicted_x";
            this.predicted_x.Size = new System.Drawing.Size(160, 26);
            this.predicted_x.TabIndex = 8;
            this.predicted_x.Text = "7 8 9";
            // 
            // vector_y
            // 
            this.vector_y.Location = new System.Drawing.Point(223, 99);
            this.vector_y.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vector_y.Name = "vector_y";
            this.vector_y.Size = new System.Drawing.Size(160, 26);
            this.vector_y.TabIndex = 9;
            this.vector_y.Text = "1 2 3 4 5";
            this.vector_y.TextChanged += new System.EventHandler(this.vector_y_TextChanged);
            // 
            // vector_x
            // 
            this.vector_x.Location = new System.Drawing.Point(223, 65);
            this.vector_x.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vector_x.Name = "vector_x";
            this.vector_x.Size = new System.Drawing.Size(160, 26);
            this.vector_x.TabIndex = 10;
            this.vector_x.Text = "1 2 3 4 5";
            this.vector_x.TextChanged += new System.EventHandler(this.vector_x_TextChanged);
            // 
            // model_fitness
            // 
            this.model_fitness.BackColor = System.Drawing.Color.Bisque;
            this.model_fitness.Location = new System.Drawing.Point(92, 160);
            this.model_fitness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.model_fitness.Name = "model_fitness";
            this.model_fitness.Size = new System.Drawing.Size(178, 39);
            this.model_fitness.TabIndex = 11;
            this.model_fitness.Text = "Model Fitness";
            this.model_fitness.UseVisualStyleBackColor = false;
            this.model_fitness.Click += new System.EventHandler(this.model_fitness_Click);
            // 
            // model_prediction
            // 
            this.model_prediction.BackColor = System.Drawing.Color.Bisque;
            this.model_prediction.Location = new System.Drawing.Point(451, 356);
            this.model_prediction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.model_prediction.Name = "model_prediction";
            this.model_prediction.Size = new System.Drawing.Size(178, 39);
            this.model_prediction.TabIndex = 11;
            this.model_prediction.Text = "Prediction";
            this.model_prediction.UseVisualStyleBackColor = false;
            this.model_prediction.Click += new System.EventHandler(this.model_fitness_Click);
            // 
            // predicted_y
            // 
            this.predicted_y.Location = new System.Drawing.Point(223, 368);
            this.predicted_y.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.predicted_y.Name = "predicted_y";
            this.predicted_y.Size = new System.Drawing.Size(160, 26);
            this.predicted_y.TabIndex = 16;
            this.predicted_y.Text = "7 8 9";
            this.predicted_y.TextChanged += new System.EventHandler(this.predicted_y_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(10, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 30);
            this.label4.TabIndex = 15;
            this.label4.Text = "3. ";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(99, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 26);
            this.label6.TabIndex = 1;
            this.label6.Text = "x.pred =";
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(99, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 26);
            this.label7.TabIndex = 1;
            this.label7.Text = "y.pred =";
            this.label7.Click += new System.EventHandler(this.label1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(10, 440);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 30);
            this.label8.TabIndex = 1;
            this.label8.Text = "4. ";
            this.label8.Click += new System.EventHandler(this.label1_Click);
            // 
            // model_evoluation
            // 
            this.model_evoluation.BackColor = System.Drawing.Color.Bisque;
            this.model_evoluation.Location = new System.Drawing.Point(92, 440);
            this.model_evoluation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.model_evoluation.Name = "model_evoluation";
            this.model_evoluation.Size = new System.Drawing.Size(178, 39);
            this.model_evoluation.TabIndex = 17;
            this.model_evoluation.Text = "Evoluation";
            this.model_evoluation.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(99, 498);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 26);
            this.label9.TabIndex = 18;
            this.label9.Text = "model.evoluation = ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Violet;
            this.ClientSize = new System.Drawing.Size(1462, 974);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.model_evoluation);
            this.Controls.Add(this.predicted_y);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.model_fitness);
            this.Controls.Add(this.model_prediction);
            this.Controls.Add(this.vector_x);
            this.Controls.Add(this.vector_y);
            this.Controls.Add(this.predicted_x);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.visualize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button visualize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox predicted_x;
        private System.Windows.Forms.TextBox vector_y;
        private System.Windows.Forms.TextBox vector_x;
        private System.Windows.Forms.Button model_fitness;
        private System.Windows.Forms.Button model_prediction;
        private System.Windows.Forms.TextBox predicted_y;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button model_evoluation;
        private System.Windows.Forms.Label label9;
    }
}

