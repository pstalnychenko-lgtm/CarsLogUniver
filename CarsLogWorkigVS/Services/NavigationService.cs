using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsLogWorkigVS.Services
{
    public class NavigationService
    {
        private readonly List<string> _history = new List<string>();
        private const int MaxHistory = 3;

        public NavigationService()
        {
            Shell.Current.Navigated += OnNavigated;
        }

        private void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            var route = Shell.Current.CurrentState.Location.ToString();
            
            if (_history.LastOrDefault() == route) return;

            _history.Add(route);
            if (_history.Count > MaxHistory + 1)
            {
                _history.RemoveAt(0);
            }
        }

        public async Task GoBackAsync()
        {
            if (_history.Count > 1)
            {
                _history.RemoveAt(_history.Count - 1);
                var previousRoute = _history.Last();
                await Shell.Current.GoToAsync(previousRoute);
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        public List<string> GetHistory() => _history.ToList();
    }
}
