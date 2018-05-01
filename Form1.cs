using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization;

    namespace Random_value
{
    public partial class Form1 : Form
    {
        int M;
        int N;
        int K;
        double lambda;

        double average, disp_ch, swipe, mediane, expect_val, disp_th;
        double interval_len;
        Array rand_val_array;
        Array gist_array;
        double[] hit_probability;
        double[] hypo_array;
        double lboard, rboard;

        double delta;
        unsafe int num = 0;
        double R0 = 0.0;

        public Form1()
        {
            InitializeComponent();
            M = System.Convert.ToInt32(textBox3.Text);
            N = System.Convert.ToInt32(textBox1.Text);
            K = (int)(1.44 * Math.Log(M) + 1);
            lambda = System.Convert.ToDouble(textBox2.Text);
            rand_val_array = new double[M];
            gist_array = new double[K];
            textBox4.Text = Convert.ToString(N / 3);
        }

        unsafe private void init()
        {
            M = System.Convert.ToInt32(textBox3.Text);
            N = System.Convert.ToInt32(textBox1.Text);
            K = (int)(1.44 * Math.Log(M) + 1);
            lambda = System.Convert.ToDouble(textBox2.Text);
            rand_val_array = new double[M];
            gist_array = new double[K];
            textBox4.Text = Convert.ToString(N / 3);
        }

        private void set_intervals()
        {
            lboard = Convert.ToDouble(rand_val_array.GetValue(0));
            rboard = Convert.ToDouble(rand_val_array.GetValue(M - 1));
            interval_len = rboard - lboard;

            delta = interval_len / K;

            for (int j = 0; j < K; ++j)
            {
                int counter = 0;
                for (int i = 0; i < M; ++i)
                {
                    if (lboard + j * delta < Convert.ToDouble(rand_val_array.GetValue(i))
                        && Convert.ToDouble(rand_val_array.GetValue(i)) < lboard + (j + 1) * delta)
                    {
                        counter++;
                    }

                }
                gist_array.SetValue((counter / (M * delta)), j); 
                counter = 0;
            }

        }

        private void hypo_intervals()
        {
            int K = Convert.ToInt16(textBox4.Text);
            lboard = Convert.ToDouble(textBox5.Text);
            rboard = Convert.ToDouble(textBox6.Text);
            delta = Convert.ToDouble(textBox7.Text);

            for (int j = 0; j < K; ++j)
            {
                int counter = 0;
                for (int i = 0; i < M; ++i)
                {
                    if (lboard + j * delta < Convert.ToDouble(rand_val_array.GetValue(i))
                        && Convert.ToDouble(rand_val_array.GetValue(i)) < lboard + (j + 1) * delta)
                    {
                        counter++;
                    }

                }
                hypo_array[j] = (counter / (M * delta));
                counter = 0;
            }

        }

        private void set_R0(int m, int k, double[] ar, double[] hit_ar)
        {
            double res = 0.0;
            for (int j = 0; j < k; ++j)
            {
                res += (Math.Pow((ar[j]/m) - hit_ar[j], 2) / ar[j]);
            }
            R0 = (res * m);
        }

        private double get_F_th(double x, double l)
        {
            return (1 - Math.Exp(-l * x));
        }

        private double get_F_ch(double x)
        {
            int counter = 0;
            for (int i = 0; (i < M) &&(Convert.ToDouble(rand_val_array.GetValue(i)) < x); ++i)
            {
                    counter++;
            }
            return (counter / (double)M);
        }

        private void set_choosen_values()
        {

            double sum = 0d;
            for (int i = 0; i < M; ++i)
            {
                sum += Convert.ToDouble(rand_val_array.GetValue(i));
            }
            double tmp = sum;            
            average = sum / (double)M;

            sum = 0d;
            for (int i = 0; i < M; ++i)
            {
                sum += Math.Pow(Convert.ToDouble(rand_val_array.GetValue(i)) - average, 2);
            }
            disp_ch = sum / (double)M;
            swipe = interval_len;

            if (M % 2 == 0)
            {
                mediane = (Convert.ToDouble(rand_val_array.GetValue(M / 2)) +
                    Convert.ToDouble(rand_val_array.GetValue((M / 2) - 1))) / 2;
            }
            else
            {
                mediane = Convert.ToDouble(rand_val_array.GetValue(M / 2));
            }
            expect_val = (N / lambda);
            disp_th = (double)N / (lambda * lambda);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        private void buttonDraw_Click(object sender, EventArgs e)
        {
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void buttonDraw_Click_1(object sender, EventArgs e)
        {
            double lambda = System.Convert.ToDouble(textBox2.Text);
            // create graph generator
            GraphFunctionGenerator gFGenerator = new GraphFunctionGenerator(50, lambda);
            // create зшсегку generator
            GraphicsImageGenerator gIGenerator =
                new GraphicsImageGenerator(
                    gFGenerator.ListOfPoints, pictureBox.Width,
                    pictureBox.Height, float.Parse(textBoxScale.Text));
            // load picture into pictureBox.
            pictureBox.Image = gIGenerator.Bitmap;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            dataGridView1.Rows.Clear();
            Array.Clear(rand_val_array, 0, M);
            Array.Clear(gist_array, 0, K);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Array.Clear(rand_val_array, 0, M);
            Array.Clear(gist_array, 0, K);
            init();
            Random rand = new Random();
            for (int i = 0; i < M; ++i)
            {
                rand_val RV = new rand_val(lambda, N, M);
                rand_val_array.SetValue(RV.single_experiment(N, lambda, rand), i);
            }
            Array.Sort(rand_val_array);

            for (int i = 0; i < M; ++i)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(i);
                dataGridView1.Rows[i].Cells[1].Value = rand_val_array.GetValue(i);
            }
            set_intervals();
            set_choosen_values();
            textBox5.Text = Convert.ToString(rand_val_array.GetValue(3));
            textBox6.Text = Convert.ToString(rand_val_array.GetValue(M - 3));
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.###";
            //dataGridView2.Rows.Add();
            for (int j = 0; j < K; ++j)
                chart1.Series["Series1"].Points.AddXY(lboard + j * delta, gist_array.GetValue(j));
            dataGridView2.Rows[0].Cells[0].Value = Convert.ToString(expect_val);
            dataGridView2.Rows[0].Cells[1].Value = Convert.ToString(average);
            dataGridView2.Rows[0].Cells[2].Value = Convert.ToString(Math.Abs(expect_val - average));
            dataGridView2.Rows[0].Cells[3].Value = Convert.ToString(disp_th);
            dataGridView2.Rows[0].Cells[4].Value = Convert.ToString(disp_ch);
            dataGridView2.Rows[0].Cells[5].Value = Convert.ToString(Math.Abs(disp_th - disp_ch));
            dataGridView2.Rows[0].Cells[6].Value = Convert.ToString(mediane);
            dataGridView2.Rows[0].Cells[7].Value = Convert.ToString(swipe);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; ++i)
            {
                button1_Click_2(sender, e);
            }
        }

