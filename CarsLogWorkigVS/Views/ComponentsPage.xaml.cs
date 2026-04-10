using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class ComponentsPage : ContentPage
    {
        private readonly AppStateService _appState;

        public ComponentsPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ComponentsCollection.ItemsSource = _appState.SelectedVehicle?.Components
                ?? new List<VehicleComponent>();
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
                var component = new VehicleComponent(
                    partName: name.Trim(),
                    installationMileage: mileage,
                    isExpired: expired,
                    installationDate: DateTime.Now
                );
                vehicle.Components.Add(component);
                ComponentsCollection.ItemsSource = vehicle.Components;
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
