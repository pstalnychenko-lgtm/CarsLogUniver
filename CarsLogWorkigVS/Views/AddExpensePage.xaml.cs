using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class AddExpensePage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;
        private readonly DatabaseService _db;

        public AddExpensePage(AppStateService appState, VehicleViewModel vm, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
            _db = db;
            CategoryPicker.SelectedIndex = 0;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { ShowError("Автомобіль не вибрано."); return; }

            if (!decimal.TryParse(AmountEntry.Text?.Replace(',', '.'), out var amount) || amount < 0)
            { ShowError("Введіть коректну суму."); return; }

            if (!_vm.ValidateExpenseAmount(amount)) { ShowError(_vm.LastError); return; }

            var category = CategoryPicker.SelectedIndex switch
            {
                0 => ExpenseCategory.Fuel,
                1 => ExpenseCategory.Service,
                2 => ExpenseCategory.Insurance,
                3 => ExpenseCategory.Fine,
                4 => ExpenseCategory.Parking,
                5 => ExpenseCategory.Washing,
                6 => ExpenseCategory.TireChange,
                _ => ExpenseCategory.Other
            };

            try
            {
                var expense = new Expense(
                    category: category,
                    amount: amount,
                    date: (DateTime)DatePicker.Date,
                    description: DescEditor.Text?.Trim() ?? string.Empty,
                    vehicleId: vehicle.Id
                );

                if (_appState.CurrentUser is Owner owner)
                    owner.AddExpenseToVehicle(vehicle, expense);
                else
                    vehicle.Expenses.Add(expense);

                await _db.SaveExpenseAsync(expense);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));

        private void ShowError(string msg)
        {
            ErrorLabel.Text = msg;
            ErrorLabel.IsVisible = true;
        }
    }
}
