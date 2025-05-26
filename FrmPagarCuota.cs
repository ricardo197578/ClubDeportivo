using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace ClubDeportivo
{
    public class FrmPagarCuota : Form
    {
        private Socio socio;
        private ListView listViewCuotas;
        private Button btnMarcarPagado;

        public FrmPagarCuota(Socio socio)
        {
            this.socio = socio;
            InitializeComponent();
            MostrarCuotasPendientes();
        }

        private void InitializeComponent()
        {
            this.Text = "Pagar Cuotas";
            this.Width = 400;
            this.Height = 300;

            listViewCuotas = new ListView();
            listViewCuotas.FullRowSelect = true;
            listViewCuotas.MultiSelect = false;

            listViewCuotas.View = View.Details;
            listViewCuotas.Columns.Add("ID", 50);
            listViewCuotas.Columns.Add("Fecha", 100);
            listViewCuotas.Columns.Add("Monto", 80);
            listViewCuotas.Columns.Add("Pagado", 80);
            listViewCuotas.Location = new Point(20, 20);
            listViewCuotas.Width = 350;
            listViewCuotas.Height = 200;
            this.Controls.Add(listViewCuotas);

            btnMarcarPagado = new Button();
            btnMarcarPagado.Text = "Marcar como Pagado";
            btnMarcarPagado.Location = new Point(120, 230);
            btnMarcarPagado.Click += BtnMarcarPagado_Click;
            this.Controls.Add(btnMarcarPagado);
        }

        
        private void MostrarCuotasPendientes()
        {
            listViewCuotas.Items.Clear();
            List<Cuota> cuotasPendientes = socio.Cuotas.FindAll(c => !c.Pagado);

            Console.WriteLine(string.Format("Cuotas pendientes encontradas: {0}", cuotasPendientes.Count)); // Debug

            foreach (Cuota cuota in cuotasPendientes)
            {
                ListViewItem item = new ListViewItem(cuota.Id.ToString());
                item.SubItems.Add(cuota.FechaPago.ToShortDateString());
                item.SubItems.Add(cuota.Monto.ToString("C"));
                item.SubItems.Add(cuota.Pagado ? "Sí" : "No");
                listViewCuotas.Items.Add(item);
            }
        }
       
        private void BtnMarcarPagado_Click(object sender, EventArgs e)
        {
            if (listViewCuotas.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione una cuota para marcarla como pagada.");
                return;
            }

            try
            {
                int cuotaId = Convert.ToInt32(listViewCuotas.SelectedItems[0].Text);

                // Marcar como pagado en BD y memoria sino da error objeto desechado
                socio.PagarCuota(cuotaId);

                // Actualizar la vista
                MostrarCuotasPendientes();

                MessageBox.Show("Cuota marcada como pagada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al marcar la cuota como pagada: {ex.Message}"));
            }
        }
    }
}
