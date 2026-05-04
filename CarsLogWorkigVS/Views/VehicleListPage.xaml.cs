using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;
using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace CarsLogWorkigVS.Views
{
    public partial class VehicleListPage : ContentPage
    {
        private readonly VehicleViewModel _vm;
        private readonly AppStateService _appState;

        public VehicleListPage(VehicleViewModel vm, AppStateService appState)
        {
            InitializeComponent();
            _vm = vm;
            _appState = appState;
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.LoadVehiclesAsync();
            ApplyRbac();
        }

        private void ApplyRbac()
        {
            var addButton = this.FindByName<Button>("AddButton");
            if (addButton != null)
                addButton.IsVisible = _appState.CanManageVehicles;
        }

        private async void OnVehicleSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Vehicle vehicle)
            {
                _appState.SelectedVehicle = vehicle;
                ((CollectionView)sender).SelectedItem = null;
                await Shell.Current.GoToAsync(nameof(VehicleDetailPage));
            }
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            if (!_appState.CanManageVehicles)
            {
                await DisplayAlert("Доступ заборонено", "Додавання авто доступне лише для Власника та Адміна.", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(AddVehiclePage));
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
    }
}
