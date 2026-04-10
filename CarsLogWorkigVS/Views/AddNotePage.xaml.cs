using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class AddNotePage : ContentPage
    {
        private readonly AppStateService _appState;

        public AddNotePage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
            CategoryPicker.SelectedIndex = 0;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { ShowError("Автомобіль не вибрано."); return; }

            var title = TitleEntry.Text?.Trim();
            var content = ContentEditor.Text?.Trim();

            if (string.IsNullOrWhiteSpace(title)) { ShowError("Вкажіть заголовок."); return; }
            if (string.IsNullOrWhiteSpace(content)) { ShowError("Вкажіть зміст нотатки."); return; }

            var category = CategoryPicker.SelectedIndex switch
            {
                1 => NoteCategory.Fuel,
                2 => NoteCategory.Service,
                3 => NoteCategory.Finance,
                4 => NoteCategory.Reminder,
                _ => NoteCategory.General
            };

            try
            {
                var note = new Note(title, content, category);

                if (_appState.CurrentUser is Owner owner)
                    owner.AddNoteToVehicle(vehicle, note);
                else
                    vehicle.Notes.Add(note);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");

        private void ShowError(string msg)
        {
            ErrorLabel.Text = msg;
            ErrorLabel.IsVisible = true;
        }
    }
}
