using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class ServiceRecordsPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public ServiceRecordsPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { ServiceCollection.ItemsSource = new List<ServiceRecord>(); return; }

            var entities = await _db.GetServiceRecordsAsync(v.Id.ToString());
            v.ServiceRecords.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var record = new ServiceRecord(e.DateOfService, e.Description, (uint)e.MileageAtService, e.Cost);
                    v.ServiceRecords.Add(record);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
                }
            }
            ServiceCollection.ItemsSource = v.ServiceRecords.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddServiceRecordPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
    }
}
