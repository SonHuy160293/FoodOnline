using FoodOnline.Application.DTOs.GetDTO;

namespace FoodOnline.Application.DTOs.ViewModel
{
    public class OrderDetailViewModel
    {
        public OrderGetDTO Order { get; set; }
        public IEnumerable<OrderDetailGetDTO> OrderDetails { get; set; }
    }
}
