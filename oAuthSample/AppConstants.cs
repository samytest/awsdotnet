using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralUtilities
{
    public class AppConstants
    {
        public static string Shopify_APIkey = "Shopify_APIkey";
        public static string Shopify_AppSecret = "Shopify_AppSecret";
        public static string Shopify_RedirectURL= "http://localhost:33337/";
        public static string Shopify_RequestURL = "https://{0}.myshopify.com/admin/oauth/access_token";
        public static string Shopify_ConnectionURL = "https://{0}.myshopify.com/admin/oauth/authorize?client_id={1}&scope=write_products,write_customers,write_orders,write_fulfillments,write_shipping,write_inventory,read_inventory,read_shopify_payments_payouts,read_products,read_product_listings,read_customers,read_orders,read_draft_orders,read_locations,read_shipping,read_reports,write_reports,read_shopify_payments_disputes,write_draft_orders&redirect_uri={2}";
    }
}
