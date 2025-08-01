using PhoneLinesApp.Core.Models;
using PhoneLinesApp.Services.Interfaces;
using PhoneLinesApp.UI.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PhoneLinesApp.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPhoneLineService _service;

        public ObservableCollection<PhoneLine> Lines { get; } = new ();

        private string _selectedStatus = "All";
        public string SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; RaisePropertyChanged(nameof(SelectedStatus)); }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set { _searchTerm = value; RaisePropertyChanged(nameof(SearchTerm)); }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(IPhoneLineService service)
        {
            _service = service;
            LoadCommand = new AsyncRelayCommand(async _ => await LoadAsync());
            AddCommand = new RelayCommand(_ => OpenEditor(null));
            EditCommand = new RelayCommand(
                                p => OpenEditor((PhoneLine)p),
                                p => p is PhoneLine);
            DeleteCommand = new AsyncRelayCommand(
                                async p => await DeleteAsync(p),
                                p => p is PhoneLine);
        }

        private async Task LoadAsync()
        {
            var items = await _service.GetAllLinesAsync(SelectedStatus, SearchTerm);
            Lines.Clear();
            foreach (var item in items) Lines.Add(item);
        }

        private void OpenEditor(PhoneLine line)
        {
            var model = line == null
                ? new PhoneLine()
                : new PhoneLine
                {
                    Id = line.Id,
                    PhoneNumber = line.PhoneNumber,
                    LineType = line.LineType,
                    LastActivationDate = line.LastActivationDate,
                    IsActive = line.IsActive,
                    Notes = line.Notes
                };

            var vm = new PhoneLineViewModel(model, _service);
            var dialog = new Views.AddEditPhoneLineWindow
            {
                DataContext = vm
            };
            if (dialog.ShowDialog() == true)
                _ = LoadAsync();
        }

        private async Task DeleteAsync(object param)
        {
            if (MessageBox.Show("تأكيد الحذف؟", "",
                    MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            var line = (PhoneLine)param;
            if (await _service.DeleteLineAsync(line.Id))
                await LoadAsync();
        }
    }
}
