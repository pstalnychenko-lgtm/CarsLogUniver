using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class ExpensesPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;

        public ExpensesPage(AppStateService appState, VehicleViewModel vm)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            var expenses = v?.Expenses ?? new List<CarsLogWorkig.Models.Expense>();
            ExpensesCollection.ItemsSource = expenses;
            TotalLabel.Text = _vm.FormatAmount(v?.GetTotalExpenses() ?? 0);
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddExpensePage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
