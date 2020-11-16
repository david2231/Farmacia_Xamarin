using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Specialized;
using System.Collections;

namespace Encuesta_Drogueria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
       //declaración del array
        ArrayList lista = new ArrayList();

        public Login()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
          
        }
      
   //Método botón enviar
        async void BtnEnviarUsuario(object sender, EventArgs e)
        {
           

            //Se declara un nuevo cliente que permite realizar la conexión con el web service, se realiza por GET
            string cliente = new WebClient().DownloadString("https://pharmaap.000webhostapp.com/login.php?pass=" + txtContra.Text + "&usuario=" + txtUsuario.Text);

            if (await validarFormulario())
            {
                //Switch que permite validar los escenarios posibles 
                switch (cliente)
                {
                    //Switch que permite validar los escenarios posibles 
                    case "contraseña incorrecta":
                        {
                            //Si los datos ingresados son incorrectos
                            DisplayAlert("", "Por Favor Verifique los datos ingresados", "OK");
                            break;
                        }


                    case "admin":
                        {
                            //Si el rol del usuario es aadministrador
                            Navigation.PushAsync(new Page1());
                            DisplayAlert("", "Bienvenido Admin :" + txtUsuario.Text, "OK");
                            break;
                        }
                    case "usuario":
                        {
                            //Si el rol es usuario
                            Navigation.PushAsync(new MainPage());
                            DisplayAlert("", "Bienvenido Usuario :" + txtUsuario.Text, "OK");
                            break;
                        }

                    default:
                        break;
                }
            }


        }
        //metodo limpiar
        private void Button_Clicked(object sender, EventArgs e)
        {
            
            txtContra.Text = "";
           
        }


        //metodo limpiar
        public void BtnRegistro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());

        }


        //Método validar formlulario
        private async Task<bool> validarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("", "El campo del nombre es obligatorio.", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtContra.Text))
            {
                // Mensaje de alerta 
                await this.DisplayAlert("", "El campo Contraseña es obligatorio.", "OK");

                return false;
            }

            return true;
        }

    }
}