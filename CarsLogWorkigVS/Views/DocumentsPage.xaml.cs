using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class DocumentsPage : ContentPage
    {
        private readonly AppStateService _appState;

        public DocumentsPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DocsCollection.ItemsSource = _appState.SelectedVehicle?.Documents
                ?? new List<CarsLogWorkig.Models.Document>();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddDocumentPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
