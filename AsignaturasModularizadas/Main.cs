using Conector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsignaturasModularizadas
{
    public class Main
    {
        Form Forma = new Form();
        public void Abrir(Form FrmMDIPadre, Inicio DatosMDIPadre, string Version, string Formulario)
        {
            switch (Formulario)
            {
                case "FrmListadoModulares":
                     Forma = new Modular();
                    break;
                case "FrmAsignacionFinancieraModular":
                    Forma = new FrmAsignacionFinancieraModular();
                    break;
                default:
                    {
                        SIIEMessageBox.Clases.SIIEMessageBox.Show("El formulario inicial especificado es incorrecto", "SIIEPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
            // Asegúrate de que estas operaciones se ejecuten en el hilo principal de la UI
            FrmMDIPadre.Invoke(new Action(() =>
            {
                // Datos de la clase INI del MDI Padre
                General.Ini = DatosMDIPadre;

                // Cargar la forma que requiera el usuario al iniciar
                Forma.MdiParent = FrmMDIPadre;
                // forma.Text = "Disponibilidad Docente"; // Ajusta el texto según sea necesario
                Forma.Show();
            }));
        }

    }
}
