using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhoneLinesApp.UI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private readonly Dictionary<string, List<string>> _errors = new ();

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return _errors.SelectMany(kv => kv.Value);
            return _errors.ContainsKey(propertyName)
                ? _errors[propertyName]
                : null;
        }

        protected void ValidateProperty(object value, string propName)
        {
            var ctx = new ValidationContext(this) { MemberName = propName };
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(value, ctx, results);

            if (results.Any())
                _errors[propName] = results.Select(r => r.ErrorMessage).ToList();
            else
                _errors.Remove(propName);

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
            RaisePropertyChanged(nameof(HasErrors));
        }

        protected bool ValidateAll()
        {
            var ctx = new ValidationContext(this);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, ctx, results, true);

            _errors.Clear();
            foreach (var r in results)
            {
                foreach (var name in r.MemberNames)
                {
                    if (!_errors.ContainsKey(name))
                        _errors[name] = new List<string>();
                    _errors[name].Add(r.ErrorMessage);
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
            }
            RaisePropertyChanged(nameof(HasErrors));
            return !_errors.Any();
        }
    }
}
