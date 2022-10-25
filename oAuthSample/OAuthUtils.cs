using GeneralUtilities.oAuth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace GeneralUtilities.oAuthLib
{

    public class OAuthUtils
    {
        public static AccessTokenDTO GetPermanentAccessToken(string PostData, string ContentType, string RequiredUrl)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.SetTcpKeepAlive(false, 0, 0);
            AccessTokenDTO dto = null;
            string resData = string.Empty;
            int BUFFERSIZE = 1024;
            try
            {

                byte[] byteArray = Encoding.UTF8.GetBytes(PostData);
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(RequiredUrl);
                GETRequest.ContentType = ContentType;
                GETRequest.ContentLength = byteArray.Length;
                GETRequest.Method = "POST";

                Stream dataStream = GETRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();
                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);

                StringBuilder responseString = new StringBuilder();
                char[] buffer = new char[BUFFERSIZE];
                int charRead = 0;
                while ((charRead = sr.ReadBlock(buffer, 0, BUFFERSIZE)) > 0)
                {
                    responseString.Append(buffer);
                    buffer = null;
                    buffer = new char[BUFFERSIZE];
                }

                resData = responseString.ToString().Replace("\0", "").Trim();
                if (resData != null && resData.Trim().Length > 0)
                {

                    dto = new CommonUtility().convertJsonToObject<AccessTokenDTO>(resData);
                    //dto.access_token = CommonUtility.Decrypt(dto.access_token);
                    //byte[] encodedDataAsBytes = System.Convert.FromBase64String(dto.access_token);
                    //dto.access_token = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
                }
                GETResponseStream.Close();
                sr.Close();
                GETResponse.Close();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (WebResponse response = ex.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                        }
                    }
                }
                throw ex;
            }
            catch
            {

            }
            return dto;
        }
    }
}