        public double get_density(double x)
        {
            return (lambda * Math.Exp(-lambda * x));
        }

        private void set_qj(double[] ar, int K)
        {
            normal_generation E = new normal_generation();
            hit_probability[0] = 0.5 * (1 + E.Erf((ar[0] - (N / lambda)) / Math.Sqrt(2 * N / (lambda * lambda))));
            for (int i = 1; i < K - 1; ++i)
            {
                hit_probability[i] = 0.5 * (1 + E.Erf((ar[i] - (N / lambda)) / Math.Sqrt(2 * N / (lambda * lambda)))) -
                    0.5 * (1 + E.Erf((ar[i - 1] - (N / lambda)) / Math.Sqrt(2 * N / (lambda * lambda))));
            }
            hit_probability[K - 1] = 1 - 0.5 * (1 + E.Erf((ar[K - 1] - (N / lambda)) / Math.Sqrt(2 * N / (lambda * lambda))));
        }

        private double get_max_dif(double[] yF, double[] yFCh, int n)
        {
            double res = 0;
            for (int i = 0; i < n; i++)
                res = Math.Max(res, Math.Abs(yF[i] - yFCh[i]));
            return res;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            for (int i = 0; i < K; ++i)
            {
                dataGridView3.Columns.Add("column", Convert.ToString(i + 1));
            }
            dataGridView3.Rows.Add();
                        dataGridView3.Rows.Add();
            for (int i = 0; i < K; ++i)
            {
                dataGridView3.Rows[0].Cells[i].Value = Convert.ToString(get_density(lboard + (i + 0.5) * delta));
                dataGridView3.Rows[1].Cells[i].Value = Convert.ToString(gist_array.GetValue(i));
                dataGridView3.Rows[2].Cells[i].Value = Convert.ToString(Math.Abs(get_density(lboard + (i + 0.5) * delta) 
                    - Convert.ToDouble(gist_array.GetValue(i))));
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }



        private void button7_Click(object sender, EventArgs e)
        {
            double Z1 = (Convert.ToDouble(textBox6.Text));

            int K = Convert.ToInt16(textBox4.Text);
            textBox7.Text = Convert.ToString((Convert.ToDouble(textBox6.Text) 
                - Convert.ToDouble(textBox5.Text)) / Convert.ToDouble(textBox4.Text));
            double delta = Convert.ToDouble(textBox7.Text);
            hit_probability = new double[K]; // qj
            hypo_array = new double[K]; // nj
            hypo_intervals(); // nj

            set_qj(hit_probability, K); //qj
            set_R0(M, K, hypo_array, hit_probability);
            //distribute_chi();




        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            num = 0;
            chart2.Series.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            normal_generation E = new normal_generation();     
            int m = 20000;
            double delta = rboard / m;
            double[] yFCh = new double[m], x = new double[m], yFTh = new double[m];
            System.Windows.Forms.DataVisualization.Charting.Series s =
                 new System.Windows.Forms.DataVisualization.Charting.Series();
            chart2.Series.Add(s);
            s.BorderWidth = 2;
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            System.Windows.Forms.DataVisualization.Charting.Series s2 =
                new System.Windows.Forms.DataVisualization.Charting.Series();
            chart2.Series.Add(s2);
            s2.BorderWidth = 2;
            s2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            num++;
            for (int i = 0; i < m; i++)
            {
                x[i] = i * delta;
                yFCh[i] = get_F_ch(x[i]);
                yFTh[i] = 0.5 * (1 + E.Erf((x[i] - (N / lambda)) / Math.Sqrt(2 * N / (lambda * lambda))));
                chart2.Series[num - 1].Points.AddXY(x[i], yFCh[i]);
                chart2.Series[num].Points.AddXY(x[i], yFTh[i]);
            }
            chart2.Series[num - 1].Points.AddXY(rboard, 1);
            chart2.Series[num - 1].Points.AddXY(rboard + 1, 1);
            num++;
            dataGridView2.Rows[0].Cells[8].Value = Convert.ToString(get_max_dif(yFTh, yFCh, m));
        }
       

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
