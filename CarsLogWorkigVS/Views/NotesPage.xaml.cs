using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class NotesPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public NotesPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { NotesCollection.ItemsSource = new List<Note>(); return; }

            var entities = await _db.GetNotesAsync(v.Id.ToString());
            v.Notes.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var note = new Note(e.TitleNote, e.NoteContent, (NoteCategory)e.Category, e.CreatedAt);
                    v.Notes.Add(note);
                }
                catch { }
            }
            NotesCollection.ItemsSource = v.Notes.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddNotePage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
