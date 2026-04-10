using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class AddDocumentPage : ContentPage
    {
        private readonly AppStateService _appState;

        public AddDocumentPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
            DocTypePicker.SelectedIndex = 0;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { ShowError("Автомобіль не вибрано."); return; }

            var title = TitleEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(title)) { ShowError("Вкажіть назву документа."); return; }

            var docType = DocTypePicker.SelectedIndex switch
            {
                0 => DocumentType.VehicleRegistration,
                1 => DocumentType.Insurance,
                2 => DocumentType.TechnicalInspection,
                _ => DocumentType.Other
            };

            try
            {
                var doc = new Document(
                    title: title,
                    dateOfIssueDoc: (DateTime)IssueDatePicker.Date,
                    documentType: docType,
                    policyNumber: PolicyEntry.Text?.Trim() ?? string.Empty
                );

                if (_appState.CurrentUser is Owner owner)
                    owner.AddDocumentToVehicle(vehicle, doc);
                else
                    vehicle.Documents.Add(doc);

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
