using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralUtilities;
using GeneralUtilities.oAuth;
using GeneralUtilities.oAuthLib;

namespace CartMig
{
    public partial class frmConnectShopify : Form
    {
        HttpListener ShopifyHttpListener = null;
        Process browserProcess = null;

        public frmConnectShopify()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //REPLACE WITH YOUR Shopify_APIkey AND Shopify_AppSecret
            //IN AppConstants.Shopify_APIkey AND AppConstants.Shopify_AppSecret

            if (ShopifyHttpListener == null)
                ShopifyHttpListener = new HttpListener();
            else
            {
                if (ShopifyHttpListener.IsListening)
                {
                    ShopifyHttpListener.Stop();
                    ShopifyHttpListener.Abort();
                }
                ShopifyHttpListener = new HttpListener();
            }
            ShopifyHttpListener.Prefixes.Add(AppConstants.Shopify_RedirectURL); //"http://localhost:33337/";
            ShopifyHttpListener.Start();


            browserProcess = new Process();
            browserProcess.StartInfo.FileName = string.Format(AppConstants.Shopify_ConnectionURL, txtShopName.Text.Trim(), AppConstants.Shopify_APIkey, AppConstants.Shopify_RedirectURL)  ;
            browserProcess.StartInfo.UseShellExecute = true;
            browserProcess.StartInfo.Verb = "";
            browserProcess.Start();
            //System.Diagnostics.Process.Start(process.StartInfo);

            var context = ShopifyHttpListener.GetContext();
            var response = context.Response;

            var code = context.Request.QueryString.Get("code");
            var shop = context.Request.QueryString.Get("shop");

            string responseString = string.Format("<html><head></head><body>You are successfully connected.</body></html>");
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                ShopifyHttpListener.Stop();
                ShopifyHttpListener = null;
            });

            string RequestString = string.Format(AppConstants.Shopify_RequestURL, txtShopName.Text.Trim());
            string postData = "client_id=" + AppConstants.Shopify_APIkey + "&client_secret=" + AppConstants.Shopify_AppSecret + "&code=" + (code);
            string ContentType = "application/x-www-form-urlencoded";

            AccessTokenDTO accessTokenDTO = OAuthUtils.GetPermanentAccessToken(postData, ContentType, RequestString);

            MessageBox.Show(accessTokenDTO.access_token);
        }


    }
}
