using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManegerController : Controller
    {

        public ProductRepo Cotext;

        public ProductManegerController()
        {
            Cotext = new ProductRepo();
        }
        // GET: ProductManeger
        public ActionResult Index()
        {
            List<Product> products = Cotext.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                Cotext.Insert(product);
                Cotext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            Product product = Cotext.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Update(Product product, string Id)
        {
            Product productToEdit = Cotext.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    productToEdit.Cetaglory = product.Cetaglory;
                    productToEdit.Desscription = product.Desscription;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    Cotext.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = Cotext.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = Cotext.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Cotext.Delete(Id);
                Cotext.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}