using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsLogWorkigVS.Services
{
    public class NavigationService
    {
        private readonly List<string> _history = new List<string>();
        private const int MaxHistory = 5;

        public NavigationService()
        {
            SubscribeToNavigated();
        }

        public void SubscribeToNavigated()
        {
            if (Shell.Current != null)
            {
                Shell.Current.Navigated -= OnNavigated;
                Shell.Current.Navigated += OnNavigated;
            }
        }

        private void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (Shell.Current?.CurrentState == null) return;
            var route = Shell.Current.CurrentState.Location.ToString();
            
            if (_history.LastOrDefault() == route) return;

            _history.Add(route);
            if (_history.Count > MaxHistory)
            {
                _history.RemoveAt(0);
            }
        }

        public async Task GoBackAsync()
        {
            if (Shell.Current == null) return;
            await Shell.Current.GoToAsync("..");
        }

        public List<string> GetHistory() => _history.ToList();
    }
}
