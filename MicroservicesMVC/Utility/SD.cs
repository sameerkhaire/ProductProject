namespace MicroservicesMVC.Utility
{
    public class SD
    {
        public static string CouponAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string ShoppingAPIBase { get; set; }
        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
