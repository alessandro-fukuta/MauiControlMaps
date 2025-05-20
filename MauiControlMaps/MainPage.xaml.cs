using Microsoft.Maui.Maps;

namespace MauiControlMaps
{
    public partial class MainPage : ContentPage
    {

        Location location;
        MapSpan mapSpan;

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnNovoLocal(object sender, EventArgs e)
        {

            double Latitude = 0;
            double Longitude = 0;

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
        }

        private void btnMapaHibrido(object sender, EventArgs e)
        {
            map.MapType = Microsoft.Maui.Maps.MapType.Hybrid;
        }

        private void btnMapaSatelite(object sender, EventArgs e)
        {
            map.MapType = Microsoft.Maui.Maps.MapType.Satellite;
        }

        private void btnAbriCoordenadas(object sender, EventArgs e)
        {
            frameCoordenadas.IsVisible = true;
            txtLatiude.Focus();
        }
    }

}
