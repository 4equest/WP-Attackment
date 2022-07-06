namespace WP_Attackment
{
    internal  class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Setup setup = new Setup();
            setup.Start(args);

        }


    }
}