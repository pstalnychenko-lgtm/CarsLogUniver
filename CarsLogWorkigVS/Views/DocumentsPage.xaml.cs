using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;
using static CarsLogWorkig.Models.Document;

namespace CarsLogWorkigVS.Views
{
    public partial class DocumentsPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public DocumentsPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { DocsCollection.ItemsSource = new List<Document>(); return; }

            var entities = await _db.GetDocumentsAsync(v.Id.ToString());
            v.Documents.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var doc = new Document(e.Title, e.DateOfIssueDoc, (DocumentType)e.DocumentCategory, e.PolicyNumber);
                    v.Documents.Add(doc);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
                }
            }
            DocsCollection.ItemsSource = v.Documents.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddDocumentPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
    }
}
