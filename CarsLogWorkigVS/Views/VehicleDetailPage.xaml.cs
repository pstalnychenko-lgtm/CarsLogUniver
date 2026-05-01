using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class VehicleDetailPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;
        private readonly DatabaseService _db;

        public VehicleDetailPage(AppStateService appState, VehicleViewModel vm, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
            _db = db;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) return;

            VehicleNameLabel.Text = $"{v.Brand} {v.Model}";
            PlateLabel.Text = v.PlateNumber;
            MileageLabel.Text = v.CurrentMileage.ToString("N0");
            FuelTypeLabel.Text = v.FuelType.ToString();
            EngineLabel.Text = v.EngineVolumeCc.ToString();

            var expired = _vm.GetExpiredComponentsReport(v);
            ExpiredComponentsLabel.Text = expired.Count > 0
                ? $"{expired.Count} компонент(и) потребують заміни"
                : "Всі компоненти в порядку";
            ExpiredComponentsLabel.TextColor = expired.Count > 0
                ? Microsoft.Maui.Graphics.Color.FromArgb("#FF3B30")
                : Microsoft.Maui.Graphics.Color.FromArgb("#34C759");
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");

        private void OnEditClicked(object sender, EventArgs e) { }

        private async void OnFuelTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(FuelEntriesPage));

        private async void OnTripsTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(TripLogsPage));

        private async void OnServiceTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ServiceRecordsPage));

        private async void OnExpensesTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ExpensesPage));

        private async void OnDocumentsTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(DocumentsPage));

        private async void OnNotesTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(NotesPage));

        private async void OnComponentsTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ComponentsPage));

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var v = _appState.SelectedVehicle;
            if (v == null) return;

            bool confirm = await DisplayAlert("Видалити", $"Видалити {v.Brand} {v.Model}?", "Так", "Скасувати");
            if (!confirm) return;

            _vm.TryRemoveVehicle(v.Id, true);
            await _db.DeleteVehicleAsync(v.Id.ToString());
            _appState.SelectedVehicle = null;
            await Shell.Current.GoToAsync("..");
        }
    }
}
