namespace WP_Attackment
{

    internal class Setup
    {
        private string BaseURL = "";
        private int ArgsType = 0;
        private int StartID = 0;
        private int StopID = 0;
        private int MaxThreads = 1;
        private string AtkMode = "page_id";
        private bool isJsonEnabled = false;
        protected private string[] AtkModeList = { "page_id", "p", "attachment_id", "author" };

        public async void Start(string[] args) 
        {

            SetArgs(args);
            if ((BaseURL == "") 
                || (StartID == 0) 
                || (StopID == 0) 
                || (MaxThreads ==0)
                || (StartID > StopID)
                || !CheckModeValid(AtkMode))
            {
                Console.WriteLine("Required arguments are missing.");
                Environment.Exit(0);
            }

            Attachment attachment = new Attachment();

             attachment.GetAttachments(BaseURL, StartID, StopID, isJsonEnabled, MaxThreads);

        }

        private static void ShowLogo()
        {
            Console.WriteLine(" ██╗       ██╗██████╗        █████╗ ████████╗████████╗ █████╗  █████╗ ██╗  ██╗███╗   ███╗███████╗███╗  ██╗████████╗\n ██║  ██╗  ██║██╔══██╗      ██╔══██╗╚══██╔══╝╚══██╔══╝██╔══██╗██╔══██╗██║ ██╔╝████╗ ████║██╔════╝████╗ ██║╚══██╔══╝\n ╚██╗████╗██╔╝██████╔╝█████╗███████║   ██║      ██║   ███████║██║  ╚═╝█████═╝ ██╔████╔██║█████╗  ██╔██╗██║   ██║   \n  ████╔═████║ ██╔═══╝ ╚════╝██╔══██║   ██║      ██║   ██╔══██║██║  ██╗██╔═██╗ ██║╚██╔╝██║██╔══╝  ██║╚████║   ██║   \n  ╚██╔╝ ╚██╔╝ ██║           ██║  ██║   ██║      ██║   ██║  ██║╚█████╔╝██║ ╚██╗██║ ╚═╝ ██║███████╗██║ ╚███║   ██║   \n   ╚═╝   ╚═╝  ╚═╝           ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚═╝  ╚═╝ ╚════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝╚═╝  ╚══╝   ╚═╝   \n");
        }

        private static bool CheckModeValid(String AtkMode) {
            if (string.IsNullOrEmpty(AtkMode))
            {
                return false;
            }

            
            return true;
        }
        private void SetArgs(String[] args)
        {
            if (args.Length > 1)
            {

                //コマンドライン引数を列挙する
                foreach (string arg in args)
                {
                    if (ArgsType == 1)
                    {
                        BaseURL = arg;
                        ArgsType = 0;
                        continue;
                    }
                    if (ArgsType == 2)
                    {
                        StartID = Convert.ToInt32(arg);
                        ArgsType = 0;
                        continue;
                    }
                    if (ArgsType == 3)
                    {
                        StopID = Convert.ToInt32(arg);
                        ArgsType = 0;
                        continue;
                    }
                    if (ArgsType == 4)
                    {
                        MaxThreads = Convert.ToInt32(arg);
                        ArgsType = 0;
                        continue;
                    }
                    if (ArgsType == 5)
                    {
                        AtkMode = arg;
                        ArgsType = 0;
                        continue;
                    }
                    if (arg == "-url")
                    {
                        ArgsType = 1;
                        continue;
                    }
                    if (arg == "-start")
                    {
                        ArgsType = 2;
                        continue;
                    }
                    if (arg == "-n")
                    {
                        ArgsType = 3;
                        continue;
                    }
                    if (arg == "-threads")
                    {
                        ArgsType = 4;
                        continue;
                    }
                    if (arg == "-json")
                    {
                        isJsonEnabled = true;
                        continue;
                    }
                    if (arg == "-mode")
                    {
                        ArgsType = 5;
                        continue;
                    }
                }

                if (isJsonEnabled == false)
                {
                    ShowLogo();
                }

            }
            else
            {
                ShowLogo();
                Console.WriteLine("\nTarget Base URL (ex. https://example.com/)");
                BaseURL = Console.ReadLine();

                Console.WriteLine("\nStart id");
                StartID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nStop id");
                StopID = Convert.ToInt32(Console.ReadLine()) ;

                Console.WriteLine("\nAttack Mode");
                AtkMode = (Console.ReadLine());

                Console.WriteLine("\nMax threads");
                MaxThreads = Convert.ToInt32(Console.ReadLine());


                
            }


        
        }
    }
}


