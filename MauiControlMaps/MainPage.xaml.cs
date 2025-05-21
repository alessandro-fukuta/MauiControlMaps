using Microsoft.Maui.Maps;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.Layouts;
using Microsoft.Maui.Controls;

namespace MauiControlMaps
{
    public partial class MainPage : ContentPage
    {

        Location location;
        MapSpan mapSpan;

        double Lat = 0;
        double Lng = 0;

        string endereco = "Rua Paulino Liboni, 600, Franca, SP";
        bool MenuOpcoes = false;

        public MainPage()
        {
            InitializeComponent();

            ChamaEndereco(endereco);

           

        }

        private async void ChamaEndereco(string Address)
        {

            var coordenadas = await GetCoordinatesAsync(Address);
            Lat = coordenadas.Latitude;
            Lng = coordenadas.Longitude;


            location = new Location(Lat,Lng);
            mapSpan = new MapSpan(location, 0.01, 0.01);

            map.MoveToRegion(mapSpan);
        }

        private void btnNovoLocal(object sender, EventArgs e)
        {

            double Latitude = 0;
            double Longitude = 0;

            if(string.IsNullOrEmpty(txtLatiude.Text) || string.IsNullOrEmpty(txtLongitude.Text))
            {
                DisplayAlert("Atenção", "Informe as coordenadas", "OK");
                return;
            }

            Latitude = double.Parse(txtLatiude.Text);
            Longitude = double.Parse(txtLongitude.Text);

            location = new Location(Latitude, Longitude);
            mapSpan = new MapSpan(location, 0.01, 0.01);

            map.MoveToRegion(mapSpan);
            frameCoordenadas.IsVisible = false;

        }

        private void btnMapaRua(object sender, EventArgs e)
        {
            map.MapType = Microsoft.Maui.Maps.MapType.Street;
            frameCoordenadas.IsVisible = false;
            frameEndereco.IsVisible = false;
            EscondeMenu();
        }

        private async void EscondeMenu()
        {
            await frameOpcoes.TranslateTo(-200, 0, 500, Easing.CubicInOut); // Move da esquerda para a posição final
            MenuOpcoes = false;

        }

        private void btnMapaHibrido(object sender, EventArgs e)
        {
            map.MapType = Microsoft.Maui.Maps.MapType.Hybrid;
            frameCoordenadas.IsVisible = false;
            frameEndereco.IsVisible = false;
            EscondeMenu();
        }

        private void btnMapaSatelite(object sender, EventArgs e)
        {
            map.MapType = Microsoft.Maui.Maps.MapType.Satellite;
            frameCoordenadas.IsVisible = false;
            frameEndereco.IsVisible = false;
            EscondeMenu();
        }

        private void btnAbriCoordenadas(object sender, EventArgs e)
        {
            EscondeMenu();

            if (frameCoordenadas.IsVisible)
            {
                frameCoordenadas.IsVisible = false;
            }
            frameEndereco.IsVisible = false;
            frameCoordenadas.IsVisible = true;
            txtLatiude.Focus();
        }

        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address)
        {
            string apiKey = "AIzaSyD41zvn7IyUrUI_c-XWrhnHxhAuwU2Vaiw"; // Substitua pela sua chave da API do Google Maps
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";

            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            JObject data = JObject.Parse(json);
            var location = data["results"]?[0]?["geometry"]?["location"];

            if (location != null)
            {
                double latitude = (double)location["lat"];
                double longitude = (double)location["lng"];
                return (latitude, longitude);
            }

            return (0, 0); // Retorno padrão caso não encontre o endereço
        }

        private async void MapaClicked(object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
        {
          
            if( MenuOpcoes )
            {
                await frameOpcoes.TranslateTo(-200, 0, 500, Easing.CubicInOut); // Move da esquerda para a posição final
                MenuOpcoes = false;
                return;
            }

                frameEndereco.IsVisible = false;
                frameCoordenadas.IsVisible = false;
              
           
            await frameOpcoes.TranslateTo(0, 0, 500, Easing.CubicInOut); // Move da esquerda para a posição final
            MenuOpcoes = true;
              
           
        }

        private void BuscaPorEndereco(object sender, EventArgs e)
        {
            ChamaEndereco(txtEndereco.Text + ", " + txtCidade.Text + ", " + txtEstado.Text);
            frameEndereco.IsVisible = false;
        }

        private void btnAbriEndereco(object sender, EventArgs e)
        {
            frameEndereco.IsVisible = true;
            txtEndereco.Focus();
        }
    }

}
