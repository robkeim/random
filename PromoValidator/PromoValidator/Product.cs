namespace PromoValidator
{
    public class Product
    {
        public int RowNum { get; set; }

        public string Sku { get; set; }

        public double ExpectedPrice { get; set; }

        public double ActualPrice { get; set; }

        public bool OutOfStock { get; set; }

        public string ProductPageUrl { get; set; }
    }
}
