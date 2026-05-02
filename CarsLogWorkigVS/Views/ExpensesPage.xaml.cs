using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class ExpensesPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;
        private readonly DatabaseService _db;

        public ExpensesPage(AppStateService appState, VehicleViewModel vm, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { ExpensesCollection.ItemsSource = new List<Expense>(); TotalLabel.Text = "0,00 грн"; return; }

            var entities = await _db.GetExpensesAsync(v.Id.ToString());
            v.Expenses.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var expense = new Expense((ExpenseCategory)e.Category, e.Amount, e.ExpenseDate, e.Description, v.Id);
                    v.Expenses.Add(expense);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
                }
            }
            ExpensesCollection.ItemsSource = v.Expenses.ToList();
            TotalLabel.Text = _vm.FormatAmount(v.GetTotalExpenses());
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddExpensePage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
    }
}
