using FoodOnline.Application.DTOs;
using FoodOnline.Application.DTOs.ViewModel;
using FoodOnline.Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodOnline.UI.Areas.Admin.Controllers
{
    [Area("Administration")]
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly ICommonService _commonService;

        public OrderController(IOrderService orderService, ICommonService commonService)
        {
            _orderService = orderService;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index(OrderSeachRequestAdmin searchRequest)
        {
            var userBranch = await _commonService.GetCurrentUserBranch();
            // Initialize searchRequest if it is null
            if (searchRequest == null)
            {
                searchRequest = new OrderSeachRequestAdmin
                {
                    PageIndex = 1, // Default to first page
                    PageSize = 10  // Default page size
                };
            }
            else
            {
                // Ensure PageIndex and PageSize have default values if not set
                searchRequest.PageIndex = searchRequest.PageIndex > 0 ? searchRequest.PageIndex : 1;
                searchRequest.PageSize = searchRequest.PageSize > 0 ? searchRequest.PageSize : 10;
            }

            // Fetch filtered and paginated orders
            var orders = await _orderService.GetOrdersByStatusAsync(searchRequest, userBranch);

            // Fetch order statuses for dropdown
            var statuses = await _orderService.GetOrderStatusesAsync();

            // Create a view model to pass to the view
            var viewModel = new OrderIndexViewModel
            {
                Orders = orders,
                SearchRequest = searchRequest,
                OrderStatuses = statuses,
                PaidOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All" },
            new SelectListItem { Value = "true", Text = "Yes" },
            new SelectListItem { Value = "false", Text = "No" }
        },
                ConfirmOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All" },
            new SelectListItem { Value = "true", Text = "Yes" },
            new SelectListItem { Value = "false", Text = "No" }
        }
            };

            return View(viewModel);
        }

    }
}
