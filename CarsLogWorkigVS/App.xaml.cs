using CarsLogWorkigVS.Services;

namespace CarsLogWorkigVS
{
    public partial class App : Application
    {
        public static NavigationService? NavigationService { get; private set; }

        public App(NavigationService navigationService)
        {
            InitializeComponent();
            NavigationService = navigationService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell()); 
        }
    }
}
