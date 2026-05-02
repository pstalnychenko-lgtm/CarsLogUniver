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

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddVehiclePage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}