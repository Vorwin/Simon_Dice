using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Simon_Dice
{
    public partial class Form1 : Form
    {
        //----------Variables Globales----------
        String user;
        int punteo = 0;
        int x = 0; 
        String modo;
        bool flag;
        List<int> SimonDice;
        int tiempo = 1000; 
        Random r;
        int auxPunteo = 0;
        int combi; 

        public Form1()
        {
            InitializeComponent();
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            lbl_menuP.Location = new Point((panel1.Width / 2) - (lbl_menuP.Width / 2), (panel1.Height / 2) - (lbl_menuP.Height / 2));
            r = new Random();
            
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------
         * -----------------------------------------------------------------Menu Princial----------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------*/

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pixfans.com/simon-el-juego-ochentero-de-mb/");
        }

        private void cambiarpantallas(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            switch (boton.Text) { 
                case "Jugar":
                    tabControl1.SelectTab(1); 
                    break;
                case "Puntajes":
                    tabControl1.SelectTab(3);
                    leer();
                    break;
                case "Instrucciones":
                    tabControl1.SelectTab(2);
                    break;
                case "":
                    txt_nombre.Text = "";
                    lbl_puntaje.Text = ""; 
                    tabControl1.SelectTab(0);
                    break; 
            }
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------
        * ----------------------------------------------------------------------------------------------------------------------------------
        * -----------------------------------------------------------------Instrucciones----------------------------------------------------
        * ----------------------------------------------------------------------------------------------------------------------------------
        * ----------------------------------------------------------------------------------------------------------------------------------*/

        private void iconButton16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------
         * -----------------------------------------------------------------Iniciar Juego----------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------*/

        private void EscojerDificultad(object sender, EventArgs e)
        {
            Button diff = (Button)sender;

            switch (diff.Text)
            {
                case "Fácil":

                    if (txt_nombre.Text == "") { MessageBox.Show("Ingrese un nombre", "!Alerta¡", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else
                    {
                        user = txt_nombre.Text;
                        modo = "Facil";
                        SimonDice = new List<int>(7);
                        tiempo = 1000;
                        combi = 7; 
                        tabControl1.SelectTab(4);
                    }

                    break;

                case "Medio":

                    if (txt_nombre.Text == "") { MessageBox.Show("Ingrese un nombre", "!Alerta¡", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else  
                    { 
                        user = txt_nombre.Text; 
                        modo = "Medio"; 
                        SimonDice = new List<int>(13);
                        tiempo = 500;
                        combi = 13;
                        tabControl1.SelectTab(4); 
                    }

                    break;

                case "Difícil":

                    if (txt_nombre.Text == "") { MessageBox.Show("Ingrese un nombre", "!Alerta¡", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else 
                    { 
                        user = txt_nombre.Text; 
                        modo = "Difícil"; 
                        SimonDice = new List<int>(20);
                        tiempo = 500;
                        combi = 20;
                        tabControl1.SelectTab(4); 
                    }

                    break;

            }
        }

        public void Verificar(int valor)
        {
            if(flag == false && SimonDice.Count > 0)
            {
                if (valor == SimonDice[x])
                {
                    x++;
                }
                else
                {
                    MessageBox.Show("¡Perdiste!\nTu punteo es: " + lbl_puntaje.Text);
                    punteo = Convert.ToInt32(lbl_puntaje.Text);
                    guardar();
                    x = 0;
                    SimonDice.Clear();
                    iconButton12.Text = "Iniciar";
                    txt_nombre.Text = "";
                    user = "";
                    modo = "";
                    punteo = 0;
                    auxPunteo = 0;
                    combi = 0;
                    tabControl1.SelectTab(0);
                }

            }
        }

        private async void Juego()
        {
            flag = true;

            foreach (int num in SimonDice)
            {
                switch (num)
                {
                    case 0:
                        rojo.BackColor = Color.FromArgb(250, 0, 0);
                        await Task.Delay(tiempo); //hace una pausa 
                        rojo.BackColor = Color.FromArgb(100, 0, 0);
                        break;
                    case 1:
                        verde.BackColor = Color.FromArgb(0, 255, 0);
                        await Task.Delay(tiempo); //hace una pausa 
                        verde.BackColor = Color.FromArgb(0, 100, 0);
                        break;
                    case 2:
                        azul.BackColor = Color.FromArgb(0, 0, 255);
                        await Task.Delay(tiempo); //hace una pausa 
                        azul.BackColor = Color.FromArgb(0, 0, 100);
                        break;
                    case 3:
                        amarillo.BackColor = Color.FromArgb(250, 255, 0);
                        await Task.Delay(tiempo); //hace una pausa 
                        amarillo.BackColor = Color.FromArgb(100, 100, 0);
                        break;
                }
                await Task.Delay(tiempo);
            }
            flag = false;
        }

        private void IniciarJuego(object sender, EventArgs e)
        {
            auxPunteo++;

            if (auxPunteo <= combi + 1)
            {
                lbl_puntaje.Text = x.ToString();
                SimonDice.Add(r.Next(0, 4));
                timer1.Start();

                iconButton12.Text = "Seguir";
                x = 0;
            } 
            else
            {
                MessageBox.Show("Juego terminado, Ganaste la fase");
                x = 0;
                punteo = Convert.ToInt32(lbl_puntaje.Text);
                guardar();
                SimonDice.Clear();
                iconButton12.Text = "Iniciar";
                txt_nombre.Text = "";
                user = "";
                modo = "";
                punteo = 0;
                auxPunteo = 0;
                combi = 0;
                tabControl1.SelectTab(0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Juego();
            if (Convert.ToInt32(lbl_puntaje.Text) >= 2)
            {
                tiempo = tiempo - 50; 
            }
            timer1.Stop();
        }

        private void botonesClick(object sender, EventArgs e)
        {
            Button botones = (Button)sender;
            Verificar(Convert.ToInt32(botones.Text));
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------
         * -----------------------------------------------------------------Punteos----------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------
         * ----------------------------------------------------------------------------------------------------------------------------------*/
        private void guardar()
        {
            String datosSalida = "";
            String ruta = Directory.GetCurrentDirectory() + @"\MejorPuntuacion.csv";

           datosSalida = user + ";" + modo + ";" + punteo + "\r\n";
           File.AppendAllText(ruta, datosSalida, Encoding.Default);// xd 
        }

        private void leer()
        {

            //Leer csv y ponerlo en un data grid view
            String ruta = Directory.GetCurrentDirectory() + @"\MejorPuntuacion.csv";
            String[] lineas = File.ReadAllLines(ruta, Encoding.UTF8);
            String[] encabezado = lineas[0].Split(';');

            DataTable tabla = new DataTable();


            //Coloca el encabezado de la tabla
            foreach (String newColumn in encabezado)
            {
                tabla.Columns.Add(newColumn);
            }
            
            //Nueva fila
            for(int i = 1; i < lineas.Length; i++)
            {
                String[] camposRegisro = lineas[i].Split(';');
                tabla.Rows.Add(camposRegisro);
            }

            dataGridView1.DataSource = tabla; 
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);

            /*if (File.Exists(ruta))
            {
                StreamReader leer = new StreamReader(ruta);
                while (!leer.EndOfStream)
                {
                    String linea = leer.ReadLine();
                    String[] valores = linea.Split(';');
                    dataGridView1.Rows.Add(valores[0], valores[1], Convert.ToInt32(valores[2]));
                }

                dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
            }*/
        }
    }
}
