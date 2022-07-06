using System.Text.RegularExpressions;

namespace WP_Attackment
{
    internal class Http
    {

        public async Task<String[]> TryHttpsAsync(string Url, int Nest)
        {
            if(Nest > 5)
            {
                return null;
            }
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            var client = new HttpClient(handler);


            try
            {

                var res = client.GetAsync(Url).Result;

                String[] ret = new string[10];


                
                ret[(int)HttpReturn.Status] =  res.StatusCode.ToString();

                if (ret[(int)HttpReturn.Status] == "OK")
                {

                    ret[(int)HttpReturn.Html] = await res.Content.ReadAsStringAsync();
                    ret[(int)HttpReturn.Title] = GetTitle(ret[(int)HttpReturn.Html]);


                }

                if (ret[(int)HttpReturn.Status] == "Moved"
                    || ret[(int)HttpReturn.Status] == "Found")
                {
                    ret = TryHttpsAsync(res.Headers.Location.ToString(), Nest + 1).Result;
                    if(ret == null)
                    {
                        return null;
                    }
                    ret[(int)HttpReturn.Uri] = res.Headers.Location.ToString();
                }




                return ret;



            }
            catch (Exception)
            {
                return null;
            }


        }

        public enum HttpReturn
        { 
            Html = 1,
            Status,
            Title,
            Uri,

        }

        //https://www.casleyconsulting.co.jp/blog/engineer/183/

        /// <summary>
        /// 正規化表現を使用してHTMLからタイトルを取得します。
        /// </summary>
        /// <param name="html">HTML文字列</param>
        /// <returns>HTML文字列から取得したタイトル</returns>
        public static string GetTitle(string html)
        {

            // 正規化表現
            // 大文字小文字区別なし       : RegexOptions.IgnoreCase
            // 「.」を改行にも適応する設定: RegexOptions.Singleline
            var reg = new Regex(@"<title>(?<title>.*?)</title>",
                         RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // html文字列内から条件にマッチしたデータを抜き取ります。
            var m = reg.Match(html);

            // 条件にマッチした文字列内からKey("title部分")にマッチした値を抜き取ります。
            return System.Web.HttpUtility.HtmlDecode(m.Groups["title"].Value);

        }

        //https://www.casleyconsulting.co.jp/blog/engineer/183/
    }
}
