using PhoneLinesApp.Core.Models;
using PhoneLinesApp.Services.Interfaces;
using PhoneLinesApp.UI.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PhoneLinesApp.UI.ViewModels
{
    public class PhoneLineViewModel : BaseViewModel
    {
        private readonly IPhoneLineService _service;
        public PhoneLine Model { get; }

        public string PhoneNumber
        {
            get => Model.PhoneNumber;
            set { Model.PhoneNumber = value; ValidateProperty(value, nameof(PhoneNumber)); }
        }

        public string LineType
        {
            get => Model.LineType;
            set { Model.LineType = value; ValidateProperty(value, nameof(LineType)); }
        }

        public DateTime LastActivationDate
        {
            get => Model.LastActivationDate;
            set { Model.LastActivationDate = value; ValidateProperty(value, nameof(LastActivationDate)); }
        }

        public bool IsActive
        {
            get => Model.IsActive;
            set { Model.IsActive = value; }
        }

        public string Notes
        {
            get => Model.Notes;
            set { Model.Notes = value; }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public PhoneLineViewModel(PhoneLine model, IPhoneLineService service)
        {
            Model = model;
            _service = service;

            if (Model.Id == 0 && Model.LastActivationDate == default)
                Model.LastActivationDate = DateTime.Now;

            SaveCommand = new AsyncRelayCommand(
                                async () => await SaveAsync(),
                                () => !HasErrors);
            CancelCommand = new RelayCommand(w => (w as Window)?.Close());
        }

        private async Task SaveAsync()
        {
            if (!ValidateAll())
                return;

            bool ok = await _service.SaveLineAsync(Model);
            if (ok)
            {
                var win = Application.Current.Windows
                            .OfType<Window>()
                            .SingleOrDefault(w => w.DataContext == this);
                win.DialogResult = true;
                win.Close();
            }
        }
    }
}
