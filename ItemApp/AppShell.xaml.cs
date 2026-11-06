namespace ItemApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("DataEntryPage", typeof(Views.DataEntryPage));

        }
    }
}
