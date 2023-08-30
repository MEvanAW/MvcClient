namespace MvcClient.Models.Basket
{
    public class BasketListModel
    {
        public string Buyer { get; set; } = string.Empty;
        public IEnumerable<BasketItemModel>? Items { get; set; }

        //public BasketListModel() { }
        //public BasketListModel(BasketItemListModel basketItemListModel)
        //{
        //    Buyer = basketItemListModel.Buyer;
        //    Items = new List<BasketItemModel>();
        //    Items.Append(basketItemListModel);
        //}
    }
}
