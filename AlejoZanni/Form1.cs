using AlejoZanni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlejoZanni
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public List<Persona> personas = new List<Persona>();
        private TextBox[] textBoxes;
        private OpenFileDialog icon = new OpenFileDialog();
        public Form1()
        {
            
            InitializeComponent();
            Instance = this;
            textBoxes = new TextBox[]{ this.textBox1, this.textBox2, this.textBox3, this.textBox4 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            icon.Filter = "(*.png)|*png|(*.jpg)|*.jpg";
            if(icon.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new System.Drawing.Bitmap(icon.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool error = false;
            foreach (TextBox item in textBoxes)
            {
                if(item.Text.Length <= 1)
                {
                    item.BackColor = Color.Red;
                    error = true;
                }
                else
                {
                    item.BackColor = Color.White;
                }
            }

            if (error) return;


            Persona persona = personas.Find(x => x.DNI == textBox2.Text);

            if(persona == null)
            {
                persona = new Persona
                {
                    Name = textBox1.Text,
                    DNI = textBox2.Text,
                    Id = int.Parse(textBox3.Text),
                    CelphoneNumber = textBox4.Text,
                    ImagePath = icon.FileName
                };
                personas.Add(persona);
            }
            else
            {
                MessageBox.Show("Ya Exíste :(");
            }

            
            Actualizar();
        }


        public void Actualizar()
        {
            dataGridView1.Rows.Clear();
            foreach (Persona persona in personas)
                {

                    DataGridViewRow fila = new DataGridViewRow();
                    fila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.Name });
                    fila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.DNI });
                    fila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.Id });
                    fila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.CelphoneNumber });
                    dataGridView1.Rows.Add(fila);
                }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Persona persona = personas.Find(x => x.DNI == dataGridView1.CurrentRow.Cells[1].Value.ToString());
            textBox1.Text = persona.Name;
            textBox2.Text = persona.DNI;
            textBox4.Text = persona.CelphoneNumber;
            textBox3.Text = persona.Id.ToString();
            pictureBox1.Image = new System.Drawing.Bitmap(persona.ImagePath);
        }
    }
}
