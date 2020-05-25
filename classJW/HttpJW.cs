using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace classJW
{
    public class HttpJW
    {
        public string PostJW(string url, object parData)
        {
            try
            {
                 HttpClient client = new HttpClient();
            //要求服務端回應的文件型態
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int pData_cnt = parData.GetType().GetProperties().Length;
            KeyValuePair<string, string>[] KVData = new KeyValuePair<string, string>[pData_cnt];
            for (int i = 0; i < pData_cnt; i++)
            {
                KVData[i] = new KeyValuePair<string, string>(parData.GetType().GetProperties()[i].Name,
                                parData.GetType().GetProperties()[i].GetValue(parData).ToString());
            }

            HttpContent aa = new FormUrlEncodedContent(KVData);

            //發出訊息
            Task<HttpResponseMessage> response = client.PostAsync(url, aa);
            response.Wait();
            var result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
        public string PostJW2(string url, object parData)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                 HttpClient client = new HttpClient();
            //要求服務端回應的文件型態
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int pData_cnt = parData.GetType().GetProperties().Length;
            KeyValuePair<string, string>[] KVData = new KeyValuePair<string, string>[pData_cnt];
            for (int i = 0; i < pData_cnt; i++)
            {
                KVData[i] = new KeyValuePair<string, string>(parData.GetType().GetProperties()[i].Name,
                                parData.GetType().GetProperties()[i].GetValue(parData).ToString());
            }

            HttpContent aa = new FormUrlEncodedContent(KVData);

                HttpRequestMessage request = new HttpRequestMessage();
                request.Content = aa;
                request.Method = HttpMethod.Post;
                request.RequestUri = new System.Uri(url);
            //發出訊息
               Task<HttpResponseMessage> response = client.SendAsync(request);

            response.Wait();
            var result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }

        public string GetJW(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                //要求服務端回應的文件型態
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //發出訊息
                Task<HttpResponseMessage> response = client.GetAsync(url);
                response.Wait();
                var result = response.Result.Content.ReadAsStringAsync().Result;

                return result;
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
    }


}
