using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;

namespace findbook.WebUI.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchasesRepository pr;
        private IDealsRepository dr;

        public PurchaseController(IPurchasesRepository purchaseRepository, IDealsRepository dealRepository) {
            pr = purchaseRepository;
            dr = dealRepository;
        }



    }
}
