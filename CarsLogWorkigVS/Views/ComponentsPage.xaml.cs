using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class ComponentsPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public ComponentsPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { ComponentsCollection.ItemsSource = new List<VehicleComponent>(); return; }

            var entities = await _db.GetComponentsAsync(v.Id.ToString());
            v.Components.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var component = new VehicleComponent(e.PartName, (uint)e.InstallationMileage, e.IsExpired, e.InstallationDate);
                    v.Components.Add(component);
                }
                catch { }
            }
            ComponentsCollection.ItemsSource = v.Components.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { await DisplayAlert("Помилка", "Автомобіль не вибрано.", "OK"); return; }

            string name = await DisplayPromptAsync("Новий компонент", "Назва запчастини:", "Додати", "Скасувати", "Масло, Фільтр...");
            if (string.IsNullOrWhiteSpace(name)) return;

            string mileageStr = await DisplayPromptAsync("Пробіг", "Поточний пробіг:", "Далі", "Скасувати",
                vehicle.CurrentMileage.ToString(), keyboard: Keyboard.Numeric);
            if (!uint.TryParse(mileageStr, out var mileage)) mileage = vehicle.CurrentMileage;

            bool expired = await DisplayAlert("Стан", "Компонент потребує заміни?", "Так", "Ні");

            try
            {
                var component = new VehicleComponent(name.Trim(), mileage, expired, DateTime.Now);
                vehicle.Components.Add(component);
                await _db.SaveComponentAsync(vehicle.Id.ToString(), component);
                ComponentsCollection.ItemsSource = vehicle.Components.ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", ex.Message, "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
