using static WP_Attackment.Http;

namespace WP_Attackment
{
    internal class Attachment
    {
        public void GetAttachments(string BaseUrl, int Start, int StopID, bool isJsonEnabled, int MaxThreads)
        {
            Console.Clear();

            Http httpTask = new Http();
            List<int> SuccessedPageIDs = new List<int>();


            Parallel.ForEach(Enumerable.Range(Start, StopID - Start + 1), new ParallelOptions { MaxDegreeOfParallelism = MaxThreads }, async i =>
            {
                Task<String[]> gethtmltask = httpTask.TryHttpsAsync(BaseUrl + "index.php?page_id=" + i, 0);
                String[] Res = gethtmltask.Result;

                if(Res == null)
                {
                    return;
                }
                if (!isJsonEnabled)
                {
                    if (Res[(int)HttpReturn.Status] == "OK")
                    {
                        Console.WriteLine("[Page ID : " +i + " " + Res[(int)HttpReturn.Title] + "] " + Res[(int)HttpReturn.Uri]);
                    }

                }
                else
                {
                    if (Res.Length != 0)
                    {
                        SuccessedPageIDs.Add(i);
                    }
                }
                Console.Title = "Progress : " + (i - Start + 1) + "/" + (StopID - Start + 1);
            });


            if (!isJsonEnabled)
            {
                Console.WriteLine("Finished");
            }
            else
            {

                Console.WriteLine(Json.MakeJson(SuccessedPageIDs));
                //Console.WriteLine("comp");
            }


        }




    }
}
