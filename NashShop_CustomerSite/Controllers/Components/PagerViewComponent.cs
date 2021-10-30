using Microsoft.AspNetCore.Mvc;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase list)
        {
            return Task.FromResult((IViewComponentResult)View("Default", list));
        }
    }
}
