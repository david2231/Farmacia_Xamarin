using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Pharmapp;

namespace Encuesta_Drogueria
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class Page1 : ContentPage
	{
       

        public Page1 ()
		{
			InitializeComponent ();
             
        }
      
        protected override void OnAppearing()
        {
            //Se cargan los valores de la tabla productos
            GetUserInfo();
        }
        async void GetUserInfo()
        {
            try
            {
                //Declaración de la url del servicio
                string url = "https://pharmaap.000webhostapp.com/usuarios.php";
                //Nuevo cliente
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                //Lista los usuarios
                var EmpList = JsonConvert.DeserializeObject<List<Usuario>>(result);
                //Actualiza la tabla
                TblUsuario.ItemsSource = null;
                TblUsuario.ItemsSource = new ObservableCollection<Usuario>(EmpList);


            }
            //Por si existe un error
            catch (Exception ex)
            {
                await DisplayAlert("Input Error","Error", "OK");
                return;
            }
        }


        private void UList_Refreshing(object sender, EventArgs e)
        {
            GetUserInfo();
            TblUsuario.IsRefreshing = false;

        }
        
        //Botón 
        private async void BtnAgregar(object sender, EventArgs e)
        {
            
            
            Navigation.PushAsync(new productos());

        }
      
        //Método item seleccionado
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            //Trae el id del producto seleccionado
            var menu = sender as MenuItem;
            // Convierte el valor a string
            string valor =(menu.CommandParameter.ToString());

            //Actualiza los campos
           
            ids.Text = valor.ToString();
            ids1.Text = valor.ToString();
            ids2.Text = valor.ToString();
            ids3.Text = valor.ToString();
            ids4.Text = valor.ToString();

            //Se declara un nuevo cliente que permite realizar la conexión con el web service, se realiza por GET
            string cliente = new WebClient().DownloadString("https://pharmaap.000webhostapp.com/eliminar.php?id=" + ids.Text);
            DisplayAlert("", "Producto Editado", "OK");
            //Switch que permite validar los escenarios posibles 
            switch (cliente)
            {
                //Switch que permite validar los escenarios posibles 
                case "actualiza":
                    {
                        //Si los datos ingresados son incorrectos
                        DisplayAlert("", "Producto Editado", "OK");
                        break;
                    }


                case "no actualiza":
                    {
                        //Si ocurre un error
                       
                        DisplayAlert("", "Error :", "OK");
                        break;
                    }
             
                default:
                    break;
            }
        }

      
        //Método eliminar
        private async void MenuItem_Clicked2(object sender, EventArgs e)
        {

            //Trae el id del producto seleccionado
            var menu = sender as MenuItem;
            // Convierte el valor a entero
            int valor = Convert.ToInt32(menu.CommandParameter.ToString());
            //Actualiza el campo con el ID
            ids.Text = valor.ToString();

            //Se declara un nuevo cliente que permite realizar la conexión con el web service, se realiza por GET
            string cliente = new WebClient().DownloadString("https://pharmaap.000webhostapp.com/eliminar.php?id=" + ids.Text);
            //Switch que permite validar los escenarios posibles 
            switch (cliente)
            {
                //Switch que permite validar los escenarios posibles 
                case "elimina":
                    {
                        //Si los datos ingresados son incorrectos
                        DisplayAlert("", "Usuario eliminado", "OK");
                        break;
                    }


                case "no elimina":
                    {
                        //Si el rol del usuario es aadministrador
                        Navigation.PushAsync(new Page1());
                        DisplayAlert("", "Bienvenido Admin :", "OK");
                        break;
                    }

                default:
                    break;
            }
        }


        //Método botón enviar
        private async void BtnObtener(object sender, EventArgs e)
        {
            // Try Catch 
            try
            {
                // Se realiza la creación de la instancia UseManager 
                UserManager manager = new UserManager();
                // Se crea una variable que permita llamar al método
                var resultado = await manager.getUsuarios();
                // Se envia los atributos a la lista

                if (resultado != null) {
                    // Se llena la lista 
                    TblUsuario.ItemsSource = resultado;
                    

                }
            }
            catch (Exception e1)
            {

            }
        }
    }
}