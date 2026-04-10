using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class NotesPage : ContentPage
    {
        private readonly AppStateService _appState;

        public NotesPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NotesCollection.ItemsSource = _appState.SelectedVehicle?.Notes
                ?? new List<CarsLogWorkig.Models.Note>();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddNotePage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
