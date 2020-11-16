//Librerias
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Encuesta_Drogueria
{
    public partial class MainPage : ContentPage
    {
            
        
        public MainPage ()

        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            //Opciones de los  picker
            PickerMed.Items.Add("Acetaminofen");
            PickerMed.Items.Add("Ibuprofeno");
            PickerMed.Items.Add("Gastrum");
            PickerMed.Items.Add("Gaviscon");
            PickerEfm.Items.Add("Gastritis");
            PickerEfm.Items.Add("Miopia");
            PickerEfm.Items.Add("Estrenimiento");
            PickerEfm.Items.Add("Mareo");
            Pickerapp.Items.Add("1");
            Pickerapp.Items.Add("2");
            Pickerapp.Items.Add("3");
            Pickerapp.Items.Add("4");
            Pickerapp.Items.Add("5");
            


            
        }
        //Método captura el valor del picker
        public void PickerMed_SelectedIndexChanged(object sender, EventArgs e)
        {
            String op = PickerMed.SelectedItem.ToString();
            
            
        }
        //Método captura el valor del picker
        public void PickerMed_SelectedIndexChangedApp(object sender, EventArgs e)
        {
            String app = Pickerapp.SelectedItem.ToString();

            
        }
        //Método captura el valor del picker
        public void PickerMed_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
            String op1 = PickerEfm.SelectedItem.ToString();
            
        }
        //Captura la fecha seleccionada
        public void SeleecionFecha(object sender, DateChangedEventArgs e)
        {

            detalles.Text=e.NewDate.ToString();
        }
        //Método edad
        public void Edadd(object sender, EventArgs e)
        {
            //Cambia el campo conforme a la selección del usuario
            Edades.Text = SliderEdad.Value.ToString();
        }


            //Método botón enviar
            async void BtnEnviar(object sender, EventArgs e)
        {

           
            //Validación de los campos del formulario
            if (await validarFormulario())
            {
                //Se declara un nuevo cliente que permite realizar la conexión con el web service
                WebClient cliente = new WebClient();
                // Variable parametros que permite almacenar los atributos
                var parametros = new System.Collections.Specialized.NameValueCollection();
                //Parametros que se envian por metodo post
                parametros.Add("usuario", EntryName.Text);
                parametros.Add("P1", EntryApll.Text);
                parametros.Add("P2", EntryCedula.Text);
                parametros.Add("P3", detalles.Text);
                parametros.Add("P4", Edades.Text);
                parametros.Add("P5", PickerMed.SelectedItem.ToString());
                parametros.Add("P6", PickerEfm.SelectedItem.ToString());
                parametros.Add("P7", Pickerapp.SelectedItem.ToString());
                parametros.Add("P8", Boletin.Text);
                parametros.Add("P9", App.Text);
                parametros.Add("P10", EntryComentario.Text);
                //URL del web service servicio 
                cliente.UploadValues("https://pharmaap.000webhostapp.com/insertar_preguntas.php", "POST", parametros);
                // Si el registro es exitoso
                try
                {
                    // Alert dialog con el mensaje exitoso

                    DisplayAlert("", "Registro Exitoso",  "OK");
                    Limpiar();
                    Navigation.PushAsync(new Login());
                }

                catch
                {
                    // Alert dialog si ocurrio algun error durante el registro
                    DisplayAlert("", "Ocurrio un error durante el registro", "OK");
                }
            }
            else
            {
               
            }
        }

        
        //Método limpiar formulario
        public void Limpiar() {

            EntryName.Text = "";

        }


        //Método validar formlulario
        private async Task<bool> validarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(EntryName.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio.", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(EntryApll.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("Advertencia", "El campo del apellido es obligatorio.", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(detalles.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("Advertencia", "La fecha no se puede dejar vacia.", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(Edades.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("Advertencia", "La edad no se puede dejar vacia.", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(Edades.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("Advertencia", "La edad no se puede dejar vacia.", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(EntryComentario.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("Advertencia", "La edad no se puede dejar vacia.", "OK");
                return false;
            }

            return true;
        }

       
    }     
}
