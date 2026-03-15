namespace adv_client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form mainForm = new AdvProductEditForm();
            NeoDEEX.Windows.Forms.FoxExceptionHandler.Register(mainForm);
            Application.Run(mainForm);
        }
    }
}