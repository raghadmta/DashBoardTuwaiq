using DashBoard.Data;
using DashBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DashBoard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var username = HttpContext.User.Identity.Name ?? null;


            HttpContext.Session.SetString("userdata", username);

            ViewBag.Username = username;


            return View();
        }

        public IActionResult AddNewItem()
        {

            ViewBag.Username = HttpContext.Session.GetString("userdata");

            var products = _context.Products.ToList();
            ViewBag.Products = products;
            return View(products);
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {

            if (ModelState.IsValid)
            {
                _context.Products.Add(p);
                _context.SaveChanges();
                TempData["Add"] = " „  «·≈÷«›… »‰Ã«Õ";
                return RedirectToAction("AddNewItem");

            }
            TempData["Add"] = "·„   „ «·≈÷«›… Ì—ÃÏ «· √ﬂœ „‰ ’Õ… «·„œŒ·« ";

            var products = _context.Products.ToList();
            return View("AddNewItem", products);


        }


        [HttpPost]
        public JsonResult Delete(int record_no)
        {
            var productdel = _context.Products.SingleOrDefault(p => p.Id == record_no);

            if (productdel != null)
            {
                _context.Products.Remove(productdel);
                _context.SaveChanges();
                TempData["del"] = true;
            }

            else
            {
                TempData["del"] = false;
            }

            return Json("Ok");

        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;

                    _context.SaveChanges();
                    TempData["Add"] = " „ «· ÕœÌÀ »‰Ã«Õ";
                }
                else
                {
                    TempData["Add"] = "«·„‰ Ã €Ì— „ÊÃÊœ";
                }

                return RedirectToAction("AddNewItem");
            }

            TempData["Add"] = "·„ Ì „ «· ÕœÌÀ Ì—ÃÏ «· √ﬂœ „‰ ’Õ… «·„œŒ·« ";
            return RedirectToAction("AddNewItem");
        }

        [HttpGet]
        public JsonResult GetProductDetails(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                return Json(product);
            }
            return Json(null);
        }

        [HttpGet]
        public JsonResult GetProductDetailsfor(int id)
        {
            var productDetails = _context.ProductDetails.SingleOrDefault(p => p.Id == id);
            if (productDetails != null)
            {
                return Json(productDetails);
            }
            return Json(null);
        }
                [HttpGet]
        public JsonResult GetDamagedproducts(int id)
        {
            var productDetails = _context.Damagedproducts.SingleOrDefault(p => p.Id == id);
            if (productDetails != null)
            {
                return Json(productDetails);
            }
            return Json(null);
        }

        public IActionResult ProductsDetails()
        {
            ViewBag.Username = HttpContext.Session.GetString("userdata");

            var ProductDetails = _context.ProductDetails.Join(

                _context.Products,

                productsdetails => productsdetails.Products_Id,
                product => product.Id,

                (productsdetails, product) => new
                {
                    id = productsdetails.Id,
                    name = product.Name,
                    color = productsdetails.Color,
                    price = productsdetails.Price,
                    qty = productsdetails.Qty,
                    img = productsdetails.Images,
                }
                ).ToList();

            var prodcts = _context.Products.ToList();

            ViewBag.products = prodcts;
            ViewBag.ProductDetails = ProductDetails;
            return View();
        }

        public IActionResult CreateDetails(ProductDetails productDetails, IFormFile photo)
        {



            if (photo == null || photo.Length == 0)
            {
                return Content("File Not Selected");
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", photo.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }

            productDetails.Images = photo.FileName;
            _context.ProductDetails.Add(productDetails);
            _context.SaveChanges();
            TempData["Add"] = " „  «·≈÷«›… »‰Ã«Õ";

            return RedirectToAction("ProductsDetails");
        }

        [HttpPost]
        public JsonResult DeleteProductDetails(int record_no)
        {
            var productDetailsdel = _context.ProductDetails.SingleOrDefault(p => p.Id == record_no);

            if (productDetailsdel != null)
            {
                _context.ProductDetails.Remove(productDetailsdel);
                _context.SaveChanges();
                TempData["del"] = true;
            }

            else
            {
                TempData["del"] = false;
            }

            return Json("Ok");

        }
        [HttpPost]
        public IActionResult UpdateProductDetails(ProductDetails product, IFormFile photo)
        {
            var existingProduct = _context.ProductDetails
                                         .SingleOrDefault(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                existingProduct.Images = photo.FileName;
            }
            else
            {
                existingProduct.Images = existingProduct.Images;
            }

            existingProduct.Price = product.Price;
            existingProduct.Qty = product.Qty;
            existingProduct.Color = product.Color;


            _context.ProductDetails.Update(existingProduct);
            _context.SaveChanges();
            TempData["Add"] = " „ «· ÕœÌÀ »‰Ã«Õ";

            return RedirectToAction("ProductsDetails");
        }

        public IActionResult Damagedproducts()
        {

            ViewBag.Username = HttpContext.Session.GetString("userdata");

            var damagedProducts = _context.Damagedproducts
                                          .Include(d => d.Product)
                                          .ThenInclude(p => p.ProductDetails)
                                          .ToList();

            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(damagedProducts);
        }

        [HttpPost]
        public IActionResult CreateDamagedproducts(Damagedproducts damagedproducts)
        {
            if (ModelState.IsValid)
            {
                _context.Damagedproducts.Add(damagedproducts);
                _context.SaveChanges();
                TempData["Add"] = " „  «·≈÷«›… »‰Ã«Õ";
                return RedirectToAction("Damagedproducts");
            }

            TempData["Add"] = "·„   „ «·≈÷«›… Ì—ÃÏ «· √ﬂœ „‰ ’Õ… «·„œŒ·« ";
            var damagedProducts = _context.Damagedproducts
                                          .Include(d => d.Product)
                                          .ThenInclude(p => p.ProductDetails)
                                          .ToList();

            ViewBag.Products = new SelectList(_context.Products, "Id", "Name", damagedproducts.ProductId);
            return View("Damagedproducts", damagedProducts);
        }


        [HttpPost]
        public JsonResult DeleteDamagedproducts(int record_no)
        {
            var productDetailsdel = _context.Damagedproducts.SingleOrDefault(p => p.Id == record_no);

            if (productDetailsdel != null)
            {
                _context.Damagedproducts.Remove(productDetailsdel);
                _context.SaveChanges();
                TempData["del"] = true;
            }

            else
            {
                TempData["del"] = false;
            }

            return Json("Ok");

        }


        [HttpPost]
        public IActionResult UpdateDamagedproducts(Damagedproducts damagedproducts)
        {
            var existingProduct = _context.Damagedproducts
                                         .SingleOrDefault(p => p.Id == damagedproducts.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }
             
             existingProduct.Qty = damagedproducts.Qty;
 

            _context.Damagedproducts.Update(existingProduct);
            _context.SaveChanges();
            TempData["Add"] = " „ «· ÕœÌÀ »‰Ã«Õ";

            return RedirectToAction("Damagedproducts");
        }
        public IActionResult Privacy()
        {

            ViewBag.Username = HttpContext.Session.GetString("userdata");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
