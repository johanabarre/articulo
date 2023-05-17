using Articulo.BusinessLogic;
using Articulo.Entities;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Articulo.View
{
    public partial class frmCategoria : MetroForm
    {
        private List<Categoria> _listado;

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            _listado = CategoriaBL.Instance.SellecALL();

            var query = from x in _listado
                        select new
                        {
                            Id = x.CategoriaId,
                            Nombre = x.Nombre,
                            Estado = x.Estado.Nombre
                        };

            metroGrid1.DataSource = query.ToList();
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (metroGrid1.CurrentRow.Cells["Editar"].Selected)
            {
                int id = (int)metroGrid1.CurrentRow.Cells[2].Value;
                string nombre = metroGrid1.CurrentRow.Cells[3].Value.ToString();
                int estadoId = _listado.FirstOrDefault(x => x.CategoriaId.Equals(id)).EstadoId;

                Categoria entity = new Categoria()
                {
                    CategoriaId = id,
                    Nombre = nombre,
                    EstadoId = estadoId
                };

                frmCategoriaNuevo frm = new frmCategoriaNuevo(entity);
                frm.ShowDialog();
                UpdateGrid();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            frmCategoriaNuevo frm = new frmCategoriaNuevo();
            frm.ShowDialog();
            UpdateGrid();
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            _listado = CategoriaBL.Instance.SellecALL();

            var busqueda = from x in _listado
                           select new
                           {
                               Id = x.CategoriaId,
                               Nombre = x.Nombre,
                               Estado = x.Estado.Nombre
                           };
            var query = busqueda.Where(x => x.Nombre.ToLower().StartsWith(metroTextBox1.Text.ToLower())).ToList();
            metroGrid1.DataSource = query;

        }
    }
}